using System.Security;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Tests.ViewModels;

public class CredentialInputDialogViewModelTests
{
    [Test]
    public void Constructor_Should_Initialize_Properties()
    {
        var viewModel = new CredentialInputDialogViewModel();

        using (Assert.EnterMultipleScope())
        {
            Assert.That(viewModel.User, Is.EqualTo(string.Empty));
            Assert.That(viewModel.IsUserEditable, Is.True);
            Assert.That(viewModel.Password, Is.Not.Null);
        }
    }

    [Test]
    public void User_Property_Should_Set_Value()
    {
        var viewModel = new CredentialInputDialogViewModel();
        var testUser = "testuser";

        viewModel.User = testUser;

        Assert.That(viewModel.User, Is.EqualTo(testUser));
    }

    [Test]
    public void IsUserEditable_Property_Should_Set_Value()
    {
        var viewModel = new CredentialInputDialogViewModel();
        var testEditable = false;

        viewModel.IsUserEditable = testEditable;

        Assert.That(viewModel.IsUserEditable, Is.EqualTo(testEditable));
    }

    [Test]
    public void Password_Property_Should_Set_Value()
    {
        var viewModel = new CredentialInputDialogViewModel();
        var testPassword = new SecureString();
        testPassword.AppendChar('p');
        testPassword.AppendChar('a');
        testPassword.AppendChar('s');
        testPassword.AppendChar('s');

        viewModel.Password = testPassword;

        Assert.That(viewModel.Password, Is.SameAs(testPassword));
    }

    [Test]
    public void Properties_Should_Be_Settable()
    {
        var viewModel = new CredentialInputDialogViewModel();
        var testUser = "testuser";
        var testEditable = false;
        var testPassword = new SecureString();
        testPassword.AppendChar('p');
        testPassword.AppendChar('a');
        testPassword.AppendChar('s');
        testPassword.AppendChar('s');

        viewModel.User = testUser;
        viewModel.IsUserEditable = testEditable;
        viewModel.Password = testPassword;

        using (Assert.EnterMultipleScope())
        {
            Assert.That(viewModel.User, Is.EqualTo(testUser));
            Assert.That(viewModel.IsUserEditable, Is.EqualTo(testEditable));
            Assert.That(viewModel.Password, Is.SameAs(testPassword));
        }
    }
}
