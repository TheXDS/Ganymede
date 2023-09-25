using System.Windows.Controls;
using System;
using System.Windows;
using System.Windows.Data;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a template builder for ranges of <see cref="decimal"/>.
/// </summary>
public class DecimalRangeInputDialogTemplateBuilder : NumericRangeInputDialogTemplateBuilder<decimal>
{
    /// <inheritdoc/>
    protected override void ConfigureValueBinding(Binding binding)
    {
        binding.StringFormat = "{0:0.00}";
    }
}