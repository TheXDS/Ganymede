namespace TheXDS.Ganymede.ViewModels;

internal sealed class SimpleTextFinishWizardViewModel<TState> : WizardViewModel<TState>
{
    internal SimpleTextFinishWizardViewModel(string message, string? label)
    {
        AddFinishInteraction(label);
        Message = message;
    }
}
