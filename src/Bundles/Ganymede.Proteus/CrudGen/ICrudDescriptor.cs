namespace TheXDS.Ganymede.CrudGen;

/// <summary>
/// Defines a set of members to be implemented by a type that exposes Crud description methods.
/// </summary>
public interface ICrudDescriptor
{
    /// <summary>
    /// Gets a reference to the current description configuration.
    /// </summary>
    ICrudDescription Description { get; }

    /// <summary>
    /// Gets a reference to the described model.
    /// </summary>
    Type Model { get; }
}