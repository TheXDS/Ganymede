using TheXDS.Ganymede.Models;

namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Implements a <see cref="DialogViewModel"/> for dialogs that lets a user
/// input text data.
/// </summary>
public class TextInputDialogViewModel : OkCancelValueDialogViewModel<string?>
{
    private int? _maxLength;

    /// <summary>
    /// Gets or sets the maximum alloed length of text to be entered in the
    /// input dialog.
    /// </summary>
    public int? MaxLength
    {
        get => _maxLength ?? int.MaxValue;
        set => Change(ref _maxLength, value);
    }
}
