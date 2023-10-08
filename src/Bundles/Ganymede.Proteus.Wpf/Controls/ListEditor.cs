using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using TheXDS.Ganymede.Controls.Base;
using TheXDS.Triton.Models.Base;
using static TheXDS.Ganymede.Helpers.DependencyObjectHelpers;

namespace TheXDS.Ganymede.Controls;

/// <summary>
/// Controls that allows the user to manage a collection of
/// <see cref="Model"/>.
/// </summary>
public class ListEditor : ObjectEditor
{
    /// <summary>
    /// Identifiees the <see cref="RemoveCommand"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty RemoveCommandProperty;

    /// <summary>
    /// Identifies the <see cref="Collection"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty CollectionProperty;

    static ListEditor()
    {
        RemoveCommandProperty = NewDp<ICommand, ListEditor>(nameof(RemoveCommand));
        CollectionProperty = NewDp<ICollection<Model>, ListEditor>(nameof(Collection));
        SetControlStyle<ListEditor>(DefaultStyleKeyProperty);
    }

    /// <summary>
    /// Gets or sets the command to invoke when the user wants to remove an
    /// item from the collection.
    /// </summary>
    public ICommand RemoveCommand
    {
        get => (ICommand)GetValue(RemoveCommandProperty);
        set => SetValue(RemoveCommandProperty, value);
    }

    /// <summary>
    /// Gets or sets the collection to be managed by this control.
    /// </summary>
    public ICollection<Model> Collection
    {
        get => (ICollection<Model>)GetValue(CollectionProperty);
        set => SetValue(CollectionProperty, value);
    }
}
