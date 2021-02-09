using System.Windows;
using System.Windows.Controls;

namespace TheXDS.Ganymede.WpfBroker.Widgets
{
    /// <summary>
    /// Host principal que encapsula y presenta páginas de Ganymede.
    /// </summary>
    public class TabHostControl : TabControl
    {
        static TabHostControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TabHostControl), new FrameworkPropertyMetadata(typeof(TabHostControl)));
        }

        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="EmptyContent"/>.
        /// </summary>
        public static readonly DependencyProperty EmptyContentProperty = DependencyProperty.Register(
            nameof(EmptyContent),
            typeof(FrameworkElement),
            typeof(TabHostControl),
            new PropertyMetadata(null));


        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="PreHeaderContent"/>.
        /// </summary>
        public static readonly DependencyProperty PreHeaderContentProperty = DependencyProperty.Register(
            nameof(PreHeaderContent),
            typeof(FrameworkElement),
            typeof(TabHostControl),
            new PropertyMetadata(null));

        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="PostHeaderContent"/>.
        /// </summary>
        public static readonly DependencyProperty PostHeaderContentProperty = DependencyProperty.Register(
            nameof(PostHeaderContent),
            typeof(FrameworkElement),
            typeof(TabHostControl),
            new PropertyMetadata(null));

        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="TabStripVisible"/>.
        /// </summary>
        public static readonly DependencyProperty TabStripVisibleProperty = DependencyProperty.Register(
            nameof(TabStripVisible),
            typeof(bool),
            typeof(TabHostControl),
            new PropertyMetadata(true));

        /// <summary>
        /// Obtiene o establece el contenido a mostrar cuando no hayan páginas
        /// activas.
        /// </summary>
        public FrameworkElement? EmptyContent
        {
            get => (FrameworkElement?)GetValue(EmptyContentProperty);
            set => SetValue(EmptyContentProperty, value);
        }

        /// <summary>
        /// Obtiene o establece el contenido a mostrar antes del encabezado de
        /// páginas.
        /// </summary>
        public FrameworkElement? PreHeaderContent
        {
            get => (FrameworkElement?)GetValue(PreHeaderContentProperty);
            set => SetValue(PreHeaderContentProperty, value);
        }

        /// <summary>
        /// Obtiene o establece el contenido a mostrar después del encabezado
        /// de páginas.
        /// </summary>
        public FrameworkElement? PostHeaderContent
        {
            get => (FrameworkElement?)GetValue(PostHeaderContentProperty);
            set => SetValue(PostHeaderContentProperty, value);
        }

        /// <summary>
        /// Obtiene o establece un valor que indica si el encabezado de páginas
        /// debe ser visible o no. Útil si se pretende implementar un mecanismo
        /// personalizado de selección, o si no se espera contener más de una
        /// página de Ganymede a la vez en un host visual.
        /// </summary>
        public bool TabStripVisible
        {
            get => (bool)GetValue(TabStripVisibleProperty);
            set => SetValue(TabStripVisibleProperty, value);
        }
    }
}
