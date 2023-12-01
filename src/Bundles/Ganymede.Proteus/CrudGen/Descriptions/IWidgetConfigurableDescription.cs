namespace TheXDS.Ganymede.CrudGen.Descriptions;

/// <summary>
/// Includes description values that allow customizations to be done on the
/// widgets themselves.
/// </summary>
public interface IWidgetConfigurableDescription : IPropertyDescription
{
    /// <summary>
    /// Indicated the desired widget size for properties that allow such
    /// configuration to be applied.
    /// </summary>
    WidgetSize WidgetSize => GetStructValue<WidgetSize>() ?? WidgetSize.Flex;
}