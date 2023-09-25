namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Contains context and state information about the active
/// <see cref="CrudEditorViewModel"/> instance.
/// </summary>
/// <param name="CreatingNew">Indicates whether or not the ViewModel was invoked to edit a new entiity.</param>
public record struct CrudEditorViewModelContext(bool CreatingNew)
{
    /// <summary>
    /// Indicates whether or not the ViewModel was invoked to edit an existing entiity.
    /// </summary>
    public readonly bool UpdatingExisting => !CreatingNew;
}
