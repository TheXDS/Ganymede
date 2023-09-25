using System.Reflection;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using TheXDS.Ganymede.CrudGen.Descriptions;

namespace TheXDS.Ganymede.CrudGen.Mappings;

/// <summary>
/// Maps <see cref="bool"/> properties to a <see cref="CheckBox"/>.
/// </summary>
public class BoolCheckBoxMapping : SimpleCrudMapping<bool, CheckBox>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BoolCheckBoxMapping"/>
    /// class.
    /// </summary>
    public BoolCheckBoxMapping() : base(ToggleButton.IsCheckedProperty)
    {
    }

    /// <inheritdoc/>
    protected override void ConfigureControl(CheckBox control, IPropertyDescription description)
    {
        control.Content = description.Label;
    }
}
