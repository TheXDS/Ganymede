using TheXDS.Ganymede.Models;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Security;
using TheXDS.MCART.Types.Extensions;
using TheXDS.Triton.Models;
using TheXDS.Triton.Services.Base;

namespace TheXDS.Ivie.Properties;

public static class DbInitSteps
{
    public static async Task EnsureUsersExist(IProgress<ProgressReport> progress, ITritonService service)
    {
        static void AddToGroup(SecurityObject obj, params UserGroup[] groups)
        {
            foreach (var group in groups)
            {
                obj.Membership.Add(new() { Group = group, SecurityObject = obj });
            }
        }

        static LoginCredential Create(string username, string password, PermissionFlags granted = PermissionFlags.None, PermissionFlags revoked = PermissionFlags.None, bool enabled = true, params UserGroup[] groups)
        {
            var u = new LoginCredential(username, PasswordStorage.CreateHash<Argon2Storage>(password.ToSecureString()))
            {
                Granted = granted,
                Revoked = revoked,
                Enabled = enabled,
            };
            AddToGroup(u, groups);
            return u;
        }

        progress.Report("Checking users...");
        using var t = service.GetTransaction();
        if (!await Task.Run(t.All<LoginCredential>().Any))
        {
            progress.Report("Creating system users and groups...");
            var adminGroup = new UserGroup("Administrators", PermissionFlags.All, PermissionFlags.None);
            var elevatable = new UserGroup("Elevatable users", PermissionFlags.Elevate, PermissionFlags.None);
            var root = Create("root", Guid.NewGuid().ToString(), PermissionFlags.All, enabled: false);
            var admin = Create("admin", "password", groups: new[] { adminGroup, elevatable });
            var user = Create("user", "password", groups: new[] { elevatable });
            t.Create(root, admin, user, adminGroup, elevatable);
        }
    }

}
