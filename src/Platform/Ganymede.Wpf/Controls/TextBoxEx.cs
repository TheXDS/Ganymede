using System.Windows;
using System.Windows.Controls;
using static TheXDS.MCART.Helpers.DependencyObjectHelpers;

namespace TheXDS.Ganymede.Controls;

/// <summary>
/// Extends the <see cref="TextBox"/> class to provide of enhanced styling and extra properties.
/// </summary>
public class TextBoxEx : TextBox
{
    /// <summary>
    /// Identifies the <see cref="Label"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty LabelProperty;

    /// <summary>
    /// Identifies the <see cref="Icon"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IconProperty;

    /// <summary>
    /// Initializes the <see cref="TextBoxEx"/> class.
    /// </summary>
    static TextBoxEx()
    {
        SetControlStyle<TextBoxEx>(DefaultStyleKeyProperty);
        LabelProperty = NewDp<string, TextBoxEx>(nameof(Label));
        IconProperty = NewDp<string, TextBoxEx>(nameof(Icon),"🖋️");
    }

    /// <summary>
    /// Gets or sets the label to be displayed on the text box.
    /// </summary>
    public string? Label
    {
        get => (string?)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    /// <summary>
    /// Gets or sets a glyph to be displayed as an iconic decoration inside the
    /// text box.
    /// </summary>
    public string Icon
    {
        get => (string)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }
}
