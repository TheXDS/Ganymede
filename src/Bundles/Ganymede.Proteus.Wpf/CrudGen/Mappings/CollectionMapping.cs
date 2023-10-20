using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Input;
using TheXDS.Ganymede.Controls;
using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Ganymede.CrudGen.Mappings.Base;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Component;
using TheXDS.MCART.Types.Extensions;
using TheXDS.Triton.Models.Base;
using St = TheXDS.Ganymede.Resources.Strings.ProteusCommon;

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
        control.RemoveCommand = CreateRemoveCommand(control, description);
    }

    private static ICommand CreateRemoveCommand(ListEditor control, ICollectionPropertyDescription description)
    {
        return new SimpleCommand(async () =>
        {
            var vm = (CrudEditorViewModel)control.DataContext;
            if (control.SelectedEntity is not { } child || description.Property.GetValue(vm.Entity) is not { } parentCollection) return;
            if (await vm.DialogService!.Ask(St.Delete, St.AreYouSureDelete))
            {
                CrudCommon.DynamicRemove(parentCollection, child);
                control.Collection.Remove(child);
                control.InvalidateProperty(ListEditor.CollectionProperty);
            }
        });
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
    protected override void OnAddNew(ListEditor control, Model newEntity, PropertyInfo parentProperty, Model parentEntity)
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
