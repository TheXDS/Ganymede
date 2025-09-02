namespace TheXDS.Ganymede.ViewModels;

internal sealed class SimpleTextWizardViewModel<TState> : WizardViewModel<TState>
{
    internal SimpleTextWizardViewModel(string message, string? label)
    {
        AddNextInteraction(label);
        AddCancelInteraction();
        Message = message;
    }
}
