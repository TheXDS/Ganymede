namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Implements a ViewModel that allows the user to select a directory path on
/// the filesystem.
/// </summary>
public class DirectoryDialogViewModel : DialogViewModel, IInputDialogViewModel<string>
{
    private string _value = string.Empty;

    /// <inheritdoc/>
    public string Value
    {
        get => _value;
        set => Change(ref _value, value);
    }
}