using System.Windows;
using System.Windows.Controls;
using TheXDS.Ganymede.Component;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.Services;
using TheXDS.Ganymede.Types.Base;
using static TheXDS.Ganymede.Helpers.DependencyObjectHelpers;

namespace TheXDS.Ganymede.Controls;

public partial class NavigationHost : Control
{
    private static readonly DependencyPropertyKey ContentPropertyKey;
    private static readonly DependencyPropertyKey OverlayContentPropertyKey;

    /// <summary>
    /// Represents the <see cref="Content"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ContentProperty;

    /// <summary>
    /// Represents the <see cref="OverlayContent"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty OverlayContentProperty;

    /// <summary>
    /// Represents the <see cref="Navigator"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty NavigatorProperty;

    /// <summary>
    /// Represents the <see cref="VisualResolver"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty VisualResolverProperty;

    /// <summary>
    /// Represents the <see cref="DialogService"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty DialogServiceProperty;

    static NavigationHost()
    {
        (ContentPropertyKey, ContentProperty) = NewDpRo<object?, NavigationHost>(nameof(Content));
        (OverlayContentPropertyKey, OverlayContentProperty) = NewDpRo<object?, NavigationHost>(nameof(OverlayContent));
        DialogServiceProperty = NewDp<INavigatingDialogService, NavigationHost>(nameof(DialogService), changedValue: OnDialogServiceChanged);
        NavigatorProperty = NewDp<INavigationService, NavigationHost>(nameof(Navigator), changedValue: OnNavigatorChanged);
        VisualResolverProperty = NewDp<IVisualResolver<FrameworkElement>, NavigationHost>(nameof(VisualResolver), changedValue: OnVisualResolverChanged);
        DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationHost), new FrameworkPropertyMetadata(typeof(NavigationHost)));
    }

    /// <summary>
    /// Gets the current content of this control.
    /// </summary>
    public object? Content
    {
        get => UiThread.Invoke(() => GetValue(ContentProperty));
        private set => SetValue(ContentPropertyKey, value);
    }

    /// <summary>
    /// Gets the current overlay content of this control.
    /// </summary>
    public object? OverlayContent
    {
        get => UiThread.Invoke(() => GetValue(OverlayContentProperty));
        private set => SetValue(OverlayContentPropertyKey, value);
    }

    /// <summary>
    /// Gets or sets a reference to the <see cref="IVisualResolver{TVisual}"/>
    /// that will be used to resolve visual containers for the ViewModels
    /// hosted in the navigation service set on this instance.
    /// </summary>
    public IVisualResolver<FrameworkElement>? VisualResolver
    {
        get => UiThread.Invoke(() => (IVisualResolver<FrameworkElement>?)GetValue(VisualResolverProperty));
        set => SetValue(VisualResolverProperty, value);
    }

    private static void OnNavigatorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        OnNavigatorChanged((NavigationHost)d,
            e.OldValue as INavigationService,
            e.NewValue as INavigationService);
    }

    private static void OnDialogServiceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        OnDialogServiceChanged((NavigationHost)d,
            e.OldValue as IDialogService,
            e.NewValue as IDialogService);
    }

    private static void OnVisualResolverChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((NavigationHost)d).Navigator?.Refresh();
    }

    private partial void SetDataContext<TVisual>(TVisual visual, IViewModel vm)
    {
        if (visual is FrameworkElement v) v.DataContext = vm;
    }
}