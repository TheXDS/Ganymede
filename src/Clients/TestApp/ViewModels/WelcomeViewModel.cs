#pragma warning disable CS1591

using System.Windows.Input;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Types.Base;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Extensions;
using TheXDS.Triton.Services.Base;
using St = TheXDS.Ganymede.Resources.Strings.Views.WelcomeView;
using SP = TheXDS.ServicePool.ServicePool;

namespace TheXDS.Ganymede.ViewModels;

public class WelcomeViewModel : ViewModel
{
    public WelcomeViewModel()
    {
        var cb = new CommandBuilder<WelcomeViewModel>(this);
        LogoutCommand = cb.BuildBusyOperation(OnLogout);
        TestNavigationCommand = cb.BuildNavigate<DummyViewModel>();
        TestBusyCommand = cb.BuildBusyOperation(() => Task.Delay(5000));
        TryProteusCommand = cb.BuildNavigate<ProteusDemoViewModel>();
        TryDialogDemoCommand = cb.BuildNavigate<DialogDemoViewModel>();
    }

    public ICommand LogoutCommand { get; }

    public ICommand TestNavigationCommand { get; }

    public ICommand TestBusyCommand { get; }

    public ICommand TryProteusCommand { get; }

    public ICommand TryDialogDemoCommand { get; }

    private async Task OnLogout(IProgress<ProgressReport> progress)
    {
        progress.Report(St.LoggingOut);
        await Task.Delay(2500);
        NavigationService!.HomePage = new LoginViewModel();
    }


    /// <inheritdoc/>
    protected override Task OnCreated()
    {
        if (SP.CommonPool.Resolve<ITritonService>() is not { } svc) return Task.CompletedTask;
        using var trans = svc.GetWriteTransaction();
        trans.Create(new User()
        {
            Id = "root",
            DisplayName = "Super User",
            Password = PasswordStorage.CreateHash<MCART.Security.Pbkdf2Storage>("password".ToSecureString())
        });
        trans.Create(new User()
        {
            Id = "admin",
            DisplayName = "Administrator",
            Password = PasswordStorage.CreateHash<MCART.Security.Pbkdf2Storage>("password".ToSecureString())
        });
        trans.Create(new User()
        {
            Id = "user",
            DisplayName = "User",
            Password = PasswordStorage.CreateHash<MCART.Security.Pbkdf2Storage>("password".ToSecureString())
        });
        return trans.CommitAsync();
    }
}
