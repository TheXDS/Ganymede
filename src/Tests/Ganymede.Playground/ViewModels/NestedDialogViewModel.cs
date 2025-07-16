using System.Windows.Input;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.Types.Extensions;
using TheXDS.Ganymede.ViewModels;

namespace Ganymede.Playground.ViewModels;

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
        await DialogService!.Message("Hello!");
    }
}
