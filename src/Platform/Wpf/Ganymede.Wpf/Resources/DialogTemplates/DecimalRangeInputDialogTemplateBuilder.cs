using System.Windows.Data;
using TheXDS.Ganymede.Controls;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a template builder for ranges of <see cref="decimal"/>.
/// </summary>
public class DecimalRangeInputDialogTemplateBuilder : NumericRangeInputDialogTemplateBuilder<decimal, DecimalTextBox>
{
    /// <inheritdoc/>
    protected override void ConfigureValueBinding(Binding binding)
    {
        base.ConfigureValueBinding(binding);
        binding.StringFormat = "{0:0.00}";
    }
}