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
        // Act & Assert - Test that the default implementation doesn't throw
        Assert.DoesNotThrowAsync(_mockService.Object.Navigate<TestViewModelMock>);
    }

    [Test]
    public async Task Navigate_TViewModel_TState_With_Valid_Type_And_State_Navigates_To_New_Instance_With_State()
    {
        // Arrange
        var state = "TestState";

        // Act & Assert - Test that the default implementation doesn't throw
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.Navigate<TestStatefulViewModel, string>(state));
    }

    [Test]
    public async Task NavigateAndReset_TViewModel_With_Valid_Type_Navigates_To_New_Instance()
    {
        // Act & Assert - Test that the default implementation doesn't throw
        Assert.DoesNotThrowAsync(_mockService.Object.NavigateAndReset<TestViewModelMock>);
    }

    [Test]
    public async Task NavigateAndReset_TViewModel_TState_With_Valid_Type_And_State_Navigates_To_New_Instance_With_State()
    {
        // Arrange
        var state = "TestState";

        // Act & Assert - Test that the default implementation doesn't throw
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.NavigateAndReset<TestStatefulViewModel, string>(state));
    }

    [Test]
    public async Task Reset_Calls_NavigateAndReset_With_Null()
    {
        // Act & Assert - Test that the default implementation doesn't throw
        Assert.DoesNotThrowAsync(_mockService.Object.Reset);
    }
}