namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Implements a <see cref="DialogViewModel"/> for dialogs that lets a user
/// input data.
/// </summary>
public class RangeInputDialogViewModel<T> : OkCancelDialogViewModel<(T Min, T Max)>, IInputDialogViewModel<T> where T : struct, IComparable<T>
{
    private T? _minimum;
    private T? _maximum;
    private T _minValue;
    private T _maxValue;

    /// <summary>
    /// Gets or sets the minimum value that the user can enter.
    /// </summary>
    public T? Minimum
    {
        get => _minimum;
        set => Change(ref _minimum, value);
    }

    /// <summary>
    /// Gets or sets the maximum value that the user can enter.
    /// </summary>
    public T? Maximum
    {
        get => _maximum;
        set => Change(ref _maximum, value);
    }

    /// <summary>
    /// Gets or sets the minimum value on the range being entered by the user.
    /// </summary>
    public T RangeStart
    {
        get => _minValue;
        set => Change(ref _minValue, value);
    }

    /// <summary>
    /// Gets or sets the maximum value on the range being entered by the user.
    /// </summary>
    public T RangeEnd
    {
        get => _maxValue;
        set => Change(ref _maxValue, value);
    }

    /// <inheritdoc/>
    public (T Min, T Max) Value
    {
        get => (RangeStart, RangeEnd);
        set => (RangeStart, RangeEnd) = value;
    }

    /// <inheritdoc/>
    protected override (T Min, T Max) GetOkValue() => (RangeStart, RangeEnd);
}
