using System.Collections.ObjectModel;
using TheXDS.Ganymede.Helpers;
using static TheXDS.MCART.Helpers.DependencyObjectHelpers;

namespace TheXDS.Ganymede.Controls;

public partial class NavigationStack : Control
{
    /// <summary>
    /// Identifies the <see cref="BackgroundStack"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty BackgroundStackProperty;
    /// <summary>
    /// Identifies the <see cref="ForegroundObject"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ForegroundObjectProperty;

    static NavigationStack()
    {
        SetControlStyle<NavigationStack>(DefaultStyleKeyProperty);
        BackgroundStackProperty = NewDp<ObservableCollection<FrameworkElement>, NavigationStack>(nameof(BackgroundStack), []);
        ForegroundObjectProperty = NewDp<FrameworkElement?, NavigationStack>(nameof(ForegroundObject));
    }

    /// <summary>
    /// Gets or sets a reference to the stack of background objects in this
    /// control.
    /// </summary>
    public ObservableCollection<FrameworkElement> BackgroundStack
    {
        get => (ObservableCollection<FrameworkElement>)UiThread.Invoke(() => GetValue(BackgroundStackProperty));
        set => SetValue(BackgroundStackProperty, value);
    }

    /// <summary>
    /// Gets or sets a reference to the foreground object in this control.
    /// </summary>
    public FrameworkElement? ForegroundObject
    {
        get => UiThread.Invoke(() => (FrameworkElement)GetValue(ForegroundObjectProperty));
        set => SetValue(ForegroundObjectProperty, value);
    }
}
