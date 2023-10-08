using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TheXDS.Ganymede.CrudGen.Descriptors;

/// <summary>
/// Defines a set of members to be implemented by a type that describes a
/// property for UI generation by Proteus.
/// </summary>
public interface IPropertyDescriptor
{
    /// <summary>
    /// Sets a description value.
    /// </summary>
    /// <param name="value">Description value to set.</param>
    /// <param name="name">Name used to identify the value.</param>
    /// <remarks>
    /// You should avoid using this method directly for model descriptions.
    /// Use the API provided by Proteus as extension methods instead.
    /// </remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal void SetValue(object? value, [CallerMemberName] string name = null!);
}
