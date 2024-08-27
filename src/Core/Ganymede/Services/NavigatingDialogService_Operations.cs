using System.Drawing;
using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ViewModels;
using St = TheXDS.Ganymede.Resources.Strings.Common;

namespace TheXDS.Ganymede.Services;

public partial class NavigatingDialogService
{
    /// <inheritdoc/>
    public async Task<bool> RunOperation(string? title, Func<CancellationToken, IProgress<ProgressReport>, Task> operation)
    {
        CancellationTokenSource ct = new();
        ButtonInteraction cancel = new(ct.Cancel, St.Cancel) { IsPrimary = true };
        var (vm, progress) = CreateOperationVm(title);
        vm.Interactions.Add(cancel);
        Navigate(vm);
        var task = operation.Invoke(ct.Token, progress);
        try { await task; }
        catch (Exception ex) { await ((IDialogService)this).Error(ex); }
        finally { await NavigateBack(); }
        return !(ct.IsCancellationRequested || task.IsFaulted);
    }

    /// <inheritdoc/>
    public async Task<T> RunOperation<T>(string? title, Func<IProgress<ProgressReport>, Task<T>> operation)
    {
        var (vm, progress) = CreateOperationVm(title);
        Navigate(vm);
        var task = operation.Invoke(progress);
        try { return await task; }
        catch (Exception ex) { await ((IDialogService)this).Error(ex); return default!; }
        finally { await NavigateBack(); }
    }

    /// <inheritdoc/>
    public async Task<DialogResult<T>> RunOperation<T>(string? title, Func<CancellationToken, IProgress<ProgressReport>, Task<T>> operation)
    {
        CancellationTokenSource ct = new();
        ButtonInteraction cancel = new(ct.Cancel, St.Cancel) { IsPrimary = true };
        var (vm, progress) = CreateOperationVm(title);
        vm.Interactions.Add(cancel);
        Navigate(vm);
        try
        {
            return new(true, await operation.Invoke(ct.Token, progress));
        }
        catch (Exception ex)
        {
            await ((IDialogService)this).Error(ex);
            return new(false, default!);
        }
        finally
        {
            await NavigateBack();
        }
    }

    /// <inheritdoc/>
    public async Task RunOperation(string? title, Func<IProgress<ProgressReport>, Task> operation)
    {
        var (vm, progress) = CreateOperationVm(title);
        Navigate(vm);
        var task = operation.Invoke(progress);
        try { await task; }
        catch (Exception ex) { await ((IDialogService)this).Error(ex); }
        finally { await NavigateBack(); }
    }

    private static (OperationDialogViewModel viewModel, IProgress<ProgressReport> progress) CreateOperationVm(string? title)
    {
        var ivm = new OperationDialogViewModel
        {
            Title = title,
            Message = string.Empty,
            Progress = double.NaN,
            Icon = "⚙",
            IconBgColor = Color.DarkGray,
        };
        void ReportProgress(ProgressReport p)
        {
            ivm.Progress = p.Progress;
            if (p.Status is not null)
            {
                ivm.Message = p.Status;
            }
        }
        return (ivm, new Progress<ProgressReport>(ReportProgress));
    }
}
