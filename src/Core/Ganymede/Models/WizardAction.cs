namespace TheXDS.Ganymede.Models;

/// <summary>
/// Enumerates the navigation options on a wizard.
/// </summary>
public enum WizardAction : byte
{
    /// <summary>
    /// The user cancelled the wizard.
    /// </summary>
    Cancel,

    /// <summary>
    /// The user navigated back on the wizard.
    /// </summary>
    Back,

    /// <summary>
    /// The user navigated forward on the wizard.
    /// interaction.
    /// </summary>
    Next,

    /// <summary>
    /// The user has successfully completed the wizard.
    /// </summary>
    Finish
}