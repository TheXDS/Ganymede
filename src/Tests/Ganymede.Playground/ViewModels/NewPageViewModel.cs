using System.Windows.Input;
using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Types.Base;
using TheXDS.MCART.Component;

namespace Ganymede.Playground.ViewModels;

public class NewPageViewModel : ViewModel, IViewModel
{
    public ICommand SpawnNewCommand { get; }

    public int Generation { get; set; } = 1;

    public bool AllowBack { get; set; } = true;

    public bool AllowForward { get; set; } = true;

    public NewPageViewModel()
    {
        SpawnNewCommand = new SimpleCommand(() => NavigationService!.Navigate(new NewPageViewModel { Generation = Generation + 1 }));
    }

    async Task IViewModel.OnNavigateBack(CancelFlag navigation)
    {
        if (!AllowBack)
        {
            navigation.Cancel();
            await DialogService!.Message("Navigation back has been cancelled (Like, in a case of unsaved changes)");
        }
    }

    async Task IViewModel.OnNavigateAway(CancelFlag navigation)
    {
        if (!AllowForward)
        {
            navigation.Cancel();
            await DialogService!.Message("Navigation forward has been cancelled (Like, if the access has been forbidden)");
        }
    }
}
