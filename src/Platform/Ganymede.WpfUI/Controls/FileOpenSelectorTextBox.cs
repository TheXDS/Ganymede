using Microsoft.Win32;
using static TheXDS.MCART.Helpers.DependencyObjectHelpers;

namespace TheXDS.Ganymede.Controls;

/// <summary>
/// Implements a <see cref="FileSelectorTextBox{T}"/> that gets a filepath to
/// be used when opening a file.
/// </summary>
public class FileOpenSelectorTextBox : FileSelectorTextBox<OpenFileDialog>
{
    /// <summary>
    /// Identifies the <see cref="AllowMultiSelect"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AllowMultiSelectProperty;

    /// <summary>
    /// Initializes the <see cref="FileOpenSelectorTextBox"/> class.
    /// </summary>
    static FileOpenSelectorTextBox()
    {
        AllowMultiSelectProperty = NewDp<bool, FileOpenSelectorTextBox>(nameof(AllowMultiSelect));
    }

    /// <summary>
    /// Gets or sets a value that indicates if the generated open file dialog
    /// should support multi file selection.
    /// </summary>
    public bool AllowMultiSelect
    {
        get => (bool)GetValue(AllowMultiSelectProperty);
        set => SetValue(AllowMultiSelectProperty, value);
    }

    /// <inheritdoc/>
    protected override OpenFileDialog CreateDialog()
    {
        var dlg = base.CreateDialog();
        dlg.Multiselect = AllowMultiSelect;
        return dlg;
    }
}
