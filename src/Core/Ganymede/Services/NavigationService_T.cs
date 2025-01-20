using System.Collections;
using System.Collections.Specialized;
using System.Windows.Input;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Types.Base;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Base;

namespace TheXDS.Ganymede.Services;

/// <summary>
/// Implements a ViewModel-based navigation service.
/// </summary>
/// <typeparam name="T">
/// ViewModel type to expose.
/// </typeparam>
public class NavigationService<T> : NotifyPropertyChanged, INavigationService<T> where T : class, IViewModel
{
    private class ManualObserver(IEnumerable<T> source) : IEnumerable<T>, INotifyCollectionChanged
    {
        private readonly IEnumerable<T> _source = source;

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

    private readonly Stack<T> _navStack;
    private readonly ManualObserver _navStackInfo;
    private T? _homePage;

    /// <inheritdoc/>
    public event EventHandler<NavigationCompletedEventArgs>? NavigationCompleted;

    /// <inheritdoc/>
    public T? CurrentViewModel => _navStack.Count != 0 && _navStack.TryPeek(out var vm) ? vm : _homePage;

    /// <inheritdoc/>
    public int NavigationSetCount => _navStack.Count;

    /// <inheritdoc/>
    public IEnumerable<T> NavigationSet => _navStackInfo;

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
            if (Change(ref _homePage, value) && _navStack.Count == 0)
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
        NavigateBackCommand = this.Create(NavigateBack)
            .ListensTo(p => p.CurrentViewModel)
            .CanExecute(_navStack.Any)
            .Build();
        _navStackInfo = UiThread.Invoke(() => new ManualObserver(_navStack));
    }

    /// <inheritdoc/>
    protected override void OnInitialize(IPropertyBroadcastSetup broadcastSetup)
    {
        broadcastSetup.RegisterPropertyChangeBroadcast(() => CurrentViewModel, () => NavigationSetCount, () => NavigationSet);
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
    public async Task NavigateBack()
    {
        if (CurrentViewModel is { } vm)
        {
            var f = new CancelFlag(false);
            await vm.OnNavigateBack(f);
            if (f.IsCancelled) return;
        }
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
