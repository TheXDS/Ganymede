using System.ComponentModel;
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
    /// Gets or sets a value that indicates whether or not this ViewModel is busy.
    /// </summary>
    bool IsBusy { get; set; }
}