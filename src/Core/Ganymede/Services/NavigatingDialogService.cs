using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Resources;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Resources.Strings;
using static TheXDS.MCART.Resources.Strings.Composition;
using St = TheXDS.Ganymede.Resources.Strings.Common;

namespace TheXDS.Ganymede.Services;

/// <summary>
/// Implements a specialized navigation service that includes dialog services.
/// </summary>
public partial class NavigatingDialogService : NavigationService<IDialogViewModel>, INavigatingDialogService
{
    /// <inheritdoc/>
    public Task Show(DialogTemplate template)
    {
        return Show<object?>(template, [(St.Ok, (object?)null)]);
    }

    /// <inheritdoc/>
    public Task Show<TViewModel>(DialogTemplate template) where TViewModel : IAwaitableDialogViewModel, new()
    {
        return Show(template.Configure(new TViewModel()));
    }

    /// <inheritdoc/>
    public async Task Show(IAwaitableDialogViewModel dialogVm)
    {
        Navigate(dialogVm);
        try { await dialogVm.DialogAwaiter; }
        finally { await NavigateBack(); }
    }

    Task IDialogService.Message(string message)
    {
        return Show(CommonDialogTemplates.Message with { Text = message });
    }

    Task IDialogService.Message(string? title, string message)
    {
        return Show(CommonDialogTemplates.Message with { Title = title, Text = message });
    }

    Task IDialogService.Warning(string message)
    {
        return Show(CommonDialogTemplates.Warning with { Text = message });
    }

    Task IDialogService.Warning(string? title, string message)
    {
        return Show(CommonDialogTemplates.Warning with { Title = title, Text = message });
    }

    Task IDialogService.Error(string message)
    {
        return Show(CommonDialogTemplates.Error with { Text = message });
    }

    Task IDialogService.Error(string? title, string message)
    {
        return Show(CommonDialogTemplates.Error with { Title = title, Text = message });
    }

    Task IDialogService.Error(Exception exception)
    {
        return Show(CommonDialogTemplates.Error with { Text = ExDump(exception, exDumpOptions) });
    }


#if DEBUG
    private const ExDumpOptions exDumpOptions = ExDumpOptions.All;
#else
    private const ExDumpOptions exDumpOptions = ExDumpOptions.Message;
#endif
}
