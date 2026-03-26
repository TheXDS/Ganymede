using Moq;
using TheXDS.Ganymede.Types.Base;

namespace TheXDS.Ganymede.Component;

internal class StaticVisualResolverTests
{
    [Test]
    public void Resolve_returns_view_for_matching_viewmodel()
    {
        IVisualResolver<object> resolver = new StaticVisualResolver<TestViewModelMock, TestViewMock>();
        var viewModel = new TestViewModelMock();
        var view = resolver.Resolve(viewModel);
        Assert.That(view, Is.InstanceOf<TestViewMock>());
    }

    [Test]
    public void Resolve_returns_null_for_non_matching_viewmodel()
    {
        IVisualResolver<object> resolver = new StaticVisualResolver<TestViewModelMock, TestViewMock>();
        var viewModel = new Mock<IViewModel>().Object;
        var view = resolver.Resolve(viewModel);
        Assert.That(view, Is.Null);
    }
}
