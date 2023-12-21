using System.Runtime.CompilerServices;
using TheXDS.Ganymede.Models;
using St = TheXDS.Ganymede.Resources.Strings.Common;

namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Base class for all ViewModels that represent Wizard dialog steps.
/// </summary>
/// <typeparam name="T">Type of state information to use.</typeparam>
public abstract class WizardViewModel<T> : AwaitableDialogViewModel<WizardAction>, IWizardViewModel<T>
{
    private T _state = default!;

    /// <inheritdoc/>
    public virtual T State
    { 
        get => _state;
        set => Change(ref _state, value);
    }

    /// <summary>
    /// Adds an interaction used to navigate back on the wizard.
    /// </summary>
    /// <param name="label">
    /// Label to be displayed on the interaction. If ommited, defaults to
    /// <see cref="St.Back"/>.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void AddBackInteraction(string? label = null)
    {
        AddWizardInteraction(WizardAction.Back, label ?? St.Back);
    }

    /// <summary>
    /// Adds an interaction used to navigate forward on the wizard.
    /// </summary>
    /// <param name="label">
    /// Label to be displayed on the interaction. If ommited, defaults to
    /// <see cref="St.Next"/>.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void AddNextInteraction(string? label = null)
    {
        AddWizardInteraction(WizardAction.Next, label ?? St.Next);
    }

    /// <summary>
    /// Adds an interaction used to exit from the wizard, cancelling it.
    /// </summary>
    /// <param name="label">
    /// Label to be displayed on the interaction. If ommited, defaults to
    /// <see cref="St.Cancel"/>.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void AddCancelInteraction(string? label = null)
    {
        AddWizardInteraction(WizardAction.Cancel, label ?? St.Cancel);
    }

    /// <summary>
    /// Adds an interaction that indicates the user has finised interactiong
    /// with this wizard page.
    /// </summary>
    /// <param name="action">Action performed by the user.</param>
    /// <param name="label">Label to be displayed on the interaction.</param>
    protected void AddWizardInteraction(WizardAction action, string label)
    {
        Interactions.Add(new(() => CloseDialog(action), label));
    }
}
