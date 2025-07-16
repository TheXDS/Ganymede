using TheXDS.Ganymede.Resources.DialogTemplates;
using TheXDS.Ganymede.ValueConverters;

namespace TheXDS.Ganymede.Helpers;

public static partial class GanymedeInitializer
{
    private static partial void InitializeDialogVisualConverter()
    {
        RegisterNumericTemplateBuilder<byte>();
        RegisterNumericTemplateBuilder<sbyte>();
        RegisterNumericTemplateBuilder<short>();
        RegisterNumericTemplateBuilder<ushort>();
        RegisterNumericTemplateBuilder<int>();
        RegisterNumericTemplateBuilder<uint>();
        RegisterNumericTemplateBuilder<long>();
        RegisterNumericTemplateBuilder<ulong>();
        RegisterNumericTemplateBuilder<float>();
        RegisterNumericTemplateBuilder<double>();
        RegisterNumericTemplateBuilder<decimal>();
    }

    private static void RegisterNumericTemplateBuilder<TValue>() where TValue : unmanaged, IComparable<TValue>
    {
        DialogVisualConverter.RegisterTemplateBuilder<NumericInputDialogTemplateBuilder<TValue>>();
        DialogVisualConverter.RegisterTemplateBuilder<NumericRangeInputDialogTemplateBuilder<TValue>>();
    }
}
