using Moq;
using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Resources.Exceptions;
using TheXDS.MCART.Types;

namespace TheXDS.Ganymede.Services;

[TestFixture]
internal class CustomizableDialogServiceTests
{
    [Test]
    public void AskYn_WithOverride_Should_Call_Override_Service()
    {
        // Arrange
        var mockOverride = new Mock<IDialogService>();
        var service = new CustomizableDialogService
        {
            AskYnOverride = mockOverride.Object
        };

        // Act - Cast to interface to call the explicit implementation
        var result = ((IDialogService)service).AskYn("Test question");

        // Assert
        mockOverride.Verify(x => x.AskYn("Test question"), Times.Once);
    }

    [Test]
    public void AskYn_With_Title_And_Question_WithOverride_Should_Call_Override_Service()
    {
        // Arrange
        var mockOverride = new Mock<IDialogService>();
        var service = new CustomizableDialogService
        {
            AskYnOverride = mockOverride.Object
        };

        // Act - Cast to interface to call the explicit implementation
        var result = ((IDialogService)service).AskYn("Test title", "Test question");

        // Assert
        mockOverride.Verify(x => x.AskYn("Test title", "Test question"), Times.Once);
    }

    [Test]
    public void AskYnc_WithOverride_Should_Call_Override_Service()
    {
        // Arrange
        var mockOverride = new Mock<IDialogService>();
        var service = new CustomizableDialogService
        {
            AskYncOverride = mockOverride.Object
        };

        // Act - Cast to interface to call the explicit implementation
        var result = ((IDialogService)service).AskYnc("Test question");

        // Assert
        mockOverride.Verify(x => x.AskYnc("Test question"), Times.Once);
    }

    [Test]
    public void AskYnc_With_Title_And_Question_WithOverride_Should_Call_Override_Service()
    {
        // Arrange
        var mockOverride = new Mock<IDialogService>();
        var service = new CustomizableDialogService
        {
            AskYncOverride = mockOverride.Object
        };

        // Act - Cast to interface to call the explicit implementation
        var result = ((IDialogService)service).AskYnc("Test title", "Test question");

        // Assert
        mockOverride.Verify(x => x.AskYnc("Test title", "Test question"), Times.Once);
    }

    [Test]
    public void Error_With_Message_WithOverride_Should_Call_Override_Service()
    {
        // Arrange
        var mockOverride = new Mock<IDialogService>();
        var service = new CustomizableDialogService
        {
            ErrorOverride = mockOverride.Object
        };

        // Act - Cast to interface to call the explicit implementation
        ((IDialogService)service).Error("Test message");

        // Assert
        mockOverride.Verify(x => x.Error("Test message"), Times.Once);
    }

    [Test]
    public void Error_With_Exception_WithOverride_Should_Call_Override_Service()
    {
        // Arrange
        var mockOverride = new Mock<IDialogService>();
        var service = new CustomizableDialogService
        {
            ErrorOverride = mockOverride.Object
        };

        // Act - Cast to interface to call the explicit implementation
        ((IDialogService)service).Error(new Exception("Test exception"));

        // Assert
        mockOverride.Verify(x => x.Error(It.IsAny<Exception>()), Times.Once);
    }

    [Test]
    public void Error_With_Title_And_Message_WithOverride_Should_Call_Override_Service()
    {
        // Arrange
        var mockOverride = new Mock<IDialogService>();
        var service = new CustomizableDialogService
        {
            ErrorOverride = mockOverride.Object
        };

        // Act - Cast to interface to call the explicit implementation
        ((IDialogService)service).Error("Test title", "Test message");

        // Assert
        mockOverride.Verify(x => x.Error("Test title", "Test message"), Times.Once);
    }

    [Test]
    public void GetCredential_WithOverride_Should_Call_Override_Service()
    {
        // Arrange
        var mockOverride = new Mock<IDialogService>();
        var service = new CustomizableDialogService
        {
            GetCredentialOverride = mockOverride.Object
        };
        var template = new DialogTemplate();

        // Act - Cast to interface to call the explicit implementation
        var result = ((IDialogService)service).GetCredential(template, "defaultUser", true);

        // Assert
        mockOverride.Verify(x => x.GetCredential(template, "defaultUser", true), Times.Once);
    }

    [Test]
    public void GetDirectoryPath_WithOverride_Should_Call_Override_Service()
    {
        // Arrange
        var mockOverride = new Mock<IDialogService>();
        var service = new CustomizableDialogService
        {
            GetDirectoryPathOverride = mockOverride.Object
        };
        var template = new DialogTemplate();

        // Act - Cast to interface to call the explicit implementation
        var result = ((IDialogService)service).GetDirectoryPath(template, "defaultPath");

        // Assert
        mockOverride.Verify(x => x.GetDirectoryPath(template, "defaultPath"), Times.Once);
    }

