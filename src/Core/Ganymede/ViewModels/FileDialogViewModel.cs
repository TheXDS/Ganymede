using TheXDS.Ganymede.Models;

namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Implements a ViewModel that creates a control that allows the user to
/// select a file path.
/// </summary>
public abstract class FileDialogViewModel : DialogViewModel, IInputDialogViewModel<string>
{
    private string _value = string.Empty;

    /// <inheritdoc/>
    public string Value
    {
        get => _value;
        set => Change(ref _value, value);
    }

    /// <summary>
    /// Gets or sets the collection of file filters to be used when invoking
    /// the File dialog.
    /// </summary>
    public IEnumerable<FileFilterItem> FileFilters { get; internal set; } = [];
}
