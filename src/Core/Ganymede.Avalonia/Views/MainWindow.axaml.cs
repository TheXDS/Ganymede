using Avalonia.Media;
using Avalonia.ReactiveUI;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Views;

/// <summary>
/// Implements the main application view, a.k.a. the main window for the application.
/// </summary>
public partial class MainWindow :  ReactiveWindow<MainWindowViewModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
        Background = new SolidColorBrush((uint)(System.Drawing.SystemColors.Window.ToArgb() & 0x80ffffff));
    }
}