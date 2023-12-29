using System.Windows;
using TheXDS.MCART.Types.Base;

namespace TheXDS.Ganymede.Controls;

/// <summary>
/// Extends the <see cref="Window"/> class to allow for easy title bar
/// customization, as well as better integration with MCART classes.
/// </summary>
public abstract class ModernWindow : Window, IWpfWindow
{
    static ModernWindow()
    {
        TitleBarContentProperty = DependencyProperty.Register(nameof(TitleBarContent), typeof(object), typeof(ModernWindow), new FrameworkPropertyMetadata(null));
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ModernWindow), new FrameworkPropertyMetadata(typeof(ModernWindow)));
    }

    /// <summary>
    /// Defines the <see cref="TitleBarContent"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TitleBarContentProperty;

    /// <summary>
    /// Gets or sets the title bar content for this window.
    /// </summary>
    public object TitleBarContent
    {
        get => GetValue(TitleBarContentProperty);
        set => SetValue(TitleBarContentProperty, value);
    }
}
