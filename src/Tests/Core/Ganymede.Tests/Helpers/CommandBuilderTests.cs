using Moq;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Services;
using TheXDS.Ganymede.Types.Base;

namespace TheXDS.Ganymede.Helpers;

public class CommandBuilderTests
{
    private class TestViewModel : IViewModel
    {
        private bool _isBusy;

        public IDialogService? DialogService { get; set; }
        public INavigationService? NavigationService { get; set; }
        public string? Title { get; set; }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (_isBusy == value) return;
                _isBusy = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsBusy)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }

    [Test]
    public void BuildSimple_Action_ExecutesAction()
    {
        var vm = new TestViewModel();
        var builder = CommandBuilder.For(vm);
        bool executed = false;
        ICommand command = builder.BuildSimple(() => executed = true);
        Assert.That(command.CanExecute(null), Is.True);
        command.Execute(null);
        Assert.That(executed, Is.True);
    }

    [Test]
    public void BuildSimple_Action_T_ExecutesAction()
    {
        var vm = new TestViewModel();
        var builder = CommandBuilder.For(vm);
        bool executed = false;
        ICommand command = builder.BuildSimple(_ => executed = true);
        Assert.That(command.CanExecute(null), Is.True);
        command.Execute(null);
        Assert.That(executed, Is.True);
    }

    [Test]
    public void BuildSimple_Func_Task_ExecutesAction()
    {
        var vm = new TestViewModel();
        var builder = CommandBuilder.For(vm);
        bool executed = false;
        ICommand command = builder.BuildSimple(() => { executed = true; return Task.CompletedTask; });
        Assert.That(command.CanExecute(null), Is.True);
        command.Execute(null);
        Assert.That(executed, Is.True);
    }

    [Test]
    public void BuildSimple_Func_T_Task_ExecutesAction()
    {
        var vm = new TestViewModel();
        var builder = CommandBuilder.For(vm);
        bool executed = false;
        ICommand command = builder.BuildSimple(_ => { executed = true; return Task.CompletedTask; });
        Assert.That(command.CanExecute(null), Is.True);
        command.Execute(null);
        Assert.That(executed, Is.True);
    }

    [Test]
    public void BuildInvalid_Throws_WhenExecuted()
    {
        var vm = new TestViewModel();
        var builder = CommandBuilder.For(vm);
        ICommand command = builder.BuildInvalid();
        Assert.That(() => command.Execute(null), Throws.TypeOf<InvalidOperationException>());
    }

    [Test]
    public async Task BuildBusyOperation_FuncTask_SetsIsBusyDuringExecutionAndResets()
    {
        var vm = new TestViewModel();
        var builder = CommandBuilder.For(vm);

        var started = new TaskCompletionSource<object?>();
        var continueSignal = new TaskCompletionSource<object?>();

        ICommand command = builder.BuildBusyOperation(async () =>
        {
            started.SetResult(null);
            await continueSignal.Task.ConfigureAwait(false);
        });

        command.Execute(null);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(await Task.WhenAny(started.Task, Task.Delay(1000)), Is.EqualTo(started.Task), "Operation did not start");
            Assert.That(vm.IsBusy, Is.True);
        }

        continueSignal.SetResult(null);

        await WaitUntil(() => !vm.IsBusy, TimeSpan.FromSeconds(2));

        Assert.That(vm.IsBusy, Is.False);
    }

    [Test]
    public void BuildNavigate_CallsNavigationServiceNavigateGeneric()
    {
        var vm = new TestViewModel();
        var nav = new Mock<INavigationService>();
        nav.Setup(n => n.Navigate<TestViewModel>()).Returns(Task.CompletedTask).Verifiable();
        vm.NavigationService = nav.Object;

        var command = CommandBuilder.For(vm).BuildNavigate<TestViewModel>();

        command.Execute(null);

        nav.Verify(n => n.Navigate<TestViewModel>(), Times.Once);
    }

    [Test]
    public void BuildNavigateBack_CallsNavigationServiceNavigateBack()
    {
        var vm = new TestViewModel();
        var nav = new Mock<INavigationService>();
        nav.Setup(n => n.NavigateBack()).Returns(Task.CompletedTask).Verifiable();
        vm.NavigationService = nav.Object;

        var command = CommandBuilder.For(vm).BuildNavigateBack();

        command.Execute(null);

        nav.Verify(n => n.NavigateBack(), Times.Once);
    }

    [Test]
    public void BuildObserving_ReturnsBuilder_ForVariousOverloads()
    {
        var vm = new TestViewModel();
        var builder = CommandBuilder.For(vm);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(builder.BuildObserving(() => { }), Is.Not.Null);
            Assert.That(builder.BuildObserving(() => Task.CompletedTask), Is.Not.Null);
            Assert.That(builder.BuildObserving(_ => { }), Is.Not.Null);
            Assert.That(builder.BuildObserving(_ => Task.CompletedTask), Is.Not.Null);
            Assert.That(builder.BuildObserving((IProgress<ProgressReport> _) => Task.CompletedTask), Is.Not.Null);
            Assert.That(builder.BuildObserving((IProgress<ProgressReport> _) => { }), Is.Not.Null);
            Assert.That(builder.BuildObserving((p, ct) => Task.CompletedTask), Is.Not.Null);
            Assert.That(builder.BuildObserving((p, ct) => { }), Is.Not.Null);
        }
    }

    [Test]
    public void BuildObserving_ActionWithProgressReport_ReturnsBuilder()
    {
        var vm = new TestViewModel();
        var builder = CommandBuilder.For(vm);
        var commandBuilder = builder.BuildObserving((IProgress<ProgressReport> _) => Task.CompletedTask);
        Assert.That(commandBuilder, Is.Not.Null);
        Assert.That(builder.BuildObserving((IProgress<ProgressReport> _) => Task.CompletedTask), Is.Not.Null);
    }

    private static async Task WaitUntil(Func<bool> condition, TimeSpan timeout)
    {
        var sw = Stopwatch.StartNew();
        while (!condition() && sw.Elapsed < timeout)
        {
            await Task.Delay(10).ConfigureAwait(false);
        }

        if (!condition())
        {
            throw new TimeoutException("Condition was not satisfied in the allotted time.");
        }
    }
}
