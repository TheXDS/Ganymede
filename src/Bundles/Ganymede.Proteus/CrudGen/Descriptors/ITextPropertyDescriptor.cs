namespace TheXDS.Ganymede.CrudGen.Descriptors;

/// <summary>
/// Defines a set of members to be implemented by a type that describes a
/// <see cref="string"/> property for UI generation by Proteus.
/// </summary>
public interface ITextPropertyDescriptor : IPropertyDescriptor<string>, INullablePropertyDescriptor
{
    /// <summary>
    /// Shortcut to set the text kind to <see cref="TextKind.Big"/>.
    /// </summary>
    /// <returns>
    /// This same descriptor instance, allowing the use of Fluent syntax.
    /// </returns>
    public ITextPropertyDescriptor Big() => Kind(TextKind.Big);

    /// <summary>
    /// Sets the kind of text the property is intended to hold.
    /// </summary>
    /// <param name="kind">
    /// Kind of text the property is intended to hold.
    /// </param>
    /// <returns>This same descriptor instance.</returns>
    public ITextPropertyDescriptor Kind(TextKind kind)
    {
        SetValue(kind);
        return this;
    }

    /// <summary>
    /// Sets the minimum allowed text length for the property.
    /// </summary>
    /// <param name="minLength">
    /// Minimum allowed text length for the property.
    /// </param>
    /// <returns>This same descriptor instance.</returns>
    ITextPropertyDescriptor MinLength(int minLength)
    {
        SetValue(minLength);
        return this;
    }

    /// <summary>
    /// Sets the maximum allowed text length for the property.
    /// </summary>
    /// <param name="maxLength">
    /// Maximum allowed text length for the property.
    /// </param>
    /// <returns>This same descriptor instance.</returns>
    ITextPropertyDescriptor MaxLength(int maxLength)
    {
        SetValue(maxLength);
        return this;
    }

    /// <summary>
    /// Sets the desired text mask for the property. Implies 
    /// <see cref="TextKind.Maskable"/>
    /// </summary>
    /// <param name="mask">
    /// Mask to use for the editing text control/widget.
    /// </param>
    /// <returns>This same descriptor instance.</returns>
    ITextPropertyDescriptor Mask(string mask)
    {
        SetValue(mask);
        return Kind(TextKind.Maskable);
    }
}

