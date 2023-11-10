using TheXDS.Triton.Models.Base;

namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Base class for a ViewModel used for CRUD operations over an entity, where
/// the reference to the entity cannot be changed.
/// </summary>
public abstract class EntityCrudViewModelBase : CrudViewModelBase
{
    /// <summary>
    /// Gets a reference to the entity being managed in this ViewModel.
    /// </summary>
    public Model Entity { get; set; } = null!;
}

/// <summary>
/// Base class for a ViewModel used for CRUD operations over an entity, where
/// the reference to the entity cannot be changed.
/// </summary>
/// <typeparam name="T">
/// Type of model that this instance holds.
/// </typeparam>
public abstract class EntityCrudViewModelBase<T> : EntityCrudViewModelBase
    where T : Model
{
    /// <summary>
    /// Gets a reference to the entity being managed in this ViewModel.
    /// </summary>
    public new T Entity
    { 
        get => (T)base.Entity;
        set =>  base.Entity = value;
    }
}