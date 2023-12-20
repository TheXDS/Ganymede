using TheXDS.Ganymede.Component;
using TheXDS.Ganymede.Types.Base;
using TheXDS.Ganymede.ViewModels;
using TheXDS.Ivie.CrudGen;
using TheXDS.Ivie.Properties;
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
    /// Initializes a new instance of the <see cref="SettingsViewModel"/>
    /// class.
    /// </summary>
    public SettingsViewModel() : base(Sp.CommonPool.Resolve<ITritonService>()!)
    {
        Title = St.Title;
        //if (!(Configuration.FirstRun ?? true))
        //{
        //    SidebarInteractions = new CrudButtonInteraction[]
        //    {
        //        CreateCrudInteraction<LoginCredentialDescriptor>(St.ManageUsers, St.UserManagement, clearStack: true),
        //        CreateCrudInteraction<UserGroupDescriptor>(St.ManageGroups, St.UserManagement, clearStack: true),
        //    }.GroupBy(p => p.Group);
        //}
        //else 
        //{
        //    SidebarInteractions = new CrudButtonInteraction[]
        //    {
        //        new(NavigateToDbConnectionSettings, "Connection settings"){ Group = "General" }
        //    }.GroupBy(p => p.Group);
        //}
    }

    private void NavigateToDbConnectionSettings()
    {
        //ChildNavService.NavigateAndReset();

    }
}