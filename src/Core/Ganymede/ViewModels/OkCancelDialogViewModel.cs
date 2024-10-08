using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Resources.Strings;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Base class for all dialogs that display a dialog with both "OK" and
/// "Cancel" interactions that can return a value.
/// </summary>
/// <typeparam name="T">Type of value returned by the dialog.</typeparam>
public abstract class OkCancelDialogViewModel<T> : AwaitableDialogViewModel<DialogResult<T>>
{
    /// <summary>
    /// Gets the value to be returned when the dialog closes successfully.
    /// </summary>
    /// <returns>
    /// A value to be returned when the dialog closes successfully.
    /// </returns>
    protected abstract T GetOkValue();

    /// <summary>
    /// Gets a value to be returned when the dialog is cancelled.
    /// </summary>
    /// <returns>
    /// A value to be returned when the dialog is cancelled.
    /// </returns>
    protected virtual T GetCancelValue() => default!;

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="OkCancelDialogViewModel{T}"/> class.
    /// </summary>
    protected OkCancelDialogViewModel()
    {
        Interactions.AddRange([
            new(() => Close(new(true, GetOkValue())), Common.Ok) { IsPrimary = true },
            new(() => Close(new(false, GetCancelValue())), Common.Cancel)
        ]);
    }
}