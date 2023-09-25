namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Implements a <see cref="DialogViewModel"/> for dialogs that display the
/// progress of long-running operations.
/// </summary>
public class OperationDialogViewModel : DialogViewModel
{
    private double _progress = double.NaN;

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationDialogViewModel"/> class.
    /// </summary>
    public OperationDialogViewModel()
    {
        RegisterPropertyChangeBroadcast(nameof(Progress), nameof(IsIndeterminate));
    }

    /// <summary>
    /// Gets or sets a value that indicates the progress of a long-running
    /// operation.
    /// </summary>
    public double Progress
    {
        get => IsIndeterminate ? 0 : _progress;
        set => Change(ref _progress, value);
    }

    /// <summary>
    /// Gets a value that indicates if the progress of the operation is not determined.
    /// </summary>
    public bool IsIndeterminate => double.IsNaN(_progress);
}
