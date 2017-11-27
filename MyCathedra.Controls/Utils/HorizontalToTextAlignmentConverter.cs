using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MyCathedra.Controls.Utils
{
    public class HorizontalToTextAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TextAlignment textAlignment;

            // All I'm doing here is simply getting the integer value of the Enumeration.
            switch ((int)value)
            {
                case 0:
                    // Left to Left
                    textAlignment = TextAlignment.Left;
                    break;
                case 1:
                    // Center to Center
                    textAlignment = TextAlignment.Center;
                    break;
                case 2:
                    // Right to Right
                    textAlignment = TextAlignment.Right;
                    break;
                default:
                    // Stretch to Justify
                    textAlignment = TextAlignment.Justify;
                    break;
            }

            return textAlignment;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            HorizontalAlignment horizontalAlignment;

            // All I'm doing here is simply getting the integer value of the Enumeration.
            switch ((int)value)
            {
                case 0:
                    // Left to Left
                    horizontalAlignment = HorizontalAlignment.Left;
                    break;
                case 1:
                    // Right to Right
                    horizontalAlignment = HorizontalAlignment.Right;
                    break;
                case 2:
                    // Center to Center
                    horizontalAlignment = HorizontalAlignment.Center;
                    break;
                default:
                    // Justify to Stretch
                    horizontalAlignment = HorizontalAlignment.Stretch;
                    break;
            }

            return horizontalAlignment;
        }
    }
}
