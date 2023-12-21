using System.Security;

namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Implements a <see cref="DialogViewModel"/> for a dialog that lets a user
/// enter its credentials to perform a login.
/// </summary>
public class CredentialInputDialogViewModel : DialogViewModel
{
    private string _user = string.Empty;
    private SecureString _password = new();

    /// <summary>
    /// Gets or sets the Username.
    /// </summary>
    public string User
    {
        get => _user;
        set => Change(ref _user, value);
    }

    /// <summary>
    /// Gets or sets the password.
    /// </summary>    
    public SecureString Password
    {
        get => _password;
        set => Change(ref _password, value);
    }
}