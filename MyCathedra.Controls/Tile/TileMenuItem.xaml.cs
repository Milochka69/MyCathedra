using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyCathedra.Controls.Tile
{
    /// <summary>
    /// Логика взаимодействия для DrawerMenuItem.xaml
    /// </summary>
    public partial class TileMenuItem : UserControl
    {
        /// <summary>
        /// The image displayed by the button.
        /// </summary>
        /// <remarks>The image is specified in XAML as an absolute or relative path.</remarks>
        [Description("The image displayed by the button."), Category("Maket")]
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
        [Description("The text displayed by the button."), Category("Maket")]
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

        [Description("Short explanations."), Category("Maket")]
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
