using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using TheXDS.Ganymede.CrudGen;
using TheXDS.Ganymede.Helpers;
using TheXDS.MCART.Types.Extensions;
using TheXDS.Triton.Models.Base;
using St = TheXDS.Ganymede.Resources.Strings.ProteusCommon;

namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// ViewModel that allows the user to edit a selected entity.
/// </summary>
/// <remarks>
/// All editing operations will be performed on this ViewModel, and only
/// written to the entity after executing the
/// <see cref="OnSave"/> method.
/// </remarks>
public class CrudEditorViewModel : CrudViewModelBase
{
    private readonly TaskCompletionSource<bool> _resultAwaiter = new();
    private readonly Model originalEntity;

    /// <summary>
    /// Gets a reference to the context and state information of this instance.
    /// </summary>
    public CrudEditorViewModelContext Context { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CrudEditorViewModel"/>
    /// class.
    /// </summary>
    /// <param name="entity">
    /// Entity to edit.
    /// </param>
    /// <param name="description">
    /// Model description for the entities.
    /// </param>
    /// <param name="context">
    /// Context and state information for this instance.
    /// </param>
    public CrudEditorViewModel(Model entity, ICrudDescription description, CrudEditorViewModelContext context) : base(CreateTempCopy(entity), description)
    {
        originalEntity = entity;
        Context = context;
        Title = context.CreatingNew ? $"New {description.FriendlyName}" : $"Edit {description.FriendlyName} \"{entity.IdAsString}\"";
    }

    /// <summary>
    /// Getts a reference to the method to invoke when cancelling the edit
    /// operation.
    /// </summary>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await the async operation.
    /// </returns>
    public async Task OnCancel()
    {
        if (await (DialogService?.Ask(St.WantToCancelChanges) ?? Task.FromResult(true)))
        {
            NavigationService?.NavigateBack();
            _resultAwaiter.SetResult(false);
        }
    }

    /// <summary>
    /// Gets a reference to the method to invoke upon attempting to save the
    /// entity.
    /// </summary>
    public void OnSave()
    {
        foreach (var callback in Context.PreSaveCallbacks)
        {
            UiThread.Invoke(() => callback.Invoke(Entity));
        }
        ShallowCopy(Entity, originalEntity, ModelType);
        NavigationService?.NavigateBack();
        _resultAwaiter.SetResult(true);
    }

    /// <summary>
    /// Gets a task that will complete after the user either saves or cancels
    /// the edit operation on the entity.
    /// </summary>
    /// <returns>
    /// A task that will complete after the user either saves or cancels the
    /// edit operation on the entity.<br/><br/>
    /// The result of the task will be <see langword="true"/> if the user saves
    /// the changes, or <see langword="false"/> if the user clicks on "Cancel".
    /// </returns>3
    public Task<bool> WaitForCompletion() => _resultAwaiter.Task;

    private static Model CreateTempCopy(Model entity)
    {
        var model = entity.GetType();
        var copy = model.New<Model>();
        ShallowCopy(entity, copy, model);
        return copy;
    }

    private static IEnumerable<PropertyInfo> GetCollectionProperties(Type type)
    {
        return type.GetProperties().Where(IsCollection);
    }

    private static PropertyInfo? GetProperty(object destination, PropertyInfo? modelProperty)
    {
        return modelProperty is not null
            ? destination.GetType().GetProperty(modelProperty.Name, modelProperty.PropertyType)
            : null;
    }

    private static IEnumerable<PropertyInfo> GetRwProperties(Type type)
    {
        return type.GetProperties().Where(IsReadWrite).Where(p => !IsCollection(p));
    }

    private static bool IsCollection(PropertyInfo p)
    {
        return p.CanRead && !p.PropertyType.IsArray && p.PropertyType.Implements(typeof(ICollection<>));
    }

    private static bool IsReadWrite(PropertyInfo p)
    {
        return p.CanRead && p.CanWrite;
    }

    private static void ShallowCopy(object source, object destination, Type model)
    {
        foreach (PropertyInfo prop in GetRwProperties(model))
        {
            GetProperty(destination, prop)!.SetValue(destination, GetProperty(source, prop)!.GetValue(source));
        }
        foreach (PropertyInfo prop in GetCollectionProperties(model))
        {
            var ct = prop.PropertyType.GetCollectionType();
            if (TryGetCollection(prop, ct, source, out var sc) && TryGetCollection(GetProperty(destination, prop), ct, destination, out var dc))
            {
                CrudCommon.DynamicPopulateCollection(sc, dc);
            }
        }
    }

    private static bool TryGetCollection(PropertyInfo? prop, Type collectionType, object instance, [NotNullWhen(true)] out object? result)
    {
        return (result = GetProperty(instance, prop!)?.GetValue(instance) is { } collection && collection.GetType().Implements(typeof(ICollection<>).MakeGenericType(collectionType)) ? collection : null) is not null;
    }
}
