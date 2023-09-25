namespace TheXDS.Ganymede.CrudGen.Descriptions;

/// <summary>
/// Enumerates the binary blob kinds supported by Proteus.
/// </summary>
public enum BlobType
{
    /// <summary>
    /// Raw bytes. The visual generator may implement a Hex editor, or a file 
    /// picker.
    /// </summary>
    Raw,
    /// <summary>
    /// File contents.
    /// </summary>
    EmbeddedFile,
    /// <summary>
    /// Encrypted password field.
    /// </summary>
    Password
}
