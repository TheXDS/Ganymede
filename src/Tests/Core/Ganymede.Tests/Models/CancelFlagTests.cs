namespace TheXDS.Ganymede.Models;

[TestFixture]
public class CancelFlagTests
{
    [Test]
    public void CancelFlag_DefaultConstructor_HasCorrectDefaultValues()
    {
        var cancelFlag = new CancelFlag(false);
        Assert.That(cancelFlag.IsCancelled, Is.False);
    }

    [Test]
    public void CancelFlag_ConstructorWithTrueValue_HasCorrectValue()
    {
        var cancelFlag = new CancelFlag(true);
        Assert.That(cancelFlag.IsCancelled, Is.True);
    }

    [Test]
    public void CancelFlag_Cancel_Method_SetsIsCancelledToTrue()
    {
        var cancelFlag = new CancelFlag(false);
        Assert.That(cancelFlag.IsCancelled, Is.False);

        cancelFlag.Cancel();
        Assert.That(cancelFlag.IsCancelled, Is.True);
    }

    [Test]
    public void CancelFlag_Cancel_Method_CanBeCalledMultipleTimes()
    {
        var cancelFlag = new CancelFlag(false);
        Assert.That(cancelFlag.IsCancelled, Is.False);

        cancelFlag.Cancel();
        Assert.That(cancelFlag.IsCancelled, Is.True);

        cancelFlag.Cancel();
        Assert.That(cancelFlag.IsCancelled, Is.True);
    }

    [Test]
    public void CancelFlag_StructEquality()
    {
        var flag1 = new CancelFlag(true);
        var flag2 = new CancelFlag(true);
        var flag3 = new CancelFlag(false);

        Assert.That(flag1, Is.EqualTo(flag2));
        Assert.That(flag1, Is.Not.EqualTo(flag3));
        Assert.That(flag2, Is.Not.EqualTo(flag3));
    }

    [Test]
    public void CancelFlag_StructHashCode()
    {
        var flag1 = new CancelFlag(true);
        var flag2 = new CancelFlag(true);
        var flag3 = new CancelFlag(false);

        Assert.That(flag1.GetHashCode(), Is.EqualTo(flag2.GetHashCode()));
        Assert.That(flag1.GetHashCode(), Is.Not.EqualTo(flag3.GetHashCode()));
    }

    [Test]
    public void CancelFlag_Implements_Equals()
    {
        using (Assert.EnterMultipleScope())
        {
#pragma warning disable NUnit2010
            Assert.That(new CancelFlag(true).Equals(new CancelFlag(true)), Is.True);
            Assert.That(new CancelFlag(true).Equals(new CancelFlag(false)), Is.False);
            Assert.That(new CancelFlag(false).Equals(new CancelFlag(false)), Is.True);
            Assert.That(new CancelFlag(false).Equals(new CancelFlag(true)), Is.False);
            Assert.That(new CancelFlag(true).Equals((CancelFlag?)null), Is.False);

            Assert.That(new CancelFlag(true).Equals((object?)new CancelFlag(true)), Is.True);
            Assert.That(new CancelFlag(true).Equals((object?)null), Is.False);
            Assert.That(new CancelFlag(true).Equals("Test"), Is.False);
            Assert.That(new CancelFlag(true).Equals(1), Is.False);
#pragma warning restore NUnit2010
        }
    }
}
