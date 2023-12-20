using System.Windows.Input;
using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Types.Base;
using TheXDS.Ganymede.ViewModels;
using TheXDS.Ivie.Models.Local;
using TheXDS.MCART.Component;
using TheXDS.MCART.Exceptions;
using TheXDS.Triton.Models;
using TheXDS.Triton.Services;
using Sp = TheXDS.ServicePool.ServicePool;
using St = TheXDS.Ivie.Resources.Strings.ViewModels.HomepageViewModel;

namespace TheXDS.Ivie.ViewModels;

/// <summary>
/// ViewModel that controls the logic of the homepage.
/// </summary>
public class HomepageViewModel : HostViewModelBase, IStatefulViewModel<LocalSession>
{
    private LocalSession state = null!;

    /// <summary>
    /// Gets the command used to log out.
    /// </summary>
    public ICommand LogoutCommand { get; }

    /// <summary>
    /// Gets the command used to navigate to the app's settings.
    /// </summary>
    public ICommand SettingsCommand { get; }

    /// <inheritdoc/>
    public LocalSession State
    { 
        get => state;
        set => Change(ref state, value);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HomepageViewModel"/>
    /// class.
    /// </summary>
    public HomepageViewModel()
    {
        Title = St.Title;
        LogoutCommand = new SimpleCommand(OnLogout);
        SettingsCommand = new SimpleCommand(() => NavigationService!.NavigateAndReset<SettingsViewModel>());
    }

    private async Task OnLogout()
    {
        await DialogService!.RunOperation(PerformLogout);
        NavigationService!.NavigateAndReset<SplashViewModel>();
    }

    private async Task PerformLogout(IProgress<ProgressReport> progress)
    {
        progress.Report(St.LoggingOut);
        var svc = Sp.CommonPool.Resolve<IUserService>() ?? throw new TamperException();
        Session? session;
        using (var t = svc.GetReadTransaction())
        {
            session = t.Read<Session>(State.RemoteSessionId)?.Result;
        }
        if (session is not null) await svc.EndSession(session);
    }
}
