using TheXDS.Ganymede.CrudGen.Descriptors;

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