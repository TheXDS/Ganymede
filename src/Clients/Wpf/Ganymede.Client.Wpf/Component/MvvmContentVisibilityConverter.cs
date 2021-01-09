using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using TheXDS.Ganymede.Mvvm;

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
    public class MvvmContentIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)(MvvmContent)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (MvvmContent)(int)value;
        }
    }
}
