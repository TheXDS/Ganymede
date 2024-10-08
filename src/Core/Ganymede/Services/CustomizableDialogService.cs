using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Resources.Exceptions;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Types;

namespace TheXDS.Ganymede.Services;

/// <summary>
/// Implements a dialog service where specific callback implementations can be
/// replaced by alternative services.
/// </summary>
public class CustomizableDialogService : IDialogService
{
    private IDialogService GetService(IDialogService? overrideService) => overrideService ?? DefaultImplementation ?? throw new UndefinedBehaviorException();

    /// <summary>
    /// Defines the default implementation for all methods in the
    /// <see cref="IDialogService"/> interface.
    /// </summary>
    public IDialogService? DefaultImplementation { get; set; }

    /// <summary>
    /// Defines the implementation override for the
    /// <see cref="IDialogService.AskYn(string)"/> method and its overloads.
    /// </summary>
    public IDialogService? AskYnOverride { get; set; }

    /// <summary>
    /// Defines the implementation override for the
    /// <see cref="IDialogService.AskYnc(string)"/> method and its overloads.
    /// </summary>
    public IDialogService? AskYncOverride { get; set; }

    /// <summary>
    /// Defines the implementation override for the
    /// <see cref="IDialogService.Error(string)"/> method and its overloads.
    /// </summary>
    public IDialogService? ErrorOverride { get; set; }

    /// <summary>
    /// Defines the implementation override for the
    /// <see cref="IDialogService.GetCredential(DialogTemplate, string?, bool)"/>
    /// method and its overloads.
    /// </summary>
    public IDialogService? GetCredentialOverride { get; set; }

    /// <summary>
    /// Defines the implementation override for the
    /// <see cref="IDialogService.GetDirectoryPath(DialogTemplate, string?)"/>
    /// method and its overloads.
    /// </summary>
    public IDialogService? GetDirectoryPathOverride { get; set; }

    /// <summary>
    /// Defines the implementation override for the
    /// <see cref="IDialogService.GetFileOpenPath(DialogTemplate, IEnumerable{FileFilterItem}, string?)"/>
    /// method and its overloads.
    /// </summary>
    public IDialogService? GetFileOpenPathOverride { get; set; }

    /// <summary>
    /// Defines the implementation override for the
    /// <see cref="IDialogService.GetFileSavePath(DialogTemplate, IEnumerable{FileFilterItem}, string?)"/>
    /// method and its overloads.
    /// </summary>
    public IDialogService? GetFileSavePathOverride { get; set; }
   
    /// <summary>
    /// Defines the implementation override for the
    /// <see cref="IDialogService.GetInputRange{T}(DialogTemplate, T?, T?, T, T)"/>
    /// method and its overloads.
    /// </summary>
    public IDialogService? GetInputRangeOverride { get; set; }

    /// <summary>
    /// Defines the implementation override for the
    /// <see cref="IDialogService.GetInputText(DialogTemplate, string?)"/>
    /// method and its overloads.
    /// </summary>
    public IDialogService? GetInputTextOverride { get; set; }

    /// <summary>
    /// Defines the implementation override for the
    /// <see cref="IDialogService.GetInputValue{T}(DialogTemplate, T?, T?, T)"/>
    /// method and its overloads.
    /// </summary>
    public IDialogService? GetInputValueOverride { get; set; }

    /// <summary>
    /// Defines the implementation override for the
    /// <see cref="IDialogService.Message(string)"/>
    /// method and its overloads.
    /// </summary>
    public IDialogService? MessageOverride { get; set; }

    /// <summary>
    /// Defines the implementation override for the
    /// <see cref="IDialogService.RunOperation(Action{CancellationToken, IProgress{ProgressReport}})"/>
    /// method and its overloads.
    /// </summary>
    public IDialogService? RunOperationOverride { get; set; }

    /// <summary>
    /// Defines the implementation override for the
    /// <see cref="IDialogService.SelectOption{T}(DialogTemplate, NamedObject{T}[])"/>
    /// method and its overloads.
    /// </summary>
    public IDialogService? SelectOptionOverride { get; set; }

    /// <summary>
    /// Defines the implementation override for the
    /// <see cref="IDialogService.Show(DialogTemplate)"/>
    /// method and its overloads.
    /// </summary>
    public IDialogService? ShowOverride { get; set; }

    /// <summary>
    /// Defines the implementation override for the
    /// <see cref="IDialogService.Warning(string)"/>
    /// method and its overloads.
    /// </summary>
    public IDialogService? WarningOverride { get; set; }

    /// <summary>
    /// Defines the implementation override for the
    /// <see cref="IDialogService.Wizard{TState}(Func{TState, IWizardViewModel{TState}}[])"/>
    /// method and its overloads.
    /// </summary>
    public IDialogService? WizardOverride { get; set; }

