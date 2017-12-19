using MyCathedra.Pages.Administrating;
using System.Windows;

namespace MyCathedra
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DatabaseManager.Manager db;
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db = new DatabaseManager.Manager();
        }

        private void _RefDocumentCategory_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new RefDocumentCategoryPage(db));
        }
    }
}
