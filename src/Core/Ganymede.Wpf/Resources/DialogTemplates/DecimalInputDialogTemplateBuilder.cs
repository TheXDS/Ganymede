using System.Windows.Data;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a template builder for <see cref="bool"/> values.
/// </summary>
public class DecimalInputDialogTemplateBuilder : NumericInputDialogTemplateBuilder<decimal>
{
    /// <inheritdoc/>
    protected override void ConfigureValueBinding(Binding binding)
    {
        binding.StringFormat = "{0:0.00}";
    }
}