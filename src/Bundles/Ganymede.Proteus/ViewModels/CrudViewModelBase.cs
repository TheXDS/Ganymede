using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.Types.Base;

namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Base class for a ViewModel used for CRUD operations over an entity.
/// </summary>
public abstract class CrudViewModelBase : ViewModel
{
    private IEnumerable<ButtonInteraction> _crudActions = Array.Empty<ButtonInteraction>();

    /// <summary>
    /// Initializes a new instance of the <see cref="CrudViewModelBase"/>
    /// class.
    /// </summary>
    protected CrudViewModelBase()
    {
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
}
