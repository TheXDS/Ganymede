using System.Windows;
using System.Windows.Controls;
using TheXDS.Ganymede.Controls;
using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Ganymede.CrudGen.Mappings.Base;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Helpers;

namespace TheXDS.Ganymede.CrudGen.Mappings;

/// <summary>
/// Maps password storage fields to a <see cref="PasswordBox"/>.
/// </summary>
public class PasswordMapping : CrudMappingBase, ICrudMapping
{
    /// <inheritdoc/>
    public bool CanMap(IPropertyDescription description)
    {
        return description is IPasswordPropertyDescription;
    }

    /// <inheritdoc/>
    public FrameworkElement CreateControl(IPropertyDescription description)
    {
        var c = new PasswordBox();
        ExtraProps.SetLabel(c, description.Label);
        c.PasswordChanged += (s, e) => {
            if (c.DataContext is CrudEditorViewModel vm)
            {
                description.Property.SetValue(vm.Entity, PasswordStorage.CreateHash(((IPasswordPropertyDescription)description).Algorithm, c.SecurePassword));
            }
        };
        return c;
    }
}
