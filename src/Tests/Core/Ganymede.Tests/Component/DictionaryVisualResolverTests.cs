using Moq;
using TheXDS.Ganymede.Types.Base;

namespace TheXDS.Ganymede.Component;

internal class DictionaryVisualResolverTests
{
    [Test]
    public void Resolve_returns_registered_view()
    {
        var resolver = new DictionaryVisualResolver<object>();
        resolver.Register<TestViewModelMock, TestViewMock>();
        var viewModel = new TestViewModelMock();
        var view = resolver.Resolve(viewModel);
        Assert.That(view, Is.InstanceOf<TestViewMock>());
    }

    [Test]
    public void Resolve_returns_null_for_unregistered_viewmodel()
    {
        var resolver = new DictionaryVisualResolver<object>();
        var viewModel = new Mock<IViewModel>().Object;
        var view = resolver.Resolve(viewModel);
        Assert.That(view, Is.Null);
    }
}
