using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TheXDS.Ganymede.Controls;
using TheXDS.Ganymede.Controls.Primitives;
using TheXDS.Ganymede.Resources.DialogTemplates;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.ValueConverters.Base;

namespace TheXDS.Ganymede.ValueConverters;

public sealed partial class DialogVisualConverter : IOneWayValueConverter<DialogViewModel, FrameworkElement?>
{
    private static partial void ManualRegistrations()
    {
        //Register well-known input dialog builders
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

    /// <inheritdoc/>
    public FrameworkElement? Convert(DialogViewModel value, object? parameter, CultureInfo? culture)
    {
        var builder = Builders.FirstOrDefault(p => p.CanBuild(value));
        var result = builder?.Build(value);
        if (result is not null)
        {
            result.Loaded += DialogContent_Loaded;
        }
        return result;
    }

    private void DialogContent_Loaded(object sender, RoutedEventArgs e)
    {
        FrameworkElement control = (FrameworkElement)sender;
        control.Focus();
        Keyboard.Focus(control);
        control.Loaded -= DialogContent_Loaded;
    }

    private static void RegisterNumericTemplateBuilder<TValue, TControl>()
        where TValue : unmanaged, IComparable<TValue>
        where TControl : NumericInputControl<TValue>, new()
    {
        RegisterTemplateBuilder<NumericInputDialogTemplateBuilder<TValue, TControl>>();
        RegisterTemplateBuilder<NumericRangeInputDialogTemplateBuilder<TValue, TControl>> ();
    }
}
