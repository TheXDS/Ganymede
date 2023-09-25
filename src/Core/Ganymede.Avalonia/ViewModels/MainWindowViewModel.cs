using System.Collections.Specialized;
using Avalonia.Controls;
using ReactiveUI;
using TheXDS.Ganymede.Component;
using TheXDS.Ganymede.Services;
using TheXDS.Ganymede.ViewModels.Base;

namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// ViewModel that implements the main window functionality for a Ganymede App.
/// </summary>
public class MainWindowViewModel : ReactiveObject
{
    private string? _title;
    private WindowIcon? _windowIcon;

    /// <summary>
    /// Creates a new <see cref="MainWindowViewModel"/> and navigates to the
    /// specified ViewModel.
    /// </summary>
    /// <typeparam name="T">Type of ViewModel to navigate to.</typeparam>
    /// <returns>
    /// A new <see cref="MainWindowViewModel"/> where the navigation stack
    /// contains the specified ViewModel.
    /// </returns>
    public static MainWindowViewModel CreateAndNavigate<T>() where T : NavigatableViewModel, new()
    {
        var vm = new MainWindowViewModel();
        vm.Navigate<T>();
        return vm;
    }

    /// <summary>
    /// Gets or sets the main window title.
    /// </summary>
    public string? Title
    {
        get => _title;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }
    
    /// <summary>
    /// Gets or sets the icon to be shown on the title bar of the main window.
    /// </summary>
    public WindowIcon? WindowIcon
    {
        get => _windowIcon;
        set => this.RaiseAndSetIfChanged(ref _windowIcon, value);
    }
    
    /// <summary>
    /// Implements the main navigation for this ViewModel.
    /// </summary>
    public UiPresenter MainContent { get; } = new();
    
    /// <summary>
    /// Implements the overlay content for this ViewModel. Normally used for Dialogs, Pop-ups, etc.
    /// </summary>
    public DialogService OverlayContent { get; } = new();

    /// <summary>
    /// Gets a value that indicates whether or not the main content should be enabled.
    /// </summary>
    /// <value>
    /// <see langword="true"/> when the Overlay navigation stack is empty,
    /// <see langword="false"/> if the main content should be disabled due to
    /// the presence of content in the overlay navigation stack.
    /// </value>
    public bool MainContentEnabled => !OverlayContent.Content.Router.NavigationStack.Any();
    
    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindowViewModel"/>
    /// class.
    /// </summary>
    public MainWindowViewModel()
    {
        OverlayContent.Content.Router.NavigationStack.CollectionChanged += OnChangeNavStack;
    }

    /// <summary>
    /// Clears the main navigation stack, and navigates to the specified
    /// ViewModel.
    /// </summary>
    /// <typeparam name="T">Type of ViewModel to navigate to.</typeparam>
    public void Navigate<T>() where T : NavigatableViewModel, new()
    {
        MainContent.Router.NavigateAndReset.Execute(new T { HostScreen = MainContent, DialogService = OverlayContent});
    }
    
    /// <summary>
    /// Clears the main navigation stack, and navigates to the specified
    /// ViewModel.
    /// </summary>
    /// <typeparam name="T">Type of ViewModel to navigate to.</typeparam>
    /// <param name="initCallback">
    /// Callback to be used to further initialize the ViewModel that has been
    /// navigated to.
    /// </param>
    public void Navigate<T>(Action<T> initCallback) where T : NavigatableViewModel, new()
    {
        var vm = new T { HostScreen = MainContent, DialogService = OverlayContent };
        initCallback(vm);
        MainContent.Router.NavigateAndReset.Execute(vm);
    }

    private void OnChangeNavStack(object? sender, NotifyCollectionChangedEventArgs e)
    {
        this.RaisePropertyChanged(nameof(MainContentEnabled));
    }
}