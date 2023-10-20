using TheXDS.Ganymede.CrudGen;
using TheXDS.Triton.Models.Base;

namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Base class for a ViewModel used for CRUD operations over an entity, where
/// its UI can be dynamically generated based upon a model description.
/// </summary>
public abstract class DynamicCrudViewModelBase : EntityCrudViewModelBase
{
    /// <summary>
    /// Gets a reference to the model description associated with this
    /// ViewModel, from which UI elements can be generated.
    /// </summary>
    public ICrudDescription ModelDescription { get; }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="DynamicCrudViewModelBase"/> class.
    /// </summary>
    /// <param name="entity">
    /// Entity to manage.
    /// </param>
    /// <param name="description">
    /// Model description for the entities.
    /// </param>
    protected DynamicCrudViewModelBase(Model entity, ICrudDescription description)
    {
        ModelDescription = description;
    }
}
