using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Types;

namespace TheXDS.Ganymede.Services;

public partial interface IDialogService
{
    /// <summary>
    /// Asks the user a question with a Yes/No answer.
    /// </summary>
    /// <param name="question">Question to ask.</param>
    /// <returns>
    /// <see langword="true"/> if the user answers "Yes",
    /// <see langword="false"/> otherwise.
    /// </returns>
    Task<bool> AskYn(string question);

    /// <summary>
    /// Asks the user a question with a Yes/No answer.
    /// </summary>
    /// <param name="title">Title of the question.</param>
    /// <param name="question">Question to ask.</param>
    /// <returns>
    /// <see langword="true"/> if the user answers "Yes",
    /// <see langword="false"/> otherwise.
    /// </returns>
    Task<bool> AskYn(string? title, string question);

    /// <summary>
    /// Asks the user a question with a Yes/No/Cancel answer.
    /// </summary>
    /// <param name="question">Question to ask.</param>
    /// <returns>
    /// <see langword="true"/> if the user answers "Yes",
    /// <see langword="false"/> if the user answers "No", or
    /// <see langword="null"/> if the user does not answer the question.
    /// </returns>
    Task<bool?> AskYnc(string question);

    /// <summary>
    /// Asks the user a question with a Yes/No/Cancel answer.
    /// </summary>
    /// <param name="title">Title of the question.</param>
    /// <param name="question">Question to ask.</param>
    /// <returns>
    /// <see langword="true"/> if the user answers "Yes",
    /// <see langword="false"/> if the user answers "No", or
    /// <see langword="null"/> if the user does not answer the question.
    /// </returns>
    Task<bool?> AskYnc(string? title, string question);

    /// <summary>
    /// Alternate value selection method that allows the user to select a value
    /// from a larger pool of items.
    /// </summary>
    /// <param name="template">
    /// Template that describes the visual properties of the dialog.
    /// </param>
    /// <param name="options">
    /// Collection of available options to choose from.
    /// </param>
    /// <returns>
    /// The selected option value in the <paramref name="options"/> array.
    /// </returns>
    Task<DialogResult<T>> SelectOption<T>(DialogTemplate template, params NamedObject<T>[] options);

    /// <summary>
    /// Displays a simple message dialog from a pre-configured visual template.
    /// </summary>
    /// <param name="template">
    /// Template that describes the visual properties of the dialog.
    /// </param>
    /// <param name="values">
    /// Array of values that can be selected on the dialog.
    /// </param>
    /// <returns>
    /// A task that can be used to await the completion of the dialog.
    /// </returns>
    Task<TResult> Show<TResult>(DialogTemplate template, NamedObject<TResult>[] values);

    /// <summary>
    /// Displays a simple dialog without additional interaction UI from a
    /// pre-configured template.
    /// </summary>
    /// <typeparam name="TViewModel">
    /// Type of ViewModel to use as the awaitable dialog instance.
    /// </typeparam>
    /// <typeparam name="TResult">
    /// Type of value returned by the awaitable dialog.
    /// </typeparam>
    /// <param name="template">
    /// Template to use when generating the dialog to be displayed.
    /// </param>
    /// <param name="values">Collection of values to associate to each dialog option.</param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await the dialog.
    /// </returns>
    Task<TResult> Show<TViewModel, TResult>(DialogTemplate template, NamedObject<TResult>[] values) where TViewModel : IAwaitableDialogViewModel<TResult>, new();

    /// <summary>
    /// Displays a dialog from a pre-configured template.
    /// </summary>
    /// <typeparam name="TViewModel">
    /// Type of ViewModel to use as the awaitable dialog instance.
    /// </typeparam>
    /// <typeparam name="TResult">
    /// Type of value returned by the awaitable dialog.
    /// </typeparam>
    /// <param name="template">
    /// Template to use when generating the dialog to be displayed.
    /// </param>
    /// <param name="values">Collection of values to associate to each dialog option.</param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await the dialog.
    /// </returns>
    Task<TResult> Show<TViewModel, TResult>(DialogTemplate template, NamedObject<Func<TViewModel, TResult>>[] values) where TViewModel : IAwaitableDialogViewModel<TResult>, new();

    /// <summary>
    /// Navigates to a user-defined dialog under the navigation system.
    /// </summary>
    /// <param name="dialogVm">Dialog ViewModel to navigate to. It must
    /// implement <see cref="IAwaitableDialogViewModel{T}"/> to be able to
    /// notify of its own completion.</param>
    /// <returns>
    /// A <see cref="Task{T}"/> that can be used to await for the completion of
    /// the dialog.
    /// </returns>
    /// <remarks>
    /// Depending on your use case, you may want to implement a custom dialog
    /// that has a return type of <see cref="DialogResult{T}"/> to support
    /// dialog cancellation.
    /// </remarks>
    /// <seealso cref="DialogResult{T}"/>
    Task<TResult> Show<TResult>(IAwaitableDialogViewModel<TResult> dialogVm);

