using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using MyCathedra.Controls.Tile;
using MyCathedra.DataManager;
using FileInfo = MyCathedra.FileManager.FileInfo;

namespace MyCathedra
{
    public partial class MainWindow : Window
    {
        private readonly FileManager.FileManager _fileManager;
        private readonly IList<Expander> _expanders;
        private readonly DbManager _dbManager;
        private readonly PasswordService _passwordService;
        private readonly Guid _userId;

        private string _currentPath;
        private string _search;

        private string TitlePath
        {
            get => _currentPath;
            set
            {
                _search = null;
                Title = $"Путь: {value}".Replace('\\', '/');
                _currentPath = value;
            }
        }


        public MainWindow()
        {
            _passwordService = new PasswordService();
            _search = null;
            _fileManager = new FileManager.FileManager();
            _dbManager = new DbManager(_passwordService);
            _expanders = new List<Expander>();
            InitializeComponent();
            _userId = Autorization();
            InitWindow();
        }

        private Guid Autorization()
        {
            var autoBox = new AutoBox(_dbManager, _passwordService);
            if (autoBox.ShowDialog() == false) Application.Current.Shutdown();
            return autoBox.UserId;
        }

        private void InitWindow()
        {
            var directories = _fileManager.GetBaseDirectories();
            foreach (var directory in directories)
            {
                var stackPanel = new StackPanel {Background = new BrushConverter().ConvertFrom("#FFE5E5E5") as Brush};
                foreach (var directoryChildren in _fileManager.GetChildren(directory))
                {
                    var item = new TileMenuItem
                    {
                        Text = directoryChildren.Name,
                        //PathSource = ItemPathSource(directoryChildren.Name),
                        Style = FindResource("TileMenuItem") as Style,
                        FontSize = 15
                    };

                    item.Click += TileMenuItem_Click;
                    stackPanel.Children.Add(item);
                }

                var element = new Expander
                {
                    Content = stackPanel,
                    Header = directory,
                    Margin = new Thickness(0.8),
                    FontSize = 17,
                    FontWeight = FontWeights.DemiBold
                };
                element.Expanded += Expander_Expanded;
                _expanders.Add(element);
                BasePanal.Children.Add(element);
            }
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            var currentExpander = sender as Expander;
            foreach (var expander in _expanders)
            {
                if (expander.Header.ToString() != currentExpander?.Header.ToString())
                {
                    expander.IsExpanded = false;
                }
            }
        }

        private void TileMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var tileMenuItem = sender as TileMenuItem;
            var stackPanel = tileMenuItem?.Parent as StackPanel;
            var expander = stackPanel?.Parent as Expander;
            var header = expander?.Header.ToString();
            var path = $"{header}/{tileMenuItem?.Text}";
            TitlePath = path;
            var fileInfos = _fileManager.GetChildren(path);
            DataGridUpdate(fileInfos);
        }

        private async void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = sender as DataGridRow;
            if (!(row?.Item is FileInfo rowItem)) return;

            if (rowItem.IsFle)
            {
                _fileManager.OpenFile(rowItem);
                await _dbManager.InsertActivity(_userId, rowItem.Path);
            }
            else
            {
                TitlePath = rowItem.Path;
                var fileInfos = _fileManager.GetChildren(rowItem.Path);
                DataGridUpdate(fileInfos);
            }
        }

        private async void Add(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitlePath)) return;

            var fileDialog = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Multiselect = false,
                Filter = "file |*.doc;*.xls;*.ppt;*.txt;*.pdf",
                RestoreDirectory = true
            };

            var showDialog = fileDialog.ShowDialog();
            if (showDialog == null || !showDialog.Value) return;

            var file = fileDialog.FileName;
            var path = _fileManager.AddFile(file, TitlePath);
            await _dbManager.InsertActivity(_userId, path);
            DataGridUpdate();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitlePath)) return;
            IEnumerable<FileInfo> fileInfos;
            if (_search != null)
            {
                _search = null;
                fileInfos = _fileManager.GetChildren(TitlePath);
            }
            else
            {
                var path = _fileManager.GetParentPath(TitlePath);
                if (string.IsNullOrWhiteSpace(path)) return;
                TitlePath = path;
                fileInfos = _fileManager.GetChildren(path);
            }

            DataGridUpdate(fileInfos);
        }

        private async void Rename(object sender, RoutedEventArgs e)
        {
            if (!(DataGrid.CurrentItem is FileInfo fileInfo)) return;

            var inputBox = new InputBox("Переименовать?", fileInfo.Name);
            if (inputBox.ShowDialog() != true) return;

            var newPath = _fileManager.Move(fileInfo, inputBox.Answer);

            if (fileInfo.IsFle) await _dbManager.InsertActivity(_userId, newPath);

            DataGridUpdate();
        }

        private void CreateFolder(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitlePath)) return;

            var inputBox = new InputBox("Имя папки");
            if (inputBox.ShowDialog() != true) return;

            _fileManager.CreateFolder(TitlePath, inputBox.Answer);

            DataGridUpdate();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (!(DataGrid.CurrentItem is FileInfo fileInfo)) return;
            var messageBoxResult =
                MessageBox.Show($@"Удалить ""{fileInfo.Name}""?", "Удаление!", MessageBoxButton.YesNo);
            if (messageBoxResult != MessageBoxResult.Yes) return;
            _fileManager.Delete(fileInfo);
            DataGridUpdate();
        }

        private async void DataGrid_Drop(object sender, DragEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitlePath)) return;
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;

            var files = (string[]) e.Data.GetData(DataFormats.FileDrop);
            if (files == null || !files.Any()) return;

            foreach (var file in files)
            {
                var filePath = _fileManager.AddFile(file, TitlePath);
                await _dbManager.InsertActivity(_userId, filePath);
            }

            DataGridUpdate();
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            var searcText = SearcText.Text;
            _search = searcText;

            DataGridUpdate();
        }

        private void DataGridUpdate()
        {
            IEnumerable<FileInfo> fileInfos;
            if (_search != null)
            {
                var isAll = SearcCB.IsChecked != null && SearcCB.IsChecked.Value;
                var path = string.IsNullOrWhiteSpace(TitlePath) || isAll
                    ? string.Empty //поиск в негде
                    : TitlePath;
                fileInfos = _fileManager.Search(path, _search);
            }
            else
            {
                fileInfos = _fileManager.GetChildren(TitlePath);
            }

            DataGridUpdate(fileInfos);
        }

        private async void DataGridUpdate(IEnumerable<FileInfo> fileInfos)
        {
            fileInfos = fileInfos.ToArray();
            var paths = fileInfos.Select(f => f.Path).ToArray();
            var userActivities = await _dbManager.GetUserActivity(paths);
            fileInfos = fileInfos.Select(f =>
            {
                f.ShowPath = f.Path.Replace('\\', '/');

                var activity = userActivities.SingleOrDefault(a => a.Path == f.ShowPath);
                if (activity != null)
                {
                    f.UserName = activity.UserName;
                    f.UpdateUtc = (f.UpdateUtc > activity.Data ? f.UpdateUtc : activity.Data).ToLocalTime();
                }
                return f;
            });
            DataGrid.ItemsSource = fileInfos;
        }
    }
}