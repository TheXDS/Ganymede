using System.Windows.Input;
using ReactiveUI;

namespace TheXDS.Ganymede.Component;

/// <summary>
/// Represents a button interaction inside the application. These might include
/// buttons in dialog boxes, dynamically generated menus, etc. 
/// </summary>
public class ButtonInteraction : ReactiveObject
{
    private string _text;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ButtonInteraction"/>
    /// class.
    /// </summary>
    /// <param name="command">
    /// Command to associate with the interaction.
    /// </param>
    /// <param name="text">Display text for the interaction.</param>
    public ButtonInteraction(ICommand command, string text)
    {
        Command = command;
        _text = text;
    }

    /// <summary>
    /// Gets a reference to the command to be executed by activating this
    /// interaction.
    /// </summary>
    public ICommand Command { get; }

    /// <summary>
    /// Gets or sets the text to be displayed on the control used to activate
    /// this interaction.
    /// </summary>
    public string Text
    {
        get => _text;
        set => this.RaiseAndSetIfChanged(ref _text, value);
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ButtonInteraction"/> class. For designer purposes only.
    /// </summary>
    public ButtonInteraction() : this(null!, string.Empty)
    {
    }
}