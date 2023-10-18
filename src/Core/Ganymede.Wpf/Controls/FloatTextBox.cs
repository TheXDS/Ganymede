using TheXDS.Ganymede.Controls.Primitives;
using static TheXDS.Ganymede.Helpers.DependencyObjectHelpers;

namespace TheXDS.Ganymede.Controls;

/// <summary>
/// Implements a <see cref="NumericInputControl{T}"/> to edit values of type
/// <see cref="float"/>.
/// </summary>
public class FloatTextBox : NumericInputControl<float>
{
    static FloatTextBox()
    {
        SetControlStyle<FloatTextBox>(DefaultStyleKeyProperty);
    }
}
