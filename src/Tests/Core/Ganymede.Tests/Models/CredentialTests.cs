using System.Security;

namespace TheXDS.Ganymede.Models;

[TestFixture]
internal class CredentialTests
{
    [Test]
    public void Ctor_initializes_properties()
    {
        var str = new SecureString();
        var cred = new Credential("Test", str);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(cred.User, Is.EqualTo("Test"));
            Assert.That(cred.Password, Is.SameAs(str));
        }
    }
}
