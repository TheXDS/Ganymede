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
    /// Gets a binding path to use when getting a friendly name for the entity.
    /// </summary>
    string? FriendlyNameBindingPath { get; }

    /// <summary>
    /// Gets a descriptive name for the described model.
    /// </summary>
    string FriendlyName { get; }

    /// <summary>
    /// Gets a reference to a method to be invoked right before executing a
    /// save operation.
    /// </summary>
    Action<Model>? SaveProlog { get; }
}
