using Moq;
using TheXDS.Ganymede.Component;

namespace TheXDS.Ganymede.Services;

[TestFixture]
internal class INavigationServiceTests
{
    private Mock<INavigationService> _mockService;

    [SetUp]
    public void Setup()
    {
        _mockService = new Mock<INavigationService> { CallBase = true };
    }

    [Test]
    public async Task Navigate_TViewModel_With_Valid_Type_Navigates_To_New_Instance()
    {
        Assert.DoesNotThrowAsync(_mockService.Object.Navigate<TestViewModelMock>);
        _mockService.Verify(p => p.Navigate(It.IsAny<TestViewModelMock>()), Times.Once);
    }

    [Test]
    public async Task Navigate_TViewModel_TState_With_Valid_Type_And_State_Navigates_To_New_Instance_With_State()
    {
        var state = "TestState";
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.Navigate<TestStatefulViewModel, string>(state));
        _mockService.Verify(p => p.Navigate(It.Is<TestStatefulViewModel>(q => q.State == state)), Times.Once);
    }

    [Test]
    public async Task NavigateAndReset_TViewModel_With_Valid_Type_Navigates_To_New_Instance()
    {
        Assert.DoesNotThrowAsync(_mockService.Object.NavigateAndReset<TestViewModelMock>);
        _mockService.Verify(p => p.NavigateAndReset(It.IsAny<TestViewModelMock>()), Times.Once);
    }

    [Test]
    public async Task NavigateAndReset_TViewModel_TState_With_Valid_Type_And_State_Navigates_To_New_Instance_With_State()
    {
        var state = "TestState";
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.NavigateAndReset<TestStatefulViewModel, string>(state));
        _mockService.Verify(p => p.NavigateAndReset(It.Is<TestStatefulViewModel>(q => q.State == state)), Times.Once);
    }

    [Test]
    public async Task Reset_Calls_NavigateAndReset_With_Null()
    {
        Assert.DoesNotThrowAsync(_mockService.Object.Reset);
        _mockService.Verify(p => p.NavigateAndReset(null), Times.Once);
    }
}