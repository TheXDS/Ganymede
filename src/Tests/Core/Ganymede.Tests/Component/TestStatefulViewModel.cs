using System.Diagnostics.CodeAnalysis;
using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Types.Base;

namespace TheXDS.Ganymede.Component;

[ExcludeFromCodeCoverage]
internal class TestStatefulViewModel : ViewModel, IStatefulViewModel<string>
{
    public bool DidOnNavigateAwayRan { get; private set; }
    public bool DidOnNavigateBackRan { get; private set; }
    public bool ShouldCancelNavigation { get; set; }
    public string State { get; set; } = string.Empty;

    Task IViewModel.OnNavigateAway(CancelFlag navigation)
    {
        DidOnNavigateAwayRan = true;
        if (ShouldCancelNavigation) navigation.Cancel();
        return Task.CompletedTask;
    }

    Task IViewModel.OnNavigateBack(CancelFlag navigation)
    {
        DidOnNavigateBackRan = true;
        if (ShouldCancelNavigation) navigation.Cancel();
        return Task.CompletedTask;
    }
}