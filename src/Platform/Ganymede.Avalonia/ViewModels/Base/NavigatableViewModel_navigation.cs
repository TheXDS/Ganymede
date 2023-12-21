using System.Reactive;
using ReactiveUI;

namespace TheXDS.Ganymede.ViewModels.Base;

public abstract partial class NavigatableViewModel
{
    /// <summary>
    /// Creates a new command used to navigate to a new ViewModel, adding
    /// it to the navigation stack.
    /// </summary>
    /// <typeparam name="TViewModel">ViewModel to navigate to.</typeparam>
    /// <returns>
    /// A new <see cref="ReactiveCommand{TParam,TResult}"/> that when
    /// invoked, will navigate to the specified ViewModel by adding it to
    /// the navigation stack. 
    /// </returns>
    protected ReactiveCommand<Unit, IRoutableViewModel> CreateNavigateCommand<TViewModel>() where TViewModel : NavigatableViewModel, new()
    {
        return CreateNavigateCommand(InitViewModel<TViewModel>);
    }
    
    /// <summary>
    /// Creates a new command used to navigate to a new ViewModel, adding
    /// it to the navigation stack.
    /// </summary>
    /// <param name="initCallback">
    /// Callback used to further initialize the ViewModel.
    /// </param>
    /// <typeparam name="TViewModel">ViewModel to navigate to.</typeparam>
    /// <returns>
    /// A new <see cref="ReactiveCommand{TParam,TResult}"/> that when
    /// invoked, will navigate to the specified ViewModel by adding it to
    /// the navigation stack. 
    /// </returns>
    protected ReactiveCommand<Unit, IRoutableViewModel> CreateNavigateCommand<TViewModel>(Action<TViewModel> initCallback) where TViewModel : NavigatableViewModel, new()
    {
        return CreateNavigateCommand(() => InitViewModel(initCallback));
    }

    /// <summary>
    /// Creates a new command used to navigate to a new ViewModel,
    /// replacing the entire navigation stack.
    /// </summary>
    /// <typeparam name="TViewModel">ViewModel to navigate to.</typeparam>
    /// <returns>
    /// A new <see cref="ReactiveCommand{TParam,TResult}"/> that when
    /// invoked, will navigate to the specified ViewModel by replacing the
    /// entire navigation stack. 
    /// </returns>
    protected ReactiveCommand<Unit, IRoutableViewModel> CreateHardNavigateCommand<TViewModel>() where TViewModel : NavigatableViewModel, new()
    {
        return CreateHardNavigateCommand(InitViewModel<TViewModel>);
    }
    
    /// <summary>
    /// Creates a new command used to navigate to a new ViewModel,
    /// replacing the entire navigation stack.
    /// </summary>
    /// <param name="initCallback">
    /// Callback used to further initialize the ViewModel.
    /// </param>
    /// <typeparam name="TViewModel">ViewModel to navigate to.</typeparam>
    /// <returns>
    /// A new <see cref="ReactiveCommand{TParam,TResult}"/> that when
    /// invoked, will navigate to the specified ViewModel by replacing the
    /// entire navigation stack. 
    /// </returns>
    protected ReactiveCommand<Unit, IRoutableViewModel> CreateHardNavigateCommand<TViewModel>(Action<TViewModel> initCallback) where TViewModel : NavigatableViewModel, new()
    {
        // return ReactiveCommand.CreateFromObservable(() =>
        // {
        //     var vm = InitViewModel<TViewModel>();
        //     initCallback(vm);
        //     return HostScreen.Router.NavigateAndReset.Execute(vm);
        // });
        return CreateHardNavigateCommand(() => InitViewModel(initCallback));
    }
    
    private static ReactiveCommand<Unit, IRoutableViewModel> CreateNavigateCommandInternal(Func<ReactiveCommand<IRoutableViewModel, IRoutableViewModel>> command, Func<NavigatableViewModel> factory)
    {
        return ReactiveCommand.CreateFromObservable(() => command().Execute(factory.Invoke()));
    }

    private ReactiveCommand<Unit, IRoutableViewModel> CreateHardNavigateCommand(Func<NavigatableViewModel> factory)
    {
        return CreateNavigateCommandInternal(() => HostScreen.Router.NavigateAndReset, factory);
    }
    
    private ReactiveCommand<Unit, IRoutableViewModel> CreateNavigateCommand(Func<NavigatableViewModel> factory)
    {
        return CreateNavigateCommandInternal(() => HostScreen.Router.Navigate, factory);
    }
    
    private TViewModel InitViewModel<TViewModel>() where TViewModel : NavigatableViewModel, new()
    {
        return new TViewModel { HostScreen = HostScreen, DialogService = DialogService };
    }
    
    private TViewModel InitViewModel<TViewModel>(Action<TViewModel> initCallback) where TViewModel : NavigatableViewModel, new()
    {
        var vm = InitViewModel<TViewModel>();
        initCallback(vm);
        return vm;
    }
}