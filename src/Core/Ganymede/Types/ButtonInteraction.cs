using System.Windows.Input;
using TheXDS.MCART.Component;
using TheXDS.MCART.Types.Base;

namespace TheXDS.Ganymede.Types;

/// <summary>
/// Represents a button interaction inside the application. These might include
/// buttons in dialog boxes, dynamically generated menus, etc. 
/// </summary>
/// <param name="command">
/// Command to associate with the interaction.
/// </param>
/// <param name="text">Display text for the interaction.</param>
public class ButtonInteraction(ICommand command, string text) : NotifyPropertyChanged
{
    private string _text = text;
    private bool _isPrimary;

    /// <summary>
    /// Initializes a new instance of the <see cref="ButtonInteraction"/>
    /// class.
    /// </summary>
    /// <param name="action">Action to be executed by the interaction.</param>
    /// <param name="text">Display text for the interaction.</param>
    public ButtonInteraction(Action action, string text) : this(new SimpleCommand(action), text)
    {
    }

    /// <summary>
    /// Gets a reference to the command to be executed by activating this
    /// interaction.
    /// </summary>
    public ICommand Command { get; } = command;

    /// <summary>
    /// Gets or sets the text to be displayed on the control used to activate
    /// this interaction.
    /// </summary>
    public string Text
    {
        get => _text;
        set => Change(ref _text, value);
    }

    /// <summary>
    /// Gets or sets a value that indicates if this interaction should be
    /// treated as a primary interaction (useful for dialogs and UI elements in
    /// a contrained context)
    /// </summary>
    public bool IsPrimary
    { 
        get => _isPrimary;
        set => Change(ref _isPrimary, value);
    }
}
