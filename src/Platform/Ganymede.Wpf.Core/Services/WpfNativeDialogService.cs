using Microsoft.Win32;
using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ViewModels;
using TheXDS.Ganymede.Views.Dialogs;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Math;
using TheXDS.MCART.Resources.Strings;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Resources.Strings.Composition;
using St = TheXDS.Ganymede.Resources.Strings.Common;

namespace TheXDS.Ganymede.Services;

/// <summary>
/// Implements a dialog service for WPF apps that uses native Microsoft Windows
/// dialogs where available, generating a generic dialog window otherwise.
/// </summary>
public class WpfNativeDialogService : IDialogService
{
    private class ProgressReportAdapter(IProgressDialog dlg) : IProgress<ProgressReport>
    {
        private readonly IProgressDialog dlg = dlg;
        private CancellationTokenSource? cts;

        public CancellationToken CancellationToken => (cts ??= new CancellationTokenSource()).Token;

        public void Report(ProgressReport value)
        {
            if (value.Progress.IsValid())
            {
                dlg.Value = (int)value.Progress;
            }
            if (value.Status is not null)
            {
                dlg.Line1 = value.Status;
            }
            if (cts is not null && dlg.HasUserCancelled) cts.Cancel();
        }
    }

#if DEBUG
    private const ExDumpOptions exDumpOptions = ExDumpOptions.All;
#else
    private const ExDumpOptions exDumpOptions = ExDumpOptions.Message;
#endif

    Task<bool> IDialogService.AskYn(string question)
    {
        return ((IDialogService)this).AskYn(question, string.Empty);
    }

