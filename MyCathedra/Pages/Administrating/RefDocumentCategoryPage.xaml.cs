using MyCathedra.DatabaseManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace MyCathedra.Pages.Administrating
{
    /// <summary>
    /// Логика взаимодействия для RefDocumentCategoryPage.xaml
    /// </summary>
    public partial class RefDocumentCategoryPage : Page
    {
        public RefDocumentCategoryPage(Manager manager)
        {
            InitializeComponent();
            _CategoriesGrid.ItemsSource = manager.RefDocumentCategories;
        }
    }
}
