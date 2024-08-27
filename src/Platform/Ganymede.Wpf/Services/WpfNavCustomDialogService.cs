using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Types.Extensions;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.Ganymede.Services;

/// <summary>
/// <see cref="NavigatingDialogService"/> which selectively overrides some
/// dialogs for their native WPF versions where apropriate.
/// </summary>
public class WpfNavCustomDialogService : NavigatingDialogService, IDialogService
{
    private static Task<DialogResult<string>> CallNativeItemDialog<T>(string? title, string? defaultPath, Action<T>? dialogSetupCallback, Func<T, string> resultCallback) where T : CommonItemDialog, new()
    {
        var dialog = new T()
        {
            DefaultDirectory = defaultPath,
            Title = title,
        };
        dialogSetupCallback?.Invoke(dialog);
        return Task.FromResult<DialogResult<string>>(dialog.ShowDialog() == true
            ? new(true, resultCallback(dialog))
            : new(false, string.Empty));
    }

    private static Task<DialogResult<string>> CallFileDialog<T>(string? title, IEnumerable<FileFilterItem> filters, string? defaultPath) where T : FileDialog, new()
    {
        void ConfigureDialog(FileDialog dialog)
        {
            dialog.Filter = filters.ToWin32Filter();
            if (!defaultPath.IsEmpty())
            {
                if (!Path.GetDirectoryName(defaultPath).IsEmpty())
                {
                    if (Directory.Exists(defaultPath))
                    {
                        dialog.DefaultDirectory = defaultPath;
                    }
                    else
                    {
                        dialog.DefaultDirectory = Path.GetDirectoryName(defaultPath);
                        dialog.FileName = Path.GetFileName(defaultPath);
                    }
                }
                else
                {
                    dialog.FileName = defaultPath;
                }
            }
        }
        return CallNativeItemDialog<T>(title, defaultPath, ConfigureDialog, d => d.FileName);
    }

    Task<DialogResult<string>> IDialogService.GetFileOpenPath(string? title, string message, IEnumerable<FileFilterItem> filters, string? defaultPath)
    {
        return CallFileDialog<OpenFileDialog>(title, filters, defaultPath);
    }

    Task<DialogResult<string>> IDialogService.GetFileSavePath(string? title, string message, IEnumerable<FileFilterItem> filters, string? defaultPath)
    {
        return CallFileDialog<SaveFileDialog>(title, filters, defaultPath);
    }

    Task<DialogResult<string>> IDialogService.GetDirectoryPath(string? title, string message, string? defaultPath)
    {
        return CallNativeItemDialog<OpenFolderDialog>(title, defaultPath, null, d => d.FolderName);
    }
}