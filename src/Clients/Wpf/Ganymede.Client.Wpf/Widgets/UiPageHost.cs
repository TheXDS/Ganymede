using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Controls;

namespace TheXDS.Ganymede.Client.Wpf.Widgets
{
    /// <summary>
    /// Host principal para el contenido visual resuelto para un
    /// <see cref="PageViewModel"/>. Contiene además distintos servicios
    /// básicos de UI.
    /// </summary>
    public class UiPageHost : Control
    {
        public static readonly DependencyProperty PageProperty = DependencyProperty.Register(nameof(Page), typeof(Page), typeof(UiPageHost), new PropertyMetadata(null, OnSetPage));

        private static void OnSetPage(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((UiPageHost)d)._content?.Navigate(e.NewValue);
        }

        static UiPageHost()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(UiPageHost), new FrameworkPropertyMetadata(typeof(UiPageHost)));
        }



        public override void OnApplyTemplate()
        {
            (_content = Get<Frame>(nameof(_content)))?.Navigate(Page);
            _selector = Get<SelectorPanel>(nameof(_selector));
            
            base.OnApplyTemplate();
        }

        private T? Get<T>(string element) where T : FrameworkElement
        {
            return GetTemplateChild($"PART{nameof(_content)}") as T;
        }

        private Frame? _content;
        private SelectorPanel? _selector;

        public Page? Page
        {
            get => (Page?)GetValue(PageProperty);
            set => SetValue(PageProperty, value);
        }
    }
}
