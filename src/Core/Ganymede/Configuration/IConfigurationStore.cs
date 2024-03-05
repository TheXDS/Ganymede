namespace TheXDS.Ganymede.Configuration;

/// <summary>
/// Defines a set of members to be implemented by a type that provides read and
/// write capabilities for settings.
/// </summary>
public interface IConfigurationStore
{
    /// <summary>
    /// Indicates if the store is available for read operations.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if the store can be opened for read,
    /// <see langword="false"/> otherwise.
    /// </returns>
    bool CanOpenStream();

    /// <summary>
    /// Gets a <see cref="Stream"/> that can be used to read the settings from.
    /// </summary>
    /// <returns>
    /// A <see cref="Stream"/> that can be used to read the settings from.
    /// </returns>
    Stream GetReadStream();

    /// <summary>
    /// Gets a <see cref="Stream"/> that can be used to write the settings
    /// onto.
    /// </summary>
    /// <returns>
    /// A <see cref="Stream"/> that can be used to write the settings onto.
    /// </returns>
    Stream GetWriteStream();
}
