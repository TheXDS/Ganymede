using System.ComponentModel;
using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Services;

namespace TheXDS.Ganymede.Types.Base;

/// <summary>
/// Defines a set of members to be implemented by a type that provides of
/// ViewModel functionality.
/// </summary>
public interface IViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// Gets or sets the dialog service instance to be accessibe to this
    /// ViewModel.
    /// </summary>
    IDialogService? DialogService { get; set; }
    /// <summary>
    /// Gets or sets the navigation service instance to be accessible to this
    /// ViewModel.
    /// </summary>
    INavigationService? NavigationService { get; set; }

    /// <summary>
    /// Gets or sets the ViewModel title.
    /// </summary>
    string? Title { get; set; }

    /// <summary>
    /// Gets or sets a value that indicates whether or not this ViewModel is
    /// busy.
    /// </summary>
    bool IsBusy { get; set; }

    /// <summary>
    /// When implemented in a ViewModel, allows for custom logic to be applied
    /// upon navigation back in the stack.
    /// </summary>
    /// <param name="navigation">
    /// Flag used to request cancellation of the navigation action.
    /// </param>
    /// <remarks>
    /// This method will be invoked only when executing a normal navigation
    /// back in the stack from the currently active ViewModel. This method is
    /// not triggered upon navigation stack reset, nor application termination.
    /// </remarks>
    Task OnNavigateBack(CancelFlag navigation) => Task.CompletedTask;
}
