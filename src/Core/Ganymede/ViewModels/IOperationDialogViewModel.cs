namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Defines a set of members to be implemented by a type that includes
/// operation dialog functionality.
/// </summary>
public interface IOperationDialogViewModel : IDialogViewModel
{
    /// <summary>
    /// Gets a value that indicates if the progress of the operation is not determined.
    /// </summary>
    public bool IsIndeterminate => double.IsNaN(Progress);

    /// <summary>
    /// Gets or sets a value that indicates the progress of a long-running
    /// operation.
    /// </summary>
    double Progress { get; set; }
}