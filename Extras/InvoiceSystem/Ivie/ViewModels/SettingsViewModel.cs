using TheXDS.Ganymede.CrudGen;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.Types.Base;
using TheXDS.Ganymede.ViewModels;
using TheXDS.Triton.Services.Base;
using Sp = TheXDS.ServicePool.ServicePool;
using St = TheXDS.Ivie.Resources.Strings.ViewModels.SettingsViewModel;

namespace TheXDS.Ivie.ViewModels;

/// <summary>
/// Implements a <see cref="ViewModel"/> used to cofigure the application.
/// </summary>
public class SettingsViewModel : ProteusHostViewModel
{
    /// <summary>
    /// Enumerates a set of options to be presented on the configuration page.
    /// </summary>
    public IEnumerable<ButtonInteraction> SettingsPages { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsViewModel"/>
    /// class.
    /// </summary>
    public SettingsViewModel() : base(Sp.CommonPool.Resolve<ITritonService>()!)
    {
        Title = St.Title;
        SettingsPages = new ButtonInteraction[]
        {
            CreateCrudInteraction<LoginCredentialDescriptor>(St.ManageUsers, clearStack: true),
            CreateCrudInteraction<UserGroupDescriptor>(St.ManageGroups, clearStack: true),
        };
    }
}