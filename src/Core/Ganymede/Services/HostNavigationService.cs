﻿using TheXDS.Ganymede.Types.Base;

namespace TheXDS.Ganymede.Services;

/// <summary>
/// Navigation service tailored for Host ViewModels.
/// </summary>
/// <typeparam name="T">
/// ViewModel type to expose.
/// </typeparam>
public class HostNavigationService<T> : NavigationService<T>, INavigationService<T> where T: class, IViewModel
{
    private IViewModel? _host;

    /// <summary>
    /// Sets the Host on this navigation service.
    /// </summary>
    /// <param name="host">Host to be set on this navigation service.</param>
    public void SetHost(IViewModel host)
    {
        _host = host;
    }

    Task INavigationService<T>.Navigate(T viewModel)
    {
        viewModel.DialogService = _host?.DialogService;
        return Navigate(viewModel);
    }

    Task INavigationService<T>.NavigateAndReset(T? viewModel)
    {
        if (viewModel is not null) viewModel.DialogService = _host?.DialogService;
        return NavigateAndReset(viewModel);
    }
}
