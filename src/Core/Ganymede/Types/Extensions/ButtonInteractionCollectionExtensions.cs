using System.Windows.Input;

namespace TheXDS.Ganymede.Types.Extensions;

/// <summary>
/// Includes extensions that help reduce boilerplate when registering commands
/// in an interaction collection.
/// </summary>
public static class ButtonInteractionCollectionExtensions
{
    /// <summary>
    /// Adds a <see cref="ICommand"/> alongside its label to a 
    /// <see cref="ButtonInteraction"/> collcetion.
    /// </summary>
    /// <param name="collection">Colection to alter.</param>
    /// <param name="command">Command to add.</param>
    /// <param name="label">Command label.</param>
    public static void Add(this ICollection<ButtonInteraction> collection, ICommand command, string label)
    { 
        collection.Add(new ButtonInteraction(command, label));
    }
}
