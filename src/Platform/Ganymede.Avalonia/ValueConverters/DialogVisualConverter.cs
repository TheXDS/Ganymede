using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using TheXDS.Ganymede.Component;
using TheXDS.Ganymede.Resources.DialogTemplates;
using TheXDS.Ganymede.Types.Base;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.ValueConverters.Base;

namespace TheXDS.Ganymede.ValueConverters;

public sealed partial class DialogVisualConverter : IOneWayValueConverter<DialogViewModel, StyledElement?>, IVisualResolver<StyledElement>
{
    //private static partial void ManualRegistrations()
    //{
    //    //Register well-known input dialog builders
    //    RegisterNumericTemplateBuilder<byte>();
    //    RegisterNumericTemplateBuilder<sbyte>();
    //    RegisterNumericTemplateBuilder<short>();
    //    RegisterNumericTemplateBuilder<ushort>();
    //    RegisterNumericTemplateBuilder<int>();
    //    RegisterNumericTemplateBuilder<uint>();
    //    RegisterNumericTemplateBuilder<long>();
    //    RegisterNumericTemplateBuilder<ulong>();
    //    RegisterNumericTemplateBuilder<float>();
    //    RegisterNumericTemplateBuilder<double>();
    //}

    /// <inheritdoc/>
    public StyledElement? Convert(DialogViewModel value, object? parameter, CultureInfo? culture)
    {
        var builder = Builders.FirstOrDefault(p => p.CanBuild(value));
        var result = builder?.Build(value);
        if (result is not null)
        {
            result.Initialized += DialogContent_Loaded;
        }
        return result;
    }

    private void DialogContent_Loaded(object? sender, EventArgs e)
    {
        if (sender is Control c)
        {
            c.Focus();
            c.Initialized -= DialogContent_Loaded;
        }
    }
    
    //private static void RegisterNumericTemplateBuilder<TValue>()
    //    where TValue : unmanaged, IComparable<TValue>
    //{
    //    RegisterTemplateBuilder<NumericInputDialogTemplateBuilder<TValue>>();
    //    RegisterTemplateBuilder<NumericRangeInputDialogTemplateBuilder<TValue>> ();
    //}

    StyledElement? IVisualResolver<StyledElement>.Resolve(IViewModel viewModel)
    {
        return viewModel is DialogViewModel vm
            ? Convert(vm, null, CultureInfo.InvariantCulture)
            : null;
    }
}
