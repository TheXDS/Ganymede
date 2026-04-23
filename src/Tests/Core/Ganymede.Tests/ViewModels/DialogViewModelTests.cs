using System.Drawing;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Tests.ViewModels;

public class DialogViewModelTests
{
    DialogViewModel viewModel;

    [SetUp]
    public void Setup()
    {
        viewModel = new DialogViewModel();
    }

    [Test]
    public void Constructor_ShouldInitializeInteractionsCollection()
    {
        Assert.That(viewModel.Interactions, Is.Not.Null);
        Assert.That(viewModel.Interactions, Is.Empty);
    }

    [Test]
    public void IsIconVisible_ShouldBeFalse_WhenIconIsNull()
    {
        Assert.That(viewModel.IsIconVisible, Is.False);
    }

    [Test]
    public void IsIconVisible_ShouldBeFalse_WhenIconIsEmpty()
    {
        viewModel.Icon = string.Empty;
        Assert.That(viewModel.IsIconVisible, Is.False);
    }

    [Test]
    public void IsIconVisible_ShouldBeTrue_WhenIconHasValue()
    {
        viewModel.Icon = "TestIcon";
        Assert.That(viewModel.IsIconVisible, Is.True);
    }

    [Test]
    public void IsTitleVisible_ShouldBeFalse_WhenTitleIsNull()
    {
        Assert.That(viewModel.IsTitleVisible, Is.False);
    }

    [Test]
    public void IsTitleVisible_ShouldBeFalse_WhenTitleIsEmpty()
    {
        viewModel.Title = string.Empty;
        Assert.That(viewModel.IsTitleVisible, Is.False);
    }

    [Test]
    public void IsTitleVisible_ShouldBeTrue_WhenTitleHasValue()
    {
        viewModel.Title = "Test Title";
        Assert.That(viewModel.IsTitleVisible, Is.True);
    }

    [Test]
    public void Icon_Property_ShouldSetAndGet_Value()
    {
        const string testIcon = "TestIcon";
        viewModel.Icon = testIcon;
        Assert.That(viewModel.Icon, Is.EqualTo(testIcon));
    }

    [Test]
    public void IconBgColor_Property_ShouldSetAndGet_Value()
    {
        var testColor = Color.Red;
        viewModel.IconBgColor = testColor;
        Assert.That(viewModel.IconBgColor, Is.EqualTo(testColor));
    }

    [Test]
    public void Message_Property_ShouldSetAndGet_Value()
    {
        const string testMessage = "Test Message";
        viewModel.Message = testMessage;
        Assert.That(viewModel.Message, Is.EqualTo(testMessage));
    }

    [Test]
    public void Interactions_Collection_ShouldAllowAddingItems()
    {
        var interaction = new ButtonInteraction(() => { }, "OK");
        viewModel.Interactions.Add(interaction);
        Assert.That(viewModel.Interactions, Contains.Item(interaction));
        Assert.That(viewModel.Interactions, Has.Count.EqualTo(1));
    }

    [Test]
    public void Title_Property_ShouldSetAndGet_Value()
    {
        const string testTitle = "Test Title";
        viewModel.Title = testTitle;
        Assert.That(viewModel.Title, Is.EqualTo(testTitle));
    }
}
