using System.Windows;
using System.Windows.Controls;

namespace TheXDS.Ganymede.WpfBroker.Component
{
    /// <summary>
    /// Contiene una serie de propiedades de dependencia adjuntas para
    /// monitorear y controlar un <see cref="PasswordBox"/>.
    /// </summary>
    public class PasswordBoxMonitor : DependencyObject
    {
        /// <summary>
        /// Permite monitorear un <see cref="PasswordBox"/>.
        /// </summary>
        public static readonly DependencyProperty IsMonitoringProperty = DependencyProperty.RegisterAttached("IsMonitoring", typeof(bool), typeof(PasswordBoxMonitor), new UIPropertyMetadata(false, OnIsMonitoringChanged));

        /// <summary>
        /// Obtiene la longitud de la contraseña presentada en el
        /// <see cref="PasswordBox"/>.
        /// </summary>
        public static readonly DependencyProperty PasswordLengthProperty = DependencyProperty.RegisterAttached("PasswordLength", typeof(int), typeof(PasswordBoxMonitor), new UIPropertyMetadata(0));

        /// <summary>
        /// Emula una propiedad <see cref="PasswordBox.Password"/> bindeable.
        /// </summary>
        public static readonly DependencyProperty PasswordProperty = DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordBoxMonitor), new UIPropertyMetadata(string.Empty));

        /// <summary>
        /// Obtiene el valor de la propiedad de dependencia adjunta
        /// <see cref="IsMonitoringProperty"/>.
        /// </summary>
        /// <param name="obj">
        /// Instancia para la cual obtener el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si esta instancia monitorea al
        /// <see cref="PasswordBox"/> especificado, <see langword="false"/> en
        /// caso contrario.
        /// </returns>
        public static bool GetIsMonitoring(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsMonitoringProperty);
        }

        /// <summary>
        /// Establece el valor de la propiedad de dependencia adjunta
        /// <see cref="IsMonitoringProperty"/> para el objeto especificado.
        /// </summary>
        /// <param name="obj">
        /// Objeto para el cual establecer el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <param name="value">Valor de la propiedad de dependencia adjunta.</param>
        public static void SetIsMonitoring(DependencyObject obj, bool value)
        {
            obj.SetValue(IsMonitoringProperty, value);
        }

        /// <summary>
        /// Obtiene el valor de la propiedad de dependencia adjunta
        /// <see cref="PasswordLengthProperty"/>.
        /// </summary>
        /// <param name="obj">
        /// Instancia para la cual obtener el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <returns>
        /// La longitud de la contraseña para el <see cref="PasswordBox"/>
        /// monitoreado.
        /// </returns>
        public static int GetPasswordLength(DependencyObject obj)
        {
            return (int)obj.GetValue(PasswordLengthProperty);
        }

        /// <summary>
        /// Establece el valor de la propiedad de dependencia adjunta
        /// <see cref="PasswordLengthProperty"/> para el objeto especificado.
        /// </summary>
        /// <param name="obj">
        /// Objeto para el cual establecer el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <param name="value">Valor de la propiedad de dependencia adjunta.</param>
        public static void SetPasswordLength(DependencyObject obj, int value)
        {
            obj.SetValue(PasswordLengthProperty, value);
        }

        /// <summary>
        /// Obtiene el valor de la propiedad de dependencia adjunta
        /// <see cref="PasswordProperty"/>.
        /// </summary>
        /// <param name="obj">
        /// Instancia para la cual obtener el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <returns>
        /// La contraseña establecida en el <see cref="PasswordBox"/>
        /// monitoreado.
        /// </returns>
        public static string GetPassword(DependencyObject obj)
        {
            return (string)obj.GetValue(PasswordProperty);
        }

        /// <summary>
        /// Establece el valor de la propiedad de dependencia adjunta
        /// <see cref="PasswordProperty"/> para el objeto especificado.
        /// </summary>
        /// <param name="obj">
        /// Objeto para el cual establecer el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <param name="value">Valor de la propiedad de dependencia adjunta.</param>
        public static void SetPassword(DependencyObject obj, string value)
        {
            obj.SetValue(PasswordProperty, value);
        }


        private static void OnIsMonitoringChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox pb)
            {
                if ((bool)e.NewValue)
                {
                    pb.PasswordChanged += PasswordChanged;
                }
                else
                {
                    pb.PasswordChanged -= PasswordChanged;
                }
            }
        }

        static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox pb)
            {
                SetPassword(pb, pb.Password);
                SetPasswordLength(pb, pb.Password.Length);
            }
        }
    }
}
