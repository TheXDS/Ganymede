using System.Linq;
using System.Windows;
using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Ganymede.CrudGen.Mappings;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Component;

/// <summary>
/// Implements a <see cref="IVisualResolver{TVisual}"/> that dynamically
/// generates Visuals for instances of <see cref="CrudEditorViewModel"/>.
/// </summary>
public class CrudEditorVisualBuilder : CrudVisualBuilderBase<CrudEditorViewModel>
{
    private readonly CrudEditorVisualBuilderSettings _settings;

    /// <summary>
    /// Initializes a new instance of the <see cref="CrudEditorVisualBuilder"/>
    /// class.
    /// </summary>
    /// <param name="settings"></param>
    public CrudEditorVisualBuilder(CrudEditorVisualBuilderSettings settings)
    {
        _settings = settings;
    }

    /// <inheritdoc/>
    protected override FrameworkElement? GetControl(IPropertyDescription description, CrudEditorViewModel viewModelContext)
    {
        if (_settings.SkipCkecks.Any(p => p.Invoke(description, viewModelContext))) return null;

        return _settings.ControlTransformations.Aggregate(
            (_settings.Mappings.FirstOrDefault(p => p.CanMap(description)) ?? FallbackMapping.Create())
            .CreateControl(description), (c, t) => t.Invoke(c, description));
    }
}
