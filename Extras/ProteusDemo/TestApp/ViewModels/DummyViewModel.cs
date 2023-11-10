using TheXDS.Ganymede.Types.Base;

namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// ViewModel that does nothing.
/// </summary>
public class DummyViewModel : ViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DummyViewModel"/> class.
    /// </summary>
    public DummyViewModel()
    {
        Title = "Dummy ViewModel";
    }
}
