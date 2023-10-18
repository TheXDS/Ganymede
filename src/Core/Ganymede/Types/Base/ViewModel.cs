﻿using TheXDS.Ganymede.Services;
using TheXDS.MCART.Types.Base;

namespace TheXDS.Ganymede.Types.Base;

/// <summary>
/// Base class for all Ganymede ViewModels.
/// </summary>
public abstract class ViewModel : ViewModelBase
{
    private string? _Title;
    private IDialogService? dialogService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ViewModel"/> class.
    /// </summary>
    protected ViewModel()
    {
        OnCreated();
    }

    /// <summary>
    /// Gets or sets this ViewModel title.
    /// </summary>
    public string? Title
    {
        get => _Title;
        set => Change(ref _Title, value);
    }

    /// <summary>
    /// Gets a reference to the current dialog service.
    /// </summary>
    public IDialogService? DialogService
    { 
        get => dialogService;
        set => dialogService = value;
    }

    /// <summary>
    /// Gets a reference to a navigation service bound to this instance.
    /// </summary>
    public INavigationService? NavigationService { get; set; }

    /// <summary>
    /// Called by the navigation service after a successful navigation to this
    /// ViewModel. Override this method to do lengthy initializations on the
    /// ViewModel, like fetching and preparing data to be displayed, etc.
    /// </summary>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await the async operation.
    /// </returns>
    protected virtual Task OnCreated() => Task.CompletedTask;

    internal void SetIsBusy_Internal(bool value) => IsBusy = value;
}