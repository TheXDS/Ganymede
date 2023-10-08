using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
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
        RegisterNumericTemplateBuilder<byte>();
        RegisterNumericTemplateBuilder<sbyte>();
        RegisterNumericTemplateBuilder<char>();
        RegisterNumericTemplateBuilder<short>();
        RegisterNumericTemplateBuilder<ushort>();
        RegisterNumericTemplateBuilder<int>();
        RegisterNumericTemplateBuilder<uint>();
        RegisterNumericTemplateBuilder<long>();
        RegisterNumericTemplateBuilder<ulong>();
        RegisterNumericTemplateBuilder<float>();
        RegisterNumericTemplateBuilder<double>();
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
    /// <typeparam name="T"></typeparam>
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

    private static void RegisterNumericTemplateBuilder<T>() where T : struct, IComparable<T>
    {
        RegisterTemplateBuilder<NumericInputDialogTemplateBuilder<T>>();
        RegisterTemplateBuilder<NumericRangeInputDialogTemplateBuilder<T>>();
    }
}

