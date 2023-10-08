namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Implements a <see cref="DialogViewModel"/> that allows an user to select a
/// value from a list of available options.
/// </summary>
public class SelectionDialogViewModel : DialogViewModel, IInputDialogViewModel<string?>
{
    private string? _value;
    private IEnumerable<string> _options = Array.Empty<string>();

    /// <summary>
    /// Gets or sets the actual value associated with this instance.
    /// </summary>
    public string? Value
    {
        get => _value;
        set => Change(ref _value, value);
    }

    /// <summary>
    /// Gets or sets the available options on this dialog.
    /// </summary>
    public IEnumerable<string> Options
    {
        get => _options;
        set => Change(ref _options, value);
    }
}