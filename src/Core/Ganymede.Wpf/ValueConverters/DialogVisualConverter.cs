using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using TheXDS.Ganymede.Controls;
using TheXDS.Ganymede.Controls.Primitives;
using TheXDS.Ganymede.Resources.DialogTemplates;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.ValueConverters.Base;

namespace TheXDS.Ganymede.ValueConverters;

/// <summary>
/// Generates Visual elements that are part of dialogs in Ganymede.
/// </summary>
public sealed class DialogVisualConverter : IOneWayValueConverter<DialogViewModel, FrameworkElement?>
{
    private static readonly List<IDialogTemplateBuilder> Builders = new();

    static DialogVisualConverter()
    {
        // Automatically add ready-to-use standard dialog builders 
        Builders.AddRange(AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(p => p.GetTypes())
            .Where(IsValidType)
            .Select(Activator.CreateInstance)
            .Cast<IDialogTemplateBuilder>());

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
        return builder?.Build(value)!;
    }

    /// <summary>
    /// Registers a new template builder available to use when creating input
    /// dialogs.
    /// </summary>
    /// <typeparam name="T">
    /// Type of dialog template builder to register.
    /// </typeparam>
    public static void RegisterTemplateBuilder<T>() where T : IDialogTemplateBuilder, new()
    {
        Builders.Add(new T());
    }

    private static bool IsValidType(Type p)
    {
        return p is { IsAbstract: false, IsInterface: false, ContainsGenericParameters: false }
        && typeof(IDialogTemplateBuilder).IsAssignableFrom(p)
        && p.GetConstructor(Type.EmptyTypes) is not null;
    }

    private static void RegisterNumericTemplateBuilder<TValue, TControl>()
        where TValue : unmanaged, IComparable<TValue>
        where TControl : NumericInputControl<TValue>, new()
    {
        RegisterTemplateBuilder<NumericInputDialogTemplateBuilder<TValue, TControl>>();
        RegisterTemplateBuilder<NumericRangeInputDialogTemplateBuilder<TValue, TControl>> ();
    }
}

