namespace TheXDS.Ganymede.Component;

/// <summary>
/// Enumerates the possible states of a Crud ViewModel to indicate widget visibility.
/// </summary>
[Flags]
public enum CrudVisibility : byte
{
    /// <summary>
    /// Indicates no visibility at all.
    /// </summary>
    Hidden = 0,

    /// <summary>
    /// Indicates visibility when no entity is selected.
    /// </summary>
    Unselected = 1,
    
    /// <summary>
    /// Indicates visibility when an entity is selected.
    /// </summary>
    Selected = 2,

    /// <summary>
    /// Indicates visibility when a new entity is being created.
    /// </summary>
    CreatingNew = 4,

    /// <summary>
    /// Indicates visibility when an existing entity is being edited.
    /// </summary>
    EditExisting = 8,

    /// <summary>
    /// Indicates visibility under all circumstances.
    /// </summary>
    All = byte.MaxValue
}
