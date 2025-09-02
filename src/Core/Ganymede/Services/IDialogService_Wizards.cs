using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Resources;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Services;

public partial interface IDialogService
{
    /// <summary>
    /// Navigates through a set of ViewModels that conform a wizard.
    /// </summary>
    /// <typeparam name="TState">
    /// Type of the state information shared by the ViewModels.
    /// </typeparam>
    /// <param name="viewModels">
    /// Collection of ViewModels to navigate to. Must be placed in the desired
    /// navigation order.
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await for the completion of
    /// the wizard.
    /// </returns>
    Task<bool> Wizard<TState>(params IWizardViewModel<TState>[] viewModels) where TState : new()
    {
        return Wizard(new TState(), viewModels);
    }

    /// <summary>
    /// Navigates through a set of ViewModels that conform a wizard.
    /// </summary>
    /// <typeparam name="TState">
    /// Type of the state information shared by the ViewModels.
    /// </typeparam>
    /// <param name="state">State information shared by the ViewModels.</param>
    /// <param name="viewModels">
    /// Collection of ViewModels to navigate to. Must be placed in the desired
    /// navigation order.
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await for the completion of
    /// the wizard.
    /// </returns>
    Task<bool> Wizard<TState>(TState state, params IWizardViewModel<TState>[] viewModels)
    {
        return Wizard(state, viewModels.Select(p => (Func<TState, IWizardViewModel<TState>>)(_ => p)).ToArray());
    }

    /// <summary>
    /// Navigates through a set of ViewModels that conform a wizard.
    /// </summary>
    /// <typeparam name="TState">
    /// Type of the state information shared by the ViewModels.
    /// </typeparam>
    /// <param name="viewModels">
    /// Collection of ViewModels to navigate to. Must be placed in the desired
    /// navigation order.
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await for the completion of
    /// the wizard.
    /// </returns>
    Task<bool> Wizard<TState>(params Func<TState, IWizardViewModel<TState>>[] viewModels) where TState : new()
    {
        return Wizard(new TState(), viewModels);
    }

    /// <summary>
    /// Navigates through a set of ViewModels that conform a wizard.
    /// </summary>
    /// <typeparam name="TState">
    /// Type of the state information shared by the ViewModels.
    /// </typeparam>
    /// <param name="state">State information shared by the ViewModels.</param>
    /// <param name="viewModels">
    /// Collection of ViewModels to navigate to. Must be placed in the desired
    /// navigation order.
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await for the completion of
    /// the wizard.
    /// </returns>
    Task<bool> Wizard<TState>(TState state, params Func<TState, IWizardViewModel<TState>>[] viewModels)
    {
        return Wizard(state, (p, i) => viewModels[i].Invoke(p));
    }

    /// <summary>
    /// Navigates through a set of ViewModels that conform a wizard.
    /// </summary>
    /// <typeparam name="TState">
    /// Type of the state information shared by the ViewModels.
    /// </typeparam>
    /// <param name="viewModels">
    /// Function that selects the next wizard step. If the function returns
    /// <see langword="null"/>, the wizard is closed and finished.
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await for the completion of
    /// the wizard.
    /// </returns>
    Task<bool> Wizard<TState>(Step<TState> viewModels) where TState : new()
    {
        return Wizard(new TState(), viewModels);
    }

    /// <summary>
    /// Navigates through a set of ViewModels that conform a wizard.
    /// </summary>
    /// <typeparam name="TState">
    /// Type of the state information shared by the ViewModels.
    /// </typeparam>
    /// <param name="state">State information shared by the ViewModels.</param>
    /// <param name="viewModels">
    /// Function that selects the next wizard step. If the function returns
    /// <see langword="null"/>, the wizard is closed and finished.
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await for the completion of
    /// the wizard.
    /// </returns>
    Task<bool> Wizard<TState>(TState state, Step<TState> viewModels)
    {
        return Wizard(CommonDialogTemplates.Wizard, state, viewModels);
    }

    /// <summary>
    /// Navigates through a set of ViewModels that conform a wizard.
    /// </summary>
    /// <typeparam name="TState">
    /// Type of the state information shared by the ViewModels.
    /// </typeparam>
    /// <param name="template">
    /// Template that describes the visual properties of the wizard.
    /// </param>
    /// <param name="viewModels">
    /// Function that selects the next wizard step. If the function returns
    /// <see langword="null"/>, the wizard is closed and finished.
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await for the completion of
    /// the wizard.
    /// </returns>
    Task<bool> Wizard<TState>(DialogTemplate template, Step<TState> viewModels) where TState : new()
    {
        return Wizard(template, new TState(), viewModels);
    }

    /// <summary>
    /// Navigates through a set of ViewModels that conform a wizard.
    /// </summary>
    /// <typeparam name="TState">
    /// Type of the state information shared by the ViewModels.
    /// </typeparam>
    /// <param name="template">
    /// Template that describes the visual properties of the wizard.
    /// </param>
    /// <param name="state">State information shared by the ViewModels.</param>
    /// <param name="viewModels">
    /// Function that selects the next wizard step. If the function returns
    /// <see langword="null"/>, the wizard is closed and finished.
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> that can be used to await for the completion of
    /// the wizard.
    /// </returns>
    Task<bool> Wizard<TState>(DialogTemplate template, TState state, Step<TState> viewModels);

    /// <summary>
    /// Defines a delegate for a function that allows a wizard to be built at
    /// runtime based on the current requested page and current wizard state.
    /// </summary>
    /// <typeparam name="TState">
    /// Type of the state information shared by the ViewModels.
    /// </typeparam>
    /// <param name="state">State information shared by the ViewModels.</param>
    /// <param name="stepNumber">
    /// Number of wizard step that has been requested.
    /// </param>
    /// <returns>
    /// An instance of <see cref="IWizardViewModel{TState}"/>, or
    /// <see langword="null"/> if the step does not exist and the wizard should
    /// be finished successfully.
    /// </returns>
    delegate IWizardViewModel<TState>? Step<TState>(TState state, int stepNumber);
}
