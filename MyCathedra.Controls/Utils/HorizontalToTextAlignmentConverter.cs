using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MyCathedra.Controls.Utils
{
    public class HorizontalToTextAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            TextAlignment textAlignment;
            switch ((int) value)
            {
                case 0:
                    textAlignment = TextAlignment.Left;
                    break;
                case 1:
                    textAlignment = TextAlignment.Center;
                    break;
                case 2:
                    textAlignment = TextAlignment.Right;
                    break;
                default:
                    textAlignment = TextAlignment.Justify;
                    break;
            }

            return textAlignment;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            HorizontalAlignment horizontalAlignment;
            switch ((int) value)
            {
                case 0:
                    horizontalAlignment = HorizontalAlignment.Left;
                    break;
                case 1:
                    horizontalAlignment = HorizontalAlignment.Right;
                    break;
                case 2:
                    horizontalAlignment = HorizontalAlignment.Center;
                    break;
                default:
                    horizontalAlignment = HorizontalAlignment.Stretch;
                    break;
            }

            return horizontalAlignment;
        }
    }
}