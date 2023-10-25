﻿using System.Windows;
using System.Windows.Controls;
using TheXDS.Ganymede.Component;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.Services;
using TheXDS.Ganymede.Types.Base;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.Ganymede.Helpers.Common;

namespace TheXDS.Ganymede.Controls;

/// <summary>
/// Hosts a ViewModel navigation service and renders its visual container.
/// </summary>
public class NavigationViewModelHost : Control
{
    private static readonly DependencyPropertyKey ContentPropertyKey;
    private static readonly DependencyPropertyKey OverlayContentPropertyKey;
    private readonly NavigatingDialogVisualResolver _dialogVisResolver = new();

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

    static NavigationViewModelHost()
    {
        ContentPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(Content),
            typeof(object),
            typeof(NavigationViewModelHost),
            new PropertyMetadata(null));

        OverlayContentPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(OverlayContent),
            typeof(object),
            typeof(NavigationViewModelHost),
            new PropertyMetadata(null));

        DialogServiceProperty = DependencyProperty.Register(
            nameof(DialogService),
            typeof(IDialogService),
            typeof(NavigationViewModelHost),
            new PropertyMetadata(null, OnDialogServiceChanged));

        NavigatorProperty = DependencyProperty.Register(
            nameof(Navigator),
            typeof(INavigationService),
            typeof(NavigationViewModelHost),
            new PropertyMetadata(null, OnNavigatorChanged));

        VisualResolverProperty = DependencyProperty.Register(
            nameof(VisualResolver),
            typeof(IVisualResolver<FrameworkElement>),
            typeof(NavigationViewModelHost),
            new PropertyMetadata(null, OnVisualResolverChanged));

        DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationViewModelHost), new FrameworkPropertyMetadata(typeof(NavigationViewModelHost)));

        ContentProperty = ContentPropertyKey.DependencyProperty;
        OverlayContentProperty = OverlayContentPropertyKey.DependencyProperty;
    }

    /// <summary>
    /// Gets the current content of this control.
    /// </summary>
    public object? Content => UiThread.Invoke(() => GetValue(ContentProperty));

    /// <summary>
    /// Gets the current content of this control.
    /// </summary>
    public object? OverlayContent => UiThread.Invoke(() => GetValue(OverlayContentProperty));

    /// <summary>
    /// Gets or sets a reference to the <see cref="IDialogService"/> to provide
    /// to the ViewModels navigated to on this host.
    /// </summary>
    public IDialogService? DialogService
    {
        get => UiInvoke(() => (IDialogService?)GetValue(DialogServiceProperty));
        set => SetValue(DialogServiceProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="INavigationService"/> instance to host.
    /// </summary>
    public INavigationService? Navigator
    {
        get => UiInvoke(() => (INavigationService?)GetValue(NavigatorProperty));
        set => SetValue(NavigatorProperty, value);
    }

    /// <summary>
    /// Gets or sets a reference to the <see cref="IVisualResolver{TVisual}"/>
    /// that will be used to resolve visual containers for the ViewModels
    /// hosted in the navigation service set on this instance.
    /// </summary>
    public IVisualResolver<FrameworkElement>? VisualResolver
    {
        get => UiInvoke(() => (IVisualResolver<FrameworkElement>?)GetValue(VisualResolverProperty));
        set => SetValue(VisualResolverProperty, value);
    }

    private static void OnNavigatorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (e.OldValue is INavigationService oldNav)
        {
            oldNav.NavigationCompleted -= ((NavigationViewModelHost)d).OnNavigationCompleted;
        }
        if (e.NewValue is INavigationService newNav)
        {
            newNav.NavigationCompleted += ((NavigationViewModelHost)d).OnNavigationCompleted;
            newNav.Refresh();
        }
    }

    private static void OnDialogServiceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (e.OldValue is INavigationService oldNav)
        {
            oldNav.NavigationCompleted -= ((NavigationViewModelHost)d).OnDialogNavigationCompleted;
        }
        if (e.NewValue is INavigationService newNav)
        {
            newNav.NavigationCompleted += ((NavigationViewModelHost)d).OnDialogNavigationCompleted;
        }
    }

    private static void OnVisualResolverChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((NavigationViewModelHost)d).Navigator?.Refresh();
    }

    private void OnNavigationCompleted(object? sender, NavigationCompletedEventArgs e)
    {
        HandleNavigation(e.ViewModel, VisualResolver, ContentPropertyKey, Navigator, DialogService, true);
    }

    private void OnDialogNavigationCompleted(object? sender, NavigationCompletedEventArgs e)
    {
        HandleNavigation(e.ViewModel, _dialogVisResolver, OverlayContentPropertyKey, DialogService as NavigatingDialogService, null, false);
    }

    private void HandleNavigation<TNav>(ViewModel? vm, IVisualResolver<FrameworkElement>? resolver, DependencyPropertyKey contentProp, TNav? navSvc, IDialogService? dlgSvc, bool skipNavStack)
        where TNav : INavigationService
    {
        if (vm is not null && (skipNavStack || (navSvc?.NavigationStack.Contains(vm) ?? false)) && UiThread.Invoke(() => resolver?.Resolve(vm)) is { } view)
        {
            UiThread.Invoke(() => view.DataContext = vm);
            UiThread.Invoke(() => SetValue(contentProp, view));
            vm.NavigationService = navSvc;
            vm.DialogService ??= dlgSvc;
        }
        else
        {
            UiThread.Invoke(() => SetValue(contentProp, null));
        }
    }
}