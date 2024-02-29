using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.Ganymede.Helpers.DependencyObjectHelpers;

namespace TheXDS.Ganymede.Controls;

/// <summary>
/// Implements a <see cref="TextBoxEx"/> stylized and extended for search
/// purposes.
/// </summary>
public class SearchBox : TextBoxEx
{
    private static readonly DependencyPropertyKey IsSearchActivePropertyKey;

    /// <summary>
    /// Identifies the <see cref="SearchCommand"/> depenency property.
    /// </summary>
    public static readonly DependencyProperty SearchCommandProperty;

    /// <summary>
    /// Identifies the <see cref="CloseSearchCommand"/> depenency property.
    /// </summary>
    public static readonly DependencyProperty CloseSearchCommandProperty;

    /// <summary>
    /// Identifies the <see cref="IsSearchActive"/> read-only depenency
    /// property.
    /// </summary>
    public static readonly DependencyProperty IsSearchActiveProperty;

    static SearchBox()
    {
        SetControlStyle<SearchBox>(DefaultStyleKeyProperty);
        (IsSearchActivePropertyKey, IsSearchActiveProperty) = NewDpRo<bool, SearchBox>(nameof(IsSearchActive));
        SearchCommandProperty = NewDp<ICommand, SearchBox>(nameof(SearchCommand));
        CloseSearchCommandProperty = NewDp<ICommand, SearchBox>(nameof(CloseSearchCommand));
    }

    /// <summary>
    /// Gets or sets the command used to search triggered by this
    /// <see cref="SearchBox"/>. The command argument will be the search
    /// query.
    /// </summary>
    public ICommand? SearchCommand
    {
        get => (ICommand?)GetValue(SearchCommandProperty);
        set => SetValue(SearchCommandProperty, value);
    }

    /// <summary>
    /// Gets or sets the command used to close the search.
    /// </summary>
    public ICommand? CloseSearchCommand
    {
        get => (ICommand?)GetValue(CloseSearchCommandProperty);
        set => SetValue(CloseSearchCommandProperty, value);
    }

    /// <summary>
    /// Gets a value that indicates if the search is active on this
    /// control.
    /// </summary>
    public bool IsSearchActive => (bool)GetValue(IsSearchActiveProperty);

    /// <summary>
    /// Initializes a new instance of the <see cref="SearchBox"/> class.
    /// </summary>
    public SearchBox()
    {
        KeyUp += OnKeyUp;
        TextChanged += OnTextChanged;
    }

    private void OnKeyUp(object sender, KeyEventArgs e)
    {
        if (sender != this) return;
        if (Keyboard.Modifiers != ModifierKeys.None) return;
        switch (e.Key)
        {
            case Key.Enter:
                SearchCommand?.Execute(Text);
                SelectAll();
                e.Handled = true;
                break;
            case Key.Escape:
                CloseSearchCommand?.Execute(null);
                Clear();
                e.Handled = true;
                break;
            default:
                return;
        }
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        SetValue(IsSearchActivePropertyKey, !Text.IsEmpty());
    }

    /// <summary>
    /// Destroys this instance of the <see cref="SearchBox"/> class.
    /// </summary>
    ~SearchBox()
    {
        KeyUp -= OnKeyUp;
        TextChanged -= OnTextChanged;
    }
}
