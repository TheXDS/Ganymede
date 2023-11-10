namespace TheXDS.Ganymede.CrudGen.Descriptions;

/// <summary>
/// Indicates the source of the described property.
/// </summary>
public enum PropertyLocation : byte
{
    /// <summary>
    /// The property exists in the entity.
    /// </summary>
    Model,
    /// <summary>
    /// The property exists in the ViewModel.
    /// </summary>
    ViewModel
}