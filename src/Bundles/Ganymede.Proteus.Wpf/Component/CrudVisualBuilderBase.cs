using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using TheXDS.Ganymede.Controls;
using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Ganymede.Types.Base;
using TheXDS.Ganymede.ViewModels;
using static TheXDS.Ganymede.Helpers.Common;

namespace TheXDS.Ganymede.Component;

/// <summary>
/// Base class for all <see cref="IVisualResolver{TVisual}"/> types that can
/// generate visual elements for CRUD operations.
/// </summary>
public abstract class CrudVisualBuilderBase<T> : IVisualResolver<FrameworkElement> where T : DynamicCrudViewModelBase
{
    /// <inheritdoc/>
    public virtual FrameworkElement? Resolve(IViewModel viewModel)
    {
        return viewModel is T vm ? UiInvoke(() => CreateRoot(vm)) : null;
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

    private FrameworkElement CreateRoot(T vm)
    {
        var pnl = new StackPanel();
        foreach (var j in vm.ModelDescription.PropertyDescriptions)
        {
            if (GetControl(j.Value.Description, vm) is { } ctrl) pnl.Children.Add(ctrl);
        }
        return new DecoratedBorder()
        {
            UseLayoutRounding = true,
            SnapsToDevicePixels = true,
            Child = pnl,
            HorizontalAlignment = HorizontalAlignment.Left,
            Margin = new Thickness(10),
            MinWidth = 400,
            Padding = new Thickness(20),
            VerticalAlignment = VerticalAlignment.Top,
        };
    }
}
