using System.Linq.Expressions;
using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Ganymede.CrudGen.Descriptors;
using TheXDS.Triton.Models.Base;

namespace TheXDS.Ganymede.CrudGen;

/// <summary>
/// Defines a set of members to be implemented by a type that esposes property
/// description functions for CRUD generation.
/// </summary>
/// <typeparam name="T">
/// Type of <see cref="Model"/> for which to describe its properties.
/// </typeparam>
public interface IPropertyConfigurator<T> where T : Model
{
    /// <summary>
    /// Begins the description of a <see cref="string"/> property.
    /// </summary>
    /// <param name="propertySelector">
    /// Lambda that selects the property to configure from the model.
    /// </param>
    /// <returns>
    /// An <see cref="ITextPropertyDescriptor"/> instance that can be used to
    /// configure the presentation and behavior of any visual elements used to
    /// show and/or edit a property.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if any previous calls to configure this property used a
    /// radically different kind of descriptor for the property.
    /// </exception>
    ITextPropertyDescriptor Property(Expression<Func<T, string?>> propertySelector);

    /// <summary>
    /// Begins the description of a <see cref="string"/> property.
    /// </summary>
    /// <param name="propertySelector">
    /// Lambda that selects the property to configure from the model.
    /// </param>
    /// <returns>
    /// An <see cref="ITextPropertyDescriptor"/> instance that can be used to
    /// configure the presentation and behavior of any visual elements used to
    /// show and/or edit a property.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if any previous calls to configure this property used a
    /// radically different kind of descriptor for the property.
    /// </exception>
    ICollectionPropertyDescriptor Property<TModel>(Expression<Func<T, ICollection<TModel>>> propertySelector) where TModel : Model;

    /// <summary>
    /// Begins the description of a <see cref="DateTime"/> property.
    /// </summary>
    /// <param name="propertySelector">
    /// Lambda that selects the property to configure from the model.
    /// </param>
    /// <returns>
    /// An <see cref="IDatePropertyDescriptor"/> instance that can be used to
    /// configure the presentation and behavior of any visual elements used to
    /// show and/or edit a property.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if any previous calls to configure this property used a
    /// radically different kind of descriptor for the property.
    /// </exception>
    IDatePropertyDescriptor Property(Expression<Func<T, DateTime>> propertySelector);

    /// <summary>
    /// Begins the description of a <see cref="DateTime"/> property.
    /// </summary>
    /// <param name="propertySelector">
    /// Lambda that selects the property to configure from the model.
    /// </param>
    /// <returns>
    /// An <see cref="IDatePropertyDescriptor"/> instance that can be used to
    /// configure the presentation and behavior of any visual elements used to
    /// show and/or edit a property.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if any previous calls to configure this property used a
    /// radically different kind of descriptor for the property.
    /// </exception>
    INullableDatePropertyDescriptor Property(Expression<Func<T, DateTime?>> propertySelector);

    /// <summary>
    /// Begins the description of a <see cref="byte"/><c>[]</c> property.
    /// </summary>
    /// <param name="propertySelector">
    /// Lambda that selects the property to configure from the model.
    /// </param>
    /// <returns>
    /// An <see cref="IBlobPropertyDescription"/> instance that can be used to
    /// configure the presentation and behavior of any visual elements used to
    /// show and/or edit a property.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if any previous calls to configure this property used a
    /// radically different kind of descriptor for the property.
    /// </exception>
    IBlobPropertyDescriptor Property(Expression<Func<T, byte[]>> propertySelector);

    /// <summary>
    /// Begins the description of a simple, generic property.
    /// </summary>
    /// <typeparam name="TValue">Property value type.</typeparam>
    /// <param name="propertySelector">
    /// Lambda that selects the property to configure from the model.
    /// </param>
    /// <returns>
    /// A <see cref="IPropertyDescriptor{T}"/> instance that can be used to
    /// configure the presentation and behavior of any visual elements used to
    /// show and/or edit a property.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if any previous calls to configure this property used a
    /// radically different kind of descriptor for the property.
    /// </exception>
    IPropertyDescriptor<TValue> Property<TValue>(Expression<Func<T, TValue>> propertySelector);

    /// <summary>
    /// Begins the description of a numeric property.
    /// </summary>
    /// <typeparam name="TValue">
    /// Property value type. It must be a primitive type, and must implement
    /// <see cref="IComparable{T}"/> of type <typeparamref name="TValue"/>.
    /// </typeparam>
    /// <param name="propertySelector">
    /// Lambda that selects the property to configure from the model.
    /// </param>
    /// <returns>
    /// An <see cref="INumericPropertyDescriptor{T}"/> instance that can be
    /// used to configure the presentation and behavior of any visual elements
    /// used to show and/or edit a property.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if any previous calls to configure this property used a
    /// radically different kind of descriptor for the property.
    /// </exception>
    INumericPropertyDescriptor<TValue> NumericProperty<TValue>(Expression<Func<T, TValue>> propertySelector) where TValue : unmanaged, IComparable<TValue>;

    /// <summary>
    /// Begins the description of a numeric property.
    /// </summary>
    /// <typeparam name="TValue">
    /// Property value type. It must be a primitive type, and must implement
    /// <see cref="IComparable{T}"/> of type <typeparamref name="TValue"/>.
    /// </typeparam>
    /// <param name="propertySelector">
    /// Lambda that selects the property to configure from the model.
    /// </param>
    /// <returns>
    /// An <see cref="INumericPropertyDescriptor{T}"/> instance that can be
    /// used to configure the presentation and behavior of any visual elements
    /// used to show and/or edit a property.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if any previous calls to configure this property used a
    /// radically different kind of descriptor for the property.
    /// </exception>
    INullableNumericPropertyDescriptor<TValue> NullableNumericProperty<TValue>(Expression<Func<T, TValue?>> propertySelector) where TValue : unmanaged, IComparable<TValue>;
}
