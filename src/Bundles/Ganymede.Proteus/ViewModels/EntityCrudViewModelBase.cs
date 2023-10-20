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
    public Model Entity { get; init; } = null!;
}