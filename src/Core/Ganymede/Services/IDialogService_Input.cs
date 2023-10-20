using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Types.Base;
using TheXDS.Ganymede.ViewModels;

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
    Task<bool> Ask(string question) => Ask(null, question);

    /// <summary>
    /// Asks the user a question with a Yes/No answer.
    /// </summary>
    /// <param name="title">Title of the question.</param>
    /// <param name="question">Question to ask.</param>
    /// <returns>
    /// <see langword="true"/> if the user answers "Yes",
    /// <see langword="false"/> otherwise.
    /// </returns>
    Task<bool> Ask(string? title, string question);

    /// <summary>
    /// Asks the user a question with a Yes/No/Cancel answer.
    /// </summary>
    /// <param name="question">Question to ask.</param>
    /// <returns>
    /// <see langword="true"/> if the user answers "Yes",
    /// <see langword="false"/> if the user answers "No", or
    /// <see langword="null"/> if the user does not answer the question.
    /// </returns>
    Task<bool?> AskYnc(string question) => AskYnc(null, question);

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
    /// Gets the index of a selected option by the user.
    /// </summary>
    /// <param name="title">Title of the question.</param>
    /// <param name="prompt">Prompt to ask.</param>
    /// <param name="options">
    /// Collection of available options to choose from.
    /// </param>
    /// <returns>
    /// The index of the selected option in the <paramref name="options"/>
    /// array.
    /// </returns>
    /// <remarks>
    /// This method works well when the pool of available options is small. If
    /// the pool of available options contains several items, consider using
    /// <see cref="SelectOption(string?, string, string[])"/> instead.
    /// </remarks>
    /// <seealso cref="SelectOption(string?, string, string[])"/>
    Task<int> GetOption(string? title, string prompt, params string[] options);

    /// <summary>
    /// Alternate value selection method that allows the user to select a value
    /// from a larger pool of items.
    /// </summary>
    /// <param name="title">Title of the question.</param>
    /// <param name="prompt">Prompt to ask.</param>
    /// <param name="options">
    /// Collection of available options to choose from.
    /// </param>
    /// <returns>
    /// The index of the selected option in the <paramref name="options"/>
    /// array.
    /// </returns>
    /// <remarks>
    /// The UI for this dialog should be
    /// different, and modeled under the asumption that the list of available
    /// options may contain several items.
    /// </remarks>
    /// <see cref="GetOption(string?, string, string[])"/>
    Task<int> SelectOption(string? title, string prompt, params string[] options) => GetOption(title, prompt, options);

    /// <summary>
    /// Gets a value from the user.
    /// </summary>
    /// <param name="title">Dialog title.</param>
    /// <param name="message">Dialog message.</param>
    /// <param name="minimum">Minimum allowed value.</param>
    /// <param name="maximum">Maximum allowed value.</param>
    /// <param name="defaultValue">Default value.</param>
    /// <typeparam name="T">Type of value to get.</typeparam>
    /// <returns>
    /// A new <see cref="InputResult{T}"/> with
    /// <see cref="InputResult{T}.Success"/> set to <see langword="true"/> and
    /// <see cref="InputResult{T}.Result"/> set to the value entered by the
    /// user, or a new <see cref="InputResult{T}"/> with
    /// <see cref="InputResult{T}.Success"/> set to <see langword="false"/> and
    /// <see cref="InputResult{T}.Result"/> set to the default value for
    /// <typeparamref name="T"/> if the user cancels the input dialog.
    /// </returns>
    Task<InputResult<T>> GetInputValue<T>(string? title, string message, T minimum, T maximum, T defaultValue = default) where T : struct, IComparable<T>;
    
    /// <summary>
    /// Gets a value from the user.
    /// </summary>
    /// <param name="title">Dialog title.</param>
    /// <param name="message">Dialog message.</param>
    /// <param name="defaultValue">Default value.</param>
    /// <typeparam name="T">Type of value to get.</typeparam>
    /// <returns>
    /// A new <see cref="InputResult{T}"/> with
    /// <see cref="InputResult{T}.Success"/> set to <see langword="true"/> and
    /// <see cref="InputResult{T}.Result"/> set to the value entered by the
    /// user, or a new <see cref="InputResult{T}"/> with
    /// <see cref="InputResult{T}.Success"/> set to <see langword="false"/> and
    /// <see cref="InputResult{T}.Result"/> set to the default value for
    /// <typeparamref name="T"/> if the user cancels the input dialog.
    /// </returns>
    Task<InputResult<T>> GetInputValue<T>(string? title, string message, T defaultValue = default) where T : struct, IComparable<T>;

    /// <summary>
    /// Gets a string from the user.
    /// </summary>
    /// <param name="title">Dialog title.</param>
    /// <param name="message">Dialog message.</param>
    /// <param name="defaultValue">Default value.</param>
    /// <returns>
    /// A new <see cref="InputResult{T}"/> with
    /// <see cref="InputResult{T}.Success"/> set to <see langword="true"/> and
    /// <see cref="InputResult{T}.Result"/> set to the value entered by the
    /// user, or a new <see cref="InputResult{T}"/> with
    /// <see cref="InputResult{T}.Success"/> set to <see langword="false"/> and
    /// <see cref="InputResult{T}.Result"/> set to
    /// <paramref name="defaultValue"/> if the user cancels the input dialog.
    /// </returns>
    Task<InputResult<string?>> GetInputText(string? title, string message, string? defaultValue = null);

    /// <summary>
    /// Gets a range of values from the user.
    /// </summary>
    /// <param name="title">Dialog title.</param>
    /// <param name="message">Dialog message.</param>
    /// <param name="defaultMin">
    /// Default value for the lower bound of the range.
    /// </param>
    /// <param name="defaultMax">
    /// Default value for the upper bound of the range.
    /// </param>
    /// <typeparam name="T">Type of value to get.</typeparam>
    /// <returns>
    /// A new <see cref="InputResult{T}"/> with
    /// <see cref="InputResult{T}.Success"/> set to <see langword="true"/> and
    /// <see cref="InputResult{T}.Result"/> set to the range of values entered
    /// by the user, or a new <see cref="InputResult{T}"/> with
    /// <see cref="InputResult{T}.Success"/> set to <see langword="false"/> and
    /// <see cref="InputResult{T}.Result"/> set to <see langword="null"/> if
    /// the user cancels the input dialog.
    /// </returns>
    Task<InputResult<(T Min, T Max)>> GetInputRange<T>(string? title, string message, T defaultMin = default, T defaultMax = default) where T : struct, IComparable<T>;

    /// <summary>
    /// Gets a range of values from the user.
    /// </summary>
    /// <param name="title">Dialog title.</param>
    /// <param name="message">Dialog message.</param>
    /// <param name="minimum">Minimum allowed value.</param>
    /// <param name="maximum">Maximum allowed value.</param>
    /// <param name="defaultMin">
    /// Default value for the lower bound of the range.
    /// </param>
    /// <param name="defaultMax">
    /// Default value for the upper bound of the range.
    /// </param>
    /// <typeparam name="T">Type of value to get.</typeparam>
    /// <returns>
    /// A new <see cref="InputResult{T}"/> with
    /// <see cref="InputResult{T}.Success"/> set to <see langword="true"/> and
    /// <see cref="InputResult{T}.Result"/> set to the range of values entered
    /// by the user, or a new <see cref="InputResult{T}"/> with
    /// <see cref="InputResult{T}.Success"/> set to <see langword="false"/> and
    /// <see cref="InputResult{T}.Result"/> set to <see langword="null"/> if
    /// the user cancels the input dialog.
    /// </returns>
    Task<InputResult<(T Min, T Max)>> GetInputRange<T>(string? title, string message, T minimum, T maximum, T defaultMin = default, T defaultMax = default) where T : struct, IComparable<T>;

    /// <summary>
    /// Gets a credential from the user.
    /// </summary>
    /// <param name="title">Dialog title.</param>
    /// <param name="message">Dialog message.</param>
    /// <param name="defaultUser">Default username to present.</param>
    /// <returns>
    /// A new <see cref="InputResult{T}"/> with
    /// <see cref="InputResult{T}.Success"/> set to <see langword="true"/> and
    /// <see cref="InputResult{T}.Result"/> set to the credential entered by
    /// the user, or a new <see cref="InputResult{T}"/> with
    /// <see cref="InputResult{T}.Success"/> set to <see langword="false"/> and
    /// <see cref="InputResult{T}.Result"/> set to <see langword="null"/> if
    /// the user cancels the input dialog.
    /// </returns>
    Task<InputResult<Credential>> GetCredential(string? title, string message, string? defaultUser = null);

    /// <summary>
    /// Gets a credential from the user.
    /// </summary>
    /// <param name="message">Dialog message.</param>
    /// <returns>
    /// A new <see cref="InputResult{T}"/> with
    /// <see cref="InputResult{T}.Success"/> set to <see langword="true"/> and
    /// <see cref="InputResult{T}.Result"/> set to the credential entered by
    /// the user, or a new <see cref="InputResult{T}"/> with
    /// <see cref="InputResult{T}.Success"/> set to <see langword="false"/> and
    /// <see cref="InputResult{T}.Result"/> set to <see langword="null"/> if
    /// the user cancels the input dialog.
    /// </returns>
    Task<InputResult<Credential>> GetCredential(string message) => GetCredential(null, message);

    /// <summary>
    /// Navigates to a user-defined <see cref="DialogViewModel"/> under the
    /// dialog navigation system.
    /// </summary>
    /// <typeparam name="TViewModel">
    /// Type of <see cref="DialogViewModel"/> to navigate to. It must implement
    /// <see cref="IAwaitableDialogViewModel"/> to be able to notify of its own
    /// completion.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// Type of value to get from the input dialog.
    /// </typeparam>
    /// <param name="title">Dialog title.</param>
    /// <param name="message">Dialog message.</param>
    /// <param name="defaultValue">Default value to set and/or return.</param>
    /// <param name="initCallback">
    /// Optional callback invoked to further configure the ViewModel before
    /// presentation.
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await for the completion of
    /// the dialog.
    /// </returns>
    Task<InputResult<TValue>> GetInput<TViewModel, TValue>(string? title, string message, TValue defaultValue = default!, Action<TViewModel>? initCallback = null) where TViewModel : DialogViewModel, IInputDialogViewModel<TValue>, new();

    /// <summary>
    /// Navigates to a user-defined <see cref="DialogViewModel"/> under the
    /// dialog navigation system.
    /// </summary>
    /// <typeparam name="TViewModel">
    /// Type of <see cref="DialogViewModel"/> to navigate to. It must implement
    /// <see cref="IAwaitableDialogViewModel"/> to be able to notify of its own
    /// completion.
    /// </typeparam>
    /// <param name="dialogVm">Dialog ViewModel to navigate to.</param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await for the completion of
    /// the dialog.
    /// </returns>
    Task CustomDialog<TViewModel>(TViewModel dialogVm) where TViewModel : ViewModel, IAwaitableDialogViewModel, new();
}
