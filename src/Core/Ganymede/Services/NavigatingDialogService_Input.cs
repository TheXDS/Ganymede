using System.Drawing;
using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.ViewModels;
using TheXDS.Ganymede.Types.Base;
using TheXDS.Ganymede.Resources.Strings;
using TheXDS.MCART.Types.Extensions;
using System.Threading;

namespace TheXDS.Ganymede.Services;

public partial class NavigatingDialogService
{
    /// <inheritdoc/>
    public Task<bool> Ask(string question) => Ask(null, question);

    /// <inheritdoc/>
    public Task<bool> Ask(string? title, string question)
    {
        return GetButtonValue("?", Color.DarkGreen, title, question, (Common.Yes, true), (Common.No, false));
    }

    /// <inheritdoc/>
    public Task<int> GetOption(string? title, string prompt, params string[] options)
    {
        static IEnumerable<(string, int)> GetOptions(string[] o)
        {
            var c = 0;
            foreach (var p in o)
            {
                yield return (p, c);
                c++;
            }
        }
        return GetButtonValue("?", Color.DarkGreen, title, prompt, GetOptions(options).ToArray());
    }

    /// <inheritdoc/>
    public async Task<int> SelectOption(string? title, string prompt, params string[] options)
    {
        TaskCompletionSource<bool> dialogAwaiter = new();
        var vm = CreateInputDialogVm<SelectionDialogViewModel>(title, prompt, dialogAwaiter);
        vm.Options = options;
        Navigate(vm);
        var result = await dialogAwaiter.Task;
        return result ? options.FindIndexOf(vm.Value) : -1;
    }

    /// <inheritdoc/>
    public Task<bool?> AskYnc(string question) => AskYnc(null, question);

    /// <inheritdoc/>
    public Task<bool?> AskYnc(string? title, string question)
    {
        return GetButtonValue("?", Color.DarkGreen, title, question, (Common.Yes, true), (Common.No, false), (Common.Cancel, (bool?)null));
    }

    /// <inheritdoc/>
    public Task<InputResult<T>> GetInputValue<T>(string? title, string message, T minimum, T maximum, T defaultValue = default) where T : struct, IComparable<T>
    {
        return GetInput<InputDialogViewModel<T>, T>(title, message, defaultValue, p => {
            p.Minimum = minimum;
            p.Maximum = maximum;
        });
    }

    /// <inheritdoc/>
    public Task<InputResult<T>> GetInputValue<T>(string? title, string message, T defaultValue = default) where T : struct, IComparable<T>
    {
        return GetInput<InputDialogViewModel<T>, T>(title, message, defaultValue);
    }

    /// <inheritdoc/>
    public Task<InputResult<string?>> GetInputText(string? title, string message, string? defaultValue = null)
    {
        return GetInput<InputDialogViewModel, string?>(title, message, defaultValue);
    }

    /// <inheritdoc/>
    public Task<InputResult<(T Min, T Max)>> GetInputRange<T>(string? title, string message, T defaultMin = default, T defaultMax = default) where T : struct, IComparable<T>
    {
        return GetInput<RangeInputDialogViewModel<T>, (T, T)>(title, message, (defaultMin, defaultMax));
    }

    /// <inheritdoc/>
    public Task<InputResult<(T Min, T Max)>> GetInputRange<T>(string? title, string message, T minimum, T maximum, T defaultMin = default, T defaultMax = default) where T : struct, IComparable<T>
    {
        return GetInput<RangeInputDialogViewModel<T>, (T, T)>(title, message, (defaultMin, defaultMax), p =>
        {
            p.Minimum = minimum;
            p.Maximum = maximum;
        });
    }

    /// <inheritdoc/>
    public async Task<InputResult<Credential>> GetCredential(string? title, string message, string? defaultUser = null)
    {
        TaskCompletionSource<bool> dialogAwaiter = new();
        var vm = new CredentialInputDialogViewModel()
        {
            Title = title,
            Message = message,
            Icon = "👤",
            IconBgColor = Color.MediumAquamarine,
            Interactions =
            {
                new(CloseDialogCommand(dialogAwaiter, true), Common.Ok),
                new(CloseDialogCommand(dialogAwaiter, false), Common.Cancel)
            },
            User = defaultUser ?? string.Empty
        };
        Navigate(vm);
        var result = await dialogAwaiter.Task;
        return new(result, result ? new Credential(vm.User, vm.Password) : null!);
    }

    /// <inheritdoc/>
    public async Task CustomDialog<TViewModel>(TViewModel dialogVm) where TViewModel : ViewModel, IAwaitableDialogViewModel, new()
    {
        Navigate(dialogVm);
        try { await dialogVm.DialogAwaiter; }
        finally { NavigateBack(); }
    }

    /// <inheritdoc/>
    public async Task<InputResult<TValue>> GetInput<TViewModel, TValue>(string? title, string message, TValue defaultValue = default!, Action<TViewModel>? initCallback = null) where TViewModel : DialogViewModel, IInputDialogViewModel<TValue>, new()
    {
        TaskCompletionSource<bool> dialogAwaiter = new();
        var vm = CreateInputDialogVm<TViewModel>(title, message, dialogAwaiter);
        vm.Value = defaultValue;
        initCallback?.Invoke(vm);
        Navigate(vm);
        var result = await dialogAwaiter.Task;
        return new(result, result ? vm.Value : defaultValue);
    }
}
