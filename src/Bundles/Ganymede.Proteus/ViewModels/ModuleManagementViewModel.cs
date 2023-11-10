using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheXDS.Ganymede.Types.Base;
using System.Windows.Input;
using TheXDS.Ganymede.CrudGen;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.Services;
using TheXDS.Ganymede.Types.Base;
using TheXDS.Triton.Services.Base;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.Component;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.Ganymede.ViewModels;

public class ModuleManagementViewModel : ViewModel
{
    private ITritonService _tritonService;

    //public static ModuleManagementViewModel Create(params ICrudDescriptor[] descriptors)
    //{

    //}

    public IEnumerable<IGrouping<string, CrudButtonInteraction>> Interactions { get; }

    /// <summary>
    /// Contains the current navigation service used to handle navigation.
    /// </summary>
    public INavigationService<ViewModel> NavService { get; } = UiThread.Invoke(() => new NavigationService<ViewModel>());

    private void OnManage(Type descriptorType)
    {
        var description = descriptorType.New<ICrudDescriptor>().Description;
        var ep = new TritonFlatEntityProvider(_tritonService, description);
        var vm = new CrudPageViewModel(new[] { description }, _tritonService, ep) { DialogService = DialogService };
        NavigationService?.Navigate(vm);
    }

}
