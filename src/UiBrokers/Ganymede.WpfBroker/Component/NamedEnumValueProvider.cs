using System;
using TheXDS.MCART.Types;

namespace TheXDS.Ganymede.WpfBroker.Component
{
    /// <summary>
    /// Provides a XAML markup extension that allows enum values to be binded
    /// to a control that accepts enum arrays as a data source.
    /// </summary>
    public class NamedEnumValueProvider : EnumValueProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumValueProvider"/>
        /// class.
        /// </summary>
        /// <param name="enumType">Enum type to be exposed.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="enumType"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="enumType"/> is not a valid
        /// <see cref="Enum"/> type.
        /// </exception>
        public NamedEnumValueProvider(Type enumType) : base(enumType)
        {
        }

        /// <inheritdoc/>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return typeof(NamedObject<>).MakeGenericType(EnumType).GetMethod("FromEnum")!.Invoke(null, null)!;
        }
    }
}