    Task<bool> IDialogService.AskYn(string? title, string question)
    {
        return Task.FromResult(MessageBox.Show(question, title ?? string.Empty, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes);
    }

    Task<bool?> IDialogService.AskYnc(string question)
    {
        return ((IDialogService)this).AskYnc(question, string.Empty);
    }

    Task<bool?> IDialogService.AskYnc(string? title, string question)
    {
        return Task.FromResult<bool?>(MessageBox.Show(question, title ?? string.Empty, MessageBoxButton.YesNoCancel, MessageBoxImage.Question) switch
        {
            MessageBoxResult.Yes => true,
            MessageBoxResult.No => false,
            _ => null
        });
    }

    Task IDialogService.Error(string message)
    {
        return ((IDialogService)this).Error(St.Error, message);
    }

    Task IDialogService.Error(Exception exception)
    {
        return ((IDialogService)this).Error(ExDump(exception, exDumpOptions));
    }

    Task IDialogService.Error(string? title, string message)
    {
        MessageBox.Show(message, title ?? string.Empty, MessageBoxButton.OK, MessageBoxImage.Error);
        return Task.CompletedTask;
    }

    Task<DialogResult<Credential?>> IDialogService.GetCredential(DialogTemplate template, string? defaultUser, bool isUserEditable)
    {
        var result = CredentialDialog.GetCredentials(new() { DefaultUser = defaultUser, Title = template.Title, Message = template.Text });
        return Task.FromResult<DialogResult<Credential?>>(new(result.HasValue, result.HasValue ? new(result.Value.Username, result.Value.Password) : null));
    }

    Task<DialogResult<string?>> IDialogService.GetDirectoryPath(DialogTemplate template, string? defaultPath)
    {
        var dlg = new OpenFolderDialog()
        {
            DefaultDirectory = defaultPath ?? string.Empty,
            Title = template.Title ?? template.Text,
            ValidateNames = true,
        };
        return Task.FromResult<DialogResult<string?>>(dlg.ShowDialog() == true ? new(true, dlg.SafeFolderName) : new(false, null));
    }

    Task<DialogResult<string?>> IDialogService.GetFileOpenPath(DialogTemplate template, IEnumerable<FileFilterItem> filters, string? defaultPath)
    {
        var dlg = new OpenFileDialog()
        {
            FileName = defaultPath ?? string.Empty,
            Title = template.Title ?? template.Text,
            ValidateNames = true,
        };
        return Task.FromResult<DialogResult<string?>>(dlg.ShowDialog() == true ? new(true, dlg.SafeFileName) : new(false, null));
    }

    Task<DialogResult<string[]?>> IDialogService.GetFilesOpenPath(DialogTemplate template, IEnumerable<FileFilterItem> filters)
    {
        var dlg = new OpenFileDialog()
        {
            FileName = string.Empty,
            Title = template.Title ?? template.Text,
            ValidateNames = true,
        };
        return Task.FromResult<DialogResult<string[]?>>(dlg.ShowDialog() == true ? new(true, dlg.FileNames) : new(false, null));
    }

    Task<DialogResult<string?>> IDialogService.GetFileSavePath(DialogTemplate template, IEnumerable<FileFilterItem> filters, string? defaultPath)
    {
        var dlg = new SaveFileDialog()
        {
            FileName = defaultPath ?? string.Empty,
            Title = template.Title ?? template.Text,
            ValidateNames = true,
        };
        return Task.FromResult<DialogResult<string?>>(dlg.ShowDialog() == true ? new(true, dlg.SafeFileName) : new(false, null));
    }

    Task<DialogResult<(T Min, T Max)>> IDialogService.GetInputRange<T>(DialogTemplate template, T? minimum, T? maximum, T defaultRangeStart, T defaultRangeEnd)
    {
        return ((IDialogService)this).Show(template.Configure(new RangeInputDialogViewModel<T>()
        {
            Minimum = minimum,
            Maximum = maximum,
            RangeStart = defaultRangeStart,
            RangeEnd = defaultRangeEnd
        }));
    }

    Task<DialogResult<string?>> IDialogService.GetInputText(DialogTemplate template, string? defaultValue)
    {
        return ((IDialogService)this).Show(template.Configure(new TextInputDialogViewModel() { Value = defaultValue }));
    }

    Task<DialogResult<T>> IDialogService.GetInputValue<T>(DialogTemplate template, T? minimum, T? maximum, T defaultValue)
    {
        return ((IDialogService)this).Show(template.Configure(new InputDialogViewModel<T>() { Minimum = minimum, Maximum = maximum, Value = defaultValue }));
    }

    Task IDialogService.Message(string message)
    {
        return ((IDialogService)this).Message(null, message);
    }

    Task IDialogService.Message(string? title, string message)
    {
        MessageBox.Show(message, title ?? string.Empty, MessageBoxButton.OK, MessageBoxImage.Error);
        return Task.CompletedTask;
    }

    Task IDialogService.RunOperation(string? title, Func<IProgress<ProgressReport>, Task> operation)
    {
        return ProgressDialog.Run(dlg => operation(new ProgressReportAdapter(dlg)));
    }

    Task<T> IDialogService.RunOperation<T>(string? title, Func<IProgress<ProgressReport>, Task<T>> operation)
    {
        return ProgressDialog.Run(dlg => operation(new ProgressReportAdapter(dlg)));
    }

    Task<bool> IDialogService.RunOperation(string? title, Func<CancellationToken, IProgress<ProgressReport>, Task> operation)
    {
        return ProgressDialog.Run(new ProgressDialogProperties { CancelButton = true }, async dlg =>
        {
            var adapter = new ProgressReportAdapter(dlg);
            await operation(adapter.CancellationToken, adapter);
            return !adapter.CancellationToken.IsCancellationRequested;
        });
    }

    Task<DialogResult<T>> IDialogService.RunOperation<T>(string? title, Func<CancellationToken, IProgress<ProgressReport>, Task<T>> operation)
    {
        return ProgressDialog.Run<DialogResult<T>>(new ProgressDialogProperties { CancelButton = true }, async dlg =>
        {
            var adapter = new ProgressReportAdapter(dlg);
            var result = await operation(adapter.CancellationToken, adapter);
            return new(!adapter.CancellationToken.IsCancellationRequested, result);
        });
    }

    Task<DialogResult<T>> IDialogService.SelectOption<T>(DialogTemplate template, params NamedObject<T>[] options)
    {
        return ((IDialogService)this).Show(template.Configure(new SelectionDialogViewModel<T>() { Options = options }));
    }

    Task<TResult> IDialogService.Show<TResult>(DialogTemplate template, NamedObject<TResult>[] values)
    {
        return ((IDialogService)this).Show<AwaitableDialogViewModel<TResult>, TResult>(template, values);
    }

    Task<TResult> IDialogService.Show<TViewModel, TResult>(DialogTemplate template, NamedObject<TResult>[] values)
    {
        NamedObject<Func<TViewModel, TResult>> GetResult(NamedObject<TResult> p) => new(_ => p.Value, p.Name);
        return ((IDialogService)this).Show(template, values.Select(GetResult).ToArray());
    }

    Task<TResult> IDialogService.Show<TViewModel, TResult>(DialogTemplate template, NamedObject<Func<TViewModel, TResult>>[] values)
    {
        var vm = template.Configure(new TViewModel());
        vm.Interactions.AddRange(values.Select(i => new ButtonInteraction(() => vm.Close(i.Value.Invoke(vm)), i.Name)));
        return ((IDialogService)this).Show(vm);
    }

    async Task<TResult> IDialogService.Show<TResult>(IAwaitableDialogViewModel<TResult> dialogVm)
    {
        var window = CreateWindow(dialogVm);
        window.Show();
        try { return await dialogVm.DialogAwaiter; }
        finally { window.Close(); }
    }

    Task IDialogService.Show(DialogTemplate template)
    {
        return ((IDialogService)this).Show<object?>(template, [(St.Ok, (object?)null)]);
    }

    Task IDialogService.Show<TViewModel>(DialogTemplate template)
    {
        return ((IDialogService)this).Show(template.Configure(new TViewModel()));
    }

    async Task IDialogService.Show(IAwaitableDialogViewModel dialogVm)
    {
        var window = CreateWindow(dialogVm);
        window.Show();
        try { await dialogVm.DialogAwaiter; }
        finally { window.Close(); }
    }

    Task IDialogService.Warning(string message)
    {
        return ((IDialogService)this).Warning(St.Warning, message);
    }

    Task IDialogService.Warning(string? title, string message)
    {
        MessageBox.Show(message, title ?? string.Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
        return Task.CompletedTask;
    }

    async Task<bool> IDialogService.Wizard<TState>(TState state, params Func<TState, IWizardViewModel<TState>>[] viewModels)
    {
        var dlg = new DialogView();
        var window = new Window()
        {
            SizeToContent = SizeToContent.WidthAndHeight,
            ResizeMode = ResizeMode.NoResize
        };
        window.Show();
        var i = 0;
        while (i < viewModels.Length)
        {
            var vm = viewModels[i].Invoke(state);
            vm.State ??= state;
            vm.Icon ??= "\xD83E\xDE84";
            vm.IconBgColor ??= System.Drawing.Color.MediumPurple;

            dlg.DataContext = vm;
            window.Content = dlg;

            switch (await vm.DialogAwaiter)
            {
                case WizardAction.Cancel: window.Close(); return false;
                case WizardAction.Back when i > 0: i--; break;
                case WizardAction.Next when i < viewModels.Length: i++; break;
            }
        }
        window.Close();
        return true;
    }

    private static Window CreateWindow(IDialogViewModel viewModel)
    {
        return new Window()
        {
            Content = new DialogView() { DataContext = viewModel },
            SizeToContent = SizeToContent.WidthAndHeight,
            ResizeMode = ResizeMode.NoResize
        };
    }
}
