using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Ganymede.CrudGen.Descriptors;
using TheXDS.MCART.Helpers;
using TheXDS.Triton.Models.Base;

namespace TheXDS.Ganymede.CrudGen;

/// <summary>
/// Implements the property configuration logic for CRUD generation.
/// </summary>
/// <typeparam name="T">Model to configure.</typeparam>
public class PropertyDescriptorConfigurator<T> : IPropertyConfigurator<T> where T : Model
{
    private readonly Dictionary<PropertyInfo, DescriptionEntry> _properties;
    private readonly ICrudDescription _parent;

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="PropertyDescriptorConfigurator{T}"/>class.
    /// </summary>
    /// <param name="properties">
    /// Dictionary to use when adding property descriptions.
    /// </param>
    /// <param name="parent">Parent CRUD description.</param>
    public PropertyDescriptorConfigurator(Dictionary<PropertyInfo, DescriptionEntry> properties, ICrudDescription parent)
    {
        _properties = properties;
        _parent = parent;
    }

    /// <summary>
    /// Enumerates the described properties for the model.
    /// </summary>
    public IReadOnlyDictionary<PropertyInfo, DescriptionEntry> PropertyDescriptions => _properties;

    /// <inheritdoc/>
    public IPropertyDescriptor<TValue> Property<TValue>(Expression<Func<T, TValue>> propertySelector) where TValue : struct
    {
        return NewDescriptor<PropertyDescriptor<TValue>, T, TValue>(propertySelector);
    }

    /// <inheritdoc/>
    public INumericPropertyDescriptor<TValue> NumericProperty<TValue>(Expression<Func<T, TValue>> propertySelector) where TValue : unmanaged, IComparable<TValue>
    {
        return NewDescriptor<NumericPropertyDescriptor<TValue>, T, TValue>(propertySelector);
    }

    /// <inheritdoc/>
    public ITextPropertyDescriptor Property(Expression<Func<T, string?>> propertySelector)
    {
        return NewDescriptor<TextPropertyDescriptor, T, string?>(propertySelector);
    }

    /// <inheritdoc/>
    public IDatePropertyDescriptor Property(Expression<Func<T, DateTime>> propertySelector)
    {
        return NewDescriptor<DatePropertyDescriptor, T, DateTime>(propertySelector);
    }

    /// <inheritdoc/>
    public INullableDatePropertyDescriptor Property(Expression<Func<T, DateTime?>> propertySelector)
    {
        return NewDescriptor<DatePropertyDescriptor, T, DateTime?>(propertySelector);
    }

    /// <inheritdoc/>
    public INullableNumericPropertyDescriptor<TValue> NullableNumericProperty<TValue>(Expression<Func<T, TValue?>> propertySelector) where TValue : unmanaged, IComparable<TValue>
    {
        return NewDescriptor<NumericPropertyDescriptor<TValue>, T, TValue?>(propertySelector);
    }

    /// <inheritdoc/>
    public IBlobPropertyDescriptor Property(Expression<Func<T, byte[]>> propertySelector)
    {
        return NewDescriptor<BlobPropertyDescriptor, T, byte[]>(propertySelector);
    }

    /// <inheritdoc/>
    public ICollectionPropertyDescriptor Property<TModel>(Expression<Func<T, ICollection<TModel>>> propertySelector) where TModel : Model
    {
        return NewDescriptor<CollectionPropertyDescriptor, T, ICollection<TModel>>(propertySelector);
    }

    /// <inheritdoc/>
    public ISingleObjectPropertyDescriptor Property(Expression<Func<T, Model?>> propertySelector)
    {
        return NewDescriptor<SingleObjectPropertyDescriptor, T, Model?>(propertySelector);
    }

    /// <inheritdoc/>
    public IEnumPropertyDescriptor EnumProperty<TEnum>(Expression<Func<T, TEnum?>> propertySelector) where TEnum : Enum
    {
        return NewDescriptor<EnumPropertyDescriptor, T, TEnum?>(propertySelector);
    }

    private TDescriptor NewDescriptor<TDescriptor, TObject, TProperty>(Expression<Func<TObject, TProperty>> propertySelector) where TDescriptor : class, IPropertyDescriptor, IPropertyDescription, new()
    {
        return RegisterDescription(propertySelector, prop =>
        {
            var d = new TDescriptor() { Property = prop, Parent = _parent };
            return new(d, d);
        }) as TDescriptor ?? throw new InvalidOperationException();
    }

    [DebuggerStepThrough]
    private IPropertyDescriptor RegisterDescription<TProperty, TObject>(Expression<Func<TObject, TProperty>> propertySelector, Func<PropertyInfo, DescriptionEntry> descriptionFactory)
    {
        var p = ReflectionHelpers.GetProperty(propertySelector);

        if (!_properties.ContainsKey(p))
        {
            _properties.Add(p, descriptionFactory.Invoke(p));
        }
        return _properties[p].Descriptor;
    }
}
