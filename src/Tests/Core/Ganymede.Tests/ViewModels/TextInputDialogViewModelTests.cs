namespace TheXDS.Ganymede.ViewModels;

[TestFixture]
public class TextInputDialogViewModelTests
{
    private TextInputDialogViewModel _viewModel = null!;

    [SetUp]
    public void Setup()
    {
        _viewModel = new TextInputDialogViewModel();
    }

    [Test]
    public void MaxLength_Property_ShouldDefaultToIntMaxValue()
    {
        Assert.That(_viewModel.MaxLength, Is.EqualTo(int.MaxValue));
    }

    [Test]
    public void MaxLength_Property_ShouldSetAndGet_Value()
    {
        _viewModel.MaxLength = 100;
        Assert.That(_viewModel.MaxLength, Is.EqualTo(100));
    }

    [Test]
    public void MaxLength_Property_ShouldAcceptNull()
    {
        _viewModel.MaxLength = null;
        Assert.That(_viewModel.MaxLength, Is.EqualTo(int.MaxValue));
    }

    [Test]
    public void Value_Property_ShouldAllowSettingString_WhenWithinMaxLength()
    {
        _viewModel.MaxLength = 10;
        _viewModel.Value = "Hello";
        Assert.That(_viewModel.Value, Is.EqualTo("Hello"));
    }

    [Test]
    public void Value_Property_ShouldRejectSettingString_WhenExceedsMaxLength()
    {
        _viewModel.MaxLength = 5;
        _viewModel.Value = "Hello World";
        Assert.That(_viewModel.Value, Is.Null);
    }

    [Test]
    public void Value_Property_ShouldAllowExactMaxLength()
    {
        _viewModel.MaxLength = 5;
        _viewModel.Value = "Hello";
        Assert.That(_viewModel.Value, Is.EqualTo("Hello"));
    }

    [Test]
    public void Value_Property_ShouldAllowEmptyString()
    {
        _viewModel.MaxLength = 5;
        _viewModel.Value = "";
        Assert.That(_viewModel.Value, Is.EqualTo(""));
    }

    [Test]
    public void Value_Property_ShouldAllowNull()
    {
        _viewModel.Value = null;
        Assert.That(_viewModel.Value, Is.Null);
    }

    [Test]
    public void Value_Property_ShouldRespectDefaultMaxLength_WhenUnlimited()
    {
        _viewModel.Value = new string('a', 10000);
        Assert.That(_viewModel.Value, Has.Length.EqualTo(10000));
    }

    [Test]
    public void MaxLength_Property_Change_ShouldAffectSubsequentValueSets()
    {
        _viewModel.MaxLength = 100;
        _viewModel.Value = "Short";
        Assert.That(_viewModel.Value, Is.EqualTo("Short"));

        _viewModel.MaxLength = 3;
        _viewModel.Value = "Too Long";
        Assert.That(_viewModel.Value, Is.EqualTo("Short"));
    }

    [Test]
    public void Value_Property_ShouldTriggerPropertyChanged()
    {
        bool notified = false;
        _viewModel.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(TextInputDialogViewModel.Value))
                notified = true;
        };

        _viewModel.Value = "Test";
        Assert.That(notified, Is.True);
    }

    [Test]
    public void MaxLength_Property_ShouldTriggerPropertyChanged()
    {
        bool notified = false;
        _viewModel.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(TextInputDialogViewModel.MaxLength))
                notified = true;
        };

        _viewModel.MaxLength = 50;
        Assert.That(notified, Is.True);
    }
}
