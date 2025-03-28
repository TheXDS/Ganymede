using System.Windows.Input;
using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Types.Base;
using TheXDS.MCART.Component;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.Ganymede.Services;

/// <summary>
/// Implements a navigation service that only allows one ViewModel to be
/// active at a time.
/// </summary>
/// <typeparam name="T">
/// Type of ViewModel to be hosted on this instance.
/// </typeparam>
public class SingletonNavigationService<T> : NotifyPropertyChanged, INavigationService<T> where T : class, IViewModel
{
    /// <inheritdoc/>
    public T? CurrentViewModel { get; set; }

    /// <inheritdoc/>
    public T? HomePage
    {
        get => CurrentViewModel;
        set => CurrentViewModel = value;
    }

    /// <inheritdoc/>
    public IEnumerable<T> NavigationSet => ((T?[])[CurrentViewModel]).NotNull();

    /// <inheritdoc/>
    public ICommand NavigateBackCommand => new SimpleCommand(() => CurrentViewModel = null);

    /// <inheritdoc/>
    public int NavigationSetCount => CurrentViewModel is not null ? 1 : 0;

    /// <inheritdoc/>
    public event EventHandler<NavigationCompletedEventArgs>? NavigationCompleted;

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="SingletonNavigationService{T}"/> class.
    /// </summary>
    public SingletonNavigationService()
    {
        Subscribe(() => CurrentViewModel, (_, p, t) => Refresh());
    }

    /// <inheritdoc/>
    public async Task Navigate(T viewModel)
    {
        var f = new CancelFlag(false);
        await (CurrentViewModel?.OnNavigateAway(f) ?? Task.CompletedTask);
        if (f.IsCancelled) return;
        CurrentViewModel = viewModel;
    }

    /// <inheritdoc/>
    public async Task NavigateAndReset(T? viewModel)
    {
        var f = new CancelFlag(false);
        await (CurrentViewModel?.OnNavigateAway(f) ?? Task.CompletedTask);
        if (f.IsCancelled) return;
        CurrentViewModel = viewModel;
    }

    /// <inheritdoc/>
    public async Task NavigateBack()
    {
        if (CurrentViewModel is { } vm)
        {
            var f = new CancelFlag(false);
            await vm.OnNavigateBack(f);
            if (f.IsCancelled) return;
        }
        CurrentViewModel = null;
    }

    /// <inheritdoc/>
    public override void Refresh()
    {
        NavigationCompleted?.Invoke(this, new(CurrentViewModel));
        Notify(nameof(CurrentViewModel));
        (CurrentViewModel as IViewModel_Internal)?.InvokeOnCreated();
    }
}