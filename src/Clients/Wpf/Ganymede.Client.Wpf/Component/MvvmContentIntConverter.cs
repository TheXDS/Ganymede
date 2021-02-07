using System;
using System.Globalization;
using System.Windows.Data;
using TheXDS.Ganymede.Client.Wpf.Widgets;
using TheXDS.Ganymede.Mvvm;

namespace Ganymede.Client.Wpf.Component
{
    /// <summary>
    /// Infiere un valor de selección por medio del cual se obtiene un bloque de UI a mostrar en un <see cref="UiPageHost"/>.
    /// </summary>
    public class MvvmContentIntConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)(MvvmContent)value;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (MvvmContent)(int)value;
        }
    }
}
