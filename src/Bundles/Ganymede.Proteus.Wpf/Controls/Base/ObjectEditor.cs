using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using TheXDS.Ganymede.Controls.Primitives;
using TheXDS.Ganymede.CrudGen;
using TheXDS.Triton.Models.Base;
using static TheXDS.Ganymede.Helpers.DependencyObjectHelpers;

namespace TheXDS.Ganymede.Controls.Base;

/// <summary>
/// Base class for all controls that can be used to manage properties that
/// contain entities, either as a single instance or as part of a collection.
/// </summary>
public class ObjectEditor : FormInputControl
{
    /// <summary>
    /// Identifies the <see cref="SelectCommand"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SelectCommandProperty;

    /// <summary>
    /// Identifies the <see cref="CreateCommand"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty CreateCommandProperty;

    /// <summary>
    /// Identifies the <see cref="CanCreate"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty CanCreateProperty;

    /// <summary>
    /// Identifies the <see cref="CanSelect"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty CanSelectProperty;

    /// <summary>
    /// Identifies the <see cref="EntitySource"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty EntitySourceProperty;

    /// <summary>
    /// Identifies the <see cref="Models"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ModelsProperty;

    /// <summary>
    /// Identifies the <see cref="LabelForeground"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty LabelForegroundProperty;

    /// <summary>
    /// Identifies the <see cref="LabelEffect"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty LabelEffectProperty;

    /// <summary>
    /// Identifies the <see cref="UpdateCommand"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty UpdateCommandProperty;

    /// <summary>
    /// Identifies the <see cref="SelectedEntity"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SelectedEntityProperty;

    static ObjectEditor()
    {
        SelectCommandProperty = NewDp<ICommand, ObjectEditor>(nameof(SelectCommand));
        CreateCommandProperty = NewDp<ICommand, ObjectEditor>(nameof(CreateCommand));
        UpdateCommandProperty = NewDp<ICommand, ObjectEditor>(nameof(UpdateCommand));
        CanCreateProperty = NewDp<bool, ObjectEditor>(nameof(CanCreate), true);
        CanSelectProperty = NewDp<bool, ObjectEditor>(nameof(CanSelect));
        EntitySourceProperty = NewDp<IEnumerable<Model>, ObjectEditor>(nameof(EntitySource));
        ModelsProperty = NewDp<ICrudDescription[], ObjectEditor>(nameof(Models));
        SelectedEntityProperty = NewDp<Model, ObjectEditor>(nameof(SelectedEntity));
        LabelForegroundProperty = NewDp<Brush, ObjectEditor>(nameof(LabelForeground), SystemColors.ControlTextBrush);
        LabelEffectProperty = NewDp<Effect, ObjectEditor>(nameof(LabelEffect));
        OverrideDefaultIcon<ObjectEditor>("📄");
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
    public ICommand CreateCommand
    {
        get => (ICommand)GetValue(CreateCommandProperty);
        set => SetValue(CreateCommandProperty, value);
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
    /// selection of items to be added.
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
    /// Gets or sets the selected entity on this control.
    /// </summary>
    public Model? SelectedEntity
    {
        get => (Model?)GetValue(SelectedEntityProperty);
        set => SetValue(SelectedEntityProperty, value);
    }
}
