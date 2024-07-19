using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a template builder for numerical <see langword="struct"/> values
/// of type <typeparam name="T"/>.
/// </summary>
public class NumericInputDialogTemplateBuilder<T> : IDialogTemplateBuilder<InputDialogViewModel<T>>
    where T : struct, IComparable<T>
{
    /// <inheritdoc/>
    public StyledElement? Build(InputDialogViewModel<T> viewModel)
    {
        return new NumericUpDown
        {
            [!NumericUpDown.MinimumProperty] = new Binding(nameof(viewModel.Minimum)),
            [!NumericUpDown.MaximumProperty] = new Binding(nameof(viewModel.Maximum)),
            [!NumericUpDown.ValueProperty] = new Binding(nameof(viewModel.Value)),
        };
    }
}