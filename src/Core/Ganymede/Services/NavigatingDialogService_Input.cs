using System.Drawing;
using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Resources.Strings;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.Ganymede.Services;

public partial class NavigatingDialogService
{
    /// <inheritdoc/>
    public Task<bool> Ask(string? title, string question)
    {
        return GetButtonValue("?", Color.DarkGreen, title, question, (Common.Yes, true), (Common.No, false));
    }

    /// <inheritdoc/>
    public Task<bool?> AskYnc(string? title, string question)
    {
        return GetButtonValue("?", Color.DarkGreen, title, question, (Common.Yes, true), (Common.No, false), (Common.Cancel, (bool?)null));
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
    public async Task<DialogResult<int>> SelectOption(string? title, string prompt, params string[] options)
    {
        TaskCompletionSource<bool> dialogAwaiter = new();
        var vm = CreateInputDialogVm<SelectionDialogViewModel>(title, prompt, dialogAwaiter);
        vm.Options = options;
        Navigate(vm);
        var result = await dialogAwaiter.Task;
        return new(result, result ? options.FindIndexOf(vm.Value) : -1);
    }

    /// <inheritdoc/>
    public Task<DialogResult<T>> GetInputValue<T>(string? title, string message, T minimum, T maximum, T defaultValue = default) where T : struct, IComparable<T>
    {
        return GetInput<InputDialogViewModel<T>, T>(title, message, defaultValue, p =>
        {
            p.Minimum = minimum;
            p.Maximum = maximum;
        });
    }

    /// <inheritdoc/>
    public Task<DialogResult<T>> GetInputValue<T>(string? title, string message, T defaultValue = default) where T : struct, IComparable<T>
    {
        return GetInput<InputDialogViewModel<T>, T>(title, message, defaultValue);
    }

    /// <inheritdoc/>
    public Task<DialogResult<string?>> GetInputText(string? title, string message, string? defaultValue = null)
    {
        return GetInput<InputDialogViewModel, string?>(title, message, defaultValue);
    }

    /// <inheritdoc/>
    public Task<DialogResult<(T Min, T Max)>> GetInputRange<T>(string? title, string message, T defaultMin = default, T defaultMax = default) where T : struct, IComparable<T>
    {
        return GetInput<RangeInputDialogViewModel<T>, (T, T)>(title, message, (defaultMin, defaultMax));
    }

    /// <inheritdoc/>
    public Task<DialogResult<(T Min, T Max)>> GetInputRange<T>(string? title, string message, T minimum, T maximum, T defaultMin = default, T defaultMax = default) where T : struct, IComparable<T>
    {
        return GetInput<RangeInputDialogViewModel<T>, (T, T)>(title, message, (defaultMin, defaultMax), p =>
        {
            p.Minimum = minimum;
            p.Maximum = maximum;
        });
    }

    /// <inheritdoc/>
    public async Task<DialogResult<Credential>> GetCredential(string? title, string message, string? defaultUser = null)
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
                new(CloseDialogCommand(dialogAwaiter, true), Common.Ok) { IsPrimary = true },
                new(CloseDialogCommand(dialogAwaiter, false), Common.Cancel)
            },
            User = defaultUser ?? string.Empty
        };
        Navigate(vm);
        var result = await dialogAwaiter.Task;
        return new(result, result ? new Credential(vm.User, vm.Password) : null!);
    }

    /// <inheritdoc/>
    public Task<DialogResult<string>> GetFileOpenPath(string? title, string message, IEnumerable<FileFilterItem> filters, string? defaultPath = null)
    {
        return GetInput<FileOpenDialogViewModel, string>(title, message, defaultPath ?? string.Empty, vm => vm.FileFilters = filters);
    }

    /// <inheritdoc/>
    public Task<DialogResult<string>> GetFileSavePath(string? title, string message, IEnumerable<FileFilterItem> filters, string? defaultPath = null)
    {
        return GetInput<FileSaveDialogViewModel, string>(title, message, defaultPath ?? string.Empty, vm => vm.FileFilters = filters);
    }

    /// <inheritdoc/>
    public Task<DialogResult<string>> GetDirectoryPath(string? title, string message, string? defaultPath = null)
    {
        return GetInput<DirectoryDialogViewModel, string>(title, message, defaultPath ?? string.Empty);
    }

    /// <inheritdoc/>
    public async Task CustomDialog(IAwaitableDialogViewModel dialogVm)
    {
        Navigate(dialogVm);
        try { await dialogVm.DialogAwaiter; }
        finally { await NavigateBack(); }
    }

    /// <inheritdoc/>
    public async Task<TValue> CustomDialog<TValue>(IAwaitableDialogViewModel<TValue> dialogVm)
    {
        Navigate(dialogVm);
        try { return await dialogVm.DialogAwaiter; }
        finally { await NavigateBack(); }
    }

    /// <inheritdoc/>
    public async Task<DialogResult<TValue>> GetInput<TViewModel, TValue>(string? title, string message, TValue defaultValue = default!, Action<TViewModel>? initCallback = null)
        where TViewModel : IInputDialogViewModel<TValue>, new()
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
