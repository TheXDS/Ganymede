using TheXDS.Triton.Services;

namespace TheXDS.Ivie.Services;

/// <summary>
/// Defines a set of memebrs to be implemented by a type that exposes service
/// functionality to get, push and process data from a repository.
/// </summary>
public interface IIvieService : IUserService
{
    /// <summary>
    /// Gets the display name for an employee given its user id.
    /// </summary>
    /// <param name="userId">User id associated to the employee.</param>
    /// <returns>
    /// A string with the display name for an employee with the associated user
    /// id.
    /// </returns>
    Task<string> GetEmployeeDisplayNameAsync(Guid userId);
}