using System.Collections;
using System.Collections.Concurrent;
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
public class NavigationService : NotifyPropertyChanged, INavigationService
{
    private class ManualObserver : IEnumerable<ViewModel>, INotifyCollectionChanged
    {
        private readonly IEnumerable<ViewModel> _source;

        public ManualObserver(IEnumerable<ViewModel> source)
        {
            _source = source;
        }

        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        public IEnumerator<ViewModel> GetEnumerator()
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
    private readonly Stack<ViewModel> _navStack;
    private readonly ManualObserver _navStackInfo;
    private ViewModel? _homePage;

    /// <inheritdoc/>
    public event EventHandler<NavigationCompletedEventArgs>? NavigationCompleted;

    /// <inheritdoc/>
    public ViewModel? CurrentViewModel => _navStack.Any() && _navStack.TryPeek(out var vm) ? vm : _homePage;

    /// <inheritdoc/>
    public int NavigationStackDepth => _navStack.Count;

    /// <inheritdoc/>
    public IEnumerable<ViewModel> NavigationStack => _navStackInfo;

    /// <summary>
    /// Gets or sets the Navigation Stack's home page.
    /// </summary>
    /// <remarks>
    /// When setting this property, the active navigation stack will remain
    /// as-is, and upon a request to navigate back until the navigation stack
    /// is empty, the active page will be set to this value.
    /// </remarks>
    public ViewModel? HomePage
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
    /// Initializes a new instance of the <see cref="NavigationService"/>
    /// class.
    /// </summary>
    public NavigationService()
    {
        //_navStack = UiThread.Invoke(() => new ConcurrentStack<ViewModel>());
        _navStack = UiThread.Invoke(() => new Stack<ViewModel>());
        RegisterPropertyChangeBroadcast(nameof(CurrentViewModel), nameof(NavigationStackDepth), nameof(NavigationStack));
        NavigateBackCommand = this.Create(NavigateBack)
            .ListensTo(p => p.CurrentViewModel)
            .CanExecute(_navStack.Any)
            .Build();
        _navStackInfo = UiThread.Invoke(() => new ManualObserver(_navStack));
    }

    /// <inheritdoc/>
    public void Navigate(ViewModel viewModel)
    {
        UiThread.Invoke(() => _navStack.Push(viewModel ?? throw new ArgumentNullException(nameof(viewModel))));
        Refresh();
    }

    /// <inheritdoc/>
    public void NavigateAndReset(ViewModel? viewModel)
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
    }
}
