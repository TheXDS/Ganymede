using System;
using System.Reactive;
using ReactiveUI;
using TheXDS.Ganymede.ViewModels.Base;

namespace TheXDS.Ganymede.ViewModels;

public class LoginViewModel : NavigatableViewModel
{
    private string _username;
    private string _password;

    public string Username
    {
        get => _username;
        set => Change(ref _username, value);
    }
    
    public string Password
    {
        get => _password;
        set => Change(ref _password, value);
    }

    public ReactiveCommand<Unit, IRoutableViewModel> LoginCommand { get; }

    public LoginViewModel()
    {
        _username = String.Empty;
        _password = String.Empty;
        LoginCommand = CreateHardNavigateCommand<HelloViewModel>(vm => vm.Username = Username );
    }
}