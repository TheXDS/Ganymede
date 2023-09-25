using ReactiveUI;
using TheXDS.Ganymede.Services;

namespace TheXDS.Ganymede.ViewModels.Base;

/// <summary>
/// Base class for all Ganymede ViewModels with navigation support; that
/// is, ViewModels that participate in the Ganymede UI system, and have
/// Views associated with them.
/// </summary>
public abstract partial class NavigatableViewModel : ViewModelBase, IRoutableViewModel
{
    /// <summary>
    /// Gets a unique navigation ID that can be used to identify this
    /// instance in a navigation context.
    /// </summary>
    public virtual string? UrlPathSegment { get; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Gets a reference to the object used to visually present this
    /// instance.
    /// </summary>
    /// <remarks>
    /// Do not interact with this property on the type constructor, as this
    /// value will not be initialized yet.
    /// </remarks>
    public IScreen HostScreen { get; internal init; } = null!;

    /// <summary>
    /// Gets a reference to the dialog service to expose to this instance.
    /// </summary>
    /// <remarks>
    /// Do not interact with this property on the type constructor, as this
    /// value will not be initialized yet.
    /// </remarks>
    public DialogService DialogService { get; internal init; } = null!;
}