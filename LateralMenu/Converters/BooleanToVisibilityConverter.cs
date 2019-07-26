using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace LateralMenu.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public Visibility TrueVisibility { get; set; } = Visibility.Visible;
        public Visibility FalseVisibility { get; set; } = Visibility.Collapsed;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool)) return FalseVisibility;

            return (bool) value ? TrueVisibility : FalseVisibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
