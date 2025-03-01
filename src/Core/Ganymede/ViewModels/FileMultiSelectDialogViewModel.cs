using TheXDS.Ganymede.Models;

namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Implements a ViewModel that allows the user to select multiple paths to
/// different files.
/// </summary>
public class FileMultiSelectDialogViewModel : OkCancelValueDialogViewModel<string[]?>
{
    /// <summary>
    /// Gets or sets the collection of file filters to be used when invoking
    /// the File dialog.
    /// </summary>
    public IEnumerable<FileFilterItem> FileFilters { get; internal set; } = [];
}
