using TheXDS.Ganymede.Models;

namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Implements a <see cref="DialogViewModel"/> for dialogs that lets a user
/// input data.
/// </summary>
public class InputDialogViewModel<T> : InputDialogViewModelBase<T>, IInputDialogViewModel<T> where T : struct, IComparable<T>
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
