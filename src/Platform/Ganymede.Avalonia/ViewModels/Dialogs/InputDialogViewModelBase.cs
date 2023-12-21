namespace TheXDS.Ganymede.ViewModels.Dialogs;

/// <summary>
/// Implements a <see cref="DialogViewModelBase"/> for dialogs that lets a user
/// input value data.
/// </summary>
public abstract class InputDialogViewModelBase<T> : DialogViewModelBase where T : struct, IComparable<T>
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