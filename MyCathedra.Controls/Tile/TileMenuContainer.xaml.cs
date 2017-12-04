using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MyCathedra.Controls.Tile
{
    /// <summary>
    /// Логика взаимодействия для TileMenuContainer.xaml
    /// </summary>
    public partial class TileMenuContainer : ScrollViewer
    {
        private int _row = 0;
        private int _column = 0;

        /// <summary>
        /// The width of the child item.
        /// </summary>
        /// <remarks>Allows set child items width.</remarks>
        [Description("The width of the child item."), Category("Appearance")]
        public Double ChildWidth
        {
            get
            {
                return itemGrid.Children.Cast<FrameworkElement>().FirstOrDefault().Width;
            }
            set
            {
                foreach (FrameworkElement child in itemGrid.Children.Cast<FrameworkElement>())
                {
                    child.Width = value;
                }
            }
        }

        /// <summary>
        /// The height of the child item.
        /// </summary>
        /// <remarks>Allows set child items height.</remarks>
        [Description("The height of the child item."), Category("Appearance")]
        public Double ChildHeight
        {
            get
            {
                return itemGrid.Children.Cast<FrameworkElement>().FirstOrDefault().Height;
            }
            set
            {
                foreach (FrameworkElement child in itemGrid.Children.Cast<FrameworkElement>())
                {
                    child.Height = value;
                }
            }
        }

        /// <summary>
        /// The margin of the child items.
        /// </summary>
        /// <remarks>Allows set child items margin.</remarks>
        [Description("The margin of the child items."), Category("Appearance")]
        public Thickness ChildMargin
        {
            get
            {
                return itemGrid.Children.Cast<FrameworkElement>().FirstOrDefault().Margin;
            }
            set
            {
                foreach (FrameworkElement child in itemGrid.Children.Cast<FrameworkElement>())
                {
                    child.Margin = value;
                }
            }
        }

        public TileMenuContainer()
        {
            InitializeComponent();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            _column = (int) (Width / (ChildWidth + ChildMargin.Bottom * 2));
        }
    }
}
