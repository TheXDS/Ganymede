using System.Windows.Input;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Types.Base;
using St = TheXDS.Ganymede.Resources.Strings.Views.DialogDemoView;

namespace TheXDS.Ganymede.ViewModels;

public class DialogDemoViewModel : ViewModel
{
    private static ICommand NewDialogCmd(CommandBuilder<DialogDemoViewModel> builder, Func<Task> action)
    {
        return builder.BuildObserving(action).CanExecuteIfNotNull(p => p.DialogService).Build();
    }

    private static ICommand NewDialogCmd(CommandBuilder<DialogDemoViewModel> builder, Action action)
    {
        return builder.BuildObserving(action).CanExecuteIfNotNull(p => p.DialogService).Build();
    }

    public DialogDemoViewModel()
    {
        var cb = new CommandBuilder<DialogDemoViewModel>(this);
        TestMessageCommand = NewDialogCmd(cb, OnTestMessage);
        TestWarningCommand = NewDialogCmd(cb, OnTestWarning);
        TestErrorCommand = NewDialogCmd(cb, OnTestError);
        TestQuestionCommand = NewDialogCmd(cb, OnTestQuestion);
        TestSelectionCommand = NewDialogCmd(cb, OnTestSelectDialog);
        TestOperationCommand = cb.BuildBusyOperation(OnTestOperation);
        TestCancellableOperationCommand = cb.BuildBusyOperation(OnTestCancellableOperation);
    }

    public ICommand TestOperationCommand { get; }

    public ICommand TestCancellableOperationCommand { get; }

    public ICommand TestMessageCommand { get; }

    public ICommand TestWarningCommand { get; }

    public ICommand TestErrorCommand { get; }

    public ICommand TestQuestionCommand { get; }

    public ICommand TestSelectionCommand { get; }

    private void OnTestMessage() => DialogService?.Message(St.Message, St.HelloWorld);

    private void OnTestError() => DialogService?.Error(St.Error, St.ErrorText);

    private void OnTestWarning() => DialogService?.Warning(St.Warning, St.WarningText);

    private async Task OnTestQuestion()
    {
        var r = await (DialogService?.Ask(St.AskText) ?? Task.FromResult(false));
        DialogService?.Message(r.ToString());
    }

    private async Task OnTestOperation(IProgress<ProgressReport> progress)
    {
        Task Simmulate(string text, int delay = 2000, double percent = double.NaN)
        {
            progress.Report(new ProgressReport(percent, text));
            return Task.Delay(delay);
        }

        await Simmulate("Establising connection...");
        for (var j  = 0; j <= 40; j++)
        {
            await Simmulate($"Writting to object {Guid.NewGuid()}...", 100, j * 2.5);
        }
        await Simmulate("Cleaning up...");
    }

    private async Task OnTestCancellableOperation(CancellationToken ct, IProgress<ProgressReport> progress)
    {
        Task Simmulate(string text, int delay = 2000, double percent = double.NaN)
        {
            progress.Report(new ProgressReport(percent, text));
            return Task.Delay(delay, ct);
        }
        try
        {
            await Simmulate("Establising connection...");
            for (var j = 0; j <= 40; j++)
            {
                await Simmulate($"Writting to object {Guid.NewGuid()}...", 100, j * 2.5);
            }
            await Simmulate("Cleaning up...");
        }
        catch (TaskCanceledException)
        {
            progress.Report("Cancelling...");
            await Task.Delay(1000);
        }
    }

    private async Task OnTestSelectDialog()
    {
        var options = Enumerable.Range(1, 5).Select(p => $"Option {p}").ToArray();
        var result = await DialogService!.SelectOption("Select option", "Select an option from the combobox below.", options);
        if (result == -1)
        {
            await DialogService!.Message("Nothing selected", "No option has been selected from the prompt.");
        }
        else
        {
            await DialogService!.Message("Option", $"The user selected \"{options[result]}\"");
        }
    }
}