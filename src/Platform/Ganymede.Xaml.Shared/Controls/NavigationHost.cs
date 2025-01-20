using System;
using System.Linq;
using TheXDS.Ganymede.Component;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.Services;
using TheXDS.Ganymede.Types.Base;

namespace TheXDS.Ganymede.Controls;

/// <summary>
/// Hosts a ViewModel navigation service and renders its visual container.
/// </summary>
public partial class NavigationHost
{
    private readonly NavigatingDialogVisualResolver _dialogVisResolver = new();

    /// <summary>
    /// Gets or sets a reference to the <see cref="INavigatingDialogService"/> to provide
    /// to the ViewModels navigated to on this host.
    /// </summary>
    public INavigatingDialogService? DialogService
    {
        get => UiThread.Invoke(() => (INavigatingDialogService?)GetValue(DialogServiceProperty));
        set => SetValue(DialogServiceProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="INavigationService"/> instance to host.
    /// </summary>
    public INavigationService? Navigator
    {
        get => UiThread.Invoke(() => (INavigationService?)GetValue(NavigatorProperty));
        set => SetValue(NavigatorProperty, value);
    }

    private static void OnDialogServiceChanged(NavigationHost instance, IDialogService? oldValue, IDialogService? newValue)
    {
        if (oldValue is INavigationService oldNav)
        {
            oldNav.NavigationCompleted -= instance.OnDialogNavigationCompleted;
        }
        if (newValue is INavigationService newNav)
        {
            newNav.NavigationCompleted += instance.OnDialogNavigationCompleted;
        }
    }

    private static void OnNavigatorChanged(NavigationHost instance, INavigationService? oldValue, INavigationService? newValue)
    {
        if (oldValue is not null)
        {
            oldValue.NavigationCompleted -= instance.OnNavigationCompleted;
        }
        if (newValue is not null)
        {
            newValue.NavigationCompleted += instance.OnNavigationCompleted;
            newValue.Refresh();
        }
    }

    private void OnNavigationCompleted(object? sender, NavigationCompletedEventArgs e)
    {
        HandleNavigation(
            e.ViewModel,
            VisualResolver,
            v => Content = v,
            Navigator,
            DialogService,
            true);
    }

    private void OnDialogNavigationCompleted(object? sender, NavigationCompletedEventArgs e)
    {
        HandleNavigation(
            e.ViewModel,
            _dialogVisResolver,
            v => OverlayContent = v,
            DialogService as NavigatingDialogService,
            null,
            false);
    }

    private void HandleNavigation<TNav, TVisual>(
        IViewModel? vm,
        IVisualResolver<TVisual>? resolver,
        Action<TVisual?> setContent,
        TNav? navSvc,
        IDialogService? dlgSvc,
        bool skipNavStack)
        where TNav : INavigationService
        where TVisual : class
    {
        if (vm is not null && (skipNavStack || (navSvc?.NavigationSet.Contains(vm) ?? false)) && UiThread.Invoke(() => resolver?.Resolve(vm)) is { } view)
        {
            UiThread.Invoke(() => SetDataContext(view, vm));
            UiThread.Invoke(() => setContent.Invoke(view));
            vm.NavigationService = navSvc;
            vm.DialogService ??= dlgSvc;
        }
        else
        {
            UiThread.Invoke(() => setContent.Invoke(null));
        }
    }

    private partial void SetDataContext<TVisual>(TVisual visual, IViewModel vm);
}
