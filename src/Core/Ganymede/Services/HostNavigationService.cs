using TheXDS.Ganymede.Types.Base;

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
    /// <param name="host"></param>
    public void SetHost(IViewModel host)
    {
        _host = host;
    }

    void INavigationService<T>.Navigate(T viewModel)
    {
        viewModel.DialogService = _host?.DialogService;
        Navigate(viewModel);
    }

    void INavigationService<T>.NavigateAndReset(T? viewModel)
    {
        if (viewModel is not null) viewModel.DialogService = _host?.DialogService;
        NavigateAndReset(viewModel);
    }
}
