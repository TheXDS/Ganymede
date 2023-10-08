using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using TheXDS.Ganymede.Controls;
using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Ganymede.CrudGen.Mappings.Base;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.Services;
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
        list.CreateCommand = CreateNewCommand(list, description);
        list.UpdateCommand = CreateUpdateCommand(list, description);
        return list;
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

    private static SimpleCommand CreateNewCommand(ListEditor list, ICollectionPropertyDescription description)
    {
        return new SimpleCommand(async () =>
        {
            var vm = (CrudEditorViewModel)list.DataContext;
            var modelDescription = description.AvailableModels.Length > 1
                ? await GetDesiredModel(vm.DialogService!, description.AvailableModels)
                : description.AvailableModels[0];

            if (modelDescription is not null)
            { 
                await OpenChildEditor(modelDescription.Model.New<Model>(), vm, modelDescription, description);
            }
        });
    }

    private static async Task<ICrudDescription?> GetDesiredModel(IDialogService dialogService, ICrudDescription[] models)
    {
        var m = new Dictionary<string, ICrudDescription>(models.Select(p => new KeyValuePair<string, ICrudDescription>(p.FriendlyName, p)));
        return await dialogService!.SelectOption("New item", "Select the type of item to be created", m.Keys.ToArray()) is { } i && i >= 0
            ? m[m.Keys.ToArray()[i]]
            : null;
    }

    private static SimpleCommand CreateUpdateCommand(ListEditor list, ICollectionPropertyDescription description)
    {
        return new SimpleCommand(() => {
            var vm = (CrudEditorViewModel)list.DataContext;
            var model = vm.Entity.GetType();
            var desc = description.AvailableModels.First(p => p.Model == model);
            return OpenChildEditor(vm.Entity, vm, desc, description, false);
        });
    }

    private static async Task<Model?> OpenChildEditor(Model entity, CrudEditorViewModel parentVm, ICrudDescription description, ICollectionPropertyDescription parentDescription, bool addNew = true)
    {
        LaunchEditorSettings s = new()
        {
            Entity = entity,
            Description = description,
            Context = new CrudEditorViewModelContext(addNew),
            NavigationService = parentVm.NavigationService!,
            DialogService = parentVm.DialogService!,
        };
        if (addNew)
        {
            s.Context.PreSaveCallbacks.Add(e => AddToEntityCollection(e, parentDescription, parentVm));
        }
        return await CrudCommon.LaunchEditor(s) ? s.Entity : null;
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