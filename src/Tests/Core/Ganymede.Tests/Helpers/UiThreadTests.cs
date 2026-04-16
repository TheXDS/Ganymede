using Moq;
using TheXDS.Ganymede.Services;
using System.Reflection;

namespace TheXDS.Ganymede.Helpers;

[TestFixture]
public class UiThreadTests
{
    private static readonly FieldInfo _instanceField = typeof(UiThread).GetField("_instance", BindingFlags.NonPublic | BindingFlags.Static)!;
    private Mock<IUiThreadProxy> _mockProxy;
    private Action _mockAction;
    private Func<int> _mockFunc;

    [SetUp]
    public void SetUp()
    {
        _mockProxy = new Mock<IUiThreadProxy>();
        _mockAction = () => { };
        _mockFunc = () => 42;
    }

    [Test]
    public void SetProxy_NullInstance_SetsInstanceToNull()
    {
        UiThread.SetProxy(null);
        Assert.That(_instanceField.GetValue(null), Is.Null);
    }

    [Test]
    public void SetProxy_NonNullInstance_SetsInstance()
    {
        UiThread.SetProxy(_mockProxy.Object);
        Assert.That(_instanceField.GetValue(null), Is.SameAs(_mockProxy.Object));
    }

    [Test]
    public void DefaultProxyIsNull_WhenNotExplicitlySet()
    {
        Assert.That(_instanceField.GetValue(null), Is.Null);
    }

    [Test]
    public void Invoke_ActionWithProxy_InvokesProxy()
    {
        UiThread.SetProxy(_mockProxy.Object);
        UiThread.Invoke(_mockAction);
        _mockProxy.Verify(x => x.Invoke(_mockAction), Times.Once);
    }

    [Test]
    public void Invoke_ActionWithoutProxy_InvokesActionDirectly()
    {
        UiThread.SetProxy(null);
        bool actionInvoked = false;
        UiThread.Invoke(() => actionInvoked = true);
        Assert.That(actionInvoked, Is.True);
    }

    [Test]
    public void InvokeFunc_FuncWithProxy_InvokesProxy()
    {
        UiThread.SetProxy(_mockProxy.Object);
        UiThread.Invoke(_mockFunc);
        _mockProxy.Verify(x => x.Invoke(_mockFunc), Times.Once);
    }

    [Test]
    public void InvokeFunc_FuncWithoutProxy_InvokesFuncDirectly()
    {
        UiThread.SetProxy(null);
        int result = UiThread.Invoke(_mockFunc);
        Assert.That(result, Is.EqualTo(_mockFunc.Invoke()));
    }

    [Test]
    public void Invoke_ActionWithNullProxy_InvokesActionDirectly()
    {
        UiThread.SetProxy(null);
        bool actionInvoked = false;
        UiThread.Invoke(() => actionInvoked = true);
        Assert.That(actionInvoked, Is.True);
    }

    [Test]
    public void InvokeFunc_FuncWithNullProxy_InvokesFuncDirectly()
    {
        UiThread.SetProxy(null);
        int result = UiThread.Invoke(() => 100);
        Assert.That(result, Is.EqualTo(100));
    }

    [Test]
    public void InvokeFunc_WithProxy_ReturnsCorrectValue()
    {
        UiThread.SetProxy(_mockProxy.Object);
        var expected = 123;
        _mockProxy.Setup(x => x.Invoke(It.IsAny<Func<int>>())).Returns(expected);
        var result = UiThread.Invoke(() => 42);
        Assert.That(result, Is.EqualTo(expected));
    }
}