    Task<bool> IDialogService.AskYn(string question) => GetService(AskYnOverride).AskYn(question);
    Task<bool> IDialogService.AskYn(string? title, string question) => GetService(AskYnOverride).AskYn(title, question);
    Task<bool?> IDialogService.AskYnc(string question) => GetService(AskYncOverride).AskYnc(question);
    Task<bool?> IDialogService.AskYnc(string? title, string question) => GetService(AskYncOverride).AskYnc(title, question);
    Task IDialogService.Error(string message) => GetService(ErrorOverride).Error(message);
    Task IDialogService.Error(Exception exception) => GetService(ErrorOverride).Error(exception);
    Task IDialogService.Error(string? title, string message) => GetService(ErrorOverride).Error(title, message);
    Task<DialogResult<Credential?>> IDialogService.GetCredential(DialogTemplate template, string? defaultUser, bool isUserEditable) => GetService(GetCredentialOverride).GetCredential(template, defaultUser, isUserEditable);
    Task<DialogResult<string?>> IDialogService.GetDirectoryPath(DialogTemplate template, string? defaultPath) => GetService(GetDirectoryPathOverride).GetDirectoryPath(template, defaultPath);
    Task<DialogResult<string?>> IDialogService.GetFileOpenPath(DialogTemplate template, IEnumerable<FileFilterItem> filters, string? defaultPath) => GetService(GetFileOpenPathOverride).GetFileOpenPath(template, filters, defaultPath);
    Task<DialogResult<string?>> IDialogService.GetFileSavePath(DialogTemplate template, IEnumerable<FileFilterItem> filters, string? defaultPath) => GetService(GetFileSavePathOverride).GetFileSavePath(template, filters, defaultPath);
    Task<DialogResult<(T Min, T Max)>> IDialogService.GetInputRange<T>(DialogTemplate template, T? minimum, T? maximum, T defaultRangeStart, T defaultRangeEnd) => GetService(GetInputRangeOverride).GetInputRange(template, minimum, maximum, defaultRangeStart, defaultRangeEnd);
    Task<DialogResult<string?>> IDialogService.GetInputText(DialogTemplate template, string? defaultValue) => GetService(GetInputTextOverride).GetInputText(template, defaultValue);
    Task<DialogResult<T>> IDialogService.GetInputValue<T>(DialogTemplate template, T? minimum, T? maximum, T defaultValue) => GetService(GetInputValueOverride).GetInputValue(template, minimum, maximum, defaultValue);
    Task IDialogService.Message(string message) => GetService(MessageOverride).Message(message);
    Task IDialogService.Message(string? title, string message) => GetService(MessageOverride).Message(title, message);
    Task IDialogService.RunOperation(string? title, Func<IProgress<ProgressReport>, Task> operation) => GetService(RunOperationOverride).RunOperation(title, operation);
    Task<T> IDialogService.RunOperation<T>(string? title, Func<IProgress<ProgressReport>, Task<T>> operation) => GetService(RunOperationOverride).RunOperation(title, operation);
    Task<bool> IDialogService.RunOperation(string? title, Func<CancellationToken, IProgress<ProgressReport>, Task> operation) => GetService(RunOperationOverride).RunOperation(title, operation);
    Task<DialogResult<T>> IDialogService.RunOperation<T>(string? title, Func<CancellationToken, IProgress<ProgressReport>, Task<T>> operation) => GetService(RunOperationOverride).RunOperation(title, operation);
    Task<DialogResult<T>> IDialogService.SelectOption<T>(DialogTemplate template, params NamedObject<T>[] options) => GetService(SelectOptionOverride).SelectOption(template, options);
    Task<TResult> IDialogService.Show<TResult>(DialogTemplate template, NamedObject<TResult>[] values) => GetService(ShowOverride).Show(template, values);
    Task<TResult> IDialogService.Show<TViewModel, TResult>(DialogTemplate template, NamedObject<TResult>[] values) => GetService(ShowOverride).Show<TViewModel, TResult>(template, values);
    Task<TResult> IDialogService.Show<TViewModel, TResult>(DialogTemplate template, NamedObject<Func<TViewModel, TResult>>[] values) => GetService(ShowOverride).Show(template, values);
    Task<TResult> IDialogService.Show<TResult>(IAwaitableDialogViewModel<TResult> dialogVm) => GetService(ShowOverride).Show(dialogVm);
    Task IDialogService.Show(DialogTemplate template) => GetService(ShowOverride).Show(template);
    Task IDialogService.Show<TViewModel>(DialogTemplate template) => GetService(ShowOverride).Show<TViewModel>(template);
    Task IDialogService.Show(IAwaitableDialogViewModel dialogVm) => GetService(ShowOverride).Show(dialogVm);
    Task IDialogService.Warning(string message) => GetService(WarningOverride).Warning(message);
    Task IDialogService.Warning(string? title, string message) => GetService(WarningOverride).Warning(title, message);
    Task<bool> IDialogService.Wizard<TState>(TState state, params Func<TState, IWizardViewModel<TState>>[] viewModels) => GetService(WizardOverride).Wizard(state, viewModels);
}
