using TheXDS.Ganymede.Controls.Primitives;
using static TheXDS.MCART.Helpers.DependencyObjectHelpers;

namespace TheXDS.Ganymede.Controls;

/// <summary>
/// Implements a <see cref="NumericInputControl{T}"/> to edit values of type
/// <see cref="short"/>.
/// </summary>
public class Int16TextBox : NumericInputControl<short>
{
    static Int16TextBox()
    {
        SetControlStyle<Int16TextBox>(DefaultStyleKeyProperty);
    }
}
