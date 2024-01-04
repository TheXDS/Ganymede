using Microsoft.Win32;

namespace TheXDS.Ganymede.Controls;

/// <summary>
/// Implements a <see cref="FileSelectorTextBox{T}"/> that gets a filepath to
/// be used when opening a file.
/// </summary>
public class FileOpenSelectorTextBox : FileSelectorTextBox<OpenFileDialog>
{
}
