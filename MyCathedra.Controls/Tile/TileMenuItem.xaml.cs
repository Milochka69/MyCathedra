using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;

namespace MyCathedra.Controls.Tile
{
    /// <summary>
    /// Логика взаимодействия для DrawerMenuItem.xaml
    /// </summary>
    public partial class TileMenuItem : Button
    {
        /// <summary>
        /// The width of image displayed by the button.
        /// </summary>
        [Description("The width of image displayed by the button."), Category("Макет")]
        public double IconWidth
        {
            get
            {
                return itemIcon.Width;
            }
            set
            {
                itemIcon.Width = value;
            }
        }
        /// <summary>
        /// The height of image displayed by the button.
        /// </summary>
        [Description("The height of image displayed by the button."), Category("Макет")]
        public double IconHeight
        {
            get
            {
                return itemIcon.Height;
            }
            set
            {
                itemIcon.Height = value;
            }
        }

        /// <summary>
        /// The color of image displayed by the button.
        /// </summary>
        [Description("The color of image displayed by the button."), Category("Кисть")]
        public Brush Iconground
        {
            get
            {
                return itemIcon.Fill;
            }
            set
            {
                itemIcon.Fill = value;
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

        /// <summary>
        /// The hint text displayed by the button.
        /// </summary>
        [Description("Short explanations."), Category("Appearance")]
        public String Hint
        {
            get
            {
                return itemHint.Text;
            }
            set
            {
                itemHint.Text = value;
            }
        }

        public TileMenuItem()
        {
            InitializeComponent();
        }
    }
}
