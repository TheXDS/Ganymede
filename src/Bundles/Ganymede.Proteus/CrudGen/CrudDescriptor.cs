using TheXDS.Triton.Models.Base;

namespace TheXDS.Ganymede.CrudGen;

/// <summary>
/// Base class for all CRUD descriptors in Proteus.
/// </summary>
/// <typeparam name="T">Model to describe.</typeparam>
public abstract class CrudDescriptor<T> : ICrudDescriptor where T : Model
{
    private readonly CrudDescriptorConfigurator<T> _configurator = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="CrudDescriptor{T}"/>
    /// class.
    /// </summary>
    public CrudDescriptor()
    {
        OnDescribeModel(_configurator);
    }

    /// <summary>
    /// Gets a runtime reference to the model described by this instance.
    /// </summary>
    public Type Model => typeof(T);

    /// <summary>
    /// Gets a reference to the descriptions repository.
    /// </summary>
    public ICrudDescription Description => _configurator;

    /// <summary>
    /// Executed whenever Proteus requests model description for the given
    /// entity type of this instance.
    /// </summary>
    /// <param name="config">
    /// Model configuration instance to use when configuring the model general
    /// settings.
    /// </param>
    protected abstract void OnDescribeModel(IModelConfigurator<T> config);
}
