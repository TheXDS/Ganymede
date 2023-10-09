using System;
using System.Collections.Generic;
using System.Reflection;
using TheXDS.Ganymede.Controls;
using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Ganymede.CrudGen.Mappings.Base;
using TheXDS.Ganymede.Helpers;
using TheXDS.MCART.Types.Extensions;
using TheXDS.Triton.Models.Base;

namespace TheXDS.Ganymede.CrudGen.Mappings;

/// <summary>
/// Implements a mapping that generates controls for managing collections of
/// entities.
/// </summary>
public class CollectionMapping : ObjectMappingBase<ListEditor, ICollectionPropertyDescription>
{
    /// <inheritdoc/>
    protected override void ConfigureControl(ListEditor control, ICollectionPropertyDescription description)
    {
        control.Height = description.WidgetSize switch
        {
            WidgetSize.Small => 58,
            WidgetSize.Medium => 90,
            WidgetSize.Large => 200,
            WidgetSize.Huge => 500,
            _ => 0
        };
    }

    /// <inheritdoc/>
    public override void SetControlValue(ListEditor control, object? propertyValue)
    {
        var controlCollection = new List<Model>();
        foreach (Model j in (IEnumerable<Model>?)propertyValue ?? Array.Empty<Model>())
        {
            controlCollection.Add(j);
        }
        control.Collection = controlCollection;
    }

    /// <inheritdoc/>
    protected override void OnAddNew(Model newEntity, PropertyInfo parentProperty, Model parentEntity)
    {
        object? targetCollection = parentProperty.GetValue(parentEntity);
        if (targetCollection is not null)
        {
            CrudCommon.DynamicAdd(targetCollection, newEntity);
        }
        else
        {
            var itemType = parentProperty.PropertyType.GetCollectionType();
            var list = typeof(List<>).MakeGenericType(itemType).New();
            CrudCommon.DynamicAdd(list, newEntity);
            parentProperty.SetValue(parentEntity, list);
        }
    }
}
