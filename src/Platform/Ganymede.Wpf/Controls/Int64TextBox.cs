using TheXDS.Ganymede.Controls.Primitives;
using static TheXDS.Ganymede.Helpers.DependencyObjectHelpers;

namespace TheXDS.Ganymede.Controls;

/// <summary>
/// Implements a <see cref="NumericInputControl{T}"/> to edit values of type
/// <see cref="long"/>.
/// </summary>
public class Int64TextBox : NumericInputControl<long>
{
    static Int64TextBox()
    {
        SetControlStyle<Int64TextBox>(DefaultStyleKeyProperty);
    }
}
