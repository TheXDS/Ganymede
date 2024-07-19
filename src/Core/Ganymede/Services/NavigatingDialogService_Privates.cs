using System.Drawing;
using TheXDS.Ganymede.Resources.Strings;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Component;

namespace TheXDS.Ganymede.Services;

/// <summary>
/// Implements a specialized navigation service that includes dialog services.
/// </summary>
public partial class NavigatingDialogService : NavigationService<IDialogViewModel>, INavigatingDialogService
{
    private async Task SimpleMessage(string icon, Color brush, string? title, string message)
    {
        _ = await GetButtonValue<object?>(icon, brush, title, message, (Common.Ok, null));
    }

    private Task<T> GetButtonValue<T>(string icon, Color color, string? title, string message, params (string Text, T Value)[] interactions)
    {
        TaskCompletionSource<T> dialogAwaiter = new();
        var vm = new DialogViewModel
        {
            Title = title,
            Message = message,
            Icon = icon,
            IconBgColor = color,
        };
        foreach (var (text, value) in interactions)
        {
            vm.Interactions.Add(new(CloseDialogCommand(dialogAwaiter, value), text) { IsPrimary = interactions.Length == 1 });
        }
        Navigate(vm);
        return dialogAwaiter.Task;
    }

    private SimpleCommand CloseDialogCommand<T>(TaskCompletionSource<T> task, T result)
    {
        return new(async () =>
        {
            await NavigateBack();
            task.SetResult(result);
        });
    }

    private T CreateInputDialogVm<T>(string? title, string message, TaskCompletionSource<bool> dialogAwaiter) where T : IDialogViewModel, new()
    {
        return new()
        {
            Title = title,
            Message = message,
            Icon = "✍",
            IconBgColor = Color.DarkGray,
            Interactions =
            {
                new(CloseDialogCommand(dialogAwaiter, true), Common.Ok) { IsPrimary = true },
                new(CloseDialogCommand(dialogAwaiter, false), Common.Cancel)
            }
        };
    }
}
