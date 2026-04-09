using Moq;
using TheXDS.Ganymede.Component;

namespace TheXDS.Ganymede.Services;

[TestFixture]
public class INavigationService_T_Tests
{
    private TestViewModelMock _mockViewModel;
    private TestViewModelMock _mockViewModel1;
    private TestViewModelMock _mockViewModel2;
    private List<TestViewModelMock> _navigationSet;
    private Mock<INavigationService<TestViewModelMock>> _mockService;

    [SetUp]
    public void SetUp()
    {
        _mockViewModel = new TestViewModelMock();
        _mockViewModel1 = new TestViewModelMock();
        _mockViewModel2 = new TestViewModelMock();
        _navigationSet = [_mockViewModel1, _mockViewModel2];
        _mockService = new Mock<INavigationService<TestViewModelMock>>(MockBehavior.Loose) { CallBase = true };
    }

    [Test]
    public void CurrentViewModel_Should_Return_Current_ViewModel()
    {
        _mockService.Setup(s => s.CurrentViewModel).Returns(_mockViewModel);
        Assert.That(_mockService.Object.CurrentViewModel, Is.EqualTo(_mockViewModel));
    }

    [Test]
    public void HomePage_Should_Set_And_Get_Home_Page()
    {
        _mockService.SetupProperty(s => s.HomePage);
        _mockService.Object.HomePage = _mockViewModel;
        Assert.That(_mockService.Object.HomePage, Is.EqualTo(_mockViewModel));
    }

    [Test]
    public void NavigationSet_Should_Return_Navigation_Set()
    {
        _mockService.Setup(s => s.NavigationSet).Returns(_navigationSet);
        Assert.That(_mockService.Object.NavigationSet, Is.EquivalentTo(_navigationSet));
    }

    [Test]
    public void Should_Implement_Generic_Interface_Correctly()
    {
        _mockService.Setup(s => s.CurrentViewModel).Returns(_mockViewModel);
        _mockService.Setup(s => s.HomePage).Returns(_mockViewModel);
        _mockService.Setup(s => s.NavigationSet).Returns(_navigationSet);

        var result = _mockService.Object;

        Assert.That(result, Is.Not.Null);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.CurrentViewModel, Is.EqualTo(_mockViewModel));
            Assert.That(result.HomePage, Is.EqualTo(_mockViewModel));
            Assert.That(result.NavigationSet, Is.EquivalentTo(_navigationSet));
        }
    }
}
