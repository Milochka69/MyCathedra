using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MyCathedra.Controls.Tile;
using MyCathedra.FileManager;

namespace MyCathedra
{
    public partial class MainWindow : Window
    {
        private readonly FileManager.FileManager _fileManager;
        private readonly IList<Expander> _expanders;

        public MainWindow()
        {
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
                foreach (var directoryChildren in _fileManager.GetChildrenDirectories(directory))
                {
                    var item = new TileMenuItem
                    {
                        Text = directoryChildren.Name,
                        PathSource = ItemPathSource(directoryChildren.Name),
                        Style = FindResource("TileMenuItem") as Style
                    };

                    item.Click += TileMenuItem_Click;
                    stackPanel.Children.Add(item);
                }

                var element = new Expander
                {
                    Content = stackPanel,
                    Header = directory,
                    Margin = new Thickness(0.8)
                };
                element.Expanded += Expander_Expanded;
                _expanders.Add(element);
                BasePanal.Children.Add(element);
            }
        }

        private static Geometry ItemPathSource(string name)
        {
            switch (name)
            {
                default:
                    return Geometry.Parse(
                        "M16.5,6.5A2,2,0,0,1,18.5,8.5A2,2,0,0,1,16.5,10.5A2,2,0,0,1,14.5,8.5A2,2,0,0,1,16.5,6.5 M16.5,12A3.5,3.5,0,0,0,20,8.5A3.5,3.5,0,0,0,16.5,5A3.5,3.5,0,0,0,13,8.5A3.5,3.5,0,0,0,16.5,12 M7.5,6.5A2,2,0,0,1,9.5,8.5A2,2,0,0,1,7.5,10.5A2,2,0,0,1,5.5,8.5A2,2,0,0,1,7.5,6.5 M7.5,12A3.5,3.5,0,0,0,11,8.5A3.5,3.5,0,0,0,7.5,5A3.5,3.5,0,0,0,4,8.5A3.5,3.5,0,0,0,7.5,12 M21.5,17.5L14,17.5 14,16.25C14,15.79 13.8,15.39 13.5,15.03 14.36,14.73 15.44,14.5 16.5,14.5 18.94,14.5 21.5,15.71 21.5,16.25 M12.5,17.5L2.5,17.5 2.5,16.25C2.5,15.71 5.06,14.5 7.5,14.5 9.94,14.5 12.5,15.71 12.5,16.25 M16.5,13C15.3,13 13.43,13.34 12,14 10.57,13.33 8.7,13 7.5,13 5.33,13 1,14.08 1,16.25L1,19 23,19 23,16.25C23,14.08,18.67,13,16.5,13z");
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
            DataGrid.ItemsSource = _fileManager.GetChildrenDirectories($"{header}/{tileMenuItem?.Text}");
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = sender as DataGridRow;
            if (!(row?.Item is FileInfo rowItem)) return;
            if (rowItem.IsFle) _fileManager.OpenFile(rowItem);
        }
    }
}