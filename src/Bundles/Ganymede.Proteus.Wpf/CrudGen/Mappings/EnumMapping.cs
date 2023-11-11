using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Ganymede.CrudGen.Mappings.Base;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.Ganymede.CrudGen.Mappings;

/// <summary>
/// Maps <see cref="Enum"/> properties for non-flag enums.
/// </summary>
public class EnumMapping : CrudMappingBase, ICrudMapping
{
    /// <inheritdoc/>
    public bool CanMap(IPropertyDescription description)
    {
        return description.Property.PropertyType.IsEnum
            && description.GetStructValue<bool>(nameof(IEnumPropertyDescription.Flags)) != true;
    }

    /// <inheritdoc/>
    public FrameworkElement CreateControl(IPropertyDescription description)
    {
        var c = new ComboBox
        {
            ItemsSource = description.Property.PropertyType.AsNamedEnum(),
            SelectedValuePath = "Value",
            DisplayMemberPath = "Name"
        };
        c.SetBinding(Selector.SelectedValueProperty, description.GetBindingString());
        return c;
    }
}
