using Microsoft.Win32;

namespace TheXDS.Ganymede.Controls;

/// <summary>
/// Base class for a <see cref="TextBoxEx"/> that includes functionality to
/// select files using the system's file picker dialog.
/// </summary>
/// <typeparam name="T">
/// Type of dialog to use when the user presses the "Browse" button.
/// </typeparam>
public abstract class FilesystemSelectorTextBox<T> : FilesystemSelectorTextBox where T : CommonItemDialog, new()
{
    /// <summary>
    /// Creates a new dialog.
    /// </summary>
    /// <returns>
    /// A new instance of the <see cref="FileDialog"/> class of type
    /// <typeparamref name="T"/>.
    /// </returns>
    protected virtual T CreateDialog() => new();

    /// <summary>
    /// Extracts the selected filesystem object full name from the dialog.
    /// </summary>
    /// <param name="dialog">Dialog to extract the selected path from.</param>
    /// <returns>
    /// A <see cref="string"/> with the selected filesystem object path.
    /// </returns>
    protected abstract string GetDialogValue(T dialog);

    /// <inheritdoc/>
    protected override void BtnBrowse_Click(object sender, RoutedEventArgs e)
    {
        var dlg = CreateDialog();
        if (DialogTitle is { } title) dlg.Title = title;
        if (dlg.ShowDialog() == true)
        {
            Text = GetDialogValue(dlg);
            GetBindingExpression(TextProperty)?.UpdateSource();
        }
    }
}
