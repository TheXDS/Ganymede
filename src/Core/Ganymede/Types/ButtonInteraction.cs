using System.Windows.Input;
using TheXDS.MCART.Component;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;

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
    /// Initializes a new instance of the <see cref="ButtonInteraction"/>
    /// class.
    /// </summary>
    /// <param name="task">Task to be executed by the interaction.</param>
    /// <param name="text">Display text for the interaction.</param>
    public ButtonInteraction(Func<Task> task, string text) : this(new SimpleCommand(task), text)
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
    /// a constrained context)
    /// </summary>
    public bool IsPrimary
    {
        get => _isPrimary;
        set => Change(ref _isPrimary, value);
    }

    /// <summary>
    /// Implicitly converts a <see cref="ButtonInteraction"/> into a
    /// <see cref="NamedObject{T}"/>.
    /// </summary>
    /// <param name="buttonInteraction">Object to be converted.</param>
    public static implicit operator NamedObject<ICommand>(ButtonInteraction buttonInteraction)
    {
        return new NamedObject<ICommand>(buttonInteraction.Command, buttonInteraction.Text);
    }

    /// <summary>
    /// Implicitly converts a <see cref="NamedObject{T}"/> into a
    /// <see cref="ButtonInteraction"/>.
    /// </summary>
    /// <param name="obj">Object to be converted.</param>
    public static implicit operator ButtonInteraction (NamedObject<ICommand> obj)
    {
        return new(obj.Value, obj.Name);
    }

    /// <summary>
    /// Implicitly converts an <see cref="Action"/> delegate into a
    /// <see cref="ButtonInteraction"/>.
    /// </summary>
    /// <param name="action">Object to be converted.</param>
    public static implicit operator ButtonInteraction (Action action)
    {
        return new(action, action.NameOf());
    }
    /// <summary>
    /// Implicitly converts an <see cref="Action"/> delegate into a
    /// <see cref="ButtonInteraction"/>.
    /// </summary>
    /// <param name="action">Object to be converted.</param>
    public static implicit operator ButtonInteraction(Func<Task> action)
    {
        return new(action, action.NameOf());
    }
}
