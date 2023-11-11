using System;
using System.Windows;
using System.Windows.Controls;
using TheXDS.Ganymede.Controls;
using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Ganymede.CrudGen.Mappings.Base;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Types.Extensions;
using TheXDS.Triton.Models.Base;

namespace TheXDS.Ganymede.CrudGen.Mappings;

/// <summary>
/// Maps password text properties to a <see cref="PasswordBox"/>.
/// </summary>
/// <remarks>
/// This mapping will write the clear-text password back into the entity. For a
/// proper way to store passwords, please change your model property to be of
/// type <c><see cref="byte"/>[]</c> and use a recommended hashing algorithm.
/// <br/><br/>
/// <b>This mapping is intended to simply obscure text from the UI. Any
/// information put into the PasswordBox will be in clear-text!</b>
/// </remarks>
public class PasswordTextMapping : CrudMappingBase, ICrudMapping
{
    /// <inheritdoc/>
    public bool CanMap(IPropertyDescription description)
    {
        return description is ITextPropertyDescription { Kind: TextKind.Password };
    }

    /// <inheritdoc/>
    public FrameworkElement CreateControl(IPropertyDescription description)
    {
        var c = new PasswordBox();
        ExtraProps.SetLabel(c, description.Label);
        c.PasswordChanged += (s, e) => {
            if (c.DataContext is CrudEditorViewModel vm)
            {
                description.Property.SetValue(vm.Entity, c.Password);
            }
        };
        return c;
    }
}

/// <summary>
/// Maps <see cref="Enum"/> properties for non-flag enums.
/// </summary>
public class FlagEnumMapping : CrudMappingBase, ICrudMapping
{
    bool ICrudMapping.MustSetValueManually => false;

    /// <inheritdoc/>
    public bool CanMap(IPropertyDescription description)
    {
        return description.Property.PropertyType.IsEnum
            && ((description is IEnumPropertyDescription e && e.Flags)
            || description.GetStructValue<bool>(nameof(IEnumPropertyDescription.Flags)) == true);
    }

    /// <inheritdoc/>
    public FrameworkElement CreateControl(IPropertyDescription description)
    {
        var pnl = new WrapPanel
        {
            Orientation = Orientation.Vertical,
            MaxHeight = 150
        };
        var groupBox = new GroupBox
        {
            Header = $"{description.Icon} {description.Label}",
            Content = new ScrollViewer
            {
                Content = pnl,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto
            }
        };
        foreach (var j in description.Property.PropertyType.AsNamedEnum())
        {
            if (j.Value.ToUnderlyingType().Equals(0)) continue;
            var chk = new CheckBox { Content = j.Name, Tag = j.Value };
            pnl.Children.Add(chk);
        }
        return groupBox;
    }

    void ICrudMapping.SetControlValue(FrameworkElement control, Model entity, IPropertyDescription description)
    {

    }
}