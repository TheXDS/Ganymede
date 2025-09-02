using System.Collections.ObjectModel;
using TheXDS.Ganymede.Component;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.Services;
using TheXDS.Ganymede.Types.Base;
using static TheXDS.MCART.Helpers.DependencyObjectHelpers;

namespace TheXDS.Ganymede.Controls;

public partial class NavigationHost : Control
{
    private static readonly DependencyPropertyKey ContentPropertyKey;
    private static readonly DependencyPropertyKey OverlayContentPropertyKey;
    private static readonly DependencyPropertyKey OverlayBackgroundStackPropertyKey;

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

    /// <summary>
    /// Represents the <see cref="OverlayBackgroundStack"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty OverlayBackgroundStackProperty;

    static NavigationHost()
    {
        (OverlayBackgroundStackPropertyKey, OverlayBackgroundStackProperty) = NewDpRo<ObservableCollection<FrameworkElement>, NavigationHost>(nameof(OverlayBackgroundStack), []);
        (ContentPropertyKey, ContentProperty) = NewDpRo<FrameworkElement?, NavigationHost>(nameof(Content));
        (OverlayContentPropertyKey, OverlayContentProperty) = NewDpRo<FrameworkElement?, NavigationHost>(nameof(OverlayContent));
        DialogServiceProperty = NewDp<IDialogService, NavigationHost>(nameof(DialogService), changedValue: OnDialogServiceChanged);
        NavigatorProperty = NewDp<INavigationService, NavigationHost>(nameof(Navigator), changedValue: OnNavigatorChanged);
        VisualResolverProperty = NewDp<IVisualResolver<FrameworkElement>, NavigationHost>(nameof(VisualResolver), changedValue: OnVisualResolverChanged);
        DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationHost), new FrameworkPropertyMetadata(typeof(NavigationHost)));
    }

    /// <summary>
    /// Gets the current content of this control.
    /// </summary>
    public FrameworkElement? Content
    {
        get => UiThread.Invoke(() => (FrameworkElement)GetValue(ContentProperty));
        private set => SetValue(ContentPropertyKey, value);
    }

    /// <summary>
    /// Gets the current overlay content of this control.
    /// </summary>
    public FrameworkElement? OverlayContent
    {
        get => UiThread.Invoke(() => (FrameworkElement)GetValue(OverlayContentProperty));
        private set => SetValue(OverlayContentPropertyKey, value);
    }

    /// <summary>
    /// Gets a reference to the stack of views that have been sent to the
    /// background.
    /// </summary>
    public ObservableCollection<FrameworkElement> OverlayBackgroundStack
    {
        get => UiThread.Invoke(() => (ObservableCollection<FrameworkElement>)GetValue(OverlayBackgroundStackProperty));
        private set => SetValue(OverlayBackgroundStackPropertyKey, value);
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

    private static partial void SetDataContext<TVisual>(TVisual visual, IViewModel vm)
    {
        if (visual is FrameworkElement v) v.DataContext = vm;
    }
}