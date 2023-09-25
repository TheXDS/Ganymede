using Avalonia.Controls;
using Avalonia.Controls.Templates;
using TheXDS.Ganymede.Resources.Templates.Dialogs;
using TheXDS.Ganymede.ViewModels.Dialogs;

namespace TheXDS.Ganymede.Component.Templating;

public static class DialogTemplateProvider
{
    private static readonly List<IDialogTemplateBuilder> Builders = new();

    public static void RegisterTemplateBuilder<T>() where T : IDialogTemplateBuilder, new()
    {
        Builders.Add(new T());
    }
    
    public static FuncDataTemplate<DialogViewModelBase> DialogTemplateBuilder { get; } = new(Build);

    static DialogTemplateProvider()
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

    private static bool IsValidType(Type p)
    {
        return p is { IsAbstract: false, IsInterface: false, ContainsGenericParameters: false } 
        && typeof(IDialogTemplateBuilder).IsAssignableFrom(p) 
        && p.GetConstructor(Type.EmptyTypes) is not null;
    }
    
    private static Control Build(DialogViewModelBase vm, INameScope _)
    {
        return Builders.FirstOrDefault(p => p.CanBuild(vm))?.Build(vm)!;
    }

    private static void RegisterNumericTemplateBuilder<T>() where T: struct, IComparable<T>
    {
        RegisterTemplateBuilder<NumericInputDialogTemplateBuilder<T>>();
        RegisterTemplateBuilder<NumericRangeInputDialogTemplateBuilder<T>>();
    }
}