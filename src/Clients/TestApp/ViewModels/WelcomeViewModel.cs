using System.Windows.Input;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Types.Base;

namespace TheXDS.Ganymede.ViewModels;

public class WelcomeViewModel : ViewModel
{
    public WelcomeViewModel()
    {
        var cb = new CommandBuilder<WelcomeViewModel>(this);
        LogoutCommand = cb.BuildBusyOperation(OnLogout);
        TestMessageCommand = cb.BuildObserving(OnTestMessage)
            .CanExecuteIfNotNull(p => p.DialogService)
            .Build();

        TestSelectionCommand = cb.BuildObserving(OnTestSelectDialog)
            .CanExecuteIfNotNull(p => p.DialogService)
            .Build();

        TestNavigationCommand = cb.BuildObserving(OnTestNavigation)
            .CanExecuteIfNotNull(p => p.NavigationService)
            .Build();

        TestBusyCommand = cb.BuildBusyOperation(() => Task.Delay(5000));
        TestOperationCommand = cb.BuildBusyOperation(OnTestOperation);
        TestCancellableOperationCommand = cb.BuildBusyOperation(OnTestCancellableOperation);
        TryProteusCommand = cb.BuildNavigate<ProteusDemoViewModel>();
    }

    public ICommand LogoutCommand { get; }

    public ICommand TestMessageCommand { get; }

    public ICommand TestSelectionCommand { get; }

    public ICommand TestNavigationCommand { get; }

    public ICommand TestBusyCommand { get; }

    public ICommand TestOperationCommand { get; }

    public ICommand TestCancellableOperationCommand { get; }

    public ICommand TryProteusCommand { get; }

    private async Task OnLogout(IProgress<ProgressReport> progress)
    {
        progress.Report("Logging out...");
        await Task.Delay(2500);
        NavigationService!.HomePage = new LoginViewModel();
    }

    private void OnTestMessage() => DialogService?.Message("Message", "Hello world!");

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

    private void OnTestNavigation() => NavigationService?.Navigate<DummyViewModel>();
}
