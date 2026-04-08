using TheXDS.Ganymede.Component;
using TheXDS.Ganymede.Types.Base;

namespace TheXDS.Ganymede.Services;

[TestFixture]
internal class SingletonNavigationServiceTests
{
    private class TestService : SingletonNavigationService<ViewModel>
    {
        public bool DidRefreshRun { get; private set; }

        public override void Refresh()
        {
            base.Refresh();
            DidRefreshRun = true;
        }
    }

    private TestViewModelMock viewModel;
    private SingletonNavigationService<ViewModel> service;

    [SetUp]
    public void Setup()
    {
        viewModel = new TestViewModelMock();
        service = new SingletonNavigationService<ViewModel>();
    }

    [Test]
    public void CurrentViewModel_Get_Returns_Value()
    {
        service.CurrentViewModel = viewModel;
        var result = service.CurrentViewModel;
        Assert.That(result, Is.SameAs(viewModel));
    }

    [Test]
    public void HomePage_Get_Returns_CurrentViewModel()
    {
        service.CurrentViewModel = viewModel;
        var result = service.HomePage;
        Assert.That(result, Is.SameAs(viewModel));
    }

    [Test]
    public void HomePage_Set_Sets_CurrentViewModel()
    {
        service.HomePage = viewModel;
        Assert.That(service.CurrentViewModel, Is.SameAs(viewModel));
    }

    [Test]
    public void NavigationSet_Returns_CurrentViewModel_When_Not_Null()
    {
        service.CurrentViewModel = viewModel;
        var result = service.NavigationSet;
        Assert.That(result, Is.EquivalentTo([viewModel]));
    }

