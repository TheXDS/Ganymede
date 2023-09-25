namespace TheXDS.Ganymede.ViewModels.Dialogs;

/// <summary>
/// Implements a <see cref="DialogViewModelBase"/> for dialogs that lets a user
/// input data.
/// </summary>
public class InputDialogViewModel<T> : InputDialogViewModelBase<T> where T : struct, IComparable<T>
{
    private T _value;
    
    /// <summary>
    /// Gets or sets the actual value associated with this instance.
    /// </summary>
    public T Value
    {
        get => _value;
        set => Change(ref _value, value);
    }
}