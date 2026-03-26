using Moq;
using TheXDS.Ganymede.Types.Base;

namespace TheXDS.Ganymede.Component;

internal class ConstVisualResolverTests
{
    private class VisualMock { }

    [Test]
    public void Resolve_returns_new_instance()
    {
        IVisualResolver<object> resolver = new ConstVisualResolver<VisualMock>();
        Assert.That(resolver.Resolve(new Mock<IViewModel>().Object), Is.InstanceOf<VisualMock>());
    }

    [Test]
    public void Resolve_returns_different_instances()
    {
        IVisualResolver<object> resolver = new ConstVisualResolver<VisualMock>();
        var instance1 = resolver.Resolve(new Mock<IViewModel>().Object);
        var instance2 = resolver.Resolve(new Mock<IViewModel>().Object);
        Assert.That(instance1, Is.Not.SameAs(instance2));
    }

    [Test]
    public void Resolve_accepts_null_viewmodel()
    {
        IVisualResolver<object> resolver = new ConstVisualResolver<VisualMock>();
        Assert.That(resolver.Resolve(null!), Is.InstanceOf<VisualMock>());
    }
}
