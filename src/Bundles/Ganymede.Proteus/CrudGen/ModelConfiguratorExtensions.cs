using TheXDS.Triton.Models.Base;

namespace TheXDS.Ganymede.CrudGen;

/// <summary>
/// Includes a set of extensions for the <see cref="IModelConfigurator{T}"/>
/// interface.
/// </summary>
public static class ModelConfiguratorExtensions
{
    /// <summary>
    /// Adds a save prolog to a model description that will automatically set a
    /// unique ID for any new entity where the ID field is of type
    /// <see cref="Guid"/>.
    /// </summary>
    /// <typeparam name="T">Model type.</typeparam>
    /// <param name="configurator">
    /// Configurator to add the desired save prolog onto.
    /// </param>
    /// <returns>
    /// The same <see cref="IModelConfigurator{T}"/> instance as
    /// <paramref name="configurator"/>, allowing the use of Fluent syntax.
    /// </returns>
    public static IModelConfigurator<T> AddDefaultGuidIdProlog<T>(this IModelConfigurator<T> configurator) where T: Model<Guid>
    {
        return configurator.AddSaveProlog(p => { if (p.Id == default) p.Id = Guid.NewGuid(); });
    }

    /// <summary>
    /// Adds a save prolog to a model description that will automatically set a
    /// unique ID for any new entity where the ID field is of type
    /// <see cref="string"/>.
    /// </summary>
    /// <typeparam name="T">Model type.</typeparam>
    /// <param name="configurator">
    /// Configurator to add the desired save prolog onto.
    /// </param>
    /// <returns>
    /// The same <see cref="IModelConfigurator{T}"/> instance as
    /// <paramref name="configurator"/>, allowing the use of Fluent syntax.
    /// </returns>
    public static IModelConfigurator<T> AddDefaultStringIdProlog<T>(this IModelConfigurator<T> configurator) where T : Model<string>
    {
        return AddDefaultStringIdProlog(configurator, _ => Guid.NewGuid().ToString());
    }

    /// <summary>
    /// Adds a save prolog to a model description that will automatically set a
    /// unique ID for any new entity where the ID field is of type
    /// <see cref="string"/>.
    /// </summary>
    /// <typeparam name="T">Model type.</typeparam>
    /// <param name="configurator">
    /// Configurator to add the desired save prolog onto.
    /// </param>
    /// <param name="idGenerator">
    /// Method to invoke when trying to generate a string ID.
    /// </param>
    /// <returns>
    /// The same <see cref="IModelConfigurator{T}"/> instance as
    /// <paramref name="configurator"/>, allowing the use of Fluent syntax.
    /// </returns>
    public static IModelConfigurator<T> AddDefaultStringIdProlog<T>(this IModelConfigurator<T> configurator, Func<T, string> idGenerator) where T : Model<string>
    {
        return configurator.AddSaveProlog(p => { if (string.IsNullOrEmpty(p.Id)) p.Id = idGenerator.Invoke(p); });
    }
}
