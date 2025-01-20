using System.Collections.ObjectModel;
using System.Drawing;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.Types.Base;

namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Base class for all Ganymede ViewModels used to implement dialogs.
/// </summary>
public class DialogViewModel : ViewModel, IDialogViewModel
{
    private string _message = string.Empty;
    private string? _icon;
    private Color? _iconBgColor;

    /// <inheritdoc/>
    protected override void OnInitialize(IPropertyBroadcastSetup broadcastSetup)
    {
        broadcastSetup.RegisterPropertyChangeBroadcast(() => Title, () => IsTitleVisible);
        broadcastSetup.RegisterPropertyChangeBroadcast(() => Icon, () => IsIconVisible);
    }

    /// <summary>
    /// Gets a collection of interactions to be displayed in the dialog.
    /// </summary>
    public ICollection<ButtonInteraction> Interactions { get; } = new ObservableCollection<ButtonInteraction>();

    /// <summary>
    /// Gets a value that indicates if the icon should be made visible in the
    /// dialog.
    /// </summary>
    public bool IsIconVisible => !string.IsNullOrWhiteSpace(Icon);

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
}
