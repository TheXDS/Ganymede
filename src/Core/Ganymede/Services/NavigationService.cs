using System.Collections;
using System.Collections.Specialized;
using System.Windows.Input;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.Types.Base;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Base;

namespace TheXDS.Ganymede.Services;

/// <summary>
/// Implements a ViewModel-based navigation service.
/// </summary>
public class NavigationService<T> : NotifyPropertyChanged, INavigationService<T> where T : class, IViewModel
{
    private class ManualObserver : IEnumerable<T>, INotifyCollectionChanged
    {
        private readonly IEnumerable<T> _source;

        public ManualObserver(IEnumerable<T> source)
        {
            _source = source;
        }

        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        public IEnumerator<T> GetEnumerator()
        {
            return _source.Reverse().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_source.Reverse()).GetEnumerator();
        }

        public void NotifyChange()
        {
            UiThread.Invoke(() => CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset)));
        }
    }

    //private readonly ConcurrentStack<ViewModel> _navStack;
    private readonly Stack<T> _navStack;
    private readonly ManualObserver _navStackInfo;
    private T? _homePage;

    /// <inheritdoc/>
    public event EventHandler<NavigationCompletedEventArgs>? NavigationCompleted;

    /// <inheritdoc/>
    public T? CurrentViewModel => _navStack.Any() && _navStack.TryPeek(out var vm) ? vm : _homePage;

    /// <inheritdoc/>
    public int NavigationStackDepth => _navStack.Count;

    /// <inheritdoc/>
    public IEnumerable<T> NavigationStack => _navStackInfo;

    /// <summary>
    /// Gets or sets the Navigation Stack's home page.
    /// </summary>
    /// <remarks>
    /// When setting this property, the active navigation stack will remain
    /// as-is, and upon a request to navigate back until the navigation stack
    /// is empty, the active page will be set to this value.
    /// </remarks>
    public T? HomePage
    {
        get => _homePage;
        set
        {
            if (Change(ref _homePage, value) && !_navStack.Any())
            {
                Refresh();
            }
        }
    }

    /// <summary>
    /// Gets a reference to a command that can be used to navigate back in the
    /// navigation stack.
    /// </summary>
    public ICommand NavigateBackCommand { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="NavigationService{T}"/>
    /// class.
    /// </summary>
    public NavigationService()
    {
        _navStack = UiThread.Invoke(() => new Stack<T>());
        RegisterPropertyChangeBroadcast(nameof(CurrentViewModel), nameof(NavigationStackDepth), nameof(NavigationStack));
        NavigateBackCommand = this.Create(NavigateBack)
            .ListensTo(p => p.CurrentViewModel)
            .CanExecute(_navStack.Any)
            .Build();
        _navStackInfo = UiThread.Invoke(() => new ManualObserver(_navStack));
    }

    /// <inheritdoc/>
    public void Navigate(T viewModel)
    {
        UiThread.Invoke(() => _navStack.Push(viewModel ?? throw new ArgumentNullException(nameof(viewModel))));
        Refresh();
    }

    /// <inheritdoc/>
    public void NavigateAndReset(T? viewModel)
    {
        _navStack.Clear();
        if (viewModel is { })
        {
            Navigate(viewModel);
        }
        else
        {
            Refresh();
        }
    }

    /// <inheritdoc/>
    public void NavigateBack()
    {
        if (_navStack.TryPop(out _))
        {
            Refresh();
        }
    }

    /// <inheritdoc/>
    public override void Refresh()
    {
        NavigationCompleted?.Invoke(this, new(CurrentViewModel));
        Notify(nameof(CurrentViewModel));
        _navStackInfo.NotifyChange();
        (CurrentViewModel as IViewModel_Internal)?.InvokeOnCreated();
    }
}
