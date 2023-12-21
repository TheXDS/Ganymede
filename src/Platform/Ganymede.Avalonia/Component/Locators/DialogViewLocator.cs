using ReactiveUI;
using TheXDS.Ganymede.ViewModels.Dialogs;
using TheXDS.Ganymede.Views.Dialogs;

namespace TheXDS.Ganymede.Component.Locators;

/// <summary>
/// Statically returns a <see cref="DialogView"/> for all implementations of
/// the <see cref="DialogViewModelBase"/> class.
/// </summary>
public class DialogViewLocator : IViewLocator
{
    /// <inheritdoc/>
    public IViewFor? ResolveView<T>(T? viewModel, string? contract = null)
    {
        return viewModel is DialogViewModelBase ? new DialogView() : null;
    }
}