using System.Collections.ObjectModel;
using System.Drawing;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.Types.Base;

namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Base class for all Ganymede ViewModels used to implement dialogs.
/// </summary>
public class DialogViewModel : ViewModel
{
    private string? _title;
    private string _message = string.Empty;
    private string? _icon;
    private Color? _iconBgColor;

    /// <summary>
    /// Initializes a new instance of the <see cref="DialogViewModel"/>
    /// class.
    /// </summary>
    public DialogViewModel()
    {
        RegisterPropertyChangeBroadcast(nameof(Title), nameof(IsTitleVisible));
        RegisterPropertyChangeBroadcast(nameof(Icon), nameof(IsIconVIsible));
    }

    /// <summary>
    /// Gets a collection of interactions to be displayed in the dialog.
    /// </summary>
    public ICollection<ButtonInteraction> Interactions { get; } = new ObservableCollection<ButtonInteraction>();

    /// <summary>
    /// Gets a value that indicates if the icon should be made visible in the
    /// dialog.
    /// </summary>
    public bool IsIconVIsible => !string.IsNullOrWhiteSpace(Icon);

    /// <summary>
    /// Gets a value that indicates if the title should be made visible.
    /// </summary>
    public bool IsTitleVisible => !string.IsNullOrWhiteSpace(Title);

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
    public Color? IconBgColor
    {
        get => _iconBgColor;
        set => Change(ref _iconBgColor, value);
    }

    /// <summary>
    /// Gets or sets a message to be displayed on the dialog.
    /// </summary>
    public string Message
    {
        get => _message;
        set => Change(ref _message, value);
    }

    /// <summary>
    /// Gets or sets the title to be displayed on the dialog.
    /// </summary>
    public string? Title
    {
        get => _title;
        set => Change(ref _title, value);
    }
}
