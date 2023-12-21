using System.Collections.ObjectModel;
using TheXDS.Ganymede.Component;
using TheXDS.Ganymede.ViewModels.Base;
using Avalonia.Media;

namespace TheXDS.Ganymede.ViewModels.Dialogs;

/// <summary>
/// Base class for all Ganymede ViewModels used to implement dialogs.
/// </summary>
public abstract class DialogViewModelBase : NavigatableViewModel
{
    static DialogViewModelBase()
    {
        RegisterNpcBroadcast(nameof(Title), nameof(IsTitleVisible));
        RegisterNpcBroadcast(nameof(Icon), nameof(IsIconVIsible));
    }
    
    private string? _title;
    private string _message = String.Empty;
    private string? _icon;
    private IBrush? _iconBrush;
    
    /// <summary>
    /// Gets or sets the icon to be displayed on the dialog.
    /// </summary>
    public string? Icon
    {
        get => _icon;
        set => Change(ref _icon, value);
    }
    
    /// <summary>
    /// Gets or sets a brush to be used to draw the background for the icon of
    /// the dialog if displayed.
    /// </summary>
    public IBrush? IconBrush
    {
        get => _iconBrush;
        set => Change(ref _iconBrush, value);
    }

    /// <summary>
    /// Gets a value that indicates if the icon should be made visible in the
    /// dialog.
    /// </summary>
    public bool IsIconVIsible => !string.IsNullOrWhiteSpace(Icon);
    /// <summary>
    /// Gets or sets the title to be displayed on the dialog.
    /// </summary>
    public string? Title
    {
        get => _title;
        set => Change(ref _title, value);
    }

    /// <summary>
    /// Gets a value that indicates if the title should be made visible.
    /// </summary>
    public bool IsTitleVisible => !string.IsNullOrWhiteSpace(Title);

    /// <summary>
    /// Gets or sets a message to be displayed on the dialog.
    /// </summary>
    public string Message
    {
        get => _message;
        set => Change(ref _message, value);
    }
    
    /// <summary>
    /// Gets a collection of interactions to be displayed in the dialog.
    /// </summary>
    public ICollection<ButtonInteraction> Interactions { get; } = new ObservableCollection<ButtonInteraction>();
}