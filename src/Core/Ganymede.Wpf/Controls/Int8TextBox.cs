using TheXDS.Ganymede.Controls.Primitives;
using static TheXDS.Ganymede.Helpers.DependencyObjectHelpers;

namespace TheXDS.Ganymede.Controls;

/// <summary>
/// Implements a <see cref="NumericInputControl{T}"/> to edit values of type
/// <see cref="sbyte"/>.
/// </summary>
public class Int8TextBox : NumericInputControl<sbyte>
{
    static Int8TextBox()
    {
        SetControlStyle<Int8TextBox>(DefaultStyleKeyProperty);
    }
}
