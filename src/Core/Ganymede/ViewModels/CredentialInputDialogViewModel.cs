using System.Security;
using TheXDS.Ganymede.Models;

namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Implements a <see cref="DialogViewModel"/> for a dialog that lets a user
/// enter its credentials to perform a login.
/// </summary>
public class CredentialInputDialogViewModel : OkCancelDialogViewModel<Credential?>
{
    private string _user = string.Empty;
    private bool _isUserEditable = true;
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
    /// Gets or sets a value that indicates if the username field should be
    /// editable.
    /// </summary>
    public bool IsUserEditable
    {
        get => _isUserEditable;
        set => Change(ref _isUserEditable, value);
    }

    /// <summary>
    /// Gets or sets the password.
    /// </summary>    
    public SecureString Password
    {
        get => _password;
        set => Change(ref _password, value);
    }

    /// <inheritdoc/>
    protected override Credential? GetOkValue()
    {
        return new(User, Password);
    }
}
