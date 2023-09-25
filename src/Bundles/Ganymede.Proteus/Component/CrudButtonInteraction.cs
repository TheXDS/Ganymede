using TheXDS.Ganymede.Types;

namespace TheXDS.Ganymede.Component;

/// <summary>
/// Extends the <see cref="ButtonInteraction"/> class to include information on
/// visibility based on the current state of the Crud ViewModel where this
/// interaction could be shown.
/// </summary>
public class CrudButtonInteraction : ButtonInteraction
{
    /// <summary>
    /// Gets a value that indicates if this interaction is visible under a set
    /// of possible states of a Crud ViewModel.
    /// </summary>
    public CrudVisibility Visibility { get; init; }
}
