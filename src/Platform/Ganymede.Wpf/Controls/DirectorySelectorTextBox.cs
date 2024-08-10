using Microsoft.Win32;
using Dp = TheXDS.MCART.Helpers.DependencyObjectHelpers;

namespace TheXDS.Ganymede.Controls;

/// <summary>
/// Implements a <see cref="FilesystemSelectorTextBox{T}"/> that allows the
/// user to select a directory in the filesystem.
/// </summary>
public class DirectorySelectorTextBox : FilesystemSelectorTextBox<OpenFolderDialog>
{
    /// <inheritdoc/>
    protected override string GetDialogValue(OpenFolderDialog dialog)
    {
        return dialog.FolderName;
    }

    static DirectorySelectorTextBox()
    {
        Dp.OverrideMetadata<DirectorySelectorTextBox>(IconProperty, "📂");
    }
}