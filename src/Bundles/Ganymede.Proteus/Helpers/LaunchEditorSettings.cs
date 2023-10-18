using TheXDS.Ganymede.CrudGen;
using TheXDS.Ganymede.Services;
using TheXDS.Ganymede.ViewModels;
using TheXDS.Triton.Models.Base;

namespace TheXDS.Ganymede.Helpers;

/// <summary>
/// Contains a set of configuration values to be used to configure the
/// generation of a new isntance of the <see cref="CrudEditorViewModel"/>
/// class.
/// </summary>
public record class LaunchEditorSettings
{
    /// <summary>
    /// Gets a reference to the entity to be edited.
    /// </summary>
    public Model Entity { get; init; } = null!;

    /// <summary>
    /// Gets a reference to the <see cref="ICrudDescription"/> to be used when
    /// generating any UI elements to edit the entity.
    /// </summary>
    public ICrudDescription Description { get; init; } = null!;

    /// <summary>
    /// Gets a reference to a context with all the information about the
    /// ViewModel state.
    /// </summary>
    public CrudEditorViewModelContext Context { get; init; } = default;

    /// <summary>
    /// Gets a reference to the navigation service to be used internally by the
    /// editor.
    /// </summary>
    public INavigationService NavigationService { get; init; } = null!;

    /// <summary>
    /// Gets a reference to the dialog service to be used by the editor.
    /// </summary>
    public IDialogService DialogService { get; init; } = null!;
}
