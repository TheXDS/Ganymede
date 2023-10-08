using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using TheXDS.Ganymede.CrudGen;
using TheXDS.Ganymede.Types;
using TheXDS.Triton.Models.Base;
using static TheXDS.Ganymede.Helpers.DependencyObjectHelpers;

namespace TheXDS.Ganymede.Controls;

/// <summary>
/// Lógica de interacción para ListEditor.xaml
/// </summary>
public partial class ListEditor : UserControl
{
    /// <summary>
    /// Identifiees the <see cref="SelectCommand"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SelectCommandProperty;

    /// <summary>
    /// Identifiees the <see cref="CreateCommands"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty CreateCommandsProperty;

    /// <summary>
    /// Identifiees the <see cref="UpdateCommand"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty UpdateCommandProperty;

    /// <summary>
    /// Identifiees the <see cref="RemoveCommand"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty RemoveCommandProperty;

    /// <summary>
    /// Identifiees the <see cref="Collection"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty CollectionProperty;

    /// <summary>
    /// Identifiees the <see cref="CanCreate"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty CanCreateProperty;

    /// <summary>
    /// Identifiees the <see cref="CanSelect"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty CanSelectProperty;

    /// <summary>
    /// Identifiees the <see cref="EntitySource"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty EntitySourceProperty;

    /// <summary>
    /// Identifiees the <see cref="Models"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ModelsProperty;

    /// <summary>
    /// Identifiees the <see cref="Label"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty LabelProperty;

    /// <summary>
    /// Identifiees the <see cref="Icon"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IconProperty;

    /// <summary>
    /// Identifiees the <see cref="LabelForeground"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty LabelForegroundProperty;
    /// <summary>
    /// Identifiees the <see cref="LabelEffect"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty LabelEffectProperty;

    static ListEditor()
    {
        SelectCommandProperty = NewDp<ICommand, ListEditor>(nameof(SelectCommand));
        CreateCommandsProperty = NewDp<ICollection<ButtonInteraction>, ListEditor>(nameof(CreateCommands));
        UpdateCommandProperty = NewDp<ICommand, ListEditor>(nameof(UpdateCommand));
        RemoveCommandProperty = NewDp<ICommand, ListEditor>(nameof(RemoveCommand));
        CollectionProperty = NewDp<ICollection<Model>, ListEditor>(nameof(Collection));
        CanCreateProperty = NewDp<bool, ListEditor>(nameof(CanCreate), true);
        CanSelectProperty = NewDp<bool, ListEditor>(nameof(CanSelect));
        EntitySourceProperty = NewDp<IEnumerable<Model>, ListEditor>(nameof(EntitySource));
        ModelsProperty = NewDp<ICrudDescription[], ListEditor>(nameof(Models));
        LabelProperty = NewDp<string, ListEditor>(nameof(Label));
        IconProperty = NewDp<string, ListEditor>(nameof(Icon), "📄");
        LabelForegroundProperty = NewDp<Brush, ListEditor>(nameof(LabelForeground), SystemColors.ControlTextBrush);
        LabelEffectProperty = NewDp<Effect, ListEditor>(nameof(LabelEffect));
    }

    /// <summary>
    /// Gets or sets the command to invoke when the user wants to add an
    /// existing item to the collection.
    /// </summary>
    public ICommand SelectCommand
    {
        get => (ICommand)GetValue(SelectCommandProperty);
        set => SetValue(SelectCommandProperty, value);
    }

    /// <summary>
    /// Gets or sets the command to invoke when the user wants to add a new
    /// item to the collection.
    /// </summary>
    public ICollection<ButtonInteraction> CreateCommands
    {
        get => (ICollection<ButtonInteraction>)GetValue(CreateCommandsProperty);
        set => SetValue(CreateCommandsProperty, value);
    }

    /// <summary>
    /// Gets or sets the command to invoke when the user wants to update an
    /// item already on the collection.
    /// </summary>
    public ICommand UpdateCommand
    {
        get => (ICommand)GetValue(UpdateCommandProperty);
        set => SetValue(UpdateCommandProperty, value);
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

    /// <summary>
    /// Gets or sets a value that indicates if this control should allow
    /// creation of new entities.
    /// </summary>
    public bool CanCreate
    {
        get => (bool)GetValue(CanCreateProperty);
        set => SetValue(CanCreateProperty, value);
    }

    /// <summary>
    /// Gets or sets a value that indicates if this control should allow
    /// selection of items to be added to this list.
    /// </summary>
    public bool CanSelect
    {
        get => (bool)GetValue(CanSelectProperty);
        set => SetValue(CanSelectProperty, value);
    }

    /// <summary>
    /// Gets or sets an entity enumeration of all the items that can be added
    /// to the list managed by this collection.
    /// </summary>
    public IEnumerable<Model>? EntitySource
    {
        get => (IEnumerable<Model>?)GetValue(EntitySourceProperty);
        set => SetValue(EntitySourceProperty, value);
    }

    /// <summary>
    /// Gets or sets a collection of the available models to be added/created
    /// on the list managed by this control.
    /// </summary>
    public ICrudDescription[] Models
    {
        get => (ICrudDescription[])GetValue(ModelsProperty);
        set => SetValue(ModelsProperty, value);
    }

    /// <summary>
    /// Gets or sets the label's foreground color.
    /// </summary>
    public Brush? LabelForeground
    {
        get => (Brush?)GetValue(LabelForegroundProperty);
        set => SetValue(LabelForegroundProperty, value);
    }

    /// <summary>
    /// Gets or sets the label's rendering effects.
    /// </summary>
    public Effect? LabelEffect
    {
        get => (Effect?)GetValue(LabelEffectProperty);
        set => SetValue(LabelEffectProperty, value);
    }

    /// <summary>
    /// Gets or sets the label to be displayed on this control.
    /// </summary>
    public string? Label
    {
        get => (string?)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    /// <summary>
    /// Gets or sets a glyph to be displayed as an iconic decoration in this
    /// control.
    /// </summary>
    public string Icon
    {
        get => (string)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ListEditor"/> class.
    /// </summary>
    public ListEditor()
    {
        InitializeComponent();
    }
}
