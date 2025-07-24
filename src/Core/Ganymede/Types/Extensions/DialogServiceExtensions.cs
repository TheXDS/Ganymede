using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Resources;
using TheXDS.Ganymede.Services;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types;

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
    /// Gets a credential from the user.
    /// </summary>
    /// <param name="svc">
    /// Dialog service onto which to invoke the dialog.
    /// </param>
    /// <param name="defaultUser">Default username to present.</param>
    /// <param name="isUserEditable">
    /// <see langword="true"/> to indicate that the username field should be
    /// editable, <see langword="false"/> to set the username field as
    /// read-only.
    /// </param>
    /// <returns>
    /// A new <see cref="DialogResult{T}"/> with
    /// <see cref="DialogResult{T}.Success"/> set to <see langword="true"/> and
    /// <see cref="DialogResult{T}.Result"/> set to the credential entered by
    /// the user, or a new <see cref="DialogResult{T}"/> with
    /// <see cref="DialogResult{T}.Success"/> set to <see langword="false"/> and
    /// <see cref="DialogResult{T}.Result"/> set to <see langword="null"/> if
    /// the user cancels the input dialog.
    /// </returns>
    public static Task<DialogResult<Credential?>> GetCredential(this IDialogService svc, string? defaultUser = null, bool isUserEditable = true)
    {
        return svc.GetCredential(CommonDialogTemplates.Login, defaultUser, isUserEditable);
    }

    /// <summary>
    /// Gets a string from the user.
    /// </summary>
    /// <param name="svc">
    /// Dialog service onto which to invoke the dialog.
    /// </param>
    /// <param name="prompt">
    /// Simple string prompt to ask the user for input.
    /// </param>
    /// <param name="defaultValue">Default value.</param>
    /// <returns>
    /// A new <see cref="DialogResult{T}"/> with
    /// <see cref="DialogResult{T}.Success"/> set to <see langword="true"/> and
    /// <see cref="DialogResult{T}.Result"/> set to the value entered by the
    /// user, or a new <see cref="DialogResult{T}"/> with
    /// <see cref="DialogResult{T}.Success"/> set to <see langword="false"/> and
    /// <see cref="DialogResult{T}.Result"/> set to
    /// <paramref name="defaultValue"/> if the user cancels the input dialog.
    /// </returns>
    public static Task<DialogResult<string?>> GetInputText(this IDialogService svc, string prompt, string? defaultValue = null)
    {
        return svc.GetInputText(CommonDialogTemplates.Input with { Title = prompt }, defaultValue);
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
    public static Task<T> GetOption<T>(this IDialogService svc, string? title, string prompt) where T : struct, Enum
    {
        return svc.Show(CommonDialogTemplates.Question with { Title = title, Text = prompt }, NamedObject.FromEnum<T>().ToArray());
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
    /// <param name="defaultPath">Default path to be selected.</param>
    /// <returns>
    /// A task that can be used to await the completion of the dialog, which
    /// includes the dialog result with the file path.
    /// </returns>
    public static Task<DialogResult<string?>> GetFileOpenPath(this IDialogService svc, string? defaultPath = null)
    {
        return GetFileOpenPath(svc, [FileFilterItem.AllFiles], defaultPath);
    }

    /// <summary>
    /// Gets a path to a file to be opened.
    /// </summary>
    /// <param name="svc">
    /// Dialog service onto which to invoke the dialog.
    /// </param>
    /// <param name="message">
    /// Descrpitive message to be displayed on the dialog.
    /// </param>
    /// <param name="fileFilters">
    /// File filters to include in the file pen dialog.
    /// </param>
    /// <param name="defaultPath">Default path to be selected.</param>
    /// <returns>
    /// A task that can be used to await the completion of the dialog, which
    /// includes the dialog result with the file path.
    /// </returns>
    public static Task<DialogResult<string?>> GetFileOpenPath(this IDialogService svc, string? message, IEnumerable<FileFilterItem> fileFilters, string? defaultPath = null)
    {
        return message is null 
            ? GetFileOpenPath(svc, fileFilters, defaultPath)
            : svc.GetFileOpenPath(CommonDialogTemplates.FileOpen with { Text = message }, fileFilters, defaultPath);
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
        return svc.GetFileOpenPath(CommonDialogTemplates.FileOpen, fileFilters, defaultPath);
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

    /// <summary>
    /// Gets a value from the user.
    /// </summary>
    /// <typeparam name="T">Type of value to get.</typeparam>
    /// <param name="svc">
    /// Dialog service onto which to invoke the dialog.
    /// </param>
    /// <param name="title">Title of the dialog.</param>
    /// <param name="message">Message to be shown on the dialog.</param>
    /// <param name="defaultValue">
    /// Default value to be initially selected.
    /// </param>
    /// <returns></returns>
    public static Task<DialogResult<T>> GetInputValue<T>(this IDialogService svc, string? title, string message, T defaultValue = default) where T : struct, IComparable<T>
    {
        return svc.GetInputValue(CommonDialogTemplates.Input with { Title = title, Text = message }, null, null, defaultValue);
    }

    /// <summary>
    /// Gets a range of values from the user.
    /// </summary>
    /// <param name="svc">
    /// Dialog service onto which to invoke the dialog.
    /// </param>
    /// <param name="title">Title of the dialog.</param>
    /// <param name="message">Message to be shown on the dialog.</param>
    /// <param name="defaultValue">
    /// Default value to be initially selected.
    /// </param>
    /// <typeparam name="T">Type of value to get.</typeparam>
    /// <returns>
    /// A new <see cref="DialogResult{T}"/> with
    /// <see cref="DialogResult{T}.Success"/> set to <see langword="true"/> and
    /// <see cref="DialogResult{T}.Result"/> set to the range of values entered
    /// by the user, or a new <see cref="DialogResult{T}"/> with
    /// <see cref="DialogResult{T}.Success"/> set to <see langword="false"/> and
    /// <see cref="DialogResult{T}.Result"/> set to <see langword="null"/> if
    /// the user cancels the input dialog.
    /// </returns>
    public static Task<DialogResult<(T Min, T Max)>> GetInputRange<T>(this IDialogService svc, string? title, string message, T defaultValue = default) where T : struct, IComparable<T>
    {
        return svc.GetInputRange(CommonDialogTemplates.Input with { Title = title, Text = message }, null, null, defaultValue, defaultValue);
    }

    /// <summary>
    /// Gets a string from the user.
    /// </summary>
    /// <param name="svc">
    /// Dialog service onto which to invoke the dialog.
    /// </param>
    /// <param name="title">Title of the dialog.</param>
    /// <param name="message">Message to be shown on the dialog.</param>
    /// <param name="defaultValue">Default value.</param>
    /// <returns>
    /// A new <see cref="DialogResult{T}"/> with
    /// <see cref="DialogResult{T}.Success"/> set to <see langword="true"/> and
    /// <see cref="DialogResult{T}.Result"/> set to the value entered by the
    /// user, or a new <see cref="DialogResult{T}"/> with
    /// <see cref="DialogResult{T}.Success"/> set to <see langword="false"/> and
    /// <see cref="DialogResult{T}.Result"/> set to
    /// <paramref name="defaultValue"/> if the user cancels the input dialog.
    /// </returns>
    public static Task<DialogResult<string?>> GetInputText(this IDialogService svc, string? title, string message, string? defaultValue = null)
    {
        return svc.GetInputText(CommonDialogTemplates.Input with { Title = title, Text = message }, defaultValue);
    }

    /// <summary>
    /// Alternate value selection method that allows the user to select a value
    /// from a larger pool of items.
    /// </summary>
    /// <param name="svc">
    /// Dialog service onto which to invoke the dialog.
    /// </param>
    /// <param name="title">Title of the dialog.</param>
    /// <param name="message">Message to be shown on the dialog.</param>
    /// <param name="options">
    /// Collection of available options to choose from.
    /// </param>
    /// <returns>
    /// The selected option value in the <paramref name="options"/> array.
    /// </returns>
    public static Task<DialogResult<T>> SelectOption<T>(this IDialogService svc, string? title, string message, params NamedObject<T>[] options)
    {
        return svc.SelectOption(CommonDialogTemplates.Input with { Title = title, Text = message }, options);
    }

    /// <summary>
    /// Alternate value selection method that allows the user to select a value
    /// from a larger pool of items.
    /// </summary>
    /// <param name="svc">
    /// Dialog service onto which to invoke the dialog.
    /// </param>
    /// <param name="title">Title of the dialog.</param>
    /// <param name="message">Message to be shown on the dialog.</param>
    /// <param name="options">
    /// Collection of available options to choose from.
    /// </param>
    /// <returns>
    /// The selected option value in the <paramref name="options"/> array.
    /// </returns>
    public static Task<DialogResult<string?>> SelectOption<T>(this IDialogService svc, string? title, string message, params string[] options)
    {
        return svc.SelectOption(title, message, options.Select(p => new NamedObject<string?>(name: p, (string?)p)).ToArray());
    }

    /// <summary>
    /// Allows the user to select an action from a list of possible options to
    /// be invoked.
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
    /// A <see cref="Task"/> that can be used to await the completion of the
    /// dialog and the subsequent execution of the selected action.
    /// </returns>
    public static async Task SelectAction(this IDialogService svc, DialogTemplate template, IEnumerable<NamedObject<Func<Task>>> options)
    {
        await ((await svc.SelectOption(template, options.ToArray())).Result?.Invoke() ?? Task.CompletedTask);
    }

    /// <summary>
    /// Gets a credential from the user.
    /// </summary>
    /// <param name="svc">
    /// Dialog service onto which to invoke the dialog.
    /// </param>
    /// <param name="title">Title of the dialog.</param>
    /// <param name="message">Message to be shown on the dialog.</param>
    /// <param name="defaultUser">Default username to present.</param>
    /// <param name="isUserEditable">
    /// <see langword="true"/> to indicate that the username field should be
    /// editable, <see langword="false"/> to set the username field as
    /// read-only.
    /// </param>
    /// <returns>
    /// A new <see cref="DialogResult{T}"/> with
    /// <see cref="DialogResult{T}.Success"/> set to <see langword="true"/> and
    /// <see cref="DialogResult{T}.Result"/> set to the credential entered by
    /// the user, or a new <see cref="DialogResult{T}"/> with
    /// <see cref="DialogResult{T}.Success"/> set to <see langword="false"/> and
    /// <see cref="DialogResult{T}.Result"/> set to <see langword="null"/> if
    /// the user cancels the input dialog.
    /// </returns>
    public static Task<DialogResult<Credential?>> GetCredential(this IDialogService svc, string? title, string message, string? defaultUser = null, bool isUserEditable = true)
    {
        return svc.GetCredential(CommonDialogTemplates.Login with { Title = title, Text = message }, defaultUser, isUserEditable);
    }

    /// <summary>
    /// Gets a path to a file to be saved.
    /// </summary>
    /// <param name="svc">
    /// Dialog service onto which to invoke the dialog.
    /// </param>
    /// <param name="defaultPath">Default path to be selected.</param>
    /// <returns>
    /// A task that can be used to await the completion of the dialog, which
    /// includes the dialog result with the file path.
    /// </returns>
    public static Task<DialogResult<string?>> GetFileSavePath(this IDialogService svc, string? defaultPath = null)
    {
        return GetFileSavePath(svc, [FileFilterItem.AllFiles], defaultPath);
    }

    /// <summary>
    /// Gets a path to a file to be saved.
    /// </summary>
    /// <param name="svc">
    /// Dialog service onto which to invoke the dialog.
    /// </param>
    /// <param name="message">
    /// Descrpitive message to be displayed on the dialog.
    /// </param>
    /// <param name="fileFilters">
    /// File filters to include in the file save dialog.
    /// </param>
    /// <param name="defaultPath">Default path to be selected.</param>
    /// <returns>
    /// A task that can be used to await the completion of the dialog, which
    /// includes the dialog result with the file path.
    /// </returns>
    public static Task<DialogResult<string?>> GetFileSavePath(this IDialogService svc, string? message, IEnumerable<FileFilterItem> fileFilters, string? defaultPath = null)
    {
        return message is null
            ? GetFileSavePath(svc, fileFilters, defaultPath)
            : svc.GetFileSavePath(CommonDialogTemplates.FileSave with { Text = message }, fileFilters, defaultPath);
    }

    /// <summary>
    /// Gets a path to a file to be saved.
    /// </summary>
    /// <param name="svc">
    /// Dialog service onto which to invoke the dialog.
    /// </param>
    /// <param name="fileFilters">
    /// File filters to include in the file save dialog.
    /// </param>
    /// <param name="defaultPath">Default path to be selected.</param>
    /// <returns>
    /// A task that can be used to await the completion of the dialog, which
    /// includes the dialog result with the file path.
    /// </returns>
    public static Task<DialogResult<string?>> GetFileSavePath(this IDialogService svc, IEnumerable<FileFilterItem> fileFilters, string? defaultPath = null)
    {
        return svc.GetFileSavePath(CommonDialogTemplates.FileSave, fileFilters, defaultPath);
    }
}
