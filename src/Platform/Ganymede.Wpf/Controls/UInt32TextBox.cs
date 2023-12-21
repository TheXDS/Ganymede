using TheXDS.Ganymede.Controls.Primitives;
using static TheXDS.Ganymede.Helpers.DependencyObjectHelpers;

namespace TheXDS.Ganymede.Controls;

/// <summary>
/// Implements a <see cref="NumericInputControl{T}"/> to edit values of type
/// <see cref="uint"/>.
/// </summary>
public class UInt32TextBox : NumericInputControl<uint>
{
    static UInt32TextBox()
    {
        SetControlStyle<UInt32TextBox>(DefaultStyleKeyProperty);
    }
}
