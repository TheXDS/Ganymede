using TheXDS.Ganymede.CrudGen.Descriptors;
using TheXDS.Triton.Models.Base;

namespace TheXDS.Ganymede.CrudGen.Descriptions;

/// <summary>
/// Defines an <see cref="IPropertyDescriptor"/> used to describe properties
/// that store binary blobs in a <c><see cref="byte"/>[]</c> array.
/// </summary>
public interface IBlobPropertyDescription : IPropertyDescription
{
    /// <summary>
    /// Gets the type of binary blob stored in this property.
    /// </summary>
    BlobType Type => GetStructValue<BlobType>() ?? BlobType.Raw;
}

/// <summary>
/// Defines an <see cref="IPropertyDescriptor"/> used to describe properties
/// that store <c><see cref="Model"/></c> collections.
/// </summary>
public interface ICollectionPropertyDescription : IPropertyDescription, IWidgetConfigurableDescription
{
    /// <summary>
    /// Indicates that a collection must support adding existing entities.
    /// </summary>
    bool Linkable => GetStructValue<bool>() ?? false;

    /// <summary>
    /// Indicates that a collection must support creating new entities. This
    /// also implies the ability to update items already on the list.
    /// </summary>
    bool Creatable => GetStructValue<bool>() ?? false;

    /// <summary>
    /// Indicates the available models to be added/created.
    /// </summary>
    Type[] AvailableModels => GetClassValue<Type[]>() ?? Type.EmptyTypes;
}

/// <summary>
/// Includes description values that allow customizations to be done on the
/// widgets themselves.
/// </summary>
public interface IWidgetConfigurableDescription : IPropertyDescription
{
    WidgetSize WidgetSize => GetStructValue<WidgetSize>() ?? WidgetSize.Medium;
}