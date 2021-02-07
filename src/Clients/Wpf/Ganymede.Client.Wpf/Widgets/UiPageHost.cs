using System.Windows;
using System.Windows.Controls;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Client.Wpf.Widgets
{
    /// <summary>
    /// Host principal para el contenido visual resuelto para un
    /// <see cref="PageViewModel"/>. Contiene además distintos servicios
    /// básicos de UI.
    /// </summary>
    public class UiPageHost : Control
    {
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="Page"/>.
        /// </summary>
        public static readonly DependencyProperty PageProperty = DependencyProperty.Register(nameof(Page), typeof(Page), typeof(UiPageHost), new PropertyMetadata(null, OnSetPage));

        private static void OnSetPage(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((UiPageHost)d)._content?.Navigate(e.NewValue);
        }

        static UiPageHost()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(UiPageHost), new FrameworkPropertyMetadata(typeof(UiPageHost)));
        }

        private Frame? _content;

        /// <summary>
        /// Obtiene o establece la página a mostrar en este control.
        /// </summary>
        public Page? Page
        {
            get => (Page?)GetValue(PageProperty);
            set => SetValue(PageProperty, value);
        }

        /// <inheritdoc/>
        public override void OnApplyTemplate()
        {
            (_content = GetTemplateChild($"PART{nameof(_content)}") as Frame)?.Navigate(Page);
            
            base.OnApplyTemplate();
        }
    }
}