    [Test]
    public void NavigationSet_Returns_Empty_When_CurrentViewModel_Is_Null()
    {
        service.CurrentViewModel = null;
        var result = service.NavigationSet;
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void NavigationSetCount_Returns_1_When_CurrentViewModel_Is_Not_Null()
    {
        service.CurrentViewModel = viewModel;
        var result = service.NavigationSetCount;
        Assert.That(result, Is.EqualTo(1));
    }

    [Test]
    public void NavigationSetCount_Returns_0_When_CurrentViewModel_Is_Null()
    {
        service.CurrentViewModel = null;
        var result = service.NavigationSetCount;
        Assert.That(result, Is.Zero);
    }

    [Test]
    public async Task Navigate_With_Valid_ViewModel_Sets_CurrentViewModel()
    {
        await service.Navigate(viewModel);
        Assert.That(service.CurrentViewModel, Is.SameAs(viewModel));
    }

    [Test]
    public async Task NavigateAndReset_With_Valid_ViewModel_Sets_CurrentViewModel()
    {
        await service.NavigateAndReset(viewModel);
        Assert.That(service.CurrentViewModel, Is.SameAs(viewModel));
    }
    [Test]
    public async Task Navigate_With_Previous_ViewModel_Invokes_OnNavigateAway()
    {
        service.CurrentViewModel = viewModel;
        Assert.That(viewModel.DidOnNavigateAwayRan, Is.False);
        await service.Navigate(new TestViewModelMock());
        Assert.That(service.CurrentViewModel, Is.Not.SameAs(viewModel));
        Assert.That(viewModel.DidOnNavigateAwayRan, Is.True);
    }

    [Test]
    public async Task NavigateAndReset_With_Previous_ViewModel_Invokes_OnNavigateAway()
    {
        service.CurrentViewModel = viewModel;
        Assert.That(viewModel.DidOnNavigateAwayRan, Is.False);
        await service.NavigateAndReset(new TestViewModelMock());
        Assert.That(service.CurrentViewModel, Is.Not.SameAs(viewModel));
        Assert.That(viewModel.DidOnNavigateAwayRan, Is.True);
    }

    [Test]
    public async Task Navigate_With_Previous_ViewModel_Invokes_OnNavigateBack()
    {
        service.CurrentViewModel = viewModel;
        Assert.That(viewModel.DidOnNavigateBackRan, Is.False);
        await service.NavigateBack();
        Assert.That(service.CurrentViewModel, Is.Null);
        Assert.That(viewModel.DidOnNavigateBackRan, Is.True);
    }

    [Test]
    public async Task Navigate_With_Previous_ViewModel_When_OnNavigateAway_Cancels_Navigation_Then_Navigation_Stops()
    {
        service.CurrentViewModel = viewModel;
        viewModel.ShouldCancelNavigation = true;
        await service.Navigate(new TestViewModelMock());
        Assert.That(service.CurrentViewModel, Is.SameAs(viewModel));
    }

    [Test]
    public async Task NavigateAndReset_With_Previous_ViewModel_When_OnNavigateAway_Cancels_Navigation_Then_Navigation_Stops()
    {
        service.CurrentViewModel = viewModel;
        viewModel.ShouldCancelNavigation = true;
        await service.NavigateAndReset(new TestViewModelMock());
        Assert.That(service.CurrentViewModel, Is.SameAs(viewModel));
    }

    [Test]
    public async Task Navigate_With_Previous_ViewModel_When_OnNavigateBack_Cancels_Navigation_Then_Navigation_Stops()
    {
        service.CurrentViewModel = viewModel;
        viewModel.ShouldCancelNavigation = true;
        await service.NavigateBack();
        Assert.That(service.CurrentViewModel, Is.SameAs(viewModel));
    }

    [Test]
    public async Task Navigate_With_Null_ViewModel_Sets_CurrentViewModel_To_Null()
    {
        await service.Navigate(null!);
        Assert.That(service.CurrentViewModel, Is.Null);
    }

    [Test]
    public async Task NavigateAndReset_With_Null_ViewModel_Sets_CurrentViewModel_To_Null()
    {
        await service.NavigateAndReset(null);
        Assert.That(service.CurrentViewModel, Is.Null);
    }

    [Test]
    public async Task NavigateBack_With_Valid_ViewModel_Sets_CurrentViewModel_To_Null()
    {
        service.CurrentViewModel = viewModel;
        await service.NavigateBack();
        Assert.That(service.CurrentViewModel, Is.Null);
    }

    [Test]
    public async Task NavigateBack_With_Null_ViewModel_Sets_CurrentViewModel_To_Null()
    {
        await service.NavigateBack();
        Assert.That(service.CurrentViewModel, Is.Null);
    }

    [Test]
    public void NavigateBackCommand_Can_Execute()
    {
        var command = service.NavigateBackCommand;
        Assert.That(command.CanExecute(null), Is.True);
    }

    [Test]
    public void NavigateBackCommand_Execute_Sets_CurrentViewModel_To_Null()
    {
        service.CurrentViewModel = viewModel;
        var command = service.NavigateBackCommand;
        command.Execute(null);
        Assert.That(service.CurrentViewModel, Is.Null);
    }

    [Test]
    public void CurrentViewModel_change_triggers_Refresh()
    {
        TestService mock = new()
        {
            CurrentViewModel = viewModel
        };
        Assert.That(mock.DidRefreshRun, Is.True);
    }

    [Test]
    public async Task Refresh_triggers_NavigationCompleted_Event()
    {
        TestViewModelMock newVm = new();
        bool didEventTrigger = false;
        void OnNavigationCompleted(object? sender, NavigationCompletedEventArgs e)
        {
            didEventTrigger = true;
            using (Assert.EnterMultipleScope())
            {
                Assert.That(sender, Is.SameAs(service));
                Assert.That(e.IsReplacingView, Is.True);
                Assert.That(e.ViewModel, Is.SameAs(newVm));
            }
        }
        service.CurrentViewModel = viewModel;
        service.NavigationCompleted += OnNavigationCompleted;
        await service.Navigate(newVm);
        Assert.That(didEventTrigger, Is.True);
        service.NavigationCompleted -= OnNavigationCompleted;
    }
}
