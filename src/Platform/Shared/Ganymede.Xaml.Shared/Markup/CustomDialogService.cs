using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Services;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.Ganymede.Markup;

/// <summary>
/// Implements a markup extension that helps define a custom dialog service.
/// </summary>
public sealed partial class CustomDialogService
{
    /// <summary>
    /// Defines the default implementation for all methods in the
    /// <see cref="IDialogService"/> interface.
    /// </summary>
    public object? DefaultImplementation { get; set; }

    /// <summary>
    /// Defines the implementation override for the
    /// <see cref="IDialogService.AskYn(string)"/> method and its overloads.
    /// </summary>
    public object? AskYnOverride { get; set; }

    /// <summary>
    /// Defines the implementation override for the
    /// <see cref="IDialogService.AskYnc(string)"/> method and its overloads.
    /// </summary>
    public object? AskYncOverride { get; set; }

    /// <summary>
    /// Defines the implementation override for the
    /// <see cref="IDialogService.Error(string)"/> method and its overloads.
    /// </summary>
    public object? ErrorOverride { get; set; }

    /// <summary>
    /// Defines the implementation override for the
    /// <see cref="IDialogService.GetCredential(DialogTemplate, string?, bool)"/>
    /// method and its overloads.
    /// </summary>
    public object? GetCredentialOverride { get; set; }

    /// <summary>
    /// Defines the implementation override for the
    /// <see cref="IDialogService.GetDirectoryPath(DialogTemplate, string?)"/>
    /// method and its overloads.
    /// </summary>
    public object? GetDirectoryPathOverride { get; set; }

    /// <summary>
    /// Defines the implementation override for the
    /// <see cref="IDialogService.GetFileOpenPath(DialogTemplate, IEnumerable{FileFilterItem}, string?)"/>
    /// method and its overloads.
    /// </summary>
    public object? GetFileOpenPathOverride { get; set; }

    /// <summary>
    /// Defines the implementation override for the
    /// <see cref="IDialogService.GetFileSavePath(DialogTemplate, IEnumerable{FileFilterItem}, string?)"/>
    /// method and its overloads.
    /// </summary>
    public object? GetFileSavePathOverride { get; set; }

    /// <summary>
    /// Defines the implementation override for the
    /// <see cref="IDialogService.GetInputRange{T}(DialogTemplate, T?, T?, T, T)"/>
    /// method and its overloads.
    /// </summary>
    public object? GetInputRangeOverride { get; set; }

    /// <summary>
    /// Defines the implementation override for the
    /// <see cref="IDialogService.GetInputText(DialogTemplate, string?)"/>
    /// method and its overloads.
    /// </summary>
    public object? GetInputTextOverride { get; set; }

    /// <summary>
    /// Defines the implementation override for the
    /// <see cref="IDialogService.GetInputValue{T}(DialogTemplate, T?, T?, T)"/>
    /// method and its overloads.
    /// </summary>
    public object? GetInputValueOverride { get; set; }

    /// <summary>
    /// Defines the implementation override for the
    /// <see cref="IDialogService.Message(string)"/>
    /// method and its overloads.
    /// </summary>
    public object? MessageOverride { get; set; }

    /// <summary>
    /// Defines the implementation override for the
    /// <see cref="IDialogService.RunOperation(Action{CancellationToken, IProgress{ProgressReport}})"/>
    /// method and its overloads.
    /// </summary>
    public object? RunOperationOverride { get; set; }

    /// <summary>
    /// Defines the implementation override for the
    /// <see cref="IDialogService.SelectOption{T}(DialogTemplate, NamedObject{T}[])"/>
    /// method and its overloads.
    /// </summary>
    public object? SelectOptionOverride { get; set; }

    /// <summary>
    /// Defines the implementation override for the
    /// <see cref="IDialogService.Show(DialogTemplate)"/>
    /// method and its overloads.
    /// </summary>
    public object? ShowOverride { get; set; }

    /// <summary>
    /// Defines the implementation override for the
    /// <see cref="IDialogService.Warning(string)"/>
    /// method and its overloads.
    /// </summary>
    public object? WarningOverride { get; set; }

    /// <summary>
    /// Defines the implementation override for the
    /// <see cref="IDialogService.Wizard{TState}(Func{TState, IWizardViewModel{TState}}[])"/>
    /// method and its overloads.
    /// </summary>
    public object? WizardOverride { get; set; }

    /// <inheritdoc/>
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return new CustomizableDialogService()
        {
            DefaultImplementation = Parse(DefaultImplementation),
            AskYnOverride = Parse(AskYnOverride),
            AskYncOverride = Parse(AskYncOverride),
            ErrorOverride = Parse(ErrorOverride),
            GetCredentialOverride = Parse(GetCredentialOverride),
            GetDirectoryPathOverride = Parse(GetDirectoryPathOverride),
            GetFileOpenPathOverride = Parse(GetFileOpenPathOverride),
            GetFileSavePathOverride = Parse(GetFileSavePathOverride),
            GetInputRangeOverride = Parse(GetInputRangeOverride),
            GetInputTextOverride = Parse(GetInputTextOverride),
            GetInputValueOverride = Parse(GetInputValueOverride),
            MessageOverride = Parse(MessageOverride),
            RunOperationOverride = Parse(RunOperationOverride),
            SelectOptionOverride = Parse(SelectOptionOverride),
            ShowOverride = Parse(ShowOverride),
            WarningOverride = Parse(WarningOverride),
            WizardOverride = Parse(WizardOverride)
        };
    }

    private static IDialogService? Parse(object? value)
    {
        return value switch
        {
            IDialogService svc => svc,
            Type t when t.Implements<IDialogService>() && t.IsInstantiable() => (IDialogService?)Activator.CreateInstance(t),
            null => null,
            _ => throw Errors.InvalidValue(nameof(value)),
        };
    }
}