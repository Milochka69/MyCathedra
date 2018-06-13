using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MyCathedra.Controls.Tile
{
    /// <summary>
    /// Логика взаимодействия для DrawerMenuItem.xaml
    /// </summary>
    public partial class TileMenuItem : Button
    {
        public static readonly DependencyProperty IcongroundProperty = DependencyProperty.RegisterAttached(
            "Iconground",
            typeof(Brush),
            typeof(TileMenuItem),
            new UIPropertyMetadata
            {
                DefaultValue = Brushes.Black,
                PropertyChangedCallback = new PropertyChangedCallback(CurrentIcongroundChanged)
            },
            new ValidateValueCallback(ValidateCurrentIconground)
        );

        private static void CurrentIcongroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TileMenuItem uc = (TileMenuItem) d;
        }

        private static bool ValidateCurrentIconground(object value)
        {
            return value is Brush;
        }

        [Description("The color of image displayed by the button."), Category("Кисть")]
        public Brush Iconground
        {
            get => (Brush) GetValue(IcongroundProperty);
            set => SetValue(IcongroundProperty, value);
        }

        [Description("The text displayed by the button."), Category("Appearance")]
        public String Text
        {
            get => itemLabel.Text;
            set => itemLabel.Text = value;
        }

        public TileMenuItem(RoutedEventHandler rename = null, RoutedEventHandler delete = null)
        {
            InitializeComponent();
            if (rename == null && delete == null) ContextMenu.Visibility = Visibility.Collapsed;
            else
            {
                if (rename != null) Rename.Click += rename;
                else Rename.Visibility = Visibility.Collapsed;
                if (delete != null) Delete.Click += delete;
                else Delete.Visibility = Visibility.Collapsed;
            }
        }
    }
}