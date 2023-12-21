using ReactiveUI;
using TheXDS.Ganymede.ViewModels;
using TheXDS.Ganymede.ViewModels.Base;

namespace TheXDS.Ganymede.Component.Locators;

/// <summary>
/// Matches registered ViewModels with their corresponding View.
/// </summary>
public class DictionaryViewLocator : IViewLocator
{
    private readonly Dictionary<Type, Type> _registry = new();

    /// <summary>
    /// Registers a ViewModel type with a corresponding View type.
    /// </summary>
    /// <typeparam name="TViewModel">Type of ViewModel to register.</typeparam>
    /// <typeparam name="TView">
    /// Type of View that supports presenting the specified ViewModel type.
    /// </typeparam>
    public void RegisterView<TViewModel, TView>() where TViewModel : ViewModelBase where TView : IViewFor<TViewModel>, new()
    {
        _registry.Add(typeof(TViewModel), typeof(TView));
    }

    /// <inheritdoc/>
    public IViewFor? ResolveView<T>(T? viewModel, string? contract = null)
    {
        return _registry.TryGetValue(typeof(T), out var view) ? (IViewFor?)Activator.CreateInstance(view) : null;
    }
}