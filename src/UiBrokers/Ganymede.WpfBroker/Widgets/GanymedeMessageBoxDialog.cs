using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TheXDS.Ganymede.WpfBroker.Widgets
{
    /// <summary>
    /// Control que define el estado visual predeterminado a mostrar cuando un
    /// <see cref="UiPageHost"/> se encuentra en un estado que muestra un
    /// cuadro de diálogo con opciones de selección con botones.
    /// </summary>
    public class GanymedeMessageBoxDialog : Control
    {
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="TitleForeground"/>.
        /// </summary>
        public static readonly DependencyProperty TitleForegroundProperty =
            DependencyProperty.Register(nameof(TitleForeground), typeof(Brush), typeof(GanymedeMessageBoxDialog), new PropertyMetadata(SystemColors.ControlTextBrush));

        /// <summary>
        /// Obtiene o establece el <see cref="Brush"/> a utilizar para el color
        /// del mensaje de diálogo.
        /// </summary>
        public Brush TitleForeground
        {
            get { return (Brush)GetValue(TitleForegroundProperty); }
            set { SetValue(TitleForegroundProperty, value); }
        }

        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="TitleForeground"/>.
        /// </summary>
        public static readonly DependencyProperty TextForegroundProperty =
            DependencyProperty.Register(nameof(TextForeground), typeof(Brush), typeof(GanymedeMessageBoxDialog), new PropertyMetadata(SystemColors.ControlTextBrush));

        /// <summary>
        /// Obtiene o establece el <see cref="Brush"/> a utilizar para el color
        /// del mensaje de diálogo.
        /// </summary>
        public Brush TextForeground
        {
            get { return (Brush)GetValue(TitleForegroundProperty); }
            set { SetValue(TitleForegroundProperty, value); }
        }


        static GanymedeMessageBoxDialog()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GanymedeMessageBoxDialog), new FrameworkPropertyMetadata(typeof(GanymedeMessageBoxDialog)));
        }
    }
}
