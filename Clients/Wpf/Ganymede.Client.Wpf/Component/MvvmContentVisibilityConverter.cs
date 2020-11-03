using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Ganymede.Client.Wpf.Component
{
    public class MvvmContentVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.Equals(parameter)) return Visibility.Visible;
            return Visibility.Collapsed;            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
