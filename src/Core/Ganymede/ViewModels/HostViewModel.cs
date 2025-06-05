using TheXDS.Ganymede.Services;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.Types.Base;
using TheXDS.MCART.Types.Base;

namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Defines a <see cref="ViewModel"/> with nested navigation capabilities.
/// </summary>
public class HostViewModel : ViewModelBase
{
    private readonly HostNavigationService<ViewModel> _navService = new();

    /// <summary>
    /// Gets a reference to the child navigation service on this
    /// instance.
    /// </summary>
    public INavigationService NavigationService => _navService;

    /// <summary>
    /// Initializes a new instance of the <see cref="HostViewModelBase"/>
    /// class, setting a new instance for the child navigation service and 
    /// reusing the dialog service from this instance to expose them to any
    /// children ViewModels.
    /// </summary>
    public HostViewModel(IViewModel parent)
    {
        _navService.SetHost(parent);
    }

    /// <summary>
    /// Creates a new <see cref="ButtonInteraction"/> used to navigate to the
    /// specified <see cref="IViewModel"/>.
    /// </summary>
    /// <typeparam name="TViewModel">
    /// Type of <see cref="IViewModel"/> to navigate to upon command
    /// invocation.
    /// </typeparam>
    /// <param name="label">Label to be displayed on the interaction.</param>
    /// <returns>
    /// A new <see cref="ButtonInteraction"/> that can be used to navigate to
    /// the specified <see cref="IViewModel"/>.
    /// </returns>
    protected ButtonInteraction CreateNavInteraction<TViewModel>(string label) where TViewModel : class, IViewModel, new()
    {
        return new(() => NavigationService.Navigate<TViewModel>(), label);
    }

    /// <summary>
    /// Creates a new <see cref="ButtonInteraction"/> used to navigate to the
    /// specified <see cref="IStatefulViewModel{TState}"/>.
    /// </summary>
    /// <typeparam name="TViewModel">
    /// Type of <see cref="IStatefulViewModel{TState}"/> to navigate to upon
    /// command invocation.
    /// </typeparam>
    /// <typeparam name="TState">
    /// Type of the <see cref="IStatefulViewModel{TState}"/> state data.
    /// </typeparam>
    /// <param name="state">
    /// State data to be set onto the <see cref="IStatefulViewModel{TState}"/>
    /// upon navigation.
    /// </param>
    /// <param name="label">Label to be displayed on the interaction.</param>
    /// <returns>
    /// A new <see cref="ButtonInteraction"/> that can be used to navigate to
    /// the specified <see cref="IStatefulViewModel{TState}"/>.
    /// </returns>
    protected ButtonInteraction CreateNavInteraction<TViewModel, TState>(TState state, string label) where TViewModel : class, IStatefulViewModel<TState>, new()
    {
        return new(() => NavigationService.Navigate<TViewModel, TState>(state), label);
    }
}
