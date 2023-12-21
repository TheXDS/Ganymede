using System.Windows;

namespace TheXDS.Ganymede.Controls;

/// <summary>
/// Includes a set of attached properties that can be used to customize the
/// style and presentation of controls where Ganymede does not offer an
/// extended control.
/// </summary>
public static class ExtraProps
{
    /// <summary>
    /// Identifies the "Icon" Attached property.
    /// </summary>
    public static readonly DependencyProperty IconProperty = DependencyProperty.RegisterAttached("Icon", typeof(string), typeof(ExtraProps), new PropertyMetadata(string.Empty));

    /// <summary>
    /// Identifies the "Label" Attached property.
    /// </summary>
    public static readonly DependencyProperty LabelProperty = DependencyProperty.RegisterAttached("Label", typeof(string), typeof(ExtraProps), new PropertyMetadata(string.Empty));

    /// <summary>
    /// Gets the value of the <see cref="IconProperty"/> attached property.
    /// </summary>
    /// <param name="element">Element to get the value from.</param>
    /// <returns>
    /// The value of the <see cref="IconProperty"/> attached property.
    /// </returns>
    public static string GetIcon(UIElement element)
    {
        return (string)element.GetValue(IconProperty);
    }

    /// <summary>
    /// Sets the value of the <see cref="IconProperty"/> attached property.
    /// </summary>
    /// <param name="element">Element to set the value onto.</param>
    /// <param name="value">New value of the attached property.</param>
    public static void SetIcon(UIElement element, string value)
    {
        element.SetValue(IconProperty, value);
    }

    /// <summary>
    /// Gets the value of the <see cref="LabelProperty"/> attached property.
    /// </summary>
    /// <param name="element">Element to get the value from.</param>
    /// <returns>
    /// The value of the <see cref="LabelProperty"/> attached property.
    /// </returns>
    public static string GetLabel(UIElement element)
    {
        return (string)element.GetValue(LabelProperty);
    }

    /// <summary>
    /// Sets the value of the <see cref="LabelProperty"/> attached property.
    /// </summary>
    /// <param name="element">Element to set the value onto.</param>
    /// <param name="value">New value of the attached property.</param>
    public static void SetLabel(UIElement element, string value)
    {
        element.SetValue(LabelProperty, value);
    }
}