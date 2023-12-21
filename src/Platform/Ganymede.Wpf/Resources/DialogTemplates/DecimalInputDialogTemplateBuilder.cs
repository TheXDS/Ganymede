using System.Windows.Data;
using TheXDS.Ganymede.Controls;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a template builder for <see cref="bool"/> values.
/// </summary>
public class DecimalInputDialogTemplateBuilder : NumericInputDialogTemplateBuilder<decimal, DecimalTextBox>
{
    /// <inheritdoc/>
    protected override void ConfigureValueBinding(Binding binding)
    {
        binding.StringFormat = "{0:0.00}";
    }
}