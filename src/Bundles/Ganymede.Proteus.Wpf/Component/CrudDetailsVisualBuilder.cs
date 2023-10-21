using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Ganymede.CrudGen.Mappings;
using TheXDS.Ganymede.Types.Base;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Component;

/// <summary>
/// Implements a <see cref="IVisualResolver{TVisual}"/> that dynamically
/// generates Visuals for instances of <see cref="CrudDetailsViewModel"/>.
/// </summary>
public class CrudDetailsVisualBuilder : CrudVisualBuilderBase<CrudDetailsViewModel>
{
    private static readonly ReadOnlyMapping _roMapping = new();

    /// <inheritdoc/>
    protected override FrameworkElement? GetControl(IPropertyDescription description, CrudDetailsViewModel _)
    {
        return description.HideFromDetails ? null : _roMapping.CreateControl(description);
    }

    /// <inheritdoc/>
    public override FrameworkElement? Resolve(IViewModel viewModel)
    {
        if (base.Resolve(viewModel) is not { } pnl) return null;
        
        return new Border()
        {
            Background = Brushes.White,
            Margin = new Thickness(10),
            Padding = new Thickness(20),
            CornerRadius = new CornerRadius(5),
            Effect = new DropShadowEffect() { Color = Colors.Black, ShadowDepth = 0 },
            Child = pnl,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
        };
    }
}
