namespace TheXDS.Ganymede.ValueConverters;

/// <summary>
/// Enumerates the different configuration values for a
/// <see cref="DescriptionCountVisibilityConverter"/>.
/// </summary>
public enum DescriptionCountVisibility : byte
{
    /// <summary>
    /// Controls will be made visible if there is a single CRUD description.
    /// </summary>
    Single,
    /// <summary>
    /// COntrols will be made visible if there is multipls CRUD descriptions.
    /// </summary>
    Multiple
}
