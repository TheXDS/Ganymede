using TheXDS.Ganymede.Component;
using TheXDS.Ganymede.CrudGen;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.Types.Base;
using TheXDS.Triton.Models.Base;

namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Base class for a ViewModel used for CRUD operations over an entity.
/// </summary>
public abstract class CrudViewModelBase : ViewModel
{
    private IEnumerable<ButtonInteraction> _crudActions = Array.Empty<ButtonInteraction>();
    private readonly ICrudDescription _modelDescription;
    private Model _entity;

    /// <summary>
    /// Initializes a new instance of the <see cref="CrudViewModelBase"/>
    /// class.
    /// </summary>
    /// <param name="entity">
    /// Entity to manage.
    /// </param>
    /// <param name="description">
    /// Model description for the entities.
    /// </param>
    protected CrudViewModelBase(Model entity, ICrudDescription description)
    {
        RegisterPropertyChangeBroadcast(nameof(Entity), nameof(ModelType));
        _entity = entity;
        _modelDescription = description;
    }

    /// <summary>
    /// Gets or sets a collection of custom, special actions available for the
    /// entity.
    /// </summary>
    public IEnumerable<ButtonInteraction> CrudActions
    {
        get => _crudActions;
        set => Change(ref _crudActions, value);
    }

    /// <summary>
    /// Gets a reference to the model description associated with this
    /// ViewModel.
    /// </summary>
    public ICrudDescription ModelDescription => _modelDescription;

    /// <summary>
    /// Gets a reference to the entity being managed in this ViewModel.
    /// </summary>
    public Model Entity
    {
        get => _entity;
        set => Change(ref _entity, value);
    }

    /// <summary>
    /// Gets or sets a value that determines if this ViewModel has been initialized.
    /// </summary>
    public bool Initialized { get; set; }

    /// <summary>
    /// Gets a reference to the Model type of the Entity.
    /// </summary>
    public virtual Type ModelType => _entity.GetType();
}