    /// <summary>
    /// Gets a string from the user.
    /// </summary>
    /// <param name="template">
    /// Template to use when generating the dialog to be displayed.
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
    Task<DialogResult<string?>> GetInputText(DialogTemplate template, string? defaultValue = null);

    /// <summary>
    /// Gets a credential from the user.
    /// </summary>
    /// <param name="template">
    /// Template to use when generating the dialog to be displayed.
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
    Task<DialogResult<Credential?>> GetCredential(DialogTemplate template, string? defaultUser = null, bool isUserEditable = true);

    /// <summary>
    /// Gets a path to a file.
    /// </summary>
    /// <param name="template">
    /// Template to use when generating the dialog to be displayed.
    /// </param>
    /// <param name="filters">Collection of filters that can be used to filter for specific file types.</param>
    /// <param name="defaultPath">Initial default file path.</param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await for the completion of
    /// the dialog.
    /// </returns>
    Task<DialogResult<string?>> GetFileOpenPath(DialogTemplate template, IEnumerable<FileFilterItem> filters, string? defaultPath = null);

    /// <summary>
    /// Gets multiple file paths.
    /// </summary>
    /// <param name="template">
    /// Template to use when generating the dialog to be displayed.
    /// </param>
    /// <param name="filters">Collection of filters that can be used to filter for specific file types.</param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await for the completion of
    /// the dialog.
    /// </returns>
    Task<DialogResult<string[]?>> GetFilesOpenPath(DialogTemplate template, IEnumerable<FileFilterItem> filters);

    /// <summary>
    /// Gets a path to a file.
    /// </summary>
    /// <param name="template">
    /// Template to use when generating the dialog to be displayed.
    /// </param>
    /// <param name="filters">
    /// Collection of filters that can be used to filter for specific file
    /// types.
    /// </param>
    /// <param name="defaultPath">Initial default file path.</param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await for the completion of
    /// the dialog.
    /// </returns>
    Task<DialogResult<string?>> GetFileSavePath(DialogTemplate template, IEnumerable<FileFilterItem> filters, string? defaultPath = null);

    /// <summary>
    /// Gets a path to a directory.
    /// </summary>
    /// <param name="template">
    /// Template to use when generating the dialog to be displayed.
    /// </param>
    /// <param name="defaultPath">Initial default directory path.</param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await for the completion of
    /// the dialog.
    /// </returns>
    Task<DialogResult<string?>> GetDirectoryPath(DialogTemplate template, string? defaultPath = null);

    /// <summary>
    /// Gets a value from the user.
    /// </summary>
    /// <param name="template">
    /// Template to use when generating the dialog to be displayed.
    /// </param>
    /// <param name="minimum">Minimum allowed value.</param>
    /// <param name="maximum">Maximum allowed value.</param>
    /// <param name="defaultValue">Default value.</param>
    /// <typeparam name="T">Type of value to get.</typeparam>
    /// <returns>
    /// A new <see cref="DialogResult{T}"/> with
    /// <see cref="DialogResult{T}.Success"/> set to <see langword="true"/> and
    /// <see cref="DialogResult{T}.Result"/> set to the value entered by the
    /// user, or a new <see cref="DialogResult{T}"/> with
    /// <see cref="DialogResult{T}.Success"/> set to <see langword="false"/> and
    /// <see cref="DialogResult{T}.Result"/> set to the default value for
    /// <typeparamref name="T"/> if the user cancels the input dialog.
    /// </returns>
    Task<DialogResult<T>> GetInputValue<T>(DialogTemplate template, T? minimum, T? maximum, T defaultValue = default) where T : struct, IComparable<T>;

    /// <summary>
    /// Gets a range of values from the user.
    /// </summary>
    /// <param name="template">
    /// Template to use when generating the dialog to be displayed.
    /// </param>
    /// <param name="minimum">Minimum allowed value.</param>
    /// <param name="maximum">Maximum allowed value.</param>
    /// <param name="defaultRangeStart">
    /// Default value for the lower bound of the range.
    /// </param>
    /// <param name="defaultRangeEnd">
    /// Default value for the upper bound of the range.
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
    Task<DialogResult<(T Min, T Max)>> GetInputRange<T>(DialogTemplate template, T? minimum, T? maximum, T defaultRangeStart = default, T defaultRangeEnd = default) where T : struct, IComparable<T>;
}
