using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using TheXDS.Ganymede.Controls;
using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Ganymede.CrudGen.Mappings.Base;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Component;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;
using TheXDS.Triton.Models.Base;

namespace TheXDS.Ganymede.CrudGen.Mappings;

/// <summary>
/// Implements a mapping that generates controls for managing collections of
/// entities.
/// </summary>
public class CollectionMapping : ICrudMapping<ICollectionPropertyDescription>
{
    bool ICrudMapping.MustSetValueManually => true;

    /// <inheritdoc/>
    public FrameworkElement CreateControl(ICollectionPropertyDescription description)
    {
        var list = new ListEditor()
        {
            Height = description.WidgetSize switch
            {
                WidgetSize.Small => 58,
                WidgetSize.Medium => 90,
                WidgetSize.Large => 200,
                WidgetSize.Huge => 500,
                _ => 0
            },
            CanCreate = description.Creatable,
            CanSelect = description.Linkable,
            Models = description.AvailableModels,
            Label = description.Label,
            CreateCommands = new List<ButtonInteraction>(),
        };
        foreach (var j in description.AvailableModels)
        {
            list.CreateCommands.Add(CreateNewCommand(list, j, description));
        }
        list.SetBinding(ListEditor.CollectionProperty, $"{nameof(CrudViewModelBase.Entity)}.{description.Property.Name}");
        return list;
    }

    private static ButtonInteraction CreateNewCommand(ListEditor editor, ICrudDescription desc, ICollectionPropertyDescription parentDescription)
    {
        return new(new SimpleCommand(async () =>
        {
            var parentVm = (CrudEditorViewModel)editor.DataContext;
            var entity = desc.Model.New<Model>();
            await OpenChildEditor(entity, parentVm, desc, parentDescription);
        }), desc.FriendlyName);
    }

    private static async Task<Model?> OpenChildEditor(Model entity, CrudEditorViewModel parentVm, ICrudDescription description, ICollectionPropertyDescription parentDescription)
    {
        LaunchEditorSettings s = new()
        {
            Entity = entity,
            Description = description,
            Context = new CrudEditorViewModelContext(true)
            {
                PreSaveCallbacks =
                {
                    e => AddToEntityCollection(e, parentDescription, parentVm)
                }
            },
            NavigationService = parentVm.NavigationService!,
            DialogService = parentVm.DialogService!,
        };
        return await CrudCommon.LaunchEditor(s) ? s.Entity : null;
    }

    /// <inheritdoc/>
    public void SetControlValue(FrameworkElement control, Model entity, ICollectionPropertyDescription description)
    {
        var controlCollection = new List<Model>();
        foreach (Model j in (IEnumerable<Model>?)description.Property.GetValue(entity) ?? Array.Empty<Model>())
        {
            controlCollection.Add(j);
        }
        ((ListEditor)control).Collection = new ObservableListWrap<Model>(controlCollection);
    }

    private static void AddToEntityCollection(Model newEntity, IPropertyDescription description, CrudViewModelBase parentVm)
    {
        object? targetCollection = description.Property.GetValue(parentVm.Entity);
        if (targetCollection is not null)
        {
            CrudCommon.DynamicAdd(targetCollection, newEntity);
        }
        else
        {
            var itemType = description.Property.PropertyType.GetCollectionType();
            var list = typeof(List<>).MakeGenericType(itemType).New();
            CrudCommon.DynamicAdd(list, newEntity);
            description.Property.SetValue(parentVm.Entity, list);
        }
    }
}