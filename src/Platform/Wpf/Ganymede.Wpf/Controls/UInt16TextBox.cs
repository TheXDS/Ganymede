using TheXDS.Ganymede.Controls.Primitives;
using static TheXDS.MCART.Helpers.DependencyObjectHelpers;

namespace TheXDS.Ganymede.Controls;

/// <summary>
/// Implements a <see cref="NumericInputControl{T}"/> to edit values of type
/// <see cref="ushort"/>.
/// </summary>
public class UInt16TextBox : NumericInputControl<ushort>
{
    static UInt16TextBox()
    {
        SetControlStyle<UInt16TextBox>(DefaultStyleKeyProperty);
    }
}
