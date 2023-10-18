#pragma warning disable CS1591

using System.Windows.Input;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Types.Base;
using St = TheXDS.Ganymede.Resources.Strings.Views.WelcomeView;

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
}
