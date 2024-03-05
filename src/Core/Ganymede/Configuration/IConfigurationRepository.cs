namespace TheXDS.Ganymede.Configuration;

/// <summary>
/// Defines a set of members to be implemented by a type that loads and saves
/// configuration records as defined in the specified type.
/// </summary>
/// <typeparam name="T">
/// Type that contains the desired configuration values.
/// </typeparam>
public interface IConfigurationRepository<T> where T : notnull
{
    /// <summary>
    /// Attempts to load the configuration from this repository.
    /// </summary>
    /// <returns>
    /// A task that upon completion returns a new instance of the
    /// <typeparamref name="T"/> class with all its property values loaded from
    /// this repository or <see langword="null"/> if the configuration could
    /// not be loaded after completing the task.
    /// </returns>
    Task<T?> Load();

    /// <summary>
    /// Saves the specified configuration into this configuration repository.
    /// </summary>
    /// <param name="configuration">Configuration to save.</param>
    /// <returns>
    /// A task that can be used to await the async operation.
    /// </returns>
    Task Save(T configuration);
}
