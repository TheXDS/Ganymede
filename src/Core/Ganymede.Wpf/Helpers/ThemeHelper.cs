using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;

namespace TheXDS.Ganymede.Helpers;

/// <summary>
/// Implements Windows 10+ theme support methods.
/// </summary>
public class ThemeHelper
{
    private const string RegistryKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";

    private const string RegistryValueName = "AppsUseLightTheme";

    private enum WindowsTheme
    {
        Light,
        Dark,
        HighContrast
    }

    /// <summary>
    /// 
    /// </summary>
    public void WatchTheme()
    {
        WindowsIdentity currentUser = WindowsIdentity.GetCurrent();
        var query = string.Format(
            CultureInfo.InvariantCulture,
            @"SELECT * FROM RegistryValueChangeEvent WHERE Hive = 'HKEY_USERS' AND KeyPath = '{0}\\{1}' AND ValueName = '{2}'",
            currentUser.User?.Value ?? "S-1-5-32-544",
            RegistryKeyPath.Replace(@"\", @"\\"),
            RegistryValueName);





        bool isHighContrast = SystemParameters.HighContrast;
        SystemParameters.StaticPropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(SystemParameters.HighContrast))
            {
                bool newIsHighContrast = SystemParameters.HighContrast;
            }
        };
        try
        {
            var watcher = new ManagementEventWatcher(query);
            watcher.EventArrived += (sender, args) =>
            {
                WindowsTheme newWindowsTheme = GetWindowsTheme();
                // React to new theme
            };

            // Start listening for events
            watcher.Start();
        }
        catch
        {
            // This can fail on Windows 7
        }

        var initialTheme = GetWindowsTheme();
    }

    private static WindowsTheme GetWindowsTheme()
    {
        using var key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath);
        var registryValueObject = key?.GetValue(RegistryValueName);
        if (registryValueObject == null)
        {
            return WindowsTheme.Light;
        }
        return ((int)registryValueObject) > 0 ? WindowsTheme.Light : WindowsTheme.Dark;
    }
}
