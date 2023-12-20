namespace TheXDS.Ivie.Properties;

/// <summary>
/// Defines a set of members to be implemented by a type that exposes
/// configuration for Ivie.
/// </summary>
/// <param name="PluginsPath">
/// Gets the path to use when loading settings.
/// </param>
/// <param name="DataProvider">
/// Gets the <see cref="Guid"/> of the data provider engine to use for data
/// operations.
/// </param>
public record Configuration(string PluginsPath, Guid DataProvider)
{
    /// <summary>
    /// Gets a new instance of the <see cref="Configuration"/> class that
    /// represents the default configuration for Ivie.
    /// </summary>
    /// <returns></returns>
    public static Configuration GetDefaults()
    {
        return new Configuration(".", Guid.Empty);
    }
}
