using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Media;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Math;

namespace TheXDS.Ganymede.Controls;

/// <summary>
/// Control that displays either normal or busy content.
/// </summary>
public class BusyContainer : ContentControl
{
    /// <summary>
    /// Identifies the <see cref="CurrentBusyEffect"/> direct property.
    /// </summary>
    public static readonly DirectProperty<BusyContainer, IEffect?> CurrentBusyEffectProperty =
        AvaloniaProperty.RegisterDirect<BusyContainer, IEffect?>(nameof(CurrentBusyEffect), b => b.IsBusy ? b.BusyEffect : null);
    
    /// <summary>
    /// Identifies the <see cref="BusyContent"/> styled property.
    /// </summary>
    public static readonly StyledProperty<object?> BusyContentProperty = 
        AvaloniaProperty.Register<BusyContainer,object?>(nameof(BusyContent), GetDefaultBusyContent());

    /// <summary>
    /// Identifies the <see cref="BusyContentTemplate"/> styled property.
    /// </summary>
    public static readonly StyledProperty<IDataTemplate?> BusyContentTemplateProperty = 
        AvaloniaProperty.Register<BusyContainer,IDataTemplate?>(nameof(BusyContentTemplate));
    
    /// <summary>
    /// Identifies the <see cref="BusyBackground"/> styled property.
    /// </summary>
    public static readonly StyledProperty<IBrush?> BusyBackgroundProperty = 
        AvaloniaProperty.Register<BusyContainer,IBrush?>(nameof(BusyBackground), GetDefaultBusyBackground());

    /// <summary>
    /// Identifies the <see cref="BusyOpacity"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double> BusyOpacityProperty = 
        AvaloniaProperty.Register<BusyContainer, double>(nameof(BusyOpacity), 0.5, validate:ChkBusyOpacity, coerce:CoerceBusyOpacity);
    
    /// <summary>
    /// Identifies the <see cref="BusyEffect"/> styled property.
    /// </summary>
    public static readonly StyledProperty<IEffect?> BusyEffectProperty =
        AvaloniaProperty.Register<BusyContainer, IEffect?>(nameof(BusyEffect), GetDefaultBusyEffect());
    
    /// <summary>
    /// Identifies the <see cref="IsBusy"/> styled property.
    /// </summary>
    public static readonly StyledProperty<bool> IsBusyProperty =
        AvaloniaProperty.Register<BusyContainer, bool>(nameof(IsBusy));
    
    private static BusyIndicator GetDefaultBusyContent()
    {
        return new BusyIndicator();
    }

    private static IBrush GetDefaultBusyBackground()
    {
        return (Application.Current?.TryGetResource("DefaultBackground", Application.Current.ActualThemeVariant,
            out var brush) ?? false) && brush is IBrush b
            ? b : Brushes.Black;
    }
    
    private static BlurEffect GetDefaultBusyEffect()
    {
        return new BlurEffect();
    }
    
    /// <summary>
    /// Gets or sets the content to be presented on this control when the
    /// <see cref="IsBusy"/> property is set to <see langword="true"/>.
    /// </summary>
    public object? BusyContent
    {
        get => GetValue(BusyContentProperty);
        set => SetValue(BusyContentProperty, value);
    }
        
    /// <summary>
    /// Gets or sets the template to be applied to the content being presented
    /// on this control when the <see cref="IsBusy"/> property is set to
    /// <see langword="true"/>.
    /// </summary>
    public IDataTemplate? BusyContentTemplate
    {
        get => GetValue(BusyContentTemplateProperty);
        set => SetValue(BusyContentTemplateProperty, value);
    }

    /// <summary>
    /// Gets or sets the background to be displayed on this control when the
    /// <see cref="IsBusy"/> property is set to <see langword="true"/>.
    /// </summary>
    public IBrush? BusyBackground
    {
        get => GetValue(BusyBackgroundProperty);
        set => SetValue(BusyBackgroundProperty, value);
    }

    /// <summary>
    /// Gets or sets the opacity of the overlay displayed on top of the content
    /// of this control when the <see cref="IsBusy"/> property is set to
    /// <see langword="true"/>.
    /// </summary>
    public double BusyOpacity
    {
        get => GetValue(BusyOpacityProperty);
        set => SetValue(BusyOpacityProperty, value);
    }

    /// <summary>
    /// Gets or sets an effect to be applied to the content being presented on
    /// this control when the <see cref="IsBusy"/> property is set to
    /// <see langword="true"/>.
    /// </summary>
    public IEffect? BusyEffect
    {
        get => GetValue(BusyEffectProperty);
        set => SetValue(BusyEffectProperty, value);
    }
    
    /// <summary>
    /// Gets or sets a value that indicates if the control should be displaying
    /// the main content or the "busy" content. 
    /// </summary>
    public bool IsBusy
    {
        get => GetValue(IsBusyProperty);
        set
        {
            SetValue(IsBusyProperty, value);
            if (value)
            {
                RaisePropertyChanged(CurrentBusyEffectProperty, null, CurrentBusyEffect);
            }
            else
            {
                RaisePropertyChanged(CurrentBusyEffectProperty, CurrentBusyEffect, null);
            }
        }
    }

    /// <summary>
    /// Gets a reference to the currently applied effect to the content of this control.
    /// </summary>
    public IEffect? CurrentBusyEffect => GetValue(CurrentBusyEffectProperty);
    
    private static double CoerceBusyOpacity(AvaloniaObject arg1, double arg2)
    {
        return arg2.Clamp(0.0, 1.0);
    }

    private static bool ChkBusyOpacity(double arg)
    {
        return arg.IsBetween(0.0, 1.0);
    }
}