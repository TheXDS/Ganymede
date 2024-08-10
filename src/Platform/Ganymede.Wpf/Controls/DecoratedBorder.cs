using System.Windows.Controls;
using static TheXDS.MCART.Helpers.DependencyObjectHelpers;

namespace TheXDS.Ganymede.Controls;

/// <summary>
/// Border with decorations.
/// </summary>
public class DecoratedBorder : Border
{
    static DecoratedBorder()
    {
        SetControlStyle<DecoratedBorder>(DefaultStyleKeyProperty);
    }
}