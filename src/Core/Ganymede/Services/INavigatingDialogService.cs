using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Services;

/// <summary>
/// Defines a set of members to be implemented by a type that provides an
/// implementation of <see cref="IDialogService"/> by using navigation
/// implementing <see cref="INavigationService"/> as well.
/// </summary>
public interface INavigatingDialogService : INavigationService, IDialogService
{
}