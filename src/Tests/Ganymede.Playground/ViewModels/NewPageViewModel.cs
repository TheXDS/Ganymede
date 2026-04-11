using System.Windows.Input;
using TheXDS.Ganymede.Types.Base;
using TheXDS.MCART.Component;

namespace Ganymede.Playground.ViewModels;

public class NewPageViewModel : ViewModel
{
    public ICommand GoBackCommand { get; }

    public ICommand SpawnNewCommand { get; }

    public int Generation { get; set; } = 1;

    public NewPageViewModel()
    {
        GoBackCommand = new SimpleCommand(() => NavigationService!.NavigateBack());
        SpawnNewCommand = new SimpleCommand(() => NavigationService!.Navigate(new NewPageViewModel { Generation = Generation + 1 }));
    }
}
