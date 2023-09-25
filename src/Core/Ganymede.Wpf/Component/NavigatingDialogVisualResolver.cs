using System.Windows;
using TheXDS.Ganymede.ViewModels;
using TheXDS.Ganymede.Views.Dialogs;

namespace TheXDS.Ganymede.Component;

/// <summary>
/// Implements a <see cref="IVisualResolver{TVisual}"/> tailored to resolve
/// dialog-specific ViewModels.
/// </summary>
public class NavigatingDialogVisualResolver : DictionaryVisualResolver<FrameworkElement>
{
    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="NavigatingDialogVisualResolver"/> class.
    /// </summary>
    public NavigatingDialogVisualResolver()
    {
        Register<DialogViewModel, DialogView>();
        Register<OperationDialogViewModel, DialogView>();
    }
}
