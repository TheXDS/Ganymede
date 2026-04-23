namespace TheXDS.Ganymede.ViewModels;

[TestFixture]
public class OkCancelDialogViewModelTests
{
    private TestOkCancelDialogViewModel _viewModel = null!;

    [SetUp]
    public void Setup()
    {
        _viewModel = new TestOkCancelDialogViewModel();
    }

    [Test]
    public void Constructor_ShouldInitializeInteractions()
    {
        Assert.That(_viewModel.Interactions.Count, Is.EqualTo(2));
        using (Assert.EnterMultipleScope())
        {
            Assert.That(_viewModel.Interactions.ElementAt(0).Text, Is.EqualTo("Ok"));
            Assert.That(_viewModel.Interactions.ElementAt(1).Text, Is.EqualTo("Cancel"));
            Assert.That(_viewModel.Interactions.ElementAt(0).IsPrimary, Is.True);
        }
    }

    [Test]
    public async Task DialogAwaiter_ShouldReturnOkValue_WhenOkIsPressed()
    {
        var awaiter = _viewModel.DialogAwaiter;
        
        _viewModel.Close(new(true, "TestValue"));
        
        var result = await awaiter;
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.Success, Is.True);
            Assert.That(result.Result, Is.EqualTo("TestValue"));
        }
    }

    [Test]
    public async Task DialogAwaiter_ShouldReturnCancelValue_WhenCancelIsPressed()
    {
        var awaiter = _viewModel.DialogAwaiter;
        
        _viewModel.Close(new(false, "CancelValue"));
        
        var result = await awaiter;
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.Success, Is.False);
            Assert.That(result.Result, Is.EqualTo("CancelValue"));
        }
    }

    private class TestOkCancelDialogViewModel : OkCancelDialogViewModel<string>
    {
        protected override string GetOkValue()
        {
            return "TestValue";
        }

        protected override string GetCancelValue()
        {
            return "CancelValue";
        }
    }
}
