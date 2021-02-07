using System.Windows;
using System.Windows.Controls;

namespace Ganymede.Client.Wpf.Component
{
    /// <summary>
    /// Define una serie de propiedades de dependencia adjuntas que auxilian en la definición de UI de un cliente Ganymede.
    /// </summary>
    public static class Props
    {
        /// <summary>
        /// Obtiene el valor de la propiedad de dependencia adjunta
        /// <see cref="AltContentProperty"/>.
        /// </summary>
        /// <param name="obj">
        /// Objeto desde el cual obtener el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <returns>
        /// El valor de la propiedad de dependencia adjunta
        /// <see cref="AltContentProperty"/>.
        /// </returns>
        public static FrameworkElement? GetAltContent(DependencyObject obj)
        {
            return (FrameworkElement?)obj.GetValue(AltContentProperty);
        }

        /// <summary>
        /// Establece el valor de la propiedad de dependencia adjunta
        /// <see cref="AltContentProperty"/>.
        /// </summary>
        /// <param name="obj">
        /// Objeto para el cual establecer el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <param name="value">Valor a establecer en la propiedad.</param>
        public static void SetAltContent(DependencyObject obj, FrameworkElement? value)
        {
            obj.SetValue(AltContentProperty, value);
        }
        
        /// <summary>
        /// Propiedad de dependencia adjunta que permite establecer un
        /// contenido alternativo para un <see cref="TabControl"/> que funciona
        /// como el Host visual de la aplicación.
        /// </summary>
        public static readonly DependencyProperty AltContentProperty =
            DependencyProperty.RegisterAttached("AltContent", typeof(FrameworkElement), typeof(Props), new PropertyMetadata(null));
    }
}
