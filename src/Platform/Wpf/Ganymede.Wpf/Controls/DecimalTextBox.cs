using TheXDS.Ganymede.Controls.Primitives;
using static TheXDS.MCART.Helpers.DependencyObjectHelpers;

namespace TheXDS.Ganymede.Controls;

/// <summary>
/// Implements a <see cref="NumericInputControl{T}"/> to edit values of type
/// <see cref="decimal"/>.
/// </summary>
public class DecimalTextBox : NumericInputControl<decimal>
{
    static DecimalTextBox()
    {
        SetControlStyle<DecimalTextBox>(DefaultStyleKeyProperty);
    }
}