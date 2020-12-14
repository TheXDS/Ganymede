using System;
using System.Globalization;
using System.Windows.Data;
using TheXDS.MCART.Types.Extensions;
using MC = System.Windows.Media.Color;

namespace TheXDS.Ganymede.Client
{
    /// <summary>
    /// Convierte un valor <see cref="TransparentColor"/> a un color utilizable por WPF.
    /// </summary>
    public class TransparentColorAdapter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is TransparentColor c && targetType == typeof(MC) ? (MC)c : targetType.Default()!;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }
}
