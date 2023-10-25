namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Blank ViewModel used to resolve a Visual to be shown whenever there is no
/// entity selected.
/// </summary>
public sealed class BlankCrudViewModel : CrudViewModelBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BlankCrudViewModel"/> class.
    /// </summary>
    public BlankCrudViewModel()
    {
        Title = "Dashboard";
    }
}