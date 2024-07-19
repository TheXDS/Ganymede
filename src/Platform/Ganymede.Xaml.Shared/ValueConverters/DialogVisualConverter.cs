using System.Collections.Generic;
using TheXDS.Ganymede.Resources.DialogTemplates;
using TheXDS.Ganymede.Types;
using TheXDS.MCART.Helpers;

namespace TheXDS.Ganymede.ValueConverters;

/// <summary>
/// Generates Visual elements that are part of dialogs in Ganymede.
/// </summary>
public sealed partial class DialogVisualConverter
{
    private static readonly List<IDialogTemplateBuilder> Builders = [];

    static DialogVisualConverter()
    {
        // Automatically add ready-to-use standard dialog builders 
        Builders.AddRange(ReflectionHelpers.FindAllObjects<IDialogTemplateBuilder>());

        // Register platform-specific and manually configured dialog builders
        ManualRegistrations();

        /* 
         * Register non-discoverable dialog builders (intended for manual
         * sorting of dialog builders)
         */
        Builders.Add(CustomDialogTemplateBuilder.Create());
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

    private static partial void ManualRegistrations();

}