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
        var operation = new Action<IProgress<ProgressReport>>(p => { });
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.RunOperation(operation));
    }

    [Test]
    public async Task RunOperation_String_Action_With_Valid_Operation_Runs_Successfully()
    {
        var title = "Test Title";
        var operation = new Action<IProgress<ProgressReport>>(p => { });
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.RunOperation(title, operation));
    }

    [Test]
    public async Task RunOperation_Func_With_Valid_Operation_Runs_Successfully()
    {
        var operation = new Func<IProgress<ProgressReport>, Task>(p => Task.CompletedTask);
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.RunOperation(operation));
    }

    [Test]
    public async Task RunOperation_String_Func_With_Valid_Operation_Runs_Successfully()
    {
        var title = "Test Title";
        var operation = new Func<IProgress<ProgressReport>, Task>(p => Task.CompletedTask);
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.RunOperation(title, operation));
    }

    [Test]
    public async Task RunOperation_T_Func_With_Valid_Operation_Runs_Successfully()
    {
        var operation = new Func<IProgress<ProgressReport>, Task<string>>(p => Task.FromResult("test"));
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.RunOperation(operation));
    }

    [Test]
    public async Task RunOperation_T_String_Func_With_Valid_Operation_Runs_Successfully()
    {
        var title = "Test Title";
        var operation = new Func<IProgress<ProgressReport>, Task<string>>(p => Task.FromResult("test"));
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.RunOperation(title, operation));
    }

    [Test]
    public async Task RunOperation_Bool_Action_With_Valid_Operation_Runs_Successfully()
    {
        var operation = new Action<IProgress<ProgressReport>, CancellationToken>((p, ct) => { });
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.RunOperation(operation));
    }

    [Test]
    public async Task RunOperation_Bool_String_Action_With_Valid_Operation_Runs_Successfully()
    {
        var title = "Test Title";
        var operation = new Action<IProgress<ProgressReport>, CancellationToken>((p, ct) => { });
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.RunOperation(title, operation));
    }

    [Test]
    public async Task RunOperation_Bool_Func_With_Valid_Operation_Runs_Successfully()
    {
        var operation = new Func<IProgress<ProgressReport>, CancellationToken, Task>(async (p, ct) => await Task.Delay(1, CancellationToken.None));
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.RunOperation(operation));
    }

    [Test]
    public async Task RunOperation_Bool_String_Func_With_Valid_Operation_Runs_Successfully()
    {
        var title = "Test Title";
        var operation = new Func<IProgress<ProgressReport>, CancellationToken, Task>(async (p, ct) => await Task.Delay(1, CancellationToken.None));
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.RunOperation(title, operation));
    }

    [Test]
    public async Task RunOperation_T_Bool_Func_With_Valid_Operation_Runs_Successfully()
    {
        var operation = new Func<IProgress<ProgressReport>, CancellationToken, Task<string>>(async (p, ct) => await Task.FromResult("test"));
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.RunOperation(operation));
    }

    [Test]
    public async Task RunOperation_T_Bool_String_Func_With_Valid_Operation_Runs_Successfully()
    {
        var title = "Test Title";
        var operation = new Func<IProgress<ProgressReport>, CancellationToken, Task<string>>(async (p, ct) => await Task.FromResult("test"));
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.RunOperation(title, operation));
    }

    [Test]
    public async Task AskYn_With_Valid_Question_Returns_Boolean()
    {
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.AskYn("Test question"));
    }

    [Test]
    public async Task AskYn_String_With_Valid_Question_Returns_Boolean()
    {
        var title = "Test Title";
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.AskYn(title, "Test question"));
    }

    [Test]
    public async Task AskYnc_With_Valid_Question_Returns_Boolean()
    {
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.AskYnc("Test question"));
    }

    [Test]
    public async Task AskYnc_String_With_Valid_Question_Returns_Boolean()
    {
        var title = "Test Title";
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.AskYnc(title, "Test question"));
    }

    [Test]
    public async Task Show_DialogTemplate_With_Valid_Template_Returns_Result()
    {
        var template = new DialogTemplate();
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.Show(template));
    }

    [Test]
    public async Task Message_With_Valid_Message_Returns_Result()
    {
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.Message("Test message"));
    }

    [Test]
    public async Task Message_String_With_Valid_Message_Returns_Result()
    {
        var title = "Test Title";
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.Message(title, "Test message"));
    }

    [Test]
    public async Task Warning_With_Valid_Message_Returns_Result()
    {
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.Warning("Test message"));
    }

    [Test]
    public async Task Warning_String_With_Valid_Message_Returns_Result()
    {
        var title = "Test Title";
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.Warning(title, "Test message"));
    }

    [Test]
    public async Task Error_With_Valid_Message_Returns_Result()
    {
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.Error("Test message"));
    }

    [Test]
    public async Task Error_Exception_With_Valid_Exception_Returns_Result()
    {
        var exception = new Exception("Test exception");
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.Error(exception));
    }

    [Test]
    public async Task Error_String_With_Valid_Message_Returns_Result()
    {
        var title = "Test Title";
        Assert.DoesNotThrowAsync(async () => await _mockService.Object.Error(title, "Test message"));
    }
}
