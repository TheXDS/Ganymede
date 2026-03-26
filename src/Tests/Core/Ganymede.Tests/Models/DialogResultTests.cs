using TheXDS.Ganymede.Models;

namespace TheXDS.Ganymede.Tests.Models;

[TestFixture]
public class DialogResultTests
{
    [Test]
    public void DialogResult_OfString_DefaultConstructor_HasCorrectValues()
    {
        var dialogResult = new DialogResult<string>(false, null!);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(dialogResult.Success, Is.False);
            Assert.That(dialogResult.Result, Is.Null);
        }
    }

    [Test]
    public void DialogResult_OfString_SuccessConstructor_HasCorrectValues()
    {
        var dialogResult = new DialogResult<string>(true, "test value");
        using (Assert.EnterMultipleScope())
        {
            Assert.That(dialogResult.Success, Is.True);
            Assert.That(dialogResult.Result, Is.EqualTo("test value"));
        }
    }

    [Test]
    public void DialogResult_Fail_ReturnsFailureResult()
    {
        var dialogResult = DialogResult<string>.Fail();
        using (Assert.EnterMultipleScope())
        {
            Assert.That(dialogResult.Success, Is.False);
            Assert.That(dialogResult.Result, Is.Null);
        }
    }

    [Test]
    public void DialogResult_ImplicitBooleanConversion_FromSuccessResult_IsTrue()
    {
        var dialogResult = new DialogResult<int>(true, 42);
        bool result = dialogResult;
        Assert.That(result, Is.True);
    }

    [Test]
    public void DialogResult_ImplicitBooleanConversion_FromFailureResult_IsFalse()
    {
        var dialogResult = new DialogResult<int>(false, 0);
        bool result = dialogResult;
        Assert.That(result, Is.False);
    }

    [Test]
    public void DialogResult_ImplicitTConversion_FromSuccessResult_ReturnsResult()
    {
        var dialogResult = new DialogResult<string>(true, "test value");
        string result = dialogResult;
        Assert.That(result, Is.EqualTo("test value"));
    }

    [Test]
    public void DialogResult_ImplicitTConversion_FromFailureResult_ReturnsDefault()
    {
        var dialogResult = new DialogResult<string>(false, null!);
        string result = dialogResult;
        Assert.That(result, Is.Null);
    }

    [Test]
    public void DialogResult_ImplicitDialogResultConversion_FromValue_ReturnsSuccessResult()
    {
        string value = "test value";
        var dialogResult = (DialogResult<string>)value;
        using (Assert.EnterMultipleScope())
        {
            Assert.That(dialogResult.Success, Is.True);
            Assert.That(dialogResult.Result, Is.EqualTo(value));
        }
    }

    [Test]
    public void DialogResult_ImplicitDialogResultConversion_FromNullValue_ReturnsSuccessResultWithNull()
    {
        string? value = null;
        var dialogResult = (DialogResult<string?>)value;
        using (Assert.EnterMultipleScope())
        {
            Assert.That(dialogResult.Success, Is.True);
            Assert.That(dialogResult.Result, Is.Null);
        }
    }

    [Test]
    public void DialogResult_OfInt_SuccessConstructor_HasCorrectValues()
    {
        var dialogResult = new DialogResult<int>(true, 42);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(dialogResult.Success, Is.True);
            Assert.That(dialogResult.Result, Is.EqualTo(42));
        }
    }

    [Test]
    public void DialogResult_OfInt_Fail_ReturnsFailureResult()
    {
        var dialogResult = DialogResult<int>.Fail();
        using (Assert.EnterMultipleScope())
        {
            Assert.That(dialogResult.Success, Is.False);
            Assert.That(dialogResult.Result, Is.Zero);
        }
    }
}