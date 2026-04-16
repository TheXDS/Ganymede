using Moq;
using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Resources.Exceptions;
using TheXDS.MCART.Types;

namespace TheXDS.Ganymede.Services;

[TestFixture]
internal class CustomizableDialogServiceTests
{
    private static readonly DialogTemplate template = new();
    private Mock<IDialogService> dialogServiceMock;
    private CustomizableDialogService service;

    [SetUp]
    public void Setup()
    {
        dialogServiceMock = new Mock<IDialogService>();
        service = new CustomizableDialogService();
    }

    [Test]
    public void AskYn_WithOverride_Should_Call_Override_Service()
    {
        service.AskYnOverride = dialogServiceMock.Object;
        _ = ((IDialogService)service).AskYn("Test question");
        dialogServiceMock.Verify(x => x.AskYn("Test question"), Times.Once);
    }

    [Test]
    public void AskYn_With_Title_And_Question_WithOverride_Should_Call_Override_Service()
    {
        service.AskYnOverride = dialogServiceMock.Object;
        _ = ((IDialogService)service).AskYn("Test title", "Test question");
        dialogServiceMock.Verify(x => x.AskYn("Test title", "Test question"), Times.Once);
    }

    [Test]
    public void AskYnc_WithOverride_Should_Call_Override_Service()
    {
        service.AskYncOverride = dialogServiceMock.Object;
        _ = ((IDialogService)service).AskYnc("Test question");
        dialogServiceMock.Verify(x => x.AskYnc("Test question"), Times.Once);
    }

    [Test]
    public void AskYnc_With_Title_And_Question_WithOverride_Should_Call_Override_Service()
    {
        service.AskYncOverride = dialogServiceMock.Object;
        _ = ((IDialogService)service).AskYnc("Test title", "Test question");
        dialogServiceMock.Verify(x => x.AskYnc("Test title", "Test question"), Times.Once);
    }

    [Test]
    public void Error_With_Message_WithOverride_Should_Call_Override_Service()
    {
        service.ErrorOverride = dialogServiceMock.Object;
        ((IDialogService)service).Error("Test message");
        dialogServiceMock.Verify(x => x.Error("Test message"), Times.Once);
    }

    [Test]
    public void Error_With_Exception_WithOverride_Should_Call_Override_Service()
    {
        service.ErrorOverride = dialogServiceMock.Object;
        ((IDialogService)service).Error(new Exception("Test exception"));
        dialogServiceMock.Verify(x => x.Error(It.IsAny<Exception>()), Times.Once);
    }

    [Test]
    public void Error_With_Title_And_Message_WithOverride_Should_Call_Override_Service()
    {
        service.ErrorOverride = dialogServiceMock.Object;
        ((IDialogService)service).Error("Test title", "Test message");
        dialogServiceMock.Verify(x => x.Error("Test title", "Test message"), Times.Once);
    }

    [Test]
    public void GetCredential_WithOverride_Should_Call_Override_Service()
    {
        service.GetCredentialOverride = dialogServiceMock.Object;
        _ = ((IDialogService)service).GetCredential(template, "defaultUser", true);
        dialogServiceMock.Verify(x => x.GetCredential(template, "defaultUser", true), Times.Once);
    }

    [Test]
    public void GetDirectoryPath_WithOverride_Should_Call_Override_Service()
    {
        service.GetDirectoryPathOverride = dialogServiceMock.Object;
        _ = ((IDialogService)service).GetDirectoryPath(template, "defaultPath");
        dialogServiceMock.Verify(x => x.GetDirectoryPath(template, "defaultPath"), Times.Once);
    }

    [Test]
    public void GetFileOpenPath_WithOverride_Should_Call_Override_Service()
    {
        service.GetFileOpenPathOverride = dialogServiceMock.Object;
        IEnumerable<FileFilterItem> filters = [];
        _ = ((IDialogService)service).GetFileOpenPath(template, filters, "defaultPath");
        dialogServiceMock.Verify(x => x.GetFileOpenPath(template, filters, "defaultPath"), Times.Once);
    }

    [Test]
    public void GetFilesOpenPath_WithOverride_Should_Call_Override_Service()
    {
        service.GetFilesOpenPathOverride = dialogServiceMock.Object;
        IEnumerable<FileFilterItem> filters = [];
        _ = ((IDialogService)service).GetFilesOpenPath(template, filters);
        dialogServiceMock.Verify(x => x.GetFilesOpenPath(template, filters), Times.Once);
    }

    [Test]
    public void GetFileSavePath_WithOverride_Should_Call_Override_Service()
    {
        service.GetFileSavePathOverride = dialogServiceMock.Object;
        IEnumerable<FileFilterItem> filters = [];
        _ = ((IDialogService)service).GetFileSavePath(template, filters, "defaultPath");
        dialogServiceMock.Verify(x => x.GetFileSavePath(template, filters, "defaultPath"), Times.Once);
    }

    [Test]
    public void GetInputRange_WithOverride_Should_Call_Override_Service()
    {
        service.GetInputRangeOverride = dialogServiceMock.Object;
        _ = ((IDialogService)service).GetInputRange(template, 1, 10, 5, 8);
        dialogServiceMock.Verify(x => x.GetInputRange(template, 1, 10, 5, 8), Times.Once);
    }

