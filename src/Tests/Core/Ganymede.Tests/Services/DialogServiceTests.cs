using Moq;
using TheXDS.Ganymede.Models;

namespace TheXDS.Ganymede.Services;

[TestFixture]
internal class DialogServiceTests
{
    private Mock<IDialogService> _mockService;

    [SetUp]
    public void Setup()
    {
        _mockService = new Mock<IDialogService> { CallBase = true };
    }

    [Test]
    public async Task RunOperation_Action_With_Valid_Operation_Runs_Successfully()
    {
        // Arrange
        var operation = new Action<IProgress<ProgressReport>>(p => { });

        // Act & Assert - Test that the default implementation doesn't throw
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.RunOperation(operation));
    }

    [Test]
    public async Task RunOperation_String_Action_With_Valid_Operation_Runs_Successfully()
    {
        // Arrange
        var title = "Test Title";
        var operation = new Action<IProgress<ProgressReport>>(p => { });

        // Act & Assert - Test that the default implementation doesn't throw
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.RunOperation(title, operation));
    }

    [Test]
    public async Task RunOperation_Func_With_Valid_Operation_Runs_Successfully()
    {
        // Arrange
        var operation = new Func<IProgress<ProgressReport>, Task>(p => Task.CompletedTask);

        // Act & Assert - Test that the default implementation doesn't throw
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.RunOperation(operation));
    }

    [Test]
    public async Task RunOperation_String_Func_With_Valid_Operation_Runs_Successfully()
    {
        // Arrange
        var title = "Test Title";
        var operation = new Func<IProgress<ProgressReport>, Task>(p => Task.CompletedTask);

        // Act & Assert - Test that the default implementation doesn't throw
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.RunOperation(title, operation));
    }

    [Test]
    public async Task RunOperation_T_Func_With_Valid_Operation_Runs_Successfully()
    {
        // Arrange
        var operation = new Func<IProgress<ProgressReport>, Task<string>>(p => Task.FromResult("test"));

        // Act & Assert - Test that the default implementation doesn't throw
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.RunOperation(operation));
    }

    [Test]
    public async Task RunOperation_T_String_Func_With_Valid_Operation_Runs_Successfully()
    {
        // Arrange
        var title = "Test Title";
        var operation = new Func<IProgress<ProgressReport>, Task<string>>(p => Task.FromResult("test"));

        // Act & Assert - Test that the default implementation doesn't throw
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.RunOperation(title, operation));
    }

    [Test]
    public async Task RunOperation_Bool_Action_With_Valid_Operation_Runs_Successfully()
    {
        // Arrange
        var operation = new Action<CancellationToken, IProgress<ProgressReport>>((ct, p) => { });

        // Act & Assert - Test that the default implementation doesn't throw
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.RunOperation(operation));
    }

    [Test]
    public async Task RunOperation_Bool_String_Action_With_Valid_Operation_Runs_Successfully()
    {
        // Arrange
        var title = "Test Title";
        var operation = new Action<CancellationToken, IProgress<ProgressReport>>((ct, p) => { });

        // Act & Assert - Test that the default implementation doesn't throw
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.RunOperation(title, operation));
    }

    [Test]
    public async Task RunOperation_Bool_Func_With_Valid_Operation_Runs_Successfully()
    {
        // Arrange
        var operation = new Func<CancellationToken, IProgress<ProgressReport>, Task>(async (ct, p) => await Task.Delay(1));

        // Act & Assert - Test that the default implementation doesn't throw
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.RunOperation(operation));
    }

    [Test]
    public async Task RunOperation_Bool_String_Func_With_Valid_Operation_Runs_Successfully()
    {
        // Arrange
        var title = "Test Title";
        var operation = new Func<CancellationToken, IProgress<ProgressReport>, Task>(async (ct, p) => await Task.Delay(1));

        // Act & Assert - Test that the default implementation doesn't throw
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.RunOperation(title, operation));
    }

    [Test]
    public async Task RunOperation_T_Bool_Func_With_Valid_Operation_Runs_Successfully()
    {
        // Arrange
        var operation = new Func<CancellationToken, IProgress<ProgressReport>, Task<string>>(async (ct, p) => await Task.FromResult("test"));

        // Act & Assert - Test that the default implementation doesn't throw
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.RunOperation(operation));
    }

    [Test]
    public async Task RunOperation_T_Bool_String_Func_With_Valid_Operation_Runs_Successfully()
    {
        // Arrange
        var title = "Test Title";
        var operation = new Func<CancellationToken, IProgress<ProgressReport>, Task<string>>(async (ct, p) => await Task.FromResult("test"));

        // Act & Assert - Test that the default implementation doesn't throw
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.RunOperation(title, operation));
    }

    [Test]
    public async Task AskYn_With_Valid_Question_Returns_Boolean()
    {
        // Act & Assert - Test that the default implementation doesn't throw
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.AskYn("Test question"));
    }

    [Test]
    public async Task AskYn_String_With_Valid_Question_Returns_Boolean()
    {
        // Arrange
        var title = "Test Title";

        // Act & Assert - Test that the default implementation doesn't throw
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.AskYn(title, "Test question"));
    }

    [Test]
    public async Task AskYnc_With_Valid_Question_Returns_Boolean()
    {
        // Act & Assert - Test that the default implementation doesn't throw
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.AskYnc("Test question"));
    }

    [Test]
    public async Task AskYnc_String_With_Valid_Question_Returns_Boolean()
    {
        // Arrange
        var title = "Test Title";

        // Act & Assert - Test that the default implementation doesn't throw
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.AskYnc(title, "Test question"));
    }

    [Test]
    public async Task Show_DialogTemplate_With_Valid_Template_Returns_Result()
    {
        // Arrange
        var template = new DialogTemplate();

        // Act & Assert - Test that the default implementation doesn't throw
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.Show(template));
    }

    [Test]
    public async Task Message_With_Valid_Message_Returns_Result()
    {
        // Act & Assert - Test that the default implementation doesn't throw
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.Message("Test message"));
    }

    [Test]
    public async Task Message_String_With_Valid_Message_Returns_Result()
    {
        // Arrange
        var title = "Test Title";

        // Act & Assert - Test that the default implementation doesn't throw
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.Message(title, "Test message"));
    }

    [Test]
    public async Task Warning_With_Valid_Message_Returns_Result()
    {
        // Act & Assert - Test that the default implementation doesn't throw
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.Warning("Test message"));
    }

    [Test]
    public async Task Warning_String_With_Valid_Message_Returns_Result()
    {
        // Arrange
        var title = "Test Title";

        // Act & Assert - Test that the default implementation doesn't throw
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.Warning(title, "Test message"));
    }

    [Test]
    public async Task Error_With_Valid_Message_Returns_Result()
    {
        // Act & Assert - Test that the default implementation doesn't throw
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.Error("Test message"));
    }

    [Test]
    public async Task Error_Exception_With_Valid_Exception_Returns_Result()
    {
        // Arrange
        var exception = new Exception("Test exception");

        // Act & Assert - Test that the default implementation doesn't throw
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.Error(exception));
    }

    [Test]
    public async Task Error_String_With_Valid_Message_Returns_Result()
    {
        // Arrange
        var title = "Test Title";

        // Act & Assert - Test that the default implementation doesn't throw
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.Error(title, "Test message"));
    }
}
