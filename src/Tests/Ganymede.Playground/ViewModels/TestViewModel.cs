using System;
using System.Linq.Expressions;
using System.Reflection.Emit;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Resources;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.Types.Base;
using TheXDS.Ganymede.Types.Extensions;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Types;

namespace Ganymede.Playground.ViewModels;

public class TestViewModel : ViewModel
{
    public List<ButtonInteraction> DemoInteractions { get; } = [];

    public TestViewModel()
    {
        Title = "Test Dialog";
        var cb = CommandBuilder.For(this);
        DemoInteractions.AddRange([
            (ButtonInteraction)OnNestedDialog,
            (ButtonInteraction)OnTextInputDemo,
            (ButtonInteraction)OnCredInputDemo,
            new ButtonInteraction(OnTestValueInput<byte>, "Test input (byte)"),
            new ButtonInteraction(OnTestValueInput<sbyte>, "Test input (sbyte)"),
            new ButtonInteraction(OnTestValueInput<short>, "Test input (short)"),
            new ButtonInteraction(OnTestValueInput<ushort>, "Test input (ushort)"),
            new ButtonInteraction(OnTestValueInput<int>, "Test input (int)"),
            new ButtonInteraction(OnTestValueInput<uint>, "Test input (uint)"),
            new ButtonInteraction(OnTestValueInput<long>, "Test input (long)"),
            new ButtonInteraction(OnTestValueInput<ulong>, "Test input (ulong)"),
            new ButtonInteraction(OnTestValueInput<float>, "Test input (float)"),
            new ButtonInteraction(OnTestValueInput<double>, "Test input (double)"),
            new ButtonInteraction(OnTestValueInput<decimal>, "Test input (decimal)"),
            new ButtonInteraction(OnTestValueInput<bool>, "Test input (bool)"),
            (ButtonInteraction)OnTestDateTimeValueInput,
            (ButtonInteraction)OnTestQuestion,
            new ButtonInteraction(cb.BuildBusyOperation(OnTestOperation), "Lengthy _operation"),
            new ButtonInteraction(cb.BuildBusyOperation(OnTestCancellableOperation), "Cancellable lengthy _operation"),
            (ButtonInteraction)OnTestSelectDialog,
            (ButtonInteraction)OnWizardTest,
        ]);
    }

    [Name("Nested dialog demo")]
    private async Task OnNestedDialog()
    {
        await DialogService!.Show<NestedDialogViewModel>(CommonDialogTemplates.Input with { Icon = "🦄" });
    }

    [Name("Text input demo")]
    private async Task OnTextInputDemo()
    {
        if (await DialogService!.GetInputText("Enter some text here:") is { Success: true, Result: string input })
        {
            await DialogService!.Message($"You entered: {input}");
        }
        else
        {
            await DialogService!.Message("No input was provided.");
        }
    }

    [Name("Credential input demo")]
    private async Task OnCredInputDemo()
    {
        if (await DialogService!.GetCredential() is { Success: true, Result: { } cred })
        {
            await DialogService!.Message($"You entered: {cred.User}, password is {cred.Password.Length} characters long.");
        }
        else
        {
            await DialogService!.Message("No credentials were provided.");
        }
    }

    private async Task OnTestValueInput<T>() where T : unmanaged, IComparable<T>
    {
        if (DialogService is null) return;
        if (await DialogService.GetInputValue<T>(CommonDialogTemplates.Input with { Title = "Test value input", Text = $"Please enter a {typeof(T)} value." }, null, null)
            is { Success: true, Result: T value })
        {
            await DialogService.Message($"You entered: {value}");
        }
    }

    [Name("Datetime input demo")]
    private async Task OnTestDateTimeValueInput()
    {
        if (DialogService is null) return;
        if (await DialogService.GetInputValue(CommonDialogTemplates.Input with { Title = "Test datetime input", Text = $"Please enter a date." })
            is { Success: true, Result: DateTime value })
        {
            await DialogService.Message($"You entered: {value}");
        }
    }

