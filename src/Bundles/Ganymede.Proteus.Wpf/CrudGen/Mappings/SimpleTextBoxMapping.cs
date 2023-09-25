using System.Windows.Controls;
using TheXDS.Ganymede.Controls;
using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Ganymede.CrudGen.Mappings.Base;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.CrudGen.Mappings;

/// <summary>
/// Implements a <see cref="ICrudMapping"/> that generates simple
/// <see cref="TextBox"/> controls for regular string properties.
/// </summary>
public class SimpleTextBoxMapping : SimpleCrudMappingBase<TextBoxEx>, ICrudMapping
{
    /// <inheritdoc/>
    public override bool CanMap(IPropertyDescription description)
    {
        return description switch
        {
            ITextPropertyDescription d => d.Kind == TextKind.Generic,
            IPropertyDescription<string> => true,
            IPropertyDescription => description.Property.PropertyType == typeof(string),
            _ => false,
        };
    }

    /// <inheritdoc/>
    protected override void ConfigureControl(TextBoxEx control, IPropertyDescription description)
    {
        control.Label = description.Label;
        control.SetBinding(TextBox.TextProperty, $"{nameof(CrudViewModelBase.Entity)}.{description.Property.Name}");
        SetIf<IPropertyDescription<string>, string>(description, p => p.DefaultValue, p => control.SetValue(TextBox.TextProperty, p));
        SetIf<ITextPropertyDescription, int>(description, p => p.MaxLength, p => control.MaxLength = p);
    }
}
