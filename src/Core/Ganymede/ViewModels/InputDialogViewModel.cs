namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Implements a <see cref="DialogViewModel"/> for dialogs that lets a user
/// input text data.
/// </summary>
public class InputDialogViewModel : DialogViewModel, IInputDialogViewModel<string?>
{
    private string? _value;
    private int? _maxLength;

    /// <summary>
    /// Gets or sets the actual value associated with this instance.
    /// </summary>
    public string? Value
    {
        get => _value;
        set => Change(ref _value, value);
    }

    /// <summary>
    /// Gets or sets the maximum alloed length of text to be entered in the
    /// input dialog.
    /// </summary>
    public int? MaxLength
    {
        get => _maxLength;
        set => Change(ref _maxLength, value);
    }
}