    [Test]
    public void GetFileOpenPath_WithOverride_Should_Call_Override_Service()
    {
        // Arrange
        var mockOverride = new Mock<IDialogService>();
        var service = new CustomizableDialogService
        {
            GetFileOpenPathOverride = mockOverride.Object
        };
        var template = new DialogTemplate();
        var filters = new List<FileFilterItem>();

        // Act - Cast to interface to call the explicit implementation
        var result = ((IDialogService)service).GetFileOpenPath(template, filters, "defaultPath");

        // Assert
        mockOverride.Verify(x => x.GetFileOpenPath(template, filters, "defaultPath"), Times.Once);
    }

    [Test]
    public void GetFilesOpenPath_WithOverride_Should_Call_Override_Service()
    {
        // Arrange
        var mockOverride = new Mock<IDialogService>();
        var service = new CustomizableDialogService
        {
            GetFilesOpenPathOverride = mockOverride.Object
        };
        var template = new DialogTemplate();
        var filters = new List<FileFilterItem>();

        // Act - Cast to interface to call the explicit implementation
        var result = ((IDialogService)service).GetFilesOpenPath(template, filters);

        // Assert
        mockOverride.Verify(x => x.GetFilesOpenPath(template, filters), Times.Once);
    }

    [Test]
    public void GetFileSavePath_WithOverride_Should_Call_Override_Service()
    {
        // Arrange
        var mockOverride = new Mock<IDialogService>();
        var service = new CustomizableDialogService
        {
            GetFileSavePathOverride = mockOverride.Object
        };
        var template = new DialogTemplate();
        var filters = new List<FileFilterItem>();

        // Act - Cast to interface to call the explicit implementation
        var result = ((IDialogService)service).GetFileSavePath(template, filters, "defaultPath");

        // Assert
        mockOverride.Verify(x => x.GetFileSavePath(template, filters, "defaultPath"), Times.Once);
    }

    [Test]
    public void GetInputRange_WithOverride_Should_Call_Override_Service()
    {
        // Arrange
        var mockOverride = new Mock<IDialogService>();
        var service = new CustomizableDialogService
        {
            GetInputRangeOverride = mockOverride.Object
        };
        var template = new DialogTemplate();

        // Act - Cast to interface to call the explicit implementation
        var result = ((IDialogService)service).GetInputRange<int>(template, 1, 10, 5, 8);

        // Assert
        mockOverride.Verify(x => x.GetInputRange<int>(template, 1, 10, 5, 8), Times.Once);
    }

    [Test]
    public void GetInputText_WithOverride_Should_Call_Override_Service()
    {
        // Arrange
        var mockOverride = new Mock<IDialogService>();
        var service = new CustomizableDialogService
        {
            GetInputTextOverride = mockOverride.Object
        };
        var template = new DialogTemplate();

        // Act - Cast to interface to call the explicit implementation
        var result = ((IDialogService)service).GetInputText(template, "defaultValue");

        // Assert
        mockOverride.Verify(x => x.GetInputText(template, "defaultValue"), Times.Once);
    }

    [Test]
    public void GetInputValue_WithOverride_Should_Call_Override_Service()
    {
        // Arrange
        var mockOverride = new Mock<IDialogService>();
        var service = new CustomizableDialogService
        {
            GetInputValueOverride = mockOverride.Object
        };
        var template = new DialogTemplate();

        // Act - Cast to interface to call the explicit implementation
        var result = ((IDialogService)service).GetInputValue<int>(template, 1, 10, 5);

        // Assert
        mockOverride.Verify(x => x.GetInputValue<int>(template, 1, 10, 5), Times.Once);
    }

    [Test]
    public void Message_With_Message_WithOverride_Should_Call_Override_Service()
    {
        // Arrange
        var mockOverride = new Mock<IDialogService>();
        var service = new CustomizableDialogService
        {
            MessageOverride = mockOverride.Object
        };

        // Act - Cast to interface to call the explicit implementation
        ((IDialogService)service).Message("Test message");

        // Assert
        mockOverride.Verify(x => x.Message("Test message"), Times.Once);
    }

    [Test]
    public void Message_With_Title_And_Message_WithOverride_Should_Call_Override_Service()
    {
        // Arrange
        var mockOverride = new Mock<IDialogService>();
        var service = new CustomizableDialogService
        {
            MessageOverride = mockOverride.Object
        };

        // Act - Cast to interface to call the explicit implementation
        ((IDialogService)service).Message("Test title", "Test message");

        // Assert
        mockOverride.Verify(x => x.Message("Test title", "Test message"), Times.Once);
    }

    [Test]
    public void RunOperation_With_Action_WithOverride_Should_Call_Override_Service()
    {
        // Arrange
        var mockOverride = new Mock<IDialogService>();
        var service = new CustomizableDialogService
        {
            RunOperationOverride = mockOverride.Object
        };

        // Act - Cast to interface to call the explicit implementation
        ((IDialogService)service).RunOperation("Test title", (progress) => Task.CompletedTask);

        // Assert
        mockOverride.Verify(x => x.RunOperation("Test title", It.IsAny<Func<IProgress<ProgressReport>, Task>>()), Times.Once);
    }

