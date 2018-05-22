using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using MyCathedra.Controls.Tile;
using FileInfo = MyCathedra.FileManager.FileInfo;

namespace MyCathedra
{
    public partial class MainWindow : Window
    {
        private readonly FileManager.FileManager _fileManager;
        private readonly IList<Expander> _expanders;

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
            _search = null;
            _fileManager = new FileManager.FileManager();
            _expanders = new List<Expander>();
            InitializeComponent();
            InitWindow();
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

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = sender as DataGridRow;
            if (!(row?.Item is FileInfo rowItem)) return;

            if (rowItem.IsFle)
            {
                _fileManager.OpenFile(rowItem);
            }
            else
            {
                TitlePath = rowItem.Path;
                var fileInfos = _fileManager.GetChildren(rowItem.Path);
                DataGridUpdate(fileInfos);
            }
        }

        private void Add(object sender, RoutedEventArgs e)
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
            _fileManager.AddFile(file, TitlePath);
            DataGridUpdate();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitlePath)) return;

            var path = _fileManager.GetParentPath(TitlePath);
            if (string.IsNullOrWhiteSpace(path)) return;
            TitlePath = path;
            var fileInfos = _fileManager.GetChildren(path);
            DataGridUpdate(fileInfos);
        }

        private void Rename(object sender, RoutedEventArgs e)
        {
            if (!(DataGrid.CurrentItem is FileInfo fileInfo)) return;

            var inputBox = new InputBox("Переименовать?", fileInfo.Name);
            if (inputBox.ShowDialog() != true) return;

            _fileManager.Move(fileInfo, inputBox.Answer);

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

        private void DataGrid_Drop(object sender, DragEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitlePath)) return;
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;

            var files = (string[]) e.Data.GetData(DataFormats.FileDrop);
            if (files == null || !files.Any()) return;

            foreach (var file in files)
            {
                _fileManager.AddFile(file, TitlePath);
            }

            DataGridUpdate();
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            var searcText = SearcText.Text;
            if (string.IsNullOrWhiteSpace(searcText)) return;
            _search = searcText;
            DataGridUpdate();
        }

        private void DataGridUpdate()
        {
            IEnumerable<FileInfo> fileInfos;
            if (_search != null)
            {
                var path = string.IsNullOrWhiteSpace(TitlePath) ? "" : TitlePath;
                fileInfos = _fileManager.Search(path, _search);
            }
            else
            {
                fileInfos = _fileManager.GetChildren(TitlePath);
            }

            DataGridUpdate(fileInfos);
        }

        private void DataGridUpdate(IEnumerable<FileInfo> fileInfos)
        {
            DataGrid.ItemsSource = fileInfos;
        }
    }
}