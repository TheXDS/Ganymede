using System.Linq.Expressions;
using TheXDS.Ganymede.CrudGen.Descriptors;
using TheXDS.Triton.Models.Base;

namespace TheXDS.Ganymede.CrudGen;

/// <summary>
/// Includes a set of extensions to the <see cref="IPropertyConfigurator{T}"/> interface.
/// </summary>
public static class PropertyConfiguratorExtensions
{
    /// <summary>
    /// Begins the description of a <see cref="byte"/> property.
    /// </summary>
    /// <param name="configurator">
    /// Property configurator instance to use.
    /// </param>
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
    public static INumericPropertyDescriptor<byte> Property<T>(this IPropertyConfigurator<T> configurator, Expression<Func<T, byte>> propertySelector) where T : Model => configurator.NumericProperty(propertySelector);

    /// <summary>
    /// Begins the description of a <see cref="sbyte"/> property.
    /// </summary>
    /// <param name="configurator">
    /// Property configurator instance to use.
    /// </param>
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
    public static INumericPropertyDescriptor<sbyte> Property<T>(this IPropertyConfigurator<T> configurator, Expression<Func<T, sbyte>> propertySelector) where T : Model => configurator.NumericProperty(propertySelector);

    /// <summary>
    /// Begins the description of a <see cref="char"/> property.
    /// </summary>
    /// <param name="configurator">
    /// Property configurator instance to use.
    /// </param>
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
    public static INumericPropertyDescriptor<char> Property<T>(this IPropertyConfigurator<T> configurator, Expression<Func<T, char>> propertySelector) where T : Model => configurator.NumericProperty(propertySelector);

    /// <summary>
    /// Begins the description of a <see cref="short"/> property.
    /// </summary>
    /// <param name="configurator">
    /// Property configurator instance to use.
    /// </param>
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
    public static INumericPropertyDescriptor<short> Property<T>(this IPropertyConfigurator<T> configurator, Expression<Func<T, short>> propertySelector) where T : Model => configurator.NumericProperty(propertySelector);

    /// <summary>
    /// Begins the description of a <see cref="ushort"/> property.
    /// </summary>
    /// <param name="configurator">
    /// Property configurator instance to use.
    /// </param>
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
    public static INumericPropertyDescriptor<ushort> Property<T>(this IPropertyConfigurator<T> configurator, Expression<Func<T, ushort>> propertySelector) where T : Model => configurator.NumericProperty(propertySelector);

    /// <summary>
    /// Begins the description of a <see cref="int"/> property.
    /// </summary>
    /// <param name="configurator">
    /// Property configurator instance to use.
    /// </param>
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
    public static INumericPropertyDescriptor<int> Property<T>(this IPropertyConfigurator<T> configurator, Expression<Func<T, int>> propertySelector) where T : Model => configurator.NumericProperty(propertySelector);

    /// <summary>
    /// Begins the description of a <see cref="uint"/> property.
    /// </summary>
    /// <param name="configurator">
    /// Property configurator instance to use.
    /// </param>
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
    public static INumericPropertyDescriptor<uint> Property<T>(this IPropertyConfigurator<T> configurator, Expression<Func<T, uint>> propertySelector) where T : Model => configurator.NumericProperty(propertySelector);

    /// <summary>
    /// Begins the description of a <see cref="long"/> property.
    /// </summary>
    /// <param name="configurator">
    /// Property configurator instance to use.
    /// </param>
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
    public static INumericPropertyDescriptor<long> Property<T>(this IPropertyConfigurator<T> configurator, Expression<Func<T, long>> propertySelector) where T : Model => configurator.NumericProperty(propertySelector);

    /// <summary>
    /// Begins the description of a <see cref="ulong"/> property.
    /// </summary>
    /// <param name="configurator">
    /// Property configurator instance to use.
    /// </param>
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
    public static INumericPropertyDescriptor<ulong> Property<T>(this IPropertyConfigurator<T> configurator, Expression<Func<T, ulong>> propertySelector) where T : Model => configurator.NumericProperty(propertySelector);

    /// <summary>
    /// Begins the description of a <see cref="float"/> property.
    /// </summary>
    /// <param name="configurator">
    /// Property configurator instance to use.
    /// </param>
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
    public static INumericPropertyDescriptor<float> Property<T>(this IPropertyConfigurator<T> configurator, Expression<Func<T, float>> propertySelector) where T : Model => configurator.NumericProperty(propertySelector);

    /// <summary>
    /// Begins the description of a <see cref="double"/> property.
    /// </summary>
    /// <param name="configurator">
    /// Property configurator instance to use.
    /// </param>
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
    public static INumericPropertyDescriptor<double> Property<T>(this IPropertyConfigurator<T> configurator, Expression<Func<T, double>> propertySelector) where T : Model => configurator.NumericProperty(propertySelector);

    /// <summary>
    /// Begins the description of a <see cref="decimal"/> property.
    /// </summary>
    /// <param name="configurator">
    /// Property configurator instance to use.
    /// </param>
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
    public static INumericPropertyDescriptor<decimal> Property<T>(this IPropertyConfigurator<T> configurator, Expression<Func<T, decimal>> propertySelector) where T : Model => configurator.NumericProperty(propertySelector);

