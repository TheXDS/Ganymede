﻿using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Types.Base;

namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Implements a <see cref="WizardViewModel{T}"/> that performs lenghty
/// operations.
/// </summary>
/// <typeparam name="T">Type of state information to use.</typeparam>
public abstract class WizardOperationViewModel<T> : WizardViewModel<T>, IOperationDialogViewModel
{
    private double _progress = double.NaN;

    /// <inheritdoc/>
    protected override void OnInitialize(IPropertyBroadcastSetup broadcastSetup)
    {
        base.OnInitialize(broadcastSetup);
        broadcastSetup.RegisterPropertyChangeBroadcast(() => Progress, () => IsIndeterminate);
    }

    /// <inheritdoc/>
    public double Progress
    {
        get => IsIndeterminate ? 0 : _progress;
        set => Change(ref _progress, value);
    }

    /// <inheritdoc/>
    public bool IsIndeterminate => double.IsNaN(_progress);

    /// <inheritdoc/>
    protected override async Task OnCreated()
    {
        await RunOperation();
        Close(WizardAction.Next);
    }

    /// <summary>
    /// Defines the operation to be performed in this <see cref="IViewModel"/>.
    /// </summary>
    /// <returns></returns>
    protected abstract Task RunOperation();
}