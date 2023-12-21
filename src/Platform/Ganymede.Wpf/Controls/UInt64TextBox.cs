using TheXDS.Ganymede.Controls.Primitives;
using static TheXDS.Ganymede.Helpers.DependencyObjectHelpers;

namespace TheXDS.Ganymede.Controls;

/// <summary>
/// Implements a <see cref="NumericInputControl{T}"/> to edit values of type
/// <see cref="ulong"/>.
/// </summary>
public class UInt64TextBox : NumericInputControl<ulong>
{
    static UInt64TextBox()
    {
        SetControlStyle<UInt64TextBox>(DefaultStyleKeyProperty);
    }
}