    /// <summary>
    /// Begins the description of a <see cref="byte"/> property.
    /// </summary>
    /// <param name="configurator">
    /// Property configurator instance to use.
    /// </param>
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
    public static INullableNumericPropertyDescriptor<byte> Property<T>(this IPropertyConfigurator<T> configurator, Expression<Func<T, byte?>> propertySelector) where T : Model => configurator.NullableNumericProperty(propertySelector);

    /// <summary>
    /// Begins the description of a <see cref="sbyte"/> property.
    /// </summary>
    /// <param name="configurator">
    /// Property configurator instance to use.
    /// </param>
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
    public static INullableNumericPropertyDescriptor<sbyte> Property<T>(this IPropertyConfigurator<T> configurator, Expression<Func<T, sbyte?>> propertySelector) where T : Model => configurator.NullableNumericProperty(propertySelector);

    /// <summary>
    /// Begins the description of a <see cref="char"/> property.
    /// </summary>
    /// <param name="configurator">
    /// Property configurator instance to use.
    /// </param>
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
    public static INullableNumericPropertyDescriptor<char> Property<T>(this IPropertyConfigurator<T> configurator, Expression<Func<T, char?>> propertySelector) where T : Model => configurator.NullableNumericProperty(propertySelector);

    /// <summary>
    /// Begins the description of a <see cref="short"/> property.
    /// </summary>
    /// <param name="configurator">
    /// Property configurator instance to use.
    /// </param>
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
    public static INullableNumericPropertyDescriptor<short> Property<T>(this IPropertyConfigurator<T> configurator, Expression<Func<T, short?>> propertySelector) where T : Model => configurator.NullableNumericProperty(propertySelector);

    /// <summary>
    /// Begins the description of a <see cref="ushort"/> property.
    /// </summary>
    /// <param name="configurator">
    /// Property configurator instance to use.
    /// </param>
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
    public static INullableNumericPropertyDescriptor<ushort> Property<T>(this IPropertyConfigurator<T> configurator, Expression<Func<T, ushort?>> propertySelector) where T : Model => configurator.NullableNumericProperty(propertySelector);

    /// <summary>
    /// Begins the description of a <see cref="int"/> property.
    /// </summary>
    /// <param name="configurator">
    /// Property configurator instance to use.
    /// </param>
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
    public static INullableNumericPropertyDescriptor<int> Property<T>(this IPropertyConfigurator<T> configurator, Expression<Func<T, int?>> propertySelector) where T : Model => configurator.NullableNumericProperty(propertySelector);

    /// <summary>
    /// Begins the description of a <see cref="uint"/> property.
    /// </summary>
    /// <param name="configurator">
    /// Property configurator instance to use.
    /// </param>
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
    public static INullableNumericPropertyDescriptor<uint> Property<T>(this IPropertyConfigurator<T> configurator, Expression<Func<T, uint?>> propertySelector) where T : Model => configurator.NullableNumericProperty(propertySelector);

    /// <summary>
    /// Begins the description of a <see cref="long"/> property.
    /// </summary>
    /// <param name="configurator">
    /// Property configurator instance to use.
    /// </param>
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
    public static INullableNumericPropertyDescriptor<long> Property<T>(this IPropertyConfigurator<T> configurator, Expression<Func<T, long?>> propertySelector) where T : Model => configurator.NullableNumericProperty(propertySelector);

    /// <summary>
    /// Begins the description of a <see cref="ulong"/> property.
    /// </summary>
    /// <param name="configurator">
    /// Property configurator instance to use.
    /// </param>
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
    public static INullableNumericPropertyDescriptor<ulong> Property<T>(this IPropertyConfigurator<T> configurator, Expression<Func<T, ulong?>> propertySelector) where T : Model => configurator.NullableNumericProperty(propertySelector);

    /// <summary>
    /// Begins the description of a <see cref="float"/> property.
    /// </summary>
    /// <param name="configurator">
    /// Property configurator instance to use.
    /// </param>
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
    public static INullableNumericPropertyDescriptor<float> Property<T>(this IPropertyConfigurator<T> configurator, Expression<Func<T, float?>> propertySelector) where T : Model => configurator.NullableNumericProperty(propertySelector);

    /// <summary>
    /// Begins the description of a <see cref="double"/> property.
    /// </summary>
    /// <param name="configurator">
    /// Property configurator instance to use.
    /// </param>
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
    public static INullableNumericPropertyDescriptor<double> Property<T>(this IPropertyConfigurator<T> configurator, Expression<Func<T, double?>> propertySelector) where T : Model => configurator.NullableNumericProperty(propertySelector);

    /// <summary>
    /// Begins the description of a <see cref="decimal"/> property.
    /// </summary>
    /// <param name="configurator">
    /// Property configurator instance to use.
    /// </param>
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
    public static INullableNumericPropertyDescriptor<decimal> Property<T>(this IPropertyConfigurator<T> configurator, Expression<Func<T, decimal?>> propertySelector) where T : Model => configurator.NullableNumericProperty(propertySelector);

}