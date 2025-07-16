using Microsoft.Win32;
using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Types.Extensions;
using static TheXDS.MCART.Helpers.DependencyObjectHelpers;
using Dp = TheXDS.MCART.Helpers.DependencyObjectHelpers;

namespace TheXDS.Ganymede.Controls;

/// <summary>
/// Base class for a <see cref="TextBox"/> that allows to select single files.
/// </summary>
/// <typeparam name="T">
/// Type of <see cref="FileDialog"/> to invoke when browsing for a path.
/// </typeparam>
public abstract class FileSelectorTextBox<T> : FilesystemSelectorTextBox<T> where T : FileDialog, new()
{
    /// <summary>
    /// Identifies the <see cref="FileFilters"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty FileFiltersProperty;

    static FileSelectorTextBox()
    {
        Dp.OverrideMetadata<FilesystemSelectorTextBox<T>>(IconProperty, "📄");
        FileFiltersProperty = NewDp<IEnumerable<FileFilterItem>, FilesystemSelectorTextBox<T>>(nameof(FileFilters));
    }

    /// <summary>
    /// Gets or sets the collection of file filters to apply to this instance.
    /// </summary>
    public IEnumerable<FileFilterItem> FileFilters
    {
        get => (IEnumerable<FileFilterItem>)GetValue(FileFiltersProperty);
        set => SetValue(FileFiltersProperty, value);
    }

    /// <inheritdoc/>
    protected override string GetDialogValue(T dialog)
    {
        return dialog.FileName;
    }

    /// <inheritdoc/>
    protected override T CreateDialog() => new()
    {
        Filter = FileFilters.ToWin32Filter(),
    };
}
