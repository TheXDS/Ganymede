﻿namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Implements a <see cref="DialogViewModel"/> for dialogs that display the
/// progress of long-running operations.
/// </summary>
public class OperationDialogViewModel : DialogViewModel, IOperationDialogViewModel
{
    private double _progress = double.NaN;

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationDialogViewModel"/> class.
    /// </summary>
    public OperationDialogViewModel()
    {
        RegisterPropertyChangeBroadcast(nameof(Progress), nameof(IsIndeterminate));
    }

    /// <inheritdoc/>
    public double Progress
    {
        get => IsIndeterminate ? 0 : _progress;
        set => Change(ref _progress, value);
    }

    /// <inheritdoc/>
    public bool IsIndeterminate => double.IsNaN(_progress);
}
