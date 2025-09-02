using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using System.Collections.ObjectModel;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Types.Extensions;

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
            [!NumericUpDown.MinimumProperty] = new Binding(nameof(viewModel.Minimum)) { FallbackValue = (T)typeof(T).GetField("MinValue")!.GetValue(null)! },
            [!NumericUpDown.MaximumProperty] = new Binding(nameof(viewModel.Maximum)) { FallbackValue = (T)typeof(T).GetField("MaxValue")!.GetValue(null)! },
            [!NumericUpDown.ValueProperty] = new Binding(nameof(viewModel.Value)) { StringFormat = InferFormatString(typeof(T)) },
            FormatString = InferFormatString(typeof(T))
        };
    }

    private static readonly ReadOnlyDictionary<Type, string> NumericFormats = new(new Dictionary<Type, string>()
    {
        { typeof(decimal), "0.00" },
        { typeof(float), "0.#" },
        { typeof(double), "0.0" },
    });

    private static string InferFormatString(Type t)
    {
        return NumericFormats.TryGetValue(t, out var format) ? format : "0";
    }
}