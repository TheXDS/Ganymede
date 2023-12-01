using System.Windows.Input;
using TheXDS.Ganymede.Types;

namespace TheXDS.Ganymede.Component;

/// <summary>
/// Extends the <see cref="ButtonInteraction"/> class to include information
/// that can be used when configuring or presenting the interaction on the UI.
/// </summary>
public class CrudButtonInteraction : ButtonInteraction
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CrudButtonInteraction"/>
    /// class.
    /// </summary>
    /// <param name="command">
    /// Command to associate with the interaction.
    /// </param>
    /// <param name="text">Display text for the interaction.</param>
    public CrudButtonInteraction(ICommand command, string text) : base(command, text)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CrudButtonInteraction"/>
    /// class.
    /// </summary>
    /// <param name="action">Action to be executed by the interaction.</param>
    /// <param name="text">Display text for the interaction.</param>
    public CrudButtonInteraction(Action action, string text) : base(action, text)
    {
    }

    /// <summary>
    /// Gets a value that indicates the group membership of the interaction.
    /// </summary>
    public string Group { get; init; } = string.Empty;

    /// <summary>
    /// Gets a value that indicates if this interaction should be considered
    /// essential, that is, if it should be presented on main panels and menus.
    /// </summary>
    public bool Essential { get; init; }
}
