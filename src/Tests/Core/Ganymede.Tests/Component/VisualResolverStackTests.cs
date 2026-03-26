using Moq;
using TheXDS.Ganymede.Types.Base;

namespace TheXDS.Ganymede.Component;

internal class VisualResolverStackTests
{
    [Test]
    public void Resolve_returns_first_non_null_view()
    {
        var resolver1 = new Mock<IVisualResolver<object>>();
        resolver1.Setup(r => r.Resolve(It.IsAny<IViewModel>())).Returns(null!);
        var resolver2 = new Mock<IVisualResolver<object>>();
        resolver2.Setup(r => r.Resolve(It.IsAny<IViewModel>())).Returns(new TestViewMock());
        var stack = new VisualResolverStack<object> { resolver1.Object, resolver2.Object };
        var viewModel = new Mock<IViewModel>().Object;
        var view = stack.Resolve(viewModel);
        Assert.That(view, Is.InstanceOf<TestViewMock>());
    }

    [Test]
    public void Resolve_returns_null_if_all_resolvers_return_null()
    {
        var resolver1 = new Mock<IVisualResolver<object>>();
        resolver1.Setup(r => r.Resolve(It.IsAny<IViewModel>())).Returns(null!);
        var resolver2 = new Mock<IVisualResolver<object>>();
        resolver2.Setup(r => r.Resolve(It.IsAny<IViewModel>())).Returns(null!);
        var stack = new VisualResolverStack<object> { resolver1.Object, resolver2.Object };
        var viewModel = new Mock<IViewModel>().Object;
        var view = stack.Resolve(viewModel);
        Assert.That(view, Is.Null);
    }
}