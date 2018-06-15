using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MyCathedra.DataManager;

namespace MyCathedra
{
    /// <summary>
    /// Логика взаимодействия для LogForm.xaml
    /// </summary>
    public partial class LogForm : Window
    {
        private readonly DbManager _dbManager;

        public LogForm(DbManager dbManager)
        {
            _dbManager = dbManager;
            InitializeComponent();
            InitLog();
        }

        private void InitLog()
        {
            DataGrid.ItemsSource = _dbManager.GetLogInfo();
        }
    }
}