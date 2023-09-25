using Avalonia.ReactiveUI;
using TheXDS.Ganymede.ViewModels.Dialogs;

namespace TheXDS.Ganymede.Views.Dialogs;

/// <summary>
/// View for the <see cref="MessageDialogViewModel"/>.
/// </summary>
public partial class DialogView : ReactiveUserControl<DialogViewModelBase>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DialogView"/>
    /// class.
    /// </summary>
    public DialogView()
    {
        InitializeComponent();
    }
}