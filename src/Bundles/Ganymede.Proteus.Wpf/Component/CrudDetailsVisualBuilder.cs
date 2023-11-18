using System.Windows;
using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Ganymede.CrudGen.Mappings;
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
}
