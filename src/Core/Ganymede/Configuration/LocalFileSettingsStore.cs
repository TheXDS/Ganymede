namespace TheXDS.Ganymede.Configuration;

/// <summary>
/// Implements a configuration store that reads and writes settings onto a file
/// on the local filesystem.
/// </summary>
/// <param name="fileName">File name to use as the store.</param>
public class LocalFileSettingsStore(string fileName) : IConfigurationStore
{
    private readonly string fileName = fileName;

    /// <inheritdoc/>
    public bool CanOpenStream()
    {
        return File.Exists(fileName);
    }

    /// <inheritdoc/>
    public Stream GetReadStream()
    {
        return new FileStream(fileName, FileMode.Open);
    }

    /// <inheritdoc/>
    public Stream GetWriteStream()
    {
        var dir = Path.GetDirectoryName(fileName);
        if (dir is not null && !Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        return new FileStream(fileName, FileMode.Create);
    }
}
