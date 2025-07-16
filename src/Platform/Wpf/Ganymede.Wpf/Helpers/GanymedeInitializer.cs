using TheXDS.Ganymede.Controls;
using TheXDS.Ganymede.Controls.Primitives;
using TheXDS.Ganymede.Resources.DialogTemplates;
using TheXDS.Ganymede.ValueConverters;

namespace TheXDS.Ganymede.Helpers;

/// <summary>
/// Initializes the Ganymede library.
/// </summary>
public static partial class GanymedeInitializer
{
    private static partial void InitializeDialogVisualConverter()
    {
        RegisterNumericTemplateBuilder<byte, UInt8TextBox>();
        RegisterNumericTemplateBuilder<sbyte, Int8TextBox>();
        RegisterNumericTemplateBuilder<short, Int16TextBox>();
        RegisterNumericTemplateBuilder<ushort, UInt16TextBox>();
        RegisterNumericTemplateBuilder<int, Int32TextBox>();
        RegisterNumericTemplateBuilder<uint, UInt32TextBox>();
        RegisterNumericTemplateBuilder<long, Int64TextBox>();
        RegisterNumericTemplateBuilder<ulong, UInt64TextBox>();
        RegisterNumericTemplateBuilder<float, FloatTextBox>();
        RegisterNumericTemplateBuilder<double, DoubleTextBox>();
    }

    private static void RegisterNumericTemplateBuilder<TValue, TControl>()
        where TValue : unmanaged, IComparable<TValue>
        where TControl : NumericInputControl<TValue>, new()
    {
        DialogVisualConverter.RegisterTemplateBuilder<NumericInputDialogTemplateBuilder<TValue, TControl>>();
        DialogVisualConverter.RegisterTemplateBuilder<NumericRangeInputDialogTemplateBuilder<TValue, TControl>>();
    }
}
