using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TheXDS.Ganymede.Controls;
using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Ganymede.CrudGen.Mappings.Base;
using TheXDS.Ganymede.Helpers;
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
        };

        list.CreateCommand = new SimpleCommand(async () =>
        {

            var models = new Dictionary<string, ICrudDescription>(description.AvailableModels.Select(p => new KeyValuePair<string, ICrudDescription>(p.FriendlyName, p)));
            var vm = (CrudEditorViewModel)list.DataContext;
            Task OpenEditor(int index)
            {
                var desc = models[models.Keys.ToArray()[0]];
                var entity = desc.Model.New<Model>();
                return OpenChildEditor(entity, vm, desc, description);
            }
            if (models.Count > 1)
            {
                if (await vm.DialogService!.SelectOption("New item", "Select the type of item to be created", models.Keys.ToArray()) is { } m && m >= 0)
                {
                    await OpenEditor(m);
                }
            }
            else
            {
                await OpenEditor(0);
            }
        });
        return list;
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