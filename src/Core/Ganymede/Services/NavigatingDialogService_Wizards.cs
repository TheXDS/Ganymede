using TheXDS.Ganymede.Models;
using static TheXDS.Ganymede.Services.IDialogService;

namespace TheXDS.Ganymede.Services;

public partial class NavigatingDialogService
{
    /// <inheritdoc/>
    public async Task<bool> Wizard<TState>(DialogTemplate template, TState state, Step<TState> viewModels)
    {
        var i = 0;
        while (viewModels.Invoke(state, i) is { } vm)
        {
            vm.State ??= state;
            template.Configure(vm);
            vm.DialogService = this;
            await NavigateAndReset(vm);
            switch (await vm.DialogAwaiter)
            {
                case WizardAction.Cancel: await NavigateAndReset(null); return false;
                case WizardAction.Finish: await NavigateAndReset(null); return true;
                case WizardAction.Back when i > 0: i--; break;
                case WizardAction.Next: i++; break;
            }
        }
        await NavigateAndReset(null);
        return true;
    }
}
