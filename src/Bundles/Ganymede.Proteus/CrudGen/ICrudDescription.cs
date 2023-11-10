using System.Reflection;
using TheXDS.Triton.Models.Base;

namespace TheXDS.Ganymede.CrudGen;

/// <summary>
/// Defines a set of members to be implemented by a type that exposes model
/// CRUD descriptions.
/// </summary>
public interface ICrudDescription
{
    /// <summary>
    /// Gets a reference to the described model type.
    /// </summary>
    Type Model { get; }

    /// <summary>
    /// Enumerates the described properties for the model.
    /// </summary>
    IReadOnlyDictionary<PropertyInfo, DescriptionEntry> PropertyDescriptions { get; }

    /// <summary>
    /// Enumerates the properties to be shown in a list view as columns.
    /// </summary>
    IEnumerable<PropertyInfo> ListViewProperties { get; }

    /// <summary>
    /// Gets a binding path to use when getting a friendly name for the entity.
    /// </summary>
    string? FriendlyNameBindingPath { get; }

    /// <summary>
    /// Gets a descriptive name for the described model.
    /// </summary>
    string FriendlyName { get; }

    /// <summary>
    /// Gets a reference to a collection of methods to be invoked right before
    /// executing a save operation.
    /// </summary>
    IEnumerable<Action<Model>> SavePrologs { get; }

    /// <summary>
    /// Gets a reference to the resource type to use when resolving labels.
    /// </summary>
    Type? ResourceType { get; }

    /// <summary>
    /// Gets a reference to the ViewModel Type to use when no entity is
    /// selected on a CRUD page.
    /// </summary>
    Type? DashboardViewModel { get; }

    /// <summary>
    /// Gets a reference to the ViewModel Type to use when an entity is
    /// selected on a CRUD page.
    /// </summary>
    Type? DetailsViewModel { get; }
}
