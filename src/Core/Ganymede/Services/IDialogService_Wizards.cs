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
    Task<bool> Wizard<TState>(TState state, params Func<TState, IWizardViewModel<TState>>[] viewModels);
}