    [Name("Simple question demo")]
    private async Task OnTestQuestion()
    {
        var r = await (DialogService?.AskYn("Is this dialog being displayed correctly?") ?? Task.FromResult(false));
        DialogService?.Message(r.ToString());
    }

    private async Task OnTestOperation(IProgress<ProgressReport> progress)
    {
        Task Simmulate(string text, int delay = 2000, double percent = double.NaN)
        {
            progress.Report(new ProgressReport(percent, text));
            return Task.Delay(delay);
        }

        await Simmulate("Operation Demo");
        for (var j = 0; j <= 40; j++)
        {
            await Simmulate($"Fake processing {Guid.NewGuid()}...", 100, j * 2.5);
        }
        await Simmulate("Fake _operation completed.");
    }

    private Task OnTestCancellableOperation(CancellationToken ct, IProgress<ProgressReport> progress)
    {
        return OnTestCancellableOperation(ct, progress, false);
    }

    private Task OnTestThrowingCancellableOperation(CancellationToken ct, IProgress<ProgressReport> progress)
    {
        return OnTestCancellableOperation(ct, progress, true);
    }

    private static async Task OnTestCancellableOperation(CancellationToken ct, IProgress<ProgressReport> progress, bool reThrowCancel)
    {
        Task Simmulate(string text, int delay = 2000, double percent = double.NaN, CancellationToken? c = null)
        {
            progress.Report(new ProgressReport(percent, text));
            return Task.Delay(delay, c ?? ct);
        }
        try
        {
            await Simmulate("Operation Demo");
            for (var j = 0; j <= 40; j++)
            {
                await Simmulate($"Fake processing {Guid.NewGuid()}...", 100, j * 2.5);
            }
            await Simmulate("Fake operation completed.");
        }
        catch (TaskCanceledException)
        {
            await Simmulate("Cancelling fake operation...", c: CancellationToken.None);
            await Task.Delay(1000);
            if (reThrowCancel) throw;
        }
    }

    [Name("Selection test")]
    private async Task OnTestSelectDialog()
    {
        var options = Enumerable.Range(1, 5).Select(p => $"Option {p}").ToArray();
        var result = await DialogService!.SelectOption(CommonDialogTemplates.Question with { Title = "Select an option", Text = "Please select an option from the list below:" }, options.Select(p => new NamedObject<string>(name: p, p)).ToArray());
        if (result.Success)
        {
            await DialogService!.Message($"You selected: {result.Result}");
        }
        else
        {
            await DialogService!.Message("Dialog was cancelled");
        }
    }

    [Name("Wizard test")]
    private async Task OnWizardTest()
    {
        await DialogService!.Wizard<WizardState>(CommonDialogTemplates.Wizard with { Title = "Test wizard" }, (s, i) =>
        {
            return i switch
            {
                0 => CommonWizardSteps.SimpleTextStep<WizardState>("Welcome to this ViewModel wizard.\n\nThis is a test, and this text needs to be long to see if the next view is replacing or wrongfully overlaying this one."),
                1 => new WizardTest1ViewModel(),
                2 when s.AskforDescription => new WizardTest2ViewModel(),
                2 when !s.AskforDescription => CommonWizardSteps.CancellableOperation<WizardState>(OnTestThrowingCancellableOperation),
                3 when s.AskforDescription => CommonWizardSteps.CancellableOperation<WizardState>(OnTestThrowingCancellableOperation),
                _ => new WizardTest3ViewModel(),
            };
        });
    }
}

public class WizardState
{
    public string? Name { get; set; }
    public bool AskforDescription { get; set; }
    public string? Description { get; set; }
}

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

public class WizardTest3ViewModel : WizardViewModel<WizardState>
{
    public WizardTest3ViewModel()
    {
        AddFinishInteraction();
    }
}
