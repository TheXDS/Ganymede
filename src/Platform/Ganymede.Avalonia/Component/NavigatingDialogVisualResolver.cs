using Avalonia;
using TheXDS.Ganymede.Types.Base;
using TheXDS.Ganymede.Views.Dialogs;

//using TheXDS.Ganymede.Views.Dialogs;

namespace TheXDS.Ganymede.Component;

/// <summary>
/// Implements a <see cref="IVisualResolver{TVisual}"/> tailored to resolve
/// dialog-specific ViewModels.
/// </summary>
public class NavigatingDialogVisualResolver : IVisualResolver<StyledElement>
{
    /// <summary>
    /// Resolves a dialog visual container to be used to host the requested Dialog ViewModel.
    /// </summary>
    /// <param name="viewModel">ViewModel to resolve.</param>
    /// <returns>
    /// This method will try to resolve the visual based on registrations, and
    /// upon failure, will return a new instance of the
    /// <see cref="DialogView"/> class. Although the signature of this method
    /// allows returning <see langword="null"/>, this method will never return
    /// it.
    /// </returns>
    public StyledElement Resolve(IViewModel viewModel)
    {
        return new DialogView();
    }
}
