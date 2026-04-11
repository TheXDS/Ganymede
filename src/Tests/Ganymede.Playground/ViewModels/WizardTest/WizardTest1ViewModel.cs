using TheXDS.Ganymede.ViewModels;

namespace Ganymede.Playground.ViewModels.WizardTest;

public class WizardTest1ViewModel : WizardViewModel<WizardState>
{
    public WizardTest1ViewModel()
    {
        AddBackInteraction();
        AddNextInteraction();
        AddCancelInteraction();
        Message = "Enter a name";
    }
}
