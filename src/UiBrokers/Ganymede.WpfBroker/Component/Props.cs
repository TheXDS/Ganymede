using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TheXDS.Ganymede.WpfBroker.Component
{
    /// <summary>
    /// Define una serie de propiedades de dependencia adjuntas que auxilian en la definición de UI de un cliente Ganymede.
    /// </summary>
    public static class Props
    {
        /// <summary>
        /// Propiedad de dependencia adjunta que permite definir un color de
        /// acento a utilizar para decorar controles cuyas plantillas lo
        /// soporten.
        /// </summary>
        public static readonly DependencyProperty AccentProperty = DependencyProperty.RegisterAttached("Accent", typeof(Brush), typeof(Props), new FrameworkPropertyMetadata(SystemColors.HighlightBrush, FrameworkPropertyMetadataOptions.AffectsRender));
        
        /// <summary>
        /// Propiedad de dependencia adjunta que permite establecer un
        /// contenido alterno para los controles que lo soporten.
        /// </summary>
        public static readonly DependencyProperty AltContentProperty = DependencyProperty.RegisterAttached("AltContent", typeof(FrameworkElement), typeof(Props), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsArrange));

        /// <summary>
        /// Propiedad de dependencia adjunta que permite establecer un
        /// contenido alterno para los controles que lo soporten.
        /// </summary>
        public static readonly DependencyProperty TabHeaderAltContentProperty = DependencyProperty.RegisterAttached("TabHeaderAltContent", typeof(FrameworkElement), typeof(Props), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsArrange));


        /// <summary>
        /// Propiedad de dependencia adjunta que permite definir un estado de
        /// ocupado para un control.
        /// </summary>
        public static readonly DependencyProperty BusyProperty = DependencyProperty.RegisterAttached("Busy", typeof(bool), typeof(Props), new PropertyMetadata(false));

        public static readonly DependencyProperty HorizontalOffsetProperty = DependencyProperty.RegisterAttached("HorizontalOffset", typeof(double), typeof(Props), new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnHorizontalOffsetPropertyChanged));

        public static readonly DependencyProperty HorizontalScrollBindingProperty = DependencyProperty.RegisterAttached("HorizontalScrollBinding", typeof(bool?), typeof(Props));

        /// <summary>
        /// Propiedad de dependencia adjunta que permite definir un ícono a
        /// mostrar en los controles cuyas plantillas lo soportan.
        /// </summary>
        public static readonly DependencyProperty IconProperty = DependencyProperty.RegisterAttached("Icon", typeof(string), typeof(Props), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty IconBrushProperty = DependencyProperty.RegisterAttached("IconBrush", typeof(Brush), typeof(Props), new PropertyMetadata((object)null!));


        /// <summary>
        /// Propiedad de dependencia adjunta que permite definir un color de
        /// acento a utilizar para decorar el texto de los controles cuyas
        /// plantillas lo soporten.
        /// </summary>
        public static readonly DependencyProperty TextAccentProperty = DependencyProperty.RegisterAttached("TextAccent", typeof(Brush), typeof(Props), new PropertyMetadata(Brushes.White));

        /// <summary>
        /// Propiedad de dependencia adjunta que permite definir un color de
        /// acento secundario a utilizar para decorar el texto de los controles
        /// cuyas plantillas lo soporten.
        /// </summary>
        public static readonly DependencyProperty TextPressAccentProperty = DependencyProperty.RegisterAttached("TextPressAccent", typeof(Brush), typeof(Props), new PropertyMetadata(Brushes.Black));

        public static readonly DependencyProperty VerticalOffsetProperty = DependencyProperty.RegisterAttached("VerticalOffset", typeof(double), typeof(Props), new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnVerticalOffsetPropertyChanged));
        
        public static readonly DependencyProperty VerticalScrollBindingProperty = DependencyProperty.RegisterAttached("VerticalScrollBinding", typeof(bool?), typeof(Props));

        /// <summary>
        /// Propiedad de dependencia adjunta que permite establecer un estado
        /// de advertencia para los controles cuya plantilla lo soporte.
        /// </summary>
        public static readonly DependencyProperty WarnedProperty = DependencyProperty.RegisterAttached("Warned", typeof(bool), typeof(Props), new PropertyMetadata(false));

        /// <summary>
        /// Propiedad de dependencia adjunta que permite a una plantilla que 
        /// soporta marca de agua mostrarla de forma permanente.
        /// </summary>
        public static readonly DependencyProperty WatermarkAlwaysVisibleProperty = DependencyProperty.RegisterAttached("WatermarkAlwaysVisible", typeof(bool), typeof(Props), new PropertyMetadata(false));
        
        /// <summary>
        /// Propiedad de dependencia adjunta que permite definir un texto a
        /// utilizar como una marca de agua para decorar los controles cuyas
        /// plantillas lo soporten.
        /// </summary>
        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.RegisterAttached("Watermark", typeof(string), typeof(Props), new PropertyMetadata(string.Empty));
        
        public static readonly DependencyProperty WatermarkBrushProperty = DependencyProperty.RegisterAttached("WatermarkBrush", typeof(Brush), typeof(Props), new PropertyMetadata((object)null!));
        
        public static readonly DependencyProperty TabStripVisibleProperty = DependencyProperty.RegisterAttached("TabStripVisible", typeof(bool), typeof(Props), new PropertyMetadata(true));

        public static void BindVerticalOffset(ScrollViewer scrollViewer)
        {
            if (scrollViewer.GetValue(VerticalScrollBindingProperty) is not null) return;
            scrollViewer.SetValue(VerticalScrollBindingProperty, true);
            scrollViewer.ScrollChanged += (s, se) =>
            {
                if (se.VerticalChange == 0) return;
                SetVerticalOffset(scrollViewer, se.VerticalOffset);
            };
        }

        public static void BindHorizontalOffset(ScrollViewer scrollViewer)
        {
            if (scrollViewer.GetValue(HorizontalScrollBindingProperty) is not null) return;
            scrollViewer.SetValue(HorizontalScrollBindingProperty, true);
            scrollViewer.ScrollChanged += (s, se) =>
            {
                if (se.HorizontalChange == 0) return;
                SetHorizontalOffset(scrollViewer, se.HorizontalOffset);
            };
        }



        /// <summary>
        /// Obtiene el valor de la propiedad de dependencia adjunta
        /// <see cref="AccentProperty"/>.
        /// </summary>
        /// <param name="obj">
        /// Objeto desde el cual obtener el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <returns>
        /// El valor de la propiedad de dependencia adjunta
        /// <see cref="AccentProperty"/>.
        /// </returns>
        public static Brush GetAccent(UIElement obj)
        {
            return (Brush)obj.GetValue(AccentProperty);
        }

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
        /// Obtiene el valor de la propiedad de dependencia adjunta
        /// <see cref="TabHeaderAltContentProperty"/>.
        /// </summary>
        /// <param name="obj">
        /// Objeto desde el cual obtener el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <returns>
        /// El valor de la propiedad de dependencia adjunta
        /// <see cref="TabHeaderAltContentProperty"/>.
        /// </returns>
        public static FrameworkElement? GetTabHeaderAltContent(DependencyObject obj)
        {
            return (FrameworkElement?)obj.GetValue(TabHeaderAltContentProperty);
        }


        /// <summary>
        /// Obtiene el valor de la propiedad de dependencia adjunta
        /// <see cref="BusyProperty"/>.
        /// </summary>
        /// <param name="obj">
        /// Objeto desde el cual obtener el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <returns>
        /// El valor de la propiedad de dependencia adjunta
        /// <see cref="BusyProperty"/>.
        /// </returns>
        public static bool GetBusy(UIElement obj)
        {
            return (bool)obj.GetValue(BusyProperty);
        }

        /// <summary>
        /// Obtiene el valor de la propiedad de dependencia adjunta
        /// <see cref="HorizontalOffsetProperty"/>.
        /// </summary>
        /// <param name="obj">
        /// Objeto desde el cual obtener el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <returns>
        /// El valor de la propiedad de dependencia adjunta
        /// <see cref="HorizontalOffsetProperty"/>.
        /// </returns>
        public static double GetHorizontalOffset(UIElement obj)
        {
            return (double)obj.GetValue(HorizontalOffsetProperty);
        }

        /// <summary>
        /// Obtiene el valor de la propiedad de dependencia adjunta
        /// <see cref="HorizontalScrollBindingProperty"/>.
        /// </summary>
        /// <param name="obj">
        /// Objeto desde el cual obtener el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <returns>
        /// El valor de la propiedad de dependencia adjunta
        /// <see cref="HorizontalScrollBindingProperty"/>.
        /// </returns>
        public static bool? GetHorizontalScrollBinding(UIElement obj)
        {
            return (bool?)obj.GetValue(HorizontalScrollBindingProperty);
        }

        /// <summary>
        /// Obtiene el valor de la propiedad de dependencia adjunta
        /// <see cref="IconProperty"/>.
        /// </summary>
        /// <param name="obj">
        /// Objeto desde el cual obtener el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <returns>
        /// El valor de la propiedad de dependencia adjunta
        /// <see cref="IconProperty"/>.
        /// </returns>
        public static string GetIcon(UIElement obj)
        {
            return (string)obj.GetValue(IconProperty);
        }

        /// <summary>
        /// Obtiene el valor de la propiedad de dependencia adjunta
        /// <see cref="TextAccentProperty"/>.
        /// </summary>
        /// <param name="obj">
        /// Objeto desde el cual obtener el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <returns>
        /// El valor de la propiedad de dependencia adjunta
        /// <see cref="TextAccentProperty"/>.
        /// </returns>
        public static Brush GetTextAccent(UIElement obj)
        {
            return (Brush)obj.GetValue(TextAccentProperty);
        }

        /// <summary>
        /// Obtiene el valor de la propiedad de dependencia adjunta
        /// <see cref="TextPressAccentProperty"/>.
        /// </summary>
        /// <param name="obj">
        /// Objeto desde el cual obtener el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <returns>
        /// El valor de la propiedad de dependencia adjunta
        /// <see cref="TextPressAccentProperty"/>.
        /// </returns>
        public static Brush GetTextPressAccent(UIElement obj)
        {
            return (Brush)obj.GetValue(TextPressAccentProperty);
        }

        /// <summary>
        /// Obtiene el valor de la propiedad de dependencia adjunta
        /// <see cref="VerticalOffsetProperty"/>.
        /// </summary>
        /// <param name="obj">
        /// Objeto desde el cual obtener el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <returns>
        /// El valor de la propiedad de dependencia adjunta
        /// <see cref="VerticalOffsetProperty"/>.
        /// </returns>
        public static double GetVerticalOffset(DependencyObject obj)
        {
            return (double)obj.GetValue(VerticalOffsetProperty);
        }

        /// <summary>
        /// Obtiene el valor de la propiedad de dependencia adjunta
        /// <see cref="VerticalScrollBindingProperty"/>.
        /// </summary>
        /// <param name="obj">
        /// Objeto desde el cual obtener el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <returns>
        /// El valor de la propiedad de dependencia adjunta
        /// <see cref="VerticalScrollBindingProperty"/>.
        /// </returns>
        public static double GetVerticalScrollBinding(DependencyObject obj)
        {
            return (double)obj.GetValue(VerticalScrollBindingProperty);
        }

        /// <summary>
        /// Obtiene el valor de la propiedad de dependencia adjunta
        /// <see cref="WarnedProperty"/>.
        /// </summary>
        /// <param name="obj">
        /// Objeto desde el cual obtener el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <returns>
        /// El valor de la propiedad de dependencia adjunta
        /// <see cref="WarnedProperty"/>.
        /// </returns>
        public static bool GetWarned(UIElement obj)
        {
            return (bool)obj.GetValue(WarnedProperty);
        }

        /// <summary>
        /// Obtiene el valor de la propiedad de dependencia adjunta
        /// <see cref="WatermarkAlwaysVisibleProperty"/>.
        /// </summary>
        /// <param name="obj">
        /// Objeto desde el cual obtener el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <returns>
        /// El valor de la propiedad de dependencia adjunta
        /// <see cref="WatermarkAlwaysVisibleProperty"/>.
        /// </returns>
        public static bool GetWatermarkAlwaysVisible(UIElement obj)
        {
            return (bool)obj.GetValue(WatermarkAlwaysVisibleProperty);
        }

        /// <summary>
        /// Obtiene el valor de la propiedad de dependencia adjunta
        /// <see cref="WatermarkProperty"/>.
        /// </summary>
        /// <param name="obj">
        /// Objeto desde el cual obtener el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <returns>
        /// El valor de la propiedad de dependencia adjunta
        /// <see cref="WatermarkProperty"/>.
        /// </returns>
        public static string GetWatermark(DependencyObject obj)
        {
            return (string)obj.GetValue(WatermarkProperty);
        }

        /// <summary>
        /// Establece el valor de la propiedad de dependencia adjunta
        /// <see cref="AccentProperty"/>.
        /// </summary>
        /// <param name="obj">
        /// Objeto para el cual establecer el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <param name="value">Valor a establecer en la propiedad.</param>
        public static void SetAccent(UIElement obj, Brush value)
        {
            obj.SetValue(AccentProperty, value);
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
        /// Establece el valor de la propiedad de dependencia adjunta
        /// <see cref="TabHeaderAltContentProperty"/>.
        /// </summary>
        /// <param name="obj">
        /// Objeto para el cual establecer el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <param name="value">Valor a establecer en la propiedad.</param>
        public static void SetTabHeaderAltContent(DependencyObject obj, FrameworkElement? value)
        {
            obj.SetValue(TabHeaderAltContentProperty, value);
        }


        /// <summary>
        /// Establece el valor de la propiedad de dependencia adjunta
        /// <see cref="BusyProperty"/>.
        /// </summary>
        /// <param name="obj">
        /// Objeto para el cual establecer el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <param name="value">Valor a establecer en la propiedad.</param>
        public static void SetBusy(UIElement obj, bool value)
        {
            obj.SetValue(BusyProperty, value);
        }

        /// <summary>
        /// Establece el valor de la propiedad de dependencia adjunta
        /// <see cref="HorizontalOffsetProperty"/>.
        /// </summary>
        /// <param name="obj">
        /// Objeto para el cual establecer el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <param name="value">Valor a establecer en la propiedad.</param>
        public static void SetHorizontalOffset(UIElement obj, double value)
        {
            obj.SetValue(HorizontalOffsetProperty, value);
        }

        /// <summary>
        /// Establece el valor de la propiedad de dependencia adjunta
        /// <see cref="HorizontalScrollBindingProperty"/>.
        /// </summary>
        /// <param name="obj">
        /// Objeto para el cual establecer el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <param name="value">Valor a establecer en la propiedad.</param>
        public static void SetHorizontalScrollBinding(UIElement obj, bool? value)
        {
            obj.SetValue(HorizontalScrollBindingProperty, value);
        }

        /// <summary>
        /// Establece el valor de la propiedad de dependencia adjunta
        /// <see cref="IconProperty"/>.
        /// </summary>
        /// <param name="obj">
        /// Objeto para el cual establecer el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <param name="value">Valor a establecer en la propiedad.</param>
        public static void SetIcon(UIElement obj, string value)
        {
            obj.SetValue(IconProperty, value);
        }

        /// <summary>
        /// Establece el valor de la propiedad de dependencia adjunta
        /// <see cref="TextAccentProperty"/>.
        /// </summary>
        /// <param name="obj">
        /// Objeto para el cual establecer el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <param name="value">Valor a establecer en la propiedad.</param>
        public static void SetTextAccent(UIElement obj, Brush value)
        {
            obj.SetValue(TextAccentProperty, value);
        }

        /// <summary>
        /// Establece el valor de la propiedad de dependencia adjunta
        /// <see cref="TextPressAccentProperty"/>.
        /// </summary>
        /// <param name="obj">
        /// Objeto para el cual establecer el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <param name="value">Valor a establecer en la propiedad.</param>
        public static void SetTextPressAccent(UIElement obj, Brush value)
        {
            obj.SetValue(TextPressAccentProperty, value);
        }

        /// <summary>
        /// Establece el valor de la propiedad de dependencia adjunta
        /// <see cref="VerticalOffsetProperty"/>.
        /// </summary>
        /// <param name="obj">
        /// Objeto para el cual establecer el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <param name="value">Valor a establecer en la propiedad.</param>
        public static void SetVerticalOffset(DependencyObject obj, double value)
        {
            obj.SetValue(VerticalOffsetProperty, value);
        }

        /// <summary>
        /// Establece el valor de la propiedad de dependencia adjunta
        /// <see cref="VerticalScrollBindingProperty"/>.
        /// </summary>
        /// <param name="obj">
        /// Objeto para el cual establecer el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <param name="value">Valor a establecer en la propiedad.</param>
        public static void SetVerticalScrollBinding(DependencyObject obj, bool? value)
        {
            obj.SetValue(VerticalScrollBindingProperty, value);
        }

        /// <summary>
        /// Establece el valor de la propiedad de dependencia adjunta
        /// <see cref="WarnedProperty"/>.
        /// </summary>
        /// <param name="obj">
        /// Objeto para el cual establecer el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <param name="value">Valor a establecer en la propiedad.</param>
        public static void SetWarned(UIElement obj, bool value)
        {
            obj.SetValue(WarnedProperty, value);
        }

        /// <summary>
        /// Establece el valor de la propiedad de dependencia adjunta
        /// <see cref="WatermarkAlwaysVisibleProperty"/>.
        /// </summary>
        /// <param name="obj">
        /// Objeto para el cual establecer el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <param name="value">Valor a establecer en la propiedad.</param>
        public static void SetWatermarkAlwaysVisible(UIElement obj, bool value)
        {
            obj.SetValue(WatermarkAlwaysVisibleProperty, value);
        }

        /// <summary>
        /// Establece el valor de la propiedad de dependencia adjunta
        /// <see cref="WatermarkProperty"/>.
        /// </summary>
        /// <param name="obj">
        /// Objeto para el cual establecer el valor de la propiedad de
        /// dependencia adjunta.
        /// </param>
        /// <param name="value">Valor a establecer en la propiedad.</param>
        public static void SetWatermark(DependencyObject obj, string value)
        {
            obj.SetValue(WatermarkProperty, value);
        }

        public static Brush? GetIconBrush(DependencyObject obj)
        {
            return (Brush?)obj.GetValue(IconBrushProperty);
        }

        public static void SetIconBrush(DependencyObject obj, Brush? value)
        {
            obj.SetValue(IconBrushProperty, value);
        }

        public static Brush? GetWatermarkBrush(DependencyObject obj)
        {
            return (Brush?)obj.GetValue(WatermarkBrushProperty);
        }

        public static void SetWatermarkBrush(DependencyObject obj, Brush? value)
        {
            obj.SetValue(WatermarkBrushProperty, value);
        }

        public static bool GetTabStripVisible(DependencyObject obj)
        {
            return (bool)obj.GetValue(TabStripVisibleProperty);
        }

        public static void SetTabStripVisible(DependencyObject obj, bool value)
        {
            obj.SetValue(TabStripVisibleProperty, value);
        }



        private static void OnHorizontalOffsetPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            switch (d)
            {
                case ScrollViewer scrollViewer:
                    //BindHorizontalOffset(scrollViewer);
                    scrollViewer.ScrollToHorizontalOffset((double)e.NewValue);
                    break;
                case ScrollContentPresenter scrollPresenter:
                    scrollPresenter.SetHorizontalOffset((double)e.NewValue);
                    break;
            }
        }
        private static void OnVerticalOffsetPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            switch (d)
            {
                case ScrollViewer scrollViewer:
                    BindVerticalOffset(scrollViewer);
                    scrollViewer.ScrollToVerticalOffset((double)e.NewValue);
                    break;
                case ScrollContentPresenter scrollPresenter:
                    scrollPresenter.SetVerticalOffset((double)e.NewValue);

                    break;
            }
        }

    }
}
