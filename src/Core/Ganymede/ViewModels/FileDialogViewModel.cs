using TheXDS.Ganymede.Models;

namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Implements a ViewModel that creates a control that allows the user to
/// select a file path.
/// </summary>
public abstract class FileDialogViewModel : OkCancelValueDialogViewModel<string?>
{
    /// <summary>
    /// Gets or sets the collection of file filters to be used when invoking
    /// the File dialog.
    /// </summary>
    public IEnumerable<FileFilterItem> FileFilters { get; internal set; } = [];
}
