using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using TheXDS.Ganymede.Controls.Base;
using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.Services;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Component;
using TheXDS.MCART.Types.Extensions;
using TheXDS.Triton.Models.Base;
using St = TheXDS.Ganymede.Resources.Strings.ProteusCommon;

namespace TheXDS.Ganymede.CrudGen.Mappings.Base;

/// <summary>
/// Base class for any implementation of <see cref="ICrudMapping"/> that maps
/// descriptions of type <see cref="IObjectPropertyDescription"/>.
/// </summary>
/// <typeparam name="TControl">Type of control to generate.</typeparam>
/// <typeparam name="TDescription">
/// Type of description to use when describing a property.
/// </typeparam>
public abstract class ObjectMappingBase<TControl, TDescription>
    : ICrudMapping<TDescription>
    where TControl : ObjectEditor, new()
    where TDescription : class, IObjectPropertyDescription
{
    bool ICrudMapping.MustSetValueManually => true;

    void ICrudMapping<TDescription>.SetControlValue(FrameworkElement control, Model entity, TDescription description)
    {
        SetControlValue((TControl)control, description.Property.GetValue(entity));
    }

    /// <inheritdoc/>
    public FrameworkElement CreateControl(TDescription description)
    {
        var control = new TControl
        {
            CanCreate = description.Creatable,
            CanSelect = description.Selectable,
            Models = description.AvailableModels,
            Label = description.Label,
            LabelForeground = Brushes.White,
            LabelEffect = new DropShadowEffect() { Color = Colors.Black, ShadowDepth = 0 }
        };
        control.CreateCommand = CreateNewCommand(control, description, OnAddNew);
        control.SelectCommand = CreateSelectCommand(control, description);
        control.UpdateCommand = CreateUpdateCommand(control, description);
        ConfigureControl(control, description);
        return control;
    }

    /// <summary>
    /// Sets the current control's value.
    /// </summary>
    /// <param name="control">Control to set the value onto.</param>
    /// <param name="propertyValue">
    /// Property value to set onto the control.
    /// </param>
    public abstract void SetControlValue(TControl control, object? propertyValue);

    /// <summary>
    /// Allows for further control configuration to be performed.
    /// </summary>
    /// <param name="control">Control to configure.</param>
    /// <param name="description">Property description.</param>
    protected virtual void ConfigureControl(TControl control, TDescription description) { }

    /// <summary>
    /// Defines the operation to be performed to add a new entity to the value
    /// of the property on the entity.
    /// </summary>
    /// <param name="newEntity"></param>
    /// <param name="parentProperty">Property information on the parent entity.</param>
    /// <param name="parentEntity">
    /// Entity that will hold the newly created child entity.
    /// </param>
    protected abstract void OnAddNew(Model newEntity, PropertyInfo parentProperty, Model parentEntity);

    /// <summary>
    /// Creates a command that allows to create a new entity.
    /// </summary>
    /// <param name="control">
    /// Control to extract the active datacontext from.
    /// </param>
    /// <param name="description">Current property description.</param>
    /// <param name="newAction">
    /// Action to execute upon successful creation of a new entity.</param>
    /// <returns>A new command that allows to create a new entity.</returns>
    protected static ICommand CreateNewCommand(TControl control, TDescription description, Action<Model, PropertyInfo, Model> newAction)
    {
        return new SimpleCommand(async () =>
        {
            var vm = (CrudEditorViewModel)control.DataContext;
            var childDescription = description.AvailableModels.Length > 1
                ? await GetDesiredModel(vm.DialogService!, description.AvailableModels)
                : description.AvailableModels[0];

            if (childDescription is not null)
            {
                await OpenChildEditor(childDescription.Model.New<Model>(), vm, childDescription, description.Property, newAction);
            }
        });
    }

    /// <summary>
    /// Creates a command that allows to edit an already existing entity.
    /// </summary>
    /// <param name="control">
    /// Control to extract the active datacontext from.
    /// </param>
    /// <param name="description">Current property description.</param>
    /// <returns>
    /// A new command that allows to edit an already existing entity.
    /// </returns>
    protected static ICommand CreateUpdateCommand(TControl control, TDescription description)
    {
        return new SimpleCommand(() =>
        {
            var vm = (CrudEditorViewModel)control.DataContext;
            var model = control.SelectedEntity?.GetType();
            if (model is null) return Task.CompletedTask;
            var desc = description.AvailableModels.First(p => p.Model == model);
            return OpenChildEditor(control.SelectedEntity!, vm, desc, description.Property, null);
        });
    }

    /// <summary>
    /// Creates a command that allows to select an existing entity to be added
    /// or set to the value of the object property.
    /// </summary>
    /// <param name="control">
    /// Control to extract the active datacontext from.
    /// </param>
    /// <param name="description">Current property description.</param>
    /// <returns>
    /// A new command that allows to add/select an existing entity.
    /// </returns>
    protected static ICommand CreateSelectCommand(TControl control, TDescription description)
    {
        return new SimpleCommand(() =>
        {
            var vm = (CrudEditorViewModel)control.DataContext;
            return vm.DialogService!.Error(new NotImplementedException());
        });
    }

    private static async Task<Model?> OpenChildEditor(Model entity, CrudEditorViewModel parentVm, ICrudDescription description, PropertyInfo parentProperty, Action<Model, PropertyInfo, Model>? addNew)
    {
        LaunchEditorSettings s = new()
        {
            Entity = entity,
            Description = description,
            Context = new CrudEditorViewModelContext(addNew is not null, description.Model, parentVm.Entity!.GetType()),
            NavigationService = parentVm.NavigationService!,
            DialogService = parentVm.DialogService!,
        };
        if (addNew is not null)
        {
            s.Context.PreSaveCallbacks.Add(e => addNew.Invoke(e, parentProperty, parentVm.Entity));
        }
        return await CrudCommon.LaunchEditor(s) ? s.Entity : null;
    }

    private static async Task<ICrudDescription?> GetDesiredModel(IDialogService dialogService, ICrudDescription[] models)
    {
        var m = new Dictionary<string, ICrudDescription>(models.Select(p => new KeyValuePair<string, ICrudDescription>(p.FriendlyName, p)));
        return await dialogService!.SelectOption(St.NewItem, St.NewItemHelp, m.Keys.ToArray()) is { } i && i >= 0
            ? m[m.Keys.ToArray()[i]]
            : null;
    }
}
