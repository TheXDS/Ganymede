namespace TheXDS.Ganymede.ViewModels.Dialogs;

/// <summary>
/// Implements a <see cref="DialogViewModelBase"/> for dialogs that display the
/// progress of long-running operations.
/// </summary>
public class OperationDialogViewModel : DialogViewModelBase
{
    static OperationDialogViewModel()
    {
        RegisterNpcBroadcast(nameof(Progress), nameof(IsIndeterminate));
    }
    
    private double _progress = double.NaN;

    /// <summary>
    /// Gets or sets a value that indicates the progress of a long-running
    /// operation.
    /// </summary>
    public double Progress
    {
        get => _progress;
        set => Change(ref _progress, value);
    }

    /// <summary>
    /// Gets a value that indicates if the progress of the operation is not determined.
    /// </summary>
    public bool IsIndeterminate => double.IsNaN(Progress);
}