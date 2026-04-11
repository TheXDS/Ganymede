using System.Windows.Input;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.Types.Base;

namespace Ganymede.Playground.ViewModels;

public class WelcomeViewModel : ViewModel
{
    public ICommand TestNavigationCommand { get; }

    public ICommand TestBusyCommand { get; }

    public ICommand TryDialogDemoCommand { get; }

    public WelcomeViewModel()
    {
        var cb = new CommandBuilder<WelcomeViewModel>(this);
        TestNavigationCommand = cb.BuildNavigate<NewPageViewModel>();
        TestBusyCommand = cb.BuildBusyOperation(() => Task.Delay(5000));
        TryDialogDemoCommand = cb.BuildNavigate<TestViewModel>();
    }
}
