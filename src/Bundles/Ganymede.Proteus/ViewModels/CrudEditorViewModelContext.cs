using TheXDS.Ganymede.CrudGen;
using TheXDS.Ganymede.Helpers;
using TheXDS.Triton.Models.Base;
using TheXDS.Triton.Services.Base;

namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Contains context and state information about the active
/// <see cref="CrudEditorViewModel"/> instance.
/// </summary>
/// <param name="CreatingNew">
/// Indicates whether or not the ViewModel was invoked to edit a new entiity.
/// </param>
/// <param name="Model">
/// Model for which the ViewModel was invoked.
/// </param>
/// <param name="ParentModel">
/// Parent model of the invoked ViewModel instance.
/// </param>
/// <param name="PageDataService">
/// Data service instance available to the whole CRUD chain.
/// </param>
public record struct CrudEditorViewModelContext(bool CreatingNew, Type Model, Type? ParentModel, ITritonService PageDataService)
{
    /// <summary>
    /// Indicates whether or not the ViewModel was invoked to edit an existing entiity.
    /// </summary>
    public readonly bool UpdatingExisting => !CreatingNew;

    /// <summary>
    /// Contains a set of actions to be invoked before applying the save
    /// operation over the entity being edited.
    /// </summary>
    /// <remarks>
    /// These actions are not the same as the
    /// <see cref="ICrudDescription.SavePrologs"/> actions, as these are
    /// executed on the temporary <see cref="CrudEditorViewModel"/> entity
    /// before the value transfer operation is performed, whereas the
    /// SavePrologs are executed after the editor has closed. These callbacks
    /// are intended to be used whenever a limitation on the UI frontend
    /// requires a workaround to apply the values from the UI widgets/controls
    /// into the temporary editing entity, and therefore should be invoked on
    /// the UI thread via the <see cref="UiThread.Invoke(Action)"/> method.
    /// </remarks>
    public ICollection<Action<Model>> PreSaveCallbacks { get; } = new List<Action<Model>>();
}
