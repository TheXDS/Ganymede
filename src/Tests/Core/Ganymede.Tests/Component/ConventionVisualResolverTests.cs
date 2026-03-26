using Moq;
using TheXDS.Ganymede.Types.Base;

namespace TheXDS.Ganymede.Component;

internal class ConventionVisualResolverTests
{

    [Test]
    public void Resolve_returns_view_based_on_naming_convention()
    {
        var resolver = new ConventionVisualResolver<object>();
        var viewModel = new TestViewModelMock();
        var view = resolver.Resolve(viewModel);
        Assert.That(view, Is.InstanceOf<TestViewMock>());
    }

    [Test]
    public void Resolve_returns_null_if_no_matching_view_found()
    {
        var resolver = new ConventionVisualResolver<object>();
        var viewModel = new Mock<IViewModel>().Object;
        var view = resolver.Resolve(viewModel);
        Assert.That(view, Is.Null);
    }

    [Test]
    public void Resolve_throws_on_null_viewmodel()
    {
        var resolver = new ConventionVisualResolver<object>();
        Assert.That(() => resolver.Resolve(null!), Throws.ArgumentNullException);
    }
}
