using Avalonia.ReactiveUI;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Views;

public partial class LoginView : ReactiveUserControl<LoginViewModel>
{
    public LoginView()
    {
        InitializeComponent();
    }
}