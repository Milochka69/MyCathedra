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
            new UIPropertyMetadata {
                DefaultValue = Brushes.Black,
                PropertyChangedCallback = new PropertyChangedCallback(CurrentIcongroundChanged)
            },
            new ValidateValueCallback(ValidateCurrentIconground)
        );

        private static void CurrentIcongroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TileMenuItem uc = (TileMenuItem)d;
            Path path = uc.itemIcon;
            path.Fill = (Brush)e.NewValue;
        }

        private static bool ValidateCurrentIconground(object value)
        {
            return value is Brush;
        }

        /// <summary>
        /// The color of image displayed by the button.
        /// </summary>
        [Description("The color of image displayed by the button."), Category("Кисть")]
        public Brush Iconground
        {
            get
            {
                return (Brush)GetValue(IcongroundProperty);
            }
            set
            {
                SetValue(IcongroundProperty, value);
            }
        }

        /// <summary>
        /// The image displayed by the button.
        /// </summary>
        /// <remarks>The image is specified in XAML as an absolute or relative path.</remarks>
        [Description("The image displayed by the button."), Category("Appearance")]
        public Geometry PathSource
        {
            get
            {
                return itemIcon.Data;
            }
            set
            {
                itemIcon.Data = value;
            }
        }

        /// <summary>
        /// The text displayed by the button.
        /// </summary>
        [Description("The text displayed by the button."), Category("Appearance")]
        public String Text
        {
            get
            {
                return itemLabel.Text;
            }
            set
            {
                itemLabel.Text = value;
            }
        }

        public TileMenuItem()
        {
            InitializeComponent();
        }
    }
}
