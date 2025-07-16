using Avalonia;
using Avalonia.Controls.Primitives;
using System.Collections.ObjectModel;
using TheXDS.Ganymede.Helpers;

namespace TheXDS.Ganymede.Controls;

public partial class NavigationStack : TemplatedControl
{
    /// <summary>
    /// Represents the <see cref="ForegroundObject"/> property.
    /// </summary>
    public static readonly StyledProperty<StyledElement?> ForegroundObjectProperty =
        AvaloniaProperty.Register<NavigationStack, StyledElement?>(nameof(ForegroundObject));

    /// <summary>
    /// Represents the <see cref="BackgroundStack"/> property.
    /// </summary>
    public static readonly StyledProperty<ObservableCollection<StyledElement>> BackgroundStackProperty =
        AvaloniaProperty.Register<NavigationStack, ObservableCollection<StyledElement>>(nameof(BackgroundStack), []);

    /// <summary>
    /// Gets or sets a reference to the stack of background objects in this
    /// control.
    /// </summary>
    public ObservableCollection<StyledElement> BackgroundStack
    {
        get => UiThread.Invoke(() => GetValue(BackgroundStackProperty));
        set => SetValue(BackgroundStackProperty, value);
    }

    /// <summary>
    /// Gets or sets a reference to the foreground object in this control.
    /// </summary>
    public StyledElement? ForegroundObject
    {
        get => UiThread.Invoke(() => GetValue(ForegroundObjectProperty));
        set => SetValue(ForegroundObjectProperty, value);
    }
}