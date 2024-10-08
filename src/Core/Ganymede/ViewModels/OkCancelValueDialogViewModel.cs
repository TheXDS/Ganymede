namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Base class for all dialogs that display a dialog with both "OK" and
/// "Cancel" interactions that can return a value that is directly exposed on
/// the ViewModel for mutation.
/// </summary>
/// <typeparam name="T">Type of value returned by the dialog.</typeparam>
public abstract class OkCancelValueDialogViewModel<T> : OkCancelDialogViewModel<T>
{
    private T _value = default!;

    /// <inheritdoc/>
    public T Value
    {
        get => _value;
        set => Change(ref _value, value);
    }

    /// <inheritdoc/>
    protected override sealed T GetOkValue() => _value;
}
