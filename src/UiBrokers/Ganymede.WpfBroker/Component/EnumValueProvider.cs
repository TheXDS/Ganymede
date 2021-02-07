using System;
using System.Windows.Markup;

namespace TheXDS.Ganymede.WpfBroker.Component
{
    /// <summary>
    /// Provides a XAML markup extension that allows enum values to be binded
    /// to a control that accepts enum arrays as a data source.
    /// </summary>
    public class EnumValueProvider : MarkupExtension
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
        public EnumValueProvider(Type enumType)
        {
            if (enumType is null)
            {
                throw new ArgumentNullException(nameof(enumType));
            }
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("A type that inherits from System.Enum was expected.");
            }
            EnumType = enumType;
        }

        /// <summary>
        /// Gets a reference to the specified enum type for which to get the
        /// enum values.
        /// </summary>
        public Type EnumType { get; }

        /// <inheritdoc/>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Enum.GetValues(EnumType);
        }
    }
}
