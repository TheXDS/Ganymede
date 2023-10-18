using TheXDS.Ganymede.Controls.Primitives;
using static TheXDS.Ganymede.Helpers.DependencyObjectHelpers;

namespace TheXDS.Ganymede.Controls;

/// <summary>
/// Implements a <see cref="NumericInputControl{T}"/> to edit values of type
/// <see cref="byte"/>.
/// </summary>
public class UInt8TextBox : NumericInputControl<byte>
{
    static UInt8TextBox()
    {
        SetControlStyle<UInt8TextBox>(DefaultStyleKeyProperty);
    }
}
