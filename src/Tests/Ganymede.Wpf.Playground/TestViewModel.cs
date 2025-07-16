using System.Windows.Input;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.Resources;
using TheXDS.Ganymede.Types.Base;
using TheXDS.Ganymede.Types.Extensions;
using TheXDS.Ganymede.ViewModels;

namespace Ganymede.Wpf.Playground;

public class TestViewModel : ViewModel
{
    public ICommand HelloCommand { get; }
    public ICommand GoodByeCommand { get; }

    public TestViewModel()
    {
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
