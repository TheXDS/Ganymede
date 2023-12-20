#pragma warning disable CS1591

using TheXDS.Ganymede.ViewModels;

namespace TheXDS.ProteusDemo.ViewModels.WizardTest;

public class WizardDemoState
{
    public string Text { get; set; } = string.Empty;
    public bool YesNo { get; set; }
}

public class Wizard1ViewModel : WizardViewModel<WizardDemoState>
{
    public Wizard1ViewModel()
    {
        AddNextInteraction();
        AddCancelInteraction();
    }
}

public class Wizard2ViewModel : WizardViewModel<WizardDemoState>
{
    public Wizard2ViewModel()
    {
        AddBackInteraction();
        AddNextInteraction("Start");
        AddCancelInteraction();
    }
}

public class Wizard3ViewModel : WizardOperationViewModel<WizardDemoState>, IOperationDialogViewModel
{
    protected override Task RunOperation()
    {
        return Task.Delay(3000);
    }
}

public class Wizard4ViewModel : WizardViewModel<WizardDemoState>
{
    public Wizard4ViewModel()
    {
        AddNextInteraction("Finish");
    }
}
