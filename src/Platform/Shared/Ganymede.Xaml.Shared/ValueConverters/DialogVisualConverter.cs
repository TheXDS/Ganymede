using TheXDS.Ganymede.Resources.DialogTemplates;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Helpers;

namespace TheXDS.Ganymede.ValueConverters;

/// <summary>
/// Generates Visual elements that are part of dialogs in Ganymede.
/// </summary>
public sealed partial class DialogVisualConverter
{
    private static readonly List<IDialogTemplateBuilder> Builders = [];
    private static readonly CustomDialogTemplateBuilder _customBuilder = CustomDialogTemplateBuilder.Create();

    private static IDialogTemplateBuilder? GetBuilder(DialogViewModel value)
    {
        return Builders.Concat([_customBuilder]).FirstOrDefault(p => p.CanBuild(value));
    }

    static DialogVisualConverter()
    {
        Builders.AddRange(ReflectionHelpers.FindAllObjects<IDialogTemplateBuilder>());
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
}