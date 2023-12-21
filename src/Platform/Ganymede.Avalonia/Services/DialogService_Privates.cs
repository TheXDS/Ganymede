using System.Windows.Input;
using Avalonia.Media;
using ReactiveUI;
using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Resources.Strings;
using TheXDS.Ganymede.ViewModels.Dialogs;

namespace TheXDS.Ganymede.Services;

public partial class DialogService
{
    private ICommand CloseDialogCommand<T>(TaskCompletionSource<T> task, T result)
    {
        return ReactiveCommand.Create(() =>
        {
            Content.Router.NavigationStack.Clear();
            task.SetResult(result);
        });
    }

    private async Task SimpleMessage(string icon, IBrush brush, string? title, string message)
    {
        _ = await GetButtonValue<object?>(icon, brush, title, message, (Common.Ok, null));
    }

    private Task<T> GetButtonValue<T>(string icon, IBrush brush, string? title, string message,
        params (string Text, T Value)[] interactions)
    {
        TaskCompletionSource<T> dialogAwaiter = new();
        var vm = new MessageDialogViewModel
        {
            Title = title,
            Message = message,
            Icon = icon,
            IconBrush = brush,
            HostScreen = Content,
        };

        foreach (var j in interactions)
        {
            vm.Interactions.Add(new(CloseDialogCommand(dialogAwaiter, j.Value), j.Text));
        }

        Content.Router.NavigateAndReset.Execute(vm);
        return dialogAwaiter.Task;
    }
    
    private T CreateInputDialogVm<T>(string? title, string message, TaskCompletionSource<bool> dialogAwaiter) where T : DialogViewModelBase, new()
    {
        return new()
        {
            Title = title,
            Message = message,
            HostScreen = Content,
            Icon = "✍",
            IconBrush = Brushes.DarkGray,
            Interactions =
            {
                new(ReactiveCommand.Create(() =>
                {
                    Content.Router.NavigationStack.Clear();
                    dialogAwaiter.SetResult(true);
                }), Common.Ok),
                new(ReactiveCommand.Create(() =>
                {
                    Content.Router.NavigationStack.Clear();
                    dialogAwaiter.SetResult(false);
                }), Common.Cancel)
            }
        };
    }

    private void CreateOperationVm(string? title, out OperationDialogViewModel vm, out Progress<ProgressReport> progress)
    {
        var ivm = vm = new OperationDialogViewModel
        {
            Title = title,
            Message = string.Empty,
            Progress = double.NaN,
            Icon = "⚙",
            IconBrush = Brushes.DarkGray,
            HostScreen = Content,
        };
        progress = new(p =>
        {
            ivm.Progress = p.Progress;
            if (p.Status is not null) ivm.Message = p.Status;
        });
    }
}