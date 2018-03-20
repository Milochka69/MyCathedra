using System.Linq;
using System.Windows.Controls;

namespace MyCathedra.Pages.Administrating
{
    /// <summary>
    /// Логика взаимодействия для RefDocumentCategoryPage.xaml
    /// </summary>
    public partial class RefDocumentCategoryPage : Page
    {
        public RefDocumentCategoryPage(FileManager.FileManager fileManager, string directory)
        {
            InitializeComponent();
            _CategoriesGrid.ItemsSource = fileManager.GetChildrenDirectories(directory)
                .Select(d => new {Name = d});
        }
    }
}