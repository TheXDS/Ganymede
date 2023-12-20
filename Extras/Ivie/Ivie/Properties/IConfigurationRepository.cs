namespace TheXDS.Ivie.Properties;

public interface IConfigurationRepository
{
    Task<Configuration?> Load();

    Task Save(Configuration configuration);
}
