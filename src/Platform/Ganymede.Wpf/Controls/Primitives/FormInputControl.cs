using System.Windows;
using System.Windows.Controls;
using static TheXDS.Ganymede.Helpers.DependencyObjectHelpers;

namespace TheXDS.Ganymede.Controls.Primitives;

/// <summary>
/// Base class for all controls that include form-oriented properties, like labels and icons.
/// </summary>
public abstract class FormInputControl : Control
{
    /// <summary>
    /// Identifies the <see cref="Label"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty LabelProperty;

    /// <summary>
    /// Identifies the <see cref="Icon"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IconProperty;

    static FormInputControl()
    {
        IconProperty = NewDp<string, FormInputControl>(nameof(Icon), "🖋️");
        LabelProperty = NewDp<string, FormInputControl>(nameof(Label));
    }

    /// <summary>
    /// Overrides the default Icon to be presented on the input control.
    /// </summary>
    /// <typeparam name="T">
    /// Actual type of the control where the override will occur.
    /// </typeparam>
    /// <param name="newIcon">Icon to set as the default control icon.</param>
    protected static void OverrideDefaultIcon<T>(string newIcon) where T : FormInputControl
    {
        IconProperty.OverrideMetadata(typeof(T), new PropertyMetadata(newIcon));
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

    /// <summary>
    /// Gets or sets the label to be displayed on the text box.
    /// </summary>
    public string? Label
    {
        get => (string?)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }
}
