using Avalonia.Media;
using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Resources.Strings;
using TheXDS.Ganymede.ViewModels.Dialogs;

namespace TheXDS.Ganymede.Services;

public partial class DialogService
{
    /// <summary>
    /// Asks the user a question with a Yes/No answer.
    /// </summary>
    /// <param name="question">Question to ask.</param>
    /// <returns>
    /// <see langword="true"/> if the user answers "Yes",
    /// <see langword="false"/> otherwise.
    /// </returns>
    public Task<bool> Ask(string question) => Ask(null, question);

    /// <summary>
    /// Asks the user a question with a Yes/No answer.
    /// </summary>
    /// <param name="title">Title of the question.</param>
    /// <param name="question">Question to ask.</param>
    /// <returns>
    /// <see langword="true"/> if the user answers "Yes",
    /// <see langword="false"/> otherwise.
    /// </returns>
    public Task<bool> Ask(string? title, string question)
    {
        return GetButtonValue("?", Brushes.DarkGreen, title, question, (Common.Yes, true), (Common.No, false));
    }

    /// <summary>
    /// Asks the user a question with a Yes/No/Cancel answer.
    /// </summary>
    /// <param name="question">Question to ask.</param>
    /// <returns>
    /// <see langword="true"/> if the user answers "Yes",
    /// <see langword="false"/> if the user answers "No", or
    /// <see langword="null"/> if the user does not answer the question.
    /// </returns>
    public Task<bool?> AskYnc(string question) => AskYnc(null, question);
    
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
    public Task<bool?> AskYnc(string? title, string question)
    {
        return GetButtonValue("?", Brushes.DarkGreen, title, question, (Common.Yes, true), (Common.No, false),
            (Common.Cancel, (bool?)null));
    }

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
    public async Task<InputResult<T>> GetInputValue<T>(string? title, string message, T defaultValue = default) where T : struct, IComparable<T>
    {
        TaskCompletionSource<bool> dialogAwaiter = new();
        var vm = CreateInputDialogVm<InputDialogViewModel<T>>(title, message, dialogAwaiter);
        vm.Value = defaultValue;
        Content.Router.NavigateAndReset.Execute(vm);
        var result = await dialogAwaiter.Task;
        return new(result, result ? vm.Value : defaultValue);
    }
    
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
    public async Task<InputResult<string?>> GetInputText(string? title, string message, string? defaultValue = null)
    {
        TaskCompletionSource<bool> dialogAwaiter = new();
        var vm = CreateInputDialogVm<InputDialogViewModel>(title, message, dialogAwaiter);
        vm.Value = defaultValue;
        Content.Router.NavigateAndReset.Execute(vm);
        var result = await dialogAwaiter.Task;
        return new(result, result ? vm.Value : defaultValue);
    }

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
    public async Task<InputResult<(T Min, T Max)?>> GetInputRange<T>(string? title, string message, T minimum, T maximum, T defaultMin = default, T defaultMax = default) where T : struct, IComparable<T>
    {
        TaskCompletionSource<bool> dialogAwaiter = new();
        var vm = CreateInputDialogVm<RangeInputDialogViewModel<T>>(title, message, dialogAwaiter);
        vm.Minimum = minimum;
        vm.MinimumValue = defaultMin;
        vm.Maximum = maximum;
        vm.MaximumValue = defaultMax;
        Content.Router.NavigateAndReset.Execute(vm);
        var result = await dialogAwaiter.Task;
        return new(result, result ? (vm.MinimumValue, vm.MaximumValue) : null);
    }
}