using TheXDS.Ganymede.ViewModels;

namespace Ganymede.Playground.ViewModels.WizardTest;

public class WizardTest2ViewModel : WizardViewModel<WizardState>
{
    public WizardTest2ViewModel()
    {
        AddBackInteraction();
        AddNextInteraction();
        AddCancelInteraction();
        Message = "Enter a description";
    }
}
