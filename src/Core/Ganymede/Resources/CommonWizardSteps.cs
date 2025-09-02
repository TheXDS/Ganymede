using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Services;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Resources;

/// <summary>
/// Includes a set of functions that create common Wizard step ViewModels.
/// </summary>
public static class CommonWizardSteps
{
    /// <summary>
    /// Generates a simple text page.
    /// </summary>
    /// <typeparam name="TState">Type of state used in the wizard.</typeparam>
    /// <param name="message">Text message to be displayed.</param>
    /// <param name="nextLabel">
    /// Alternative label for the "Next" button.
    /// </param>
    /// <returns>
    /// A new <see cref="IWizardViewModel{TState}"/> that can be added to a wizard invocation.
    /// </returns>
    /// <seealso cref="IDialogService.Wizard{TState}(DialogTemplate, TState, IDialogService.Step{TState})"/>
    public static IWizardViewModel<TState> SimpleTextStep<TState>(string message, string? nextLabel = null)
    {
        return new SimpleTextWizardViewModel<TState>(message, nextLabel);
    }

    /// <summary>
    /// Generates a page that runs a cancellable operation in the wizard.
    /// </summary>
    /// <typeparam name="TState">Type of state used in the wizard.</typeparam>
    /// <param name="operation">Operation to be executed.</param>
    /// <returns>
    /// A new <see cref="IWizardViewModel{TState}"/> that can be added to a wizard invocation.
    /// </returns>
    /// <seealso cref="IDialogService.Wizard{TState}(DialogTemplate, TState, IDialogService.Step{TState})"/>
    public static IWizardViewModel<TState> CancellableOperation<TState>(Func<CancellationToken, IProgress<ProgressReport>, Task> operation)
    {
        return new CancellableOperationWizardViewModel<TState>(operation);
    }

    /// <summary>
    /// Generates a simple text page with a button that finishes the wizard.
    /// </summary>
    /// <typeparam name="TState">Type of state used in the wizard.</typeparam>
    /// <param name="message">Text message to be displayed.</param>
    /// <param name="finishLabel">
    /// Alternative label for the "Finish" button.
    /// </param>
    /// <returns>
    /// A new <see cref="IWizardViewModel{TState}"/> that can be added to a wizard invocation.
    /// </returns>
    /// <seealso cref="IDialogService.Wizard{TState}(DialogTemplate, TState, IDialogService.Step{TState})"/>
    public static IWizardViewModel<TState> FinishPage<TState>(string message, string? finishLabel = null)
    {
        return new SimpleTextFinishWizardViewModel<TState>(message, finishLabel);
    }
}
