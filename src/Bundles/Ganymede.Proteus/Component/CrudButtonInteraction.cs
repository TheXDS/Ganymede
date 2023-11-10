using TheXDS.Ganymede.Types;

namespace TheXDS.Ganymede.Component;

/// <summary>
/// Extends the <see cref="ButtonInteraction"/> class to include information
/// that can be used when configuring or presenting the interaction on the UI.
/// </summary>
public class CrudButtonInteraction : ButtonInteraction
{
    /// <summary>
    /// Gets a value that indicates the group membership of the interaction.
    /// </summary>
    public string Group { get; init; }

    /// <summary>
    /// Gets a value that indicates if this interaction should be considered
    /// essential, that is, if it should be presented on main panels and menus.
    /// </summary>
    public bool Essential { get; init; }
}
