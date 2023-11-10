namespace TheXDS.Ganymede.CrudGen.Descriptions;

/// <summary>
/// Defines a set of properties to be exposed by a descriptor for
/// <see cref="string"/> properties normally associated with text.
/// </summary>
public interface ITextPropertyDescription : IPropertyDescription<string>, INullablePropertyDescription, IWidgetConfigurableDescription
{
    /// <summary>
    /// Gets a value that indicates the kind of text that this property is
    /// intended to contain.
    /// </summary>
    TextKind Kind => GetStructValue<TextKind>() ?? TextKind.Generic;

    /// <summary>
    /// Gets a value that indicates the minimum valid length for the text being
    /// inserted into the editing control/widget.
    /// </summary>
    int? MinLength => GetStructValue<int>();


    /// <summary>
    /// Gets a value that indicates the maximum valid length for the text being
    /// inserted into the editing control/widget.
    /// </summary>
    int? MaxLength => GetStructValue<int>();

    /// <summary>
    /// Gets a string that represents the mask to be applied to a text editing
    /// control/widget, or <see langword="null"/> if no mask was specified.
    /// Only valid if <see cref="Kind"/> is equal to
    /// <see cref="TextKind.Maskable"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown if this property is read and the <see cref="Kind"/> property of
    /// this descriptor is not equal to <see cref="TextKind.Maskable"/>.
    /// </exception>
    string? Mask => Kind == TextKind.Maskable ? GetClassValue<string>() : throw new InvalidOperationException();
}
