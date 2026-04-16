using Moq;
using TheXDS.Ganymede.Types.Base;

namespace TheXDS.Ganymede.Component;

internal class VisualResolverStackTests
{
    Mock<IVisualResolver<object>> resolver1;
    Mock<IVisualResolver<object>> resolver2;
    IViewModel viewModel;
    VisualResolverStack<object> stack;

    [SetUp]
    public void Setup()
    {
        resolver1 = new Mock<IVisualResolver<object>>();
        resolver2 = new Mock<IVisualResolver<object>>();
        viewModel = new Mock<IViewModel>().Object;
        stack = [resolver1.Object, resolver2.Object];
        resolver1.Setup(r => r.Resolve(It.IsAny<IViewModel>())).Returns(null!).Verifiable(Times.Once);
    }

    [TearDown]
    public void Teardown()
    {
        resolver1.Verify();
        resolver2.Verify();
    }

    [Test]
    public void Resolve_returns_first_non_null_view()
    {
        resolver2.Setup(r => r.Resolve(It.IsAny<IViewModel>())).Returns(new TestViewMock()).Verifiable(Times.Once);
        Assert.That(stack.Resolve(viewModel), Is.InstanceOf<TestViewMock>());
    }

    [Test]
    public void Resolve_returns_null_if_all_resolvers_return_null()
    {
        resolver2.Setup(r => r.Resolve(It.IsAny<IViewModel>())).Returns(null!).Verifiable(Times.Once);
        Assert.That(stack.Resolve(viewModel), Is.Null);
    }
}
