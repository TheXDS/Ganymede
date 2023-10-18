using System.Security;

namespace TheXDS.Ganymede.Models;

/// <summary>
/// Represents a login credential that includes username and password.
/// </summary>
/// <param name="User">String that identifies the user.</param>
/// <param name="Password">Password.</param>
public record class Credential(string User, SecureString Password);
