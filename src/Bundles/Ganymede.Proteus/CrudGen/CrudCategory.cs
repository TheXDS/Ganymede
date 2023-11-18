namespace TheXDS.Ganymede.CrudGen;

/// <summary>
/// Defines a category that a described model might belong to.
/// </summary>
public enum CrudCategory
{
    /// <summary>
    /// The model is used for management.
    /// </summary>
    Management,
    /// <summary>
    /// The model is used for operations.
    /// </summary>
    Operation,
    /// <summary>
    /// The model represents a catalog.
    /// </summary>
    Catalog,
    /// <summary>
    /// The model is used to store module settings.
    /// </summary>
    Settings
}
