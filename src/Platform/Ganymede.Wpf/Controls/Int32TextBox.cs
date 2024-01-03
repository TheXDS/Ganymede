using System.Windows.Controls.Primitives;
using TheXDS.Ganymede.Controls.Primitives;
using static TheXDS.Ganymede.Helpers.DependencyObjectHelpers;

namespace TheXDS.Ganymede.Controls;

/// <summary>
/// Implements a <see cref="NumericInputControl{T}"/> to edit values of type
/// <see cref="int"/>.
/// </summary>
public class Int32TextBox : NumericInputControl<int>
{
    static Int32TextBox()
    {
        SetControlStyle<Int32TextBox>(DefaultStyleKeyProperty);
    }
}
