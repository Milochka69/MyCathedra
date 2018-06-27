using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MyCathedra.DataManager;

namespace MyCathedra
{
    /// <summary>
    /// Логика взаимодействия для LogForm.xaml
    /// </summary>
    public partial class LogForm
    {
        private readonly DbManager _dbManager;
        private bool _addingData;
        private int _page;

        public LogForm(DbManager dbManager)
        {
            _page = 0;
            _addingData = false;
            _dbManager = dbManager;
            InitializeComponent();
            InitNewData();
        }

        private void ScrollViewer_OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (!(sender is ScrollViewer sv) || _addingData) return;

            if (sv.ScrollableHeight - e.VerticalOffset != 0) return;
            _addingData = true;
            InitNewData();
            _addingData = false;
        }

        private void InitNewData()
        {
//            DataGrid.ItemsSource = _dbManager.GetLogInfo(_page, 20);
            foreach (var logInfo in _dbManager.GetLogInfo(_page, 20))
            {
                DataGrid.Items.Add(logInfo);
            }
            _page++;
        }

        private void UIElement_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var scv = (ScrollViewer) sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
    }
}