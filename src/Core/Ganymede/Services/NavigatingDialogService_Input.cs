using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Resources;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;
using St = TheXDS.Ganymede.Resources.Strings.Common;

namespace TheXDS.Ganymede.Services;

public partial class NavigatingDialogService : NavigationService<IDialogViewModel>, INavigatingDialogService
{
    /// <inheritdoc/>
    public Task<TResult> Show<TResult>(DialogTemplate template, NamedObject<TResult>[] values)
    {
        return Show<AwaitableDialogViewModel<TResult>, TResult>(template, values);
    }

    /// <inheritdoc/>
    public Task<TResult> Show<TViewModel, TResult>(DialogTemplate template, NamedObject<TResult>[] values) where TViewModel : IAwaitableDialogViewModel<TResult>, new()
    {
        NamedObject<Func<TViewModel, TResult>> GetResult(NamedObject<TResult> p) => new(_ => p.Value, p.Name);
        return Show(template, values.Select(GetResult).ToArray());
    }

    /// <inheritdoc/>
    public Task<TResult> Show<TViewModel, TResult>(DialogTemplate template, NamedObject<Func<TViewModel, TResult>>[] values) where TViewModel : IAwaitableDialogViewModel<TResult>, new()
    {
        var vm = template.Configure(new TViewModel());
        vm.Interactions.AddRange(values.Select(i => new ButtonInteraction(() => vm.Close(i.Value.Invoke(vm)), i.Name)));
        return Show(vm);
    }

    /// <inheritdoc/>
    public async Task<TResult> Show<TResult>(IAwaitableDialogViewModel<TResult> dialogVm)
    {
        Navigate(dialogVm);
        try { return await dialogVm.DialogAwaiter; }
        finally { await NavigateBack(); }
    }

    Task<bool> IDialogService.AskYn(string? title, string question)
    {
        return Show<bool>(CommonDialogTemplates.Question with { Title = title, Text = question }, [(St.Yes, true), (St.No, false)]);
    }

    Task<bool> IDialogService.AskYn(string question)
    {
        return Show<bool>(CommonDialogTemplates.Question with { Text = question }, [(St.Yes, true), (St.No, false)]);
    }

    Task<bool?> IDialogService.AskYnc(string? title, string question)
    {
        return Show<bool?>(CommonDialogTemplates.Question with { Title = title, Text = question }, [(St.Yes, true), (St.No, false), (St.Cancel, null)]);
    }

    Task<bool?> IDialogService.AskYnc(string question)
    {
        return Show<bool?>(CommonDialogTemplates.Question with { Text = question }, [(St.Yes, true), (St.No, false), (St.Cancel, null)]);
    }

    Task<DialogResult<T>> IDialogService.SelectOption<T>(DialogTemplate template, NamedObject<T>[] options)
    {
        return Show(template.Configure(new SelectionDialogViewModel<T>() { Options = options }));
    }

    Task<DialogResult<string?>> IDialogService.GetInputText(DialogTemplate template, string? defaultValue)
    {
        return Show(template.Configure(new TextInputDialogViewModel() { Value = defaultValue }));
    }

    Task<DialogResult<Credential?>> IDialogService.GetCredential(DialogTemplate template, string? defaultUser, bool isUserEditable)
    {
        return Show(template.Configure(new CredentialInputDialogViewModel() { User = defaultUser ?? string.Empty, IsUserEditable = isUserEditable }));
    }

    Task<DialogResult<string?>> IDialogService.GetFileOpenPath(DialogTemplate template, IEnumerable<FileFilterItem> filters, string? defaultPath)
    {
        return Show(template.Configure(new FileOpenDialogViewModel() { FileFilters = filters, Value = defaultPath }));
    }

    Task<DialogResult<string?>> IDialogService.GetFileSavePath(DialogTemplate template, IEnumerable<FileFilterItem> filters, string? defaultPath)
    {
        return Show(template.Configure(new FileSaveDialogViewModel() { FileFilters = filters, Value = defaultPath }));
    }

    Task<DialogResult<string?>> IDialogService.GetDirectoryPath(DialogTemplate template, string? defaultPath)
    {
        return Show(template.Configure(new DirectoryDialogViewModel() { Value = defaultPath }));
    }

    Task<DialogResult<T>> IDialogService.GetInputValue<T>(DialogTemplate template, T? minimum, T? maximum, T defaultValue)
    {
        return Show(template.Configure(new InputDialogViewModel<T>() { Minimum = minimum, Maximum = maximum, Value = defaultValue }));
    }

    Task<DialogResult<(T Min, T Max)>> IDialogService.GetInputRange<T>(DialogTemplate template, T? minimum, T? maximum, T defaultRangeStart, T defaultRangeEnd)
    {
        return Show(template.Configure(new RangeInputDialogViewModel<T>()
        { 
            Minimum = minimum,
            Maximum = maximum,
            RangeStart = defaultRangeStart,
            RangeEnd = defaultRangeEnd
        }));
    }
}
