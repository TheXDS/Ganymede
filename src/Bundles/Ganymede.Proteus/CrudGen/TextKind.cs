namespace TheXDS.Ganymede.CrudGen;

/// <summary>
/// Enumerates the possible kinds of text that a <see cref="string"/> property
/// may contain.
/// </summary>
public enum TextKind : byte
{
    /// <summary>
    /// Generic, short text.
    /// </summary>
    Generic,

    /// <summary>
    /// Formatted text with an input mask.
    /// </summary>
    Maskable,

    /// <summary>
    /// Generic text obscured by a password mask.
    /// </summary>
    Password,

    /// <summary>
    /// Long block of text.
    /// </summary>
    Big,

    /// <summary>
    /// Enriched text. May use RTF format.
    /// </summary>
    Rich,

    /// <summary>
    /// Path to a file.
    /// </summary>
    FilePath,

    /// <summary>
    /// Path to an image file. The visual presenter may include a preview pane.
    /// </summary>
    ImagePath,

    /// <summary>
    /// Path to a directory.
    /// </summary>
    DirectoryPath,

    /// <summary>
    /// Url, normally used for web (http/https) resources.
    /// </summary>
    Url
}