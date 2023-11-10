using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Ganymede.CrudGen.Mappings.Base;

namespace TheXDS.Ganymede.CrudGen.Mappings;

/// <summary>
/// Implements a <see cref="ICrudMapping"/> that generates a read-only control
/// for displaying the value of a property.
/// </summary>
public class ReadOnlyMapping : ICrudMapping
{
    /// <inheritdoc/>
    public bool CanMap(IPropertyDescription description) => description.ReadOnly;

    /// <inheritdoc/>
    public FrameworkElement CreateControl(IPropertyDescription description)
    {
        var i = new Run();
        i.SetBinding(Run.TextProperty, new Binding(description.GetBindingString()) { Mode = BindingMode.OneWay });
        return new DockPanel
        {
            Margin = new Thickness(5),
            Children =
            {
                new TextBlock() { Text = description.Icon ?? "✍️", Foreground = Brushes.SkyBlue },
                new TextBlock() { Text = description.Label, FontWeight = FontWeights.Bold, Margin = new Thickness(5,0,5,0) },
                new TextBlock() { Inlines = { i }, TextWrapping = TextWrapping.Wrap }
            }
        };
    }
}
