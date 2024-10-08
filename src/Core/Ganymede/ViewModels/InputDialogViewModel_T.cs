﻿using TheXDS.Ganymede.Models;

namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Implements a <see cref="DialogViewModel"/> for dialogs that lets a user
/// input data.
/// </summary>
public class InputDialogViewModel<T> : OkCancelValueDialogViewModel<T>, IInputDialogViewModel<T> where T : struct, IComparable<T>
{
    private T? _minimum;
    private T? _maximum;

    /// <summary>
    /// Gets or sets the actual value associated with this instance.
    /// </summary>
    public T? Minimum
    {
        get => _minimum;
        set => Change(ref _minimum, value);
    }

    /// <summary>
    /// Gets or sets the actual value associated with this instance.
    /// </summary>
    public T? Maximum
    {
        get => _maximum;
        set => Change(ref _maximum, value);
    }
}
