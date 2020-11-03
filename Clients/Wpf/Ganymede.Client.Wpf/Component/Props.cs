using System.Windows;

namespace Ganymede.Client.Wpf.Component
{
    public static class Props
    {
        public static FrameworkElement? GetAltContent(DependencyObject obj)
        {
            return (FrameworkElement?)obj.GetValue(AltContentProperty);
        }

        public static void SetAltContent(DependencyObject obj, FrameworkElement? value)
        {
            obj.SetValue(AltContentProperty, value);
        }
        
        public static readonly DependencyProperty AltContentProperty =
            DependencyProperty.RegisterAttached("AltContent", typeof(FrameworkElement), typeof(Props), new PropertyMetadata(null));
    }
}
