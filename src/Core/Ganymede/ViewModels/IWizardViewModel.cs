using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Types.Base;

namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Defines a set of members to be implemented by a type that can be used as a
/// member of a wizard.
/// </summary>
/// <typeparam name="TState">
/// Type of state information used by this wizard member.
/// </typeparam>
public interface IWizardViewModel<TState> : IAwaitableDialogViewModel<WizardAction>, IStatefulViewModel<TState>
{
}
