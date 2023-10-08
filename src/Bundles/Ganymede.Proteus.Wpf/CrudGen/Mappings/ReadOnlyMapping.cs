using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Ganymede.CrudGen.Mappings.Base;
using TheXDS.Ganymede.ViewModels;

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
        i.SetBinding(Run.TextProperty, new Binding($"{nameof(CrudViewModelBase.Entity)}.{description.Property.Name}") { Mode = BindingMode.OneWay });
        return new TextBlock()
        {
            Inlines =
            {
                new Run($"{description.Label}: "),
                i
            }
        };
    }
}
