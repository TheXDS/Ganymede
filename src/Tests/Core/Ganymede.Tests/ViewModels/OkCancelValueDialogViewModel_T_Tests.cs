using System.Windows.Input;
using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.ViewModels;

[TestFixture]
public class OkCancelValueDialogViewModel_T_Tests
{
    private TestViewModel _viewModel = null!;

    [SetUp]
    public void Setup()
    {
        _viewModel = new TestViewModel();
    }

    [Test]
    public void Value_Property_ShouldSetAndGet_Value()
    {
        const string testValue = "TestValue";
        _viewModel.Value = testValue;
        Assert.That(_viewModel.Value, Is.EqualTo(testValue));
    }

    [Test]
    public void Value_Property_ShouldDefaultToNull_ForReferenceTypes()
    {
        Assert.That(_viewModel.Value, Is.Null);
    }

    [Test]
    public void Value_Property_ShouldDefaultToZero_ForValueType()
    {
        var intViewModel = new TestIntViewModel();
        Assert.That(intViewModel.Value, Is.EqualTo(0));
    }

    [Test]
    public void Value_Property_Setter_ShouldTriggerNotification()
    {
        bool notified = false;
        _viewModel.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(OkCancelValueDialogViewModel<string>.Value))
                notified = true;
        };

        _viewModel.Value = "First";
        Assert.That(notified, Is.True);

        notified = false;
        _viewModel.Value = "Second";
        Assert.That(notified, Is.True);
    }

    [Test]
    public void GetOkValue_ShouldReturnCurrentValue()
    {
        _viewModel.Value = "OkReturnValue";
        Assert.That(_viewModel.GetOkValue(), Is.EqualTo("OkReturnValue"));
    }

    [Test]
    public void GetOkValue_ShouldReturnDefaultValue_WhenValueNotSet()
    {
        Assert.That(_viewModel.GetOkValue(), Is.Null);
    }

    [Test]
    public void Interactions_ShouldContainOkAndCancel_WhenOkValueChanged()
    {
        _viewModel.Value = "ModifiedValue";
        Assert.That(_viewModel.Interactions, Has.Count.EqualTo(2));
        using (Assert.EnterMultipleScope())
        {
            Assert.That(_viewModel.Interactions.First().Text, Is.EqualTo("Ok"));
            Assert.That(_viewModel.Interactions.Last().Text, Is.EqualTo("Cancel"));
        }
    }

    [Test]
    public async Task DialogAwaiter_ShouldReturnOkWithValue_WhenOkPressed()
    {
        _viewModel.Value = "DialogResultValue";
        var awaiter = ((IAwaitableDialogViewModel<DialogResult<string>>)_viewModel).DialogAwaiter;
        
        ((ICommand)_viewModel.Interactions.First().Command).Execute(null);
        
        var result = await awaiter;
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.Success, Is.True);
            Assert.That(result.Result, Is.EqualTo("DialogResultValue"));
        }
    }

    [Test]
    public async Task DialogAwaiter_ShouldReturnCancelWithValue_WhenCancelPressed()
    {
        _viewModel.Value = "CancelValue";
        var awaiter = ((IAwaitableDialogViewModel<DialogResult<string>>)_viewModel).DialogAwaiter;
        
        ((ICommand)_viewModel.Interactions.Last().Command).Execute(null);
        
        var result = await awaiter;
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.Success, Is.False);
            Assert.That(result.Result, Is.Null);
        }
    }

    [Test]
    public void Value_Property_ShouldWorkWithStructTypes()
    {
        var viewModel = new TestStructViewModel();
        Assert.That(viewModel.Value, Is.EqualTo(default(Guid)));

        var testGuid = Guid.NewGuid();
        viewModel.Value = testGuid;
        Assert.That(viewModel.Value, Is.EqualTo(testGuid));
    }

    private class TestViewModel : OkCancelValueDialogViewModel<string>
    {
        public new string GetOkValue() => base.GetOkValue();
    }

    private class TestIntViewModel : OkCancelValueDialogViewModel<int>
    {
    }

    private class TestStructViewModel : OkCancelValueDialogViewModel<Guid>
    {
    }
}