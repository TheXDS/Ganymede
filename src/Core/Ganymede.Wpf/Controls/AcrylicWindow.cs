using System.Windows;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.Ganymede.Controls;

/// <summary>
/// Defines a window that automatically applies acrylic background effects
/// and allows for title bar customization.
/// </summary>
public abstract class AcrylicWindow : ModernWindow
{
    static AcrylicWindow()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(AcrylicWindow), new FrameworkPropertyMetadata(typeof(AcrylicWindow)));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AcrylicWindow"/>
    /// class.
    /// </summary>
    public AcrylicWindow()
    {
        Loaded += (_, __) => this.EnableMicaIfSupported();
    }
}
