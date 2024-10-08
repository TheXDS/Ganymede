using TheXDS.MCART.Types;

namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Implements a <see cref="DialogViewModel"/> that allows an user to select a
/// value from a list of available options.
/// </summary>
public class SelectionDialogViewModel<TValue> : OkCancelDialogViewModel<TValue>
{
    private NamedObject<TValue>? _value;
    private IEnumerable<NamedObject<TValue>> _options = [];

    /// <summary>
    /// Gets or sets the actual value associated with this instance.
    /// </summary>
    public NamedObject<TValue>? Value
    {
        get => _value;
        set => Change(ref _value, value);
    }

    /// <summary>
    /// Gets or sets the available options on this dialog.
    /// </summary>
    public IEnumerable<NamedObject<TValue>> Options
    {
        get => _options;
        set => Change(ref _options, value);
    }

    /// <inheritdoc/>
    protected override TValue GetOkValue() => Value ?? default;
}