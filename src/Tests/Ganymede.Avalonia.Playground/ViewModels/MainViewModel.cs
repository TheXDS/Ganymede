using System.Threading.Tasks;
using System.Windows.Input;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.Resources;
using TheXDS.Ganymede.Types.Base;
using TheXDS.Ganymede.Types.Extensions;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Types.Base;

namespace Ganymede.Avalonia.Playground.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";
}
public class TestViewModel : ViewModel
{
    public ICommand HelloCommand { get; }
    public ICommand GoodByeCommand { get; }

    public TestViewModel()
    {
        Title = "Test Dialog";
        var cb = CommandBuilder.For(this);
        HelloCommand = cb.BuildSimple(OnHello);
        GoodByeCommand = cb.BuildNavigateBack();
    }

    private async Task OnHello()
    {
        await DialogService!.Show<NestedDialogViewModel>(CommonDialogTemplates.Input);
    }
}
public class NestedDialogViewModel : AwaitableDialogViewModel
{
    public ICommand HelloCommand { get; }
    public ICommand GoodByeCommand { get; }

    public NestedDialogViewModel()
    {
        Title = "Nested Dialog";
        var cb = CommandBuilder.For(this);
        HelloCommand = cb.BuildSimple(OnHello);
        GoodByeCommand = cb.BuildCloseCommand();
    }

    private async Task OnHello()
    {
        //await DialogService!.Show<NestedDialogViewModel>(CommonDialogTemplates.Input);
        await DialogService!.Message("Hello!");
    }
}
