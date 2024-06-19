using St = TheXDS.Ganymede.Resources.Strings.Common;

namespace TheXDS.Ganymede.Models;

/// <summary>
/// Represents an object that can be used to filter files on a file picker
/// dialog.
/// </summary>
/// <param name="Name">Descriptive name for the file type filter.</param>
/// <param name="Extensions">
/// Collection of valid file extensions that the specified file type may have.
/// </param>
public record struct FileFilterItem(string Name, string[] Extensions)
{
    /// <summary>
    /// Represents a file filter for all files.
    /// </summary>
    public static readonly FileFilterItem AllFiles = new(St.AllFiles, "*.*");

    /// <summary>
    /// Creates a new, simple <see cref="FileFilterItem"/> from a file
    /// extension.
    /// </summary>
    /// <param name="extension">
    /// File extension. Avoid including wildcards and the dot separating
    /// filename and extension.
    /// </param>
    /// <returns>
    /// A new <see cref="FileFilterItem"/> for the specified file extension.
    /// </returns>
    public static FileFilterItem Simple(string extension)
    {
        return new FileFilterItem(string.Format(St.SimpleFileFilter, extension.ToUpper()), $"*.{extension}");
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FileFilterItem"/> struct.
    /// </summary>
    /// <param name="name">Descriptive name for the file type filter.</param>
    /// <param name="extension">
    /// File extensions that the specified file should have.
    /// </param>
    public FileFilterItem(string name, string extension) : this(name, new[] { extension })
    {
    }
}