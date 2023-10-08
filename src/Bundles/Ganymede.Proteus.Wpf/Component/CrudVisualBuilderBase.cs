using System.Windows;
using System.Windows.Controls;
using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Types.Base;
using static TheXDS.Ganymede.Helpers.Common;

namespace TheXDS.Ganymede.Component;

/// <summary>
/// Base class for all <see cref="IVisualResolver{TVisual}"/> types that can
/// generate visual elements for CRUD operations.
/// </summary>
public abstract class CrudVisualBuilderBase<T> : IVisualResolver<FrameworkElement> where T : CrudViewModelBase
{
    /// <inheritdoc/>
    public virtual FrameworkElement? Resolve(ViewModelBase viewModel)
    {
        return viewModel is T vm ? (FrameworkElement)UiInvoke(() =>
        {
            var pnl = new StackPanel();
            foreach (var j in vm.ModelDescription.PropertyDescriptions)
            {
                if (GetControl(j.Value.Description, vm) is { } ctrl) pnl.Children.Add(ctrl);
            }
            vm.Initialized = true;
            return pnl;
        }) : null;
    }

    /// <summary>
    /// Gets a control for the specified property using the given description.
    /// </summary>
    /// <param name="description">
    /// Property description that provides information on control generation
    /// customization.
    /// </param>
    /// <param name="viewModelContext">
    /// Context information about the underlying ViewModel.
    /// </param>
    /// <returns>
    /// A new <see cref="FrameworkElement"/> that can be presented in a visual
    /// for a <see cref="CrudViewModelBase"/> ViewModel, or
    /// <see langword="null"/> if the property should not be presented.
    /// </returns>
    protected abstract FrameworkElement? GetControl(IPropertyDescription description, T viewModelContext);
}
