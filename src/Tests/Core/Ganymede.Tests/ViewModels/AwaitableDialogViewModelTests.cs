namespace TheXDS.Ganymede.ViewModels;

public class AwaitableDialogViewModelTests
{
    [Test]
    public void DialogAwaiter_Should_Return_Valid_Task()
    {
        var viewModel = new AwaitableDialogViewModel();
        var awaiter = ((IAwaitableDialogViewModel)viewModel).DialogAwaiter;
        Assert.That(awaiter, Is.Not.Null);
        Assert.That(awaiter, Is.TypeOf<Task>());
    }

    [Test]
    public async Task Close_Should_Complete_Awaiter_Task()
    {
        var viewModel = new AwaitableDialogViewModel();
        var awaiter = ((IAwaitableDialogViewModel)viewModel).DialogAwaiter;
        viewModel.Close();
        await awaiter;
        Assert.That(awaiter.IsCompleted, Is.True);
    }

    [Test]
    public void Close_Should_Set_IsBusy_To_False()
    {
        var viewModel = new AwaitableDialogViewModel();
        viewModel.Close();
        Assert.That(viewModel.IsBusy, Is.False);
    }

    [Test]
    public async Task Close_Should_Reset_Awaiter_With_New_Task()
    {
        var viewModel = new AwaitableDialogViewModel();
        var initialAwaiter = ((IAwaitableDialogViewModel)viewModel).DialogAwaiter;
        viewModel.Close();
        var newAwaiter = ((IAwaitableDialogViewModel)viewModel).DialogAwaiter;
        Assert.That(newAwaiter, Is.Not.Null);
        Assert.That(newAwaiter, Is.Not.SameAs(initialAwaiter));
        Assert.That(newAwaiter.IsCompleted, Is.False);
    }

    [Test]
    public async Task Multiple_Close_Calls_Should_Create_New_Tasks()
    {
        var viewModel = new AwaitableDialogViewModel();
        var awaiter1 = ((IAwaitableDialogViewModel)viewModel).DialogAwaiter;
        viewModel.Close();
        Assert.That(awaiter1.IsCompleted, Is.True);
        var awaiter2 = ((IAwaitableDialogViewModel)viewModel).DialogAwaiter;
        viewModel.Close();
        Assert.That(awaiter2.IsCompleted, Is.True);
        var awaiter3 = ((IAwaitableDialogViewModel)viewModel).DialogAwaiter;
        Assert.That(awaiter3.IsCompleted, Is.False);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(awaiter2, Is.Not.SameAs(awaiter1));
            Assert.That(awaiter3, Is.Not.SameAs(awaiter2));
            Assert.That(awaiter3, Is.Not.SameAs(awaiter1));
        }
        viewModel.Close();
        await Task.WhenAll(awaiter1, awaiter2, awaiter3);
    }
}
