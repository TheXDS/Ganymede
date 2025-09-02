using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Types;

namespace TheXDS.Ganymede.ViewModels;

internal sealed class CancellableOperationWizardViewModel<TState> : OperationDialogViewModel, IWizardViewModel<TState>
{
    private readonly Func<CancellationToken, IProgress<ProgressReport>, Task> _operation;
    private readonly CancellationTokenSource _token = new();
    private TState _state = default!;
    private TaskCompletionSource<WizardAction> awaiter = new();

    public CancellableOperationWizardViewModel(Func<CancellationToken, IProgress<ProgressReport>, Task> operation)
    {
        Interactions.Add(new ButtonInteraction(_token.Cancel, "Cancel") { IsPrimary = true });
        _operation = operation;
    }

    public Task<WizardAction> DialogAwaiter => awaiter.Task;

    public TState State
    {
        get => _state;
        set => Change(ref _state, value);
    }

    public void Close(WizardAction result)
    {
        IsBusy = false;
        awaiter.SetResult(result);
        awaiter = new TaskCompletionSource<WizardAction>();
    }

    protected override async Task OnCreated()
    {
        void ReportProgress(ProgressReport p)
        {
            Progress = p.Progress;
            if (p.Status is not null)
            {
                Message = p.Status;
            }
        }
        try
        {
            IsBusy = false;
            await _operation.Invoke(_token.Token, new Progress<ProgressReport>(ReportProgress));
            Close(WizardAction.Next);
        }
        catch (TaskCanceledException)
        {
            Close(WizardAction.Cancel);
        }
    }
}
