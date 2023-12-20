namespace TheXDS.Ivie.Properties;

public interface IConfigurationStore
{
    Stream GetReadStream();
    Stream GetWriteStream();
}
