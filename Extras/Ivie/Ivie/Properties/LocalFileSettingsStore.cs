namespace TheXDS.Ivie.Properties;

public class LocalFileSettingsStore : IConfigurationStore
{
    private readonly string fileName;

    public LocalFileSettingsStore(string fileName)
    {
        this.fileName = fileName;
    }

    public Stream GetReadStream()
    {
        return new FileStream(fileName, FileMode.Open);
    }

    public Stream GetWriteStream()
    {
        return new FileStream(fileName, FileMode.Create);
    }
}
