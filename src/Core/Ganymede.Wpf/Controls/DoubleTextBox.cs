using TheXDS.Ganymede.Controls.Primitives;
using static TheXDS.Ganymede.Helpers.DependencyObjectHelpers;

namespace TheXDS.Ganymede.Controls;

/// <summary>
/// Implements a <see cref="NumericInputControl{T}"/> to edit values of type
/// <see cref="double"/>.
/// </summary>
public class DoubleTextBox : NumericInputControl<double>
{
    static DoubleTextBox()
    {
        SetControlStyle<DoubleTextBox>(DefaultStyleKeyProperty);
    }
}
