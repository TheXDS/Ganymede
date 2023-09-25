using System.Collections.Generic;
using TheXDS.Ganymede.CrudGen.Mappings.Base;

namespace TheXDS.Ganymede.Component;

/// <summary>
/// Represents a collection of settings to be used when generating controls
/// using an instance of the <see cref="CrudEditorVisualBuilder"/> class.
/// </summary>
public class CrudEditorVisualBuilderSettings
{
    /// <summary>
    /// Gets the collection of available mappings to generate controls.
    /// </summary>
    public ICollection<ICrudMapping> Mappings { get; } = new List<ICrudMapping>();

    /// <summary>
    /// List of transformations that can be applied to a control upon generation.
    /// </summary>
    public IList<ControlTransform> ControlTransformations { get; } = new List<ControlTransform>();

    /// <summary>
    /// List of skip checks to be performed for each described property.
    /// </summary>
    public IList<CrudGenSkipCheck> SkipCkecks { get; } = new List<CrudGenSkipCheck>();
}
