using static TheXDS.MCART.Helpers.DependencyObjectHelpers;

namespace TheXDS.Ganymede.Controls;

/// <summary>
/// Base class for a <see cref="TextBoxEx"/> that includes functionality to
/// select files using the system's file picker dialog.
/// </summary>
public abstract class FilesystemSelectorTextBox : TextBoxEx
{
    /// <summary>
    /// Identifies the <see cref="DialogTitle"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty DialogTitleProperty;

    /// <summary>
    /// Initializes the <see cref="TextBoxEx"/> class.
    /// </summary>
    static FilesystemSelectorTextBox()
    {
        SetControlStyle<FilesystemSelectorTextBox>(DefaultStyleKeyProperty);
        DialogTitleProperty = NewDp<string, FilesystemSelectorTextBox>(nameof(DialogTitle));
    }

    private Button? _btnBrowse;

    /// <summary>
    /// Gets or sets a custom dialog title to be used when pressing the
    /// "Browse" button.
    /// </summary>
    public string? DialogTitle
    {
        get => (string)GetValue(DialogTitleProperty);
        set => SetValue(DialogTitleProperty, value);
    }

    /// <inheritdoc/>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        if (Template.FindName("PART_btnBrowse", this) is Button btnBrowse)
        {
            if (_btnBrowse is not null)
            {
                _btnBrowse.Click -= BtnBrowse_Click;
            }
            _btnBrowse = btnBrowse;
            btnBrowse.Click += BtnBrowse_Click;
        }
    }

    /// <summary>
    /// Event handler for the Click event on the "Browse" button.
    /// </summary>
    /// <param name="sender">Object that generated the event.</param>
    /// <param name="e">Event arguments.</param>
    protected abstract void BtnBrowse_Click(object sender, RoutedEventArgs e);
}