    [Test]
    public void GetInputText_WithOverride_Should_Call_Override_Service()
    {
        service.GetInputTextOverride = dialogServiceMock.Object;
        _ = ((IDialogService)service).GetInputText(template, "defaultValue");
        dialogServiceMock.Verify(x => x.GetInputText(template, "defaultValue"), Times.Once);
    }

    [Test]
    public void GetInputValue_WithOverride_Should_Call_Override_Service()
    {
        service.GetInputValueOverride = dialogServiceMock.Object;
        _ = ((IDialogService)service).GetInputValue(template, 1, 10, 5);
        dialogServiceMock.Verify(x => x.GetInputValue(template, 1, 10, 5), Times.Once);
    }

    [Test]
    public void Message_With_Message_WithOverride_Should_Call_Override_Service()
    {
        service.MessageOverride = dialogServiceMock.Object;
        ((IDialogService)service).Message("Test message");
        dialogServiceMock.Verify(x => x.Message("Test message"), Times.Once);
    }

    [Test]
    public void Message_With_Title_And_Message_WithOverride_Should_Call_Override_Service()
    {
        service.MessageOverride = dialogServiceMock.Object;
        ((IDialogService)service).Message("Test title", "Test message");
        dialogServiceMock.Verify(x => x.Message("Test title", "Test message"), Times.Once);
    }

    [Test]
    public void RunOperation_With_Action_WithOverride_Should_Call_Override_Service()
    {
        service.RunOperationOverride = dialogServiceMock.Object;
        ((IDialogService)service).RunOperation("Test title", (progress) => Task.CompletedTask);
        dialogServiceMock.Verify(x => x.RunOperation("Test title", It.IsAny<Func<IProgress<ProgressReport>, Task>>()), Times.Once);
    }

    [Test]
    public void RunOperation_With_Function_WithOverride_Should_Call_Override_Service()
    {
        service.RunOperationOverride = dialogServiceMock.Object;
        ((IDialogService)service).RunOperation("Test title", (progress) => Task.FromResult("result"));
        dialogServiceMock.Verify(x => x.RunOperation("Test title", It.IsAny<Func<IProgress<ProgressReport>, Task<string>>>()), Times.Once);
    }

    [Test]
    public void RunOperation_With_CancellationToken_WithOverride_Should_Call_Override_Service()
    {
        service.RunOperationOverride = dialogServiceMock.Object;
        ((IDialogService)service).RunOperation("Test title", (ct, progress) => Task.CompletedTask);
        dialogServiceMock.Verify(x => x.RunOperation("Test title", It.IsAny<Func<IProgress<ProgressReport>, CancellationToken, Task>>()), Times.Once);
    }

    [Test]
    public void RunOperation_With_CancellationToken_And_Function_WithOverride_Should_Call_Override_Service()
    {
        service.RunOperationOverride = dialogServiceMock.Object;
        ((IDialogService)service).RunOperation("Test title", (ct, progress) => Task.FromResult("result"));
        dialogServiceMock.Verify(x => x.RunOperation("Test title", It.IsAny<Func<IProgress<ProgressReport>, CancellationToken, Task<string>>>()), Times.Once);
    }

    [Test]
    public void SelectOption_WithOverride_Should_Call_Override_Service()
    {
        service.SelectOptionOverride = dialogServiceMock.Object;
        var options = new NamedObject<int>[] { new("Option1", 1), new("Option2", 2) };
        _ = ((IDialogService)service).SelectOption(template, options);
        dialogServiceMock.Verify(x => x.SelectOption(template, options), Times.Once);
    }

    [Test]
    public void Show_With_Template_WithOverride_Should_Call_Override_Service()
    {
        service.ShowOverride = dialogServiceMock.Object;
        ((IDialogService)service).Show(template);
        dialogServiceMock.Verify(x => x.Show(template), Times.Once);
    }

    [Test]
    public void Warning_With_Message_WithOverride_Should_Call_Override_Service()
    {
        service.WarningOverride = dialogServiceMock.Object;
        ((IDialogService)service).Warning("Test message");
        dialogServiceMock.Verify(x => x.Warning("Test message"), Times.Once);
    }

    [Test]
    public void Warning_With_Title_And_Message_WithOverride_Should_Call_Override_Service()
    {
        service.WarningOverride = dialogServiceMock.Object;
        ((IDialogService)service).Warning("Test title", "Test message");
        dialogServiceMock.Verify(x => x.Warning("Test title", "Test message"), Times.Once);
    }

    [Test]
    public void When_No_Override_And_No_Default_Implementation_Then_UndefinedBehaviorException_Is_Thrown()
    {
        Assert.That(async () => await ((IDialogService)service).AskYn("Test question"), Throws.TypeOf<UndefinedBehaviorException>());
    }

    [Test]
    public void When_No_Override_But_Has_Default_Implementation_Then_Default_Implementation_Is_Used()
    {
        service.DefaultImplementation = dialogServiceMock.Object;
        _ = ((IDialogService)service).AskYn("Test question");
        dialogServiceMock.Verify(x => x.AskYn("Test question"), Times.Once);
    }

    [Test]
    public void When_Override_And_Default_Implementation_Then_Override_Is_Used()
    {
        service.DefaultImplementation = dialogServiceMock.Object;
        var overrideMock = new Mock<IDialogService>();
        service.AskYnOverride = overrideMock.Object;
        _ = ((IDialogService)service).AskYn("Test question");
        dialogServiceMock.Verify(x => x.AskYn("Test question"), Times.Never);
        overrideMock.Verify(x => x.AskYn("Test question"), Times.Once);
    }
}
