using System.Text.Json;
using System.Text.Json.Serialization;

namespace TheXDS.Ivie.Properties;

public class JsonConfiguration : IConfigurationRepository
{
    private readonly IConfigurationStore store;
    private static readonly JsonSerializerOptions jsonOptions = new()
    { 
        Converters =
        { 
            new JsonStringEnumConverter()
        }
    };

    public JsonConfiguration(IConfigurationStore store)
    {
        this.store = store;
    }

    public async Task<Configuration?> Load()
    {
        using var fs = store.GetReadStream();
        return await JsonSerializer.DeserializeAsync<Configuration>(fs, jsonOptions);
    }

    public Task Save(Configuration configuration)
    {
        using var fs = store.GetWriteStream();
        return JsonSerializer.SerializeAsync(fs, configuration, jsonOptions);
    }
}
