using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;
using TheXDS.Ganymede.Component;
using TheXDS.Ganymede.Services;
using TheXDS.MCART.Types.Extensions;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.Types.Base;

namespace TheXDS.Ganymede.Controls;

public partial class NavigationHost : ContentControl
{
    /// <summary>
    /// Represents the <see cref="OverlayContent"/> Avalonia property.
    /// </summary>
    public static readonly DirectProperty<NavigationHost, StyledElement?> OverlayContentProperty;
    
    /// <summary>
    /// Represents the <see cref="OverlayContentTemplate"/> property.
    /// </summary>
    public static readonly StyledProperty<IDataTemplate?> OverlayContentTemplateProperty;

    /// <summary>
    /// Represents the <see cref="DialogService"/> Avalonia property.
    /// </summary>
    public static readonly StyledProperty<INavigatingDialogService?> DialogServiceProperty;

    /// <summary>
    /// Represents the <see cref="Navigator"/> Avalonia property.
    /// </summary>
    public static readonly StyledProperty<INavigationService?> NavigatorProperty;

    /// <summary>
    /// Represents the <see cref="VisualResolver"/> Avalonia property.
    /// </summary>
    public static readonly StyledProperty<IVisualResolver<StyledElement>?> VisualResolverProperty;

    static NavigationHost()
    {
        OverlayContentProperty =
            AvaloniaProperty.RegisterDirect<NavigationHost, StyledElement?>(nameof(OverlayContent),
                o => o.OverlayContent);
        DialogServiceProperty =
            AvaloniaProperty.Register<NavigationHost, INavigatingDialogService?>(nameof(DialogService));
        DialogServiceProperty.OnChanged<NavigationHost, INavigatingDialogService?>(OnDialogServiceChanged);
        VisualResolverProperty =
            AvaloniaProperty.Register<NavigationHost, IVisualResolver<StyledElement>?>(nameof(VisualResolver));
        VisualResolverProperty.OnChanged<NavigationHost, IVisualResolver<StyledElement>?>(OnVisualResolverChanged);
        NavigatorProperty = 
            AvaloniaProperty.Register<NavigationHost, INavigationService?>(nameof(Navigator));
        NavigatorProperty.OnChanged<NavigationHost, INavigationService?>(OnNavigatorChanged);
        OverlayContentTemplateProperty = 
            AvaloniaProperty.Register<ContentControl, IDataTemplate?>(nameof(OverlayContentTemplate));
    }

    private StyledElement? _overlayContent;

    /// <summary>
    /// Gets the current overlay content of this control.
    /// </summary>
    [DependsOn(nameof(OverlayContentTemplate))]
    public StyledElement? OverlayContent
    {
         get => UiThread.Invoke(() => _overlayContent);
         private set => SetAndRaise(OverlayContentProperty, ref _overlayContent, value);
    }
    
    /// <summary>
    /// Gets or sets the data template used to display the overlay content of
    /// the control.
    /// </summary>
    public IDataTemplate? OverlayContentTemplate
    {
        get => GetValue(OverlayContentTemplateProperty);
        set => SetValue(OverlayContentTemplateProperty, value);
    }
    
    /// <summary>
    /// Gets or sets a reference to the <see cref="IVisualResolver{TVisual}"/>
    /// that will be used to resolve visual containers for the ViewModels
    /// hosted in the navigation service set on this instance.
    /// </summary>
    public IVisualResolver<StyledElement>? VisualResolver
    {
        get => UiThread.Invoke(() => GetValue(VisualResolverProperty));
        set => SetValue(VisualResolverProperty, value);
    }

    private static void OnVisualResolverChanged(NavigationHost instance, IVisualResolver<StyledElement>? _,
        IVisualResolver<StyledElement>? __)
    {
        instance.Navigator?.Refresh();
    }

    private static partial void SetDataContext<TVisual>(TVisual visual, IViewModel vm)
    {
        if (visual is StyledElement v) v.DataContext = vm;
    }
}