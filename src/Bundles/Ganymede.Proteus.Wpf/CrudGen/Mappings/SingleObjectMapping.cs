using System.Reflection;
using TheXDS.Ganymede.Controls;
using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Ganymede.CrudGen.Mappings.Base;
using TheXDS.Triton.Models.Base;

namespace TheXDS.Ganymede.CrudGen.Mappings;

/// <summary>
/// Implements a mapping that generates controls for managing properties that
/// hold entity references.
/// </summary>
public class SingleObjectMapping : ObjectMappingBase<SingleObjectEditor, ISingleObjectPropertyDescription>
{
    /// <inheritdoc/>
    public override void SetControlValue(SingleObjectEditor control, object? propertyValue)
    {
        control.SelectedEntity = propertyValue as Model;
    }

    /// <inheritdoc/>
    protected override void OnAddNew(SingleObjectEditor control, Model newEntity, PropertyInfo parentProperty, Model parentEntity)
    {
        parentProperty.SetValue(parentEntity, newEntity);
        control.SelectedEntity = newEntity;
    }
}