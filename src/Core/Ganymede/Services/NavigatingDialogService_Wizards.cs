using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Services;

public partial class NavigatingDialogService
{
    /// <inheritdoc/>
    public async Task<bool> Wizard<TState>(TState state, params Func<TState, IWizardViewModel<TState>>[] viewModels)
    {
        var i = 0;
        while (i < viewModels.Length)
        {
            var vm = viewModels[i].Invoke(state);
            vm.State ??= state;
            vm.Icon ??= "\xD83E\xDE84";
            vm.IconBgColor ??= System.Drawing.Color.MediumPurple;
            vm.DialogService = this;
            Navigate(vm);
            switch (await vm.DialogAwaiter)
            {
                case Models.WizardAction.Cancel: NavigateAndReset(null); return false;
                case Models.WizardAction.Back when i > 0: i--; break;
                case Models.WizardAction.Next when i < viewModels.Length: i++; break;
            }
        }
        NavigateAndReset(null);
        return true;
    }
}
