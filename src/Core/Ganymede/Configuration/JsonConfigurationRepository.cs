using System.Text.Json.Serialization;
using System.Text.Json;

namespace TheXDS.Ganymede.Configuration;

/// <summary>
/// Defines a configuration repository that loads and saves configuration
/// values in Json format.
/// </summary>
/// <typeparam name="T">Type of configuration object.</typeparam>
/// <param name="store">Configuration store to use.</param>
/// <param name="jsonOptions">Json serialization options to use.</param>
public class JsonConfigurationRepository<T>(IConfigurationStore store, JsonSerializerOptions jsonOptions) : IConfigurationRepository<T> where T : notnull
{
    private static JsonSerializerOptions GetDefaultSerializationOptions() => new() { Converters = { new JsonStringEnumConverter() } };
    private readonly IConfigurationStore store = store;
    private readonly JsonSerializerOptions jsonOptions = jsonOptions;

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="JsonConfigurationRepository{T}"/> class, specifying the
    /// configuration store to use and leaving all Json serialization options
    /// to their defaults.
    /// </summary>
    /// <param name="store">Configuration store to use.</param>
    public JsonConfigurationRepository(IConfigurationStore store) : this(store, GetDefaultSerializationOptions()) { }

    async Task<T?> IConfigurationRepository<T>.Load()
    {
        if (store.CanOpenStream())
        {
            using var fs = store.GetReadStream();
            return await JsonSerializer.DeserializeAsync<T>(fs, jsonOptions);
        }
        return default;
    }

    Task IConfigurationRepository<T>.Save(T configuration)
    {
        using var fs = store.GetWriteStream();
        return JsonSerializer.SerializeAsync(fs, configuration, jsonOptions);
    }
}