    [Test]
    public void RunOperation_With_Function_WithOverride_Should_Call_Override_Service()
    {
        // Arrange
        var mockOverride = new Mock<IDialogService>();
        var service = new CustomizableDialogService
        {
            RunOperationOverride = mockOverride.Object
        };

        // Act - Cast to interface to call the explicit implementation
        ((IDialogService)service).RunOperation<string>("Test title", (progress) => Task.FromResult("result"));

        // Assert
        mockOverride.Verify(x => x.RunOperation<string>("Test title", It.IsAny<Func<IProgress<ProgressReport>, Task<string>>>()),
            Times.Once);
    }

    [Test]
    public void RunOperation_With_CancellationToken_WithOverride_Should_Call_Override_Service()
    {
        // Arrange
        var mockOverride = new Mock<IDialogService>();
        var service = new CustomizableDialogService
        {
            RunOperationOverride = mockOverride.Object
        };

        // Act - Cast to interface to call the explicit implementation
        ((IDialogService)service).RunOperation("Test title", (ct, progress) => Task.CompletedTask);

        // Assert
        mockOverride.Verify(x => x.RunOperation("Test title", It.IsAny<Func<IProgress<ProgressReport>, CancellationToken, Task>>()),
            Times.Once);
    }

    [Test]
    public void RunOperation_With_CancellationToken_And_Function_WithOverride_Should_Call_Override_Service()
    {
        // Arrange
        var mockOverride = new Mock<IDialogService>();
        var service = new CustomizableDialogService
        {
            RunOperationOverride = mockOverride.Object
        };

        // Act - Cast to interface to call the explicit implementation
        ((IDialogService)service).RunOperation<string>("Test title", (ct, progress) => Task.FromResult("result"));

        // Assert
        mockOverride.Verify(x => x.RunOperation<string>("Test title", It.IsAny<Func<IProgress<ProgressReport>, CancellationToken, Task<string>>>()),
            Times.Once);
    }

    [Test]
    public void SelectOption_WithOverride_Should_Call_Override_Service()
    {
        // Arrange
        var mockOverride = new Mock<IDialogService>();
        var service = new CustomizableDialogService
        {
            SelectOptionOverride = mockOverride.Object
        };
        var template = new DialogTemplate();
        var options = new NamedObject<int>[] { new("Option1", 1), new("Option2", 2) };

        // Act - Cast to interface to call the explicit implementation
        var result = ((IDialogService)service).SelectOption<int>(template, options);

        // Assert
        mockOverride.Verify(x => x.SelectOption<int>(template, options), Times.Once);
    }

    [Test]
    public void Show_With_Template_WithOverride_Should_Call_Override_Service()
    {
        // Arrange
        var mockOverride = new Mock<IDialogService>();
        var service = new CustomizableDialogService
        {
            ShowOverride = mockOverride.Object
        };
        var template = new DialogTemplate();

        // Act - Cast to interface to call the explicit implementation
        ((IDialogService)service).Show(template);

        // Assert
        mockOverride.Verify(x => x.Show(template), Times.Once);
    }

    [Test]
    public void Warning_With_Message_WithOverride_Should_Call_Override_Service()
    {
        // Arrange
        var mockOverride = new Mock<IDialogService>();
        var service = new CustomizableDialogService
        {
            WarningOverride = mockOverride.Object
        };

        // Act - Cast to interface to call the explicit implementation
        ((IDialogService)service).Warning("Test message");

        // Assert
        mockOverride.Verify(x => x.Warning("Test message"), Times.Once);
    }

    [Test]
    public void Warning_With_Title_And_Message_WithOverride_Should_Call_Override_Service()
    {
        // Arrange
        var mockOverride = new Mock<IDialogService>();
        var service = new CustomizableDialogService
        {
            WarningOverride = mockOverride.Object
        };

        // Act - Cast to interface to call the explicit implementation
        ((IDialogService)service).Warning("Test title", "Test message");

        // Assert
        mockOverride.Verify(x => x.Warning("Test title", "Test message"), Times.Once);
    }

    [Test]
    public void When_No_Override_And_No_Default_Implementation_Then_UndefinedBehaviorException_Is_Thrown()
    {
        // Arrange
        var service = new CustomizableDialogService();

        // Act & Assert
        Assert.That(async () => await ((IDialogService)service).AskYn("Test question"),
            Throws.TypeOf<UndefinedBehaviorException>());
    }

    [Test]
    public void When_No_Override_But_Has_Default_Implementation_Then_Default_Implementation_Is_Used()
    {
        // Arrange
        var mockDefault = new Mock<IDialogService>();
        var service = new CustomizableDialogService
        {
            DefaultImplementation = mockDefault.Object
        };

        // Act - Cast to interface to call the explicit implementation
        var result = ((IDialogService)service).AskYn("Test question");

        // Assert
        mockDefault.Verify(x => x.AskYn("Test question"), Times.Once);
    }
}
