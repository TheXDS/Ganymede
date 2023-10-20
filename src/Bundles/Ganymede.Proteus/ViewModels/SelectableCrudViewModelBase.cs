using TheXDS.Triton.Models.Base;

namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Base class for a ViewModel used for CRUD operations over an entity, where
/// the active entity may be changed.
/// </summary>
public abstract class SelectableCrudViewModelBase : CrudViewModelBase
{
    private Model? entity;

    /// <summary>
    /// Gets or sets a reference to the entity being managed in this ViewModel.
    /// </summary>
    public Model? Entity
    {
        get => entity;
        set => Change(ref entity, value);
    }
}
