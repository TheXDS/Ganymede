using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Resources;
using TheXDS.Ganymede.Services;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;
using St = TheXDS.Ganymede.Resources.Strings.Common;

namespace TheXDS.Ganymede.Types.Extensions;

/// <summary>
/// Includes a set of extensions for any implementors of the
/// <see cref="IDialogService"/> interface.
/// </summary>
public static class DialogServiceExtensions
{
    /// <summary>
    /// Displays a simple message dialog from a pre-configured visual template.
    /// </summary>
    /// <param name="svc">
    /// Dialog service onto which to invoke the dialog.
    /// </param>
    /// <param name="template">
    /// Template that describes the visual properties of the dialog.
    /// </param>
    /// <param name="interactions">
    /// Array of possible interactions to execute.
    /// </param>
    /// <returns>
    /// A task that can be used to await the completion of the dialog.
    /// </returns>
    public static async Task Show(this IDialogService svc, DialogTemplate template, (string Name, Action Action)[] interactions)
    {
        (await Show<Action>(svc, template, interactions)).Invoke();
    }

    /// <summary>
    /// Displays a simple message dialog from a pre-configured visual template.
    /// </summary>
    /// <param name="svc">
    /// Dialog service onto which to invoke the dialog.
    /// </param>
    /// <param name="template">
    /// Template that describes the visual properties of the dialog.
    /// </param>
    /// <param name="values">
    /// Array of values that can be selected on the dialog.
    /// </param>
    /// <returns>
    /// A task that can be used to await the completion of the dialog.
    /// </returns>
    public static Task<T> Show<T>(this IDialogService svc, DialogTemplate template, (string, T)[] values)
    {
        return svc.Show(template, values.Select(p => (NamedObject<T>)p).ToArray());
    }

    /// <summary>
    /// Displays a simple message dialog from a pre-configured visual template.
    /// </summary>
    /// <param name="svc">
    /// Dialog service onto which to invoke the dialog.
    /// </param>
    /// <param name="template">
    /// Template that describes the visual properties of the dialog.
    /// </param>
    /// <param name="values">
    /// Array of values that can be selected on the dialog.
    /// </param>
    /// <returns>
    /// A task that can be used to await the completion of the dialog.
    /// </returns>
    public static Task<T> Show<T>(this IDialogService svc, DialogTemplate template, (T, string)[] values)
    {
        return svc.Show(template, values.Select(p => (NamedObject<T>)p).ToArray());
    }

    /// <summary>
    /// Displays a dialog that prompts the user to select a value from a
    /// <see cref="Enum"/> definition.
    /// </summary>
    /// <typeparam name="T">
    /// Type of <see cref="Enum"/> values to present to the user.
    /// </typeparam>
    /// <param name="svc">
    /// Dialog service onto which to invoke the dialog.
    /// </param>
    /// <param name="title">Title of the question.</param>
    /// <param name="prompt">Prompt to ask.</param>
    /// <returns>
    /// A task that can be used to await the completion of the dialog, which
    /// includes the selected <see cref="Enum"/> value.
    /// </returns>
    public static async Task<T> GetOption<T>(this IDialogService svc, string? title, string prompt) where T : Enum
    {
        return (T)await svc.Show(CommonDialogTemplates.Question with { Title = title, Text = prompt }, typeof(T).ToNamedEnum().ToArray());
    }

    /// <summary>
    /// Gets the index of a selected option by the user.
    /// </summary>
    /// <param name="svc">
    /// Dialog service onto which to invoke the dialog.
    /// </param>
    /// <param name="title">Title of the question.</param>
    /// <param name="prompt">Prompt to ask.</param>
    /// <param name="options">
    /// Collection of available options to choose from.
    /// </param>
    /// <returns>
    /// The index of the selected option in the <paramref name="options"/>
    /// array.
    /// </returns>
    public static Task<int> GetOption(this IDialogService svc, string? title, string prompt, params string[] options)
    {
        return svc.Show(CommonDialogTemplates.Input with { Title = title, Text = prompt }, options.WithIndex().ToArray());
    }

    /// <summary>
    /// Gets the value associated with an option selectev by the user using
    /// direct buttons.
    /// </summary>
    /// <param name="svc">
    /// Dialog service onto which to invoke the dialog.
    /// </param>
    /// <param name="prompt">Prompt to ask.</param>
    /// <param name="options">
    /// Collection of available options to choose from.
    /// </param>
    /// <returns>
    /// The selected option value in the <paramref name="options"/> array.
    /// </returns>
    public static Task<int> GetOption(this IDialogService svc, string prompt, params string[] options)
    {
        return svc.Show(CommonDialogTemplates.Input with { Text = prompt }, options.WithIndex().ToArray());
    }

    /// <summary>
    /// Alternate value selection method that allows the user to select a value
    /// from a larger pool of items.
    /// </summary>
    /// <param name="svc">
    /// Dialog service onto which to invoke the dialog.
    /// </param>
    /// <param name="template">
    /// Template that describes the visual properties of the dialog.
    /// </param>
    /// <param name="options">
    /// Collection of available options to choose from.
    /// </param>
    /// <returns>
    /// The index of the selected option in the <paramref name="options"/>
    /// array.
    /// </returns>
    public static Task<DialogResult<int>> SelectOption(this IDialogService svc, DialogTemplate template, params string[] options)
    {
        return svc.SelectOption(template, options.WithIndex().Select(p => (NamedObject<int>)p).ToArray());
    }

    /// <summary>
    /// Gets a path to a file to be opened.
    /// </summary>
    /// <param name="svc">
    /// Dialog service onto which to invoke the dialog.
    /// </param>
    /// <returns>
    /// A task that can be used to await the completion of the dialog, which
    /// includes the dialog result with the file path.
    /// </returns>
    public static Task<DialogResult<string?>> GetFileOpenPath(this IDialogService svc)
    {
        return GetFileOpenPath(svc, [FileFilterItem.AllFiles]);
    }

    /// <summary>
    /// Gets a path to a file to be opened.
    /// </summary>
    /// <param name="svc">
    /// Dialog service onto which to invoke the dialog.
    /// </param>
    /// <param name="fileFilters">
    /// File filters to include in the file pen dialog.
    /// </param>
    /// <param name="defaultPath">Default path to be selected.</param>
    /// <returns>
    /// A task that can be used to await the completion of the dialog, which
    /// includes the dialog result with the file path.
    /// </returns>
    public static Task<DialogResult<string?>> GetFileOpenPath(this IDialogService svc, IEnumerable<FileFilterItem> fileFilters, string? defaultPath = null)
    {
        return svc.GetFileOpenPath(CommonDialogTemplates.FileOpen with { Title = St.Open }, fileFilters, defaultPath);
    }

    /// <summary>
    /// Gets a value from the user.
    /// </summary>
    /// <typeparam name="T">Type of value to get.</typeparam>
    /// <param name="svc">
    /// Dialog service onto which to invoke the dialog.
    /// </param>
    /// <param name="template">
    /// Template that describes the visual properties of the dialog.
    /// </param>
    /// <param name="defaultValue">
    /// Default value to be initially selected.
    /// </param>
    /// <returns></returns>
    public static Task<DialogResult<T>> GetInputValue<T>(this IDialogService svc, DialogTemplate template, T defaultValue = default) where T : struct, IComparable<T>
    {
        return svc.GetInputValue(template, null, null, defaultValue);
    }
}
