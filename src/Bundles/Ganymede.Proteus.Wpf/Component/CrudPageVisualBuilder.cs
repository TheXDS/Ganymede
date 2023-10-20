using System.Windows;
using TheXDS.Ganymede.Types.Base;
using TheXDS.Ganymede.ViewModels;
using TheXDS.Ganymede.Views;
using static TheXDS.Ganymede.Helpers.Common;

namespace TheXDS.Ganymede.Component;

/// <summary>
/// Resolves a <see cref="CrudPageViewModel"/> to a <see cref="CrudHostView"/>.
/// </summary>
public class CrudPageVisualBuilder : IVisualResolver<FrameworkElement>
{
    /// <inheritdoc/>
    public FrameworkElement? Resolve(IViewModel viewModel)
    {
        return (viewModel is CrudPageViewModel vm) ? UiInvoke(() =>
        {
            var page = new CrudHostView() { DataContext = vm };
            return page;
        }) : (FrameworkElement?)null;
    }
}
