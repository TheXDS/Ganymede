namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Implements a <see cref="DialogViewModel"/> for dialogs that lets a user
/// input data.
/// </summary>
public class RangeInputDialogViewModel<T> : InputDialogViewModelBase<T>, IInputDialogViewModel<(T Min, T Max)> where T : struct, IComparable<T>
{
    private T _min;
    private T _max;

    /// <summary>
    /// Gets or sets the minimum value on the range being input by the user.
    /// </summary>
    public T MinimumValue
    {
        get => _min;
        set => Change(ref _min, value);
    }

    /// <summary>
    /// Gets or sets the maximum value on the range being input by the user.
    /// </summary>
    public T MaximumValue
    {
        get => _max;
        set => Change(ref _max, value);
    }

    /// <inheritdoc/>
    public (T Min, T Max) Value
    {
        get => (MinimumValue, MaximumValue);
        set => (MinimumValue, MaximumValue) = value;
    }
}
