using System.Windows;
using System.Windows.Controls;
using TheXDS.Ganymede.Controls;
using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Ganymede.CrudGen.Mappings.Base;

namespace TheXDS.Ganymede.CrudGen.Mappings;

/// <summary>
/// Implements a <see cref="ICrudMapping"/> that generates simple
/// <see cref="TextBox"/> controls for large blocks of text.
/// </summary>
public class BigTextBoxMapping : SimpleTextBoxMapping
{
    /// <inheritdoc/>
    public override bool CanMap(IPropertyDescription description)
    {
        return description is ITextPropertyDescription { Kind: TextKind.Paragraph };
    }

    /// <inheritdoc/>
    protected override void ConfigureControl(TextBoxEx control, IPropertyDescription description)
    {
        base.ConfigureControl(control, description);
        if (Application.Current.FindResource("BigTextBoxEx") is Style st) control.Style = st;
        if (((ITextPropertyDescription)description).WidgetSize != WidgetSize.Flex)
        {
            control.Height = ((ITextPropertyDescription)description).WidgetSize switch
            {
                WidgetSize.Small => 58,
                WidgetSize.Medium => 90,
                WidgetSize.Large => 200,
                WidgetSize.Huge => 500,
                _ => 0
            };
        }
    }
}