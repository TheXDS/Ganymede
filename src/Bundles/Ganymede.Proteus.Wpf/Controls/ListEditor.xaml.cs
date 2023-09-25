using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TheXDS.Triton.Models.Base;
using static TheXDS.Ganymede.Helpers.DependencyObjectHelpers;

namespace TheXDS.Ganymede.Controls;

/// <summary>
/// Lógica de interacción para ListEditor.xaml
/// </summary>
public partial class ListEditor : UserControl
{
    public static DependencyProperty SelectCommandProperty;
    public static DependencyProperty CreateCommandProperty;
    public static DependencyProperty UpdateCommandProperty;
    public static DependencyProperty RemoveCommandProperty;
    public static DependencyProperty CollectionProperty;
    public static DependencyProperty CanCreateProperty;
    public static DependencyProperty CanSelectProperty;
    public static DependencyProperty EntitySourceProperty;
    public static DependencyProperty ModelsProperty;
    public static DependencyProperty LabelProperty;
    public static DependencyProperty IconProperty;

    static ListEditor()
    {
        SelectCommandProperty = NewDp<ICommand, ListEditor>(nameof(SelectCommand));
        CreateCommandProperty = NewDp<ICommand, ListEditor>(nameof(CreateCommand));
        UpdateCommandProperty = NewDp<ICommand, ListEditor>(nameof(UpdateCommand));
        RemoveCommandProperty = NewDp<ICommand, ListEditor>(nameof(RemoveCommand));
        CollectionProperty = NewDp<ICollection<Model>, ListEditor>(nameof(Collection));
        CanCreateProperty = NewDp<bool, ListEditor>(nameof(CanCreate), true);
        CanSelectProperty = NewDp<bool, ListEditor>(nameof(CanSelect));
        EntitySourceProperty = NewDp<IEnumerable<Model>, ListEditor>(nameof(EntitySource));
        ModelsProperty = NewDp<Type[], ListEditor>(nameof(Models));
        LabelProperty = NewDp<string, ListEditor>(nameof(Label));
        IconProperty = NewDp<string, ListEditor>(nameof(Icon), "📄");
    }

    public ICommand SelectCommand
    {
        get => (ICommand)GetValue(SelectCommandProperty);
        set => SetValue(SelectCommandProperty, value);
    }
    public ICommand CreateCommand
    {
        get => (ICommand)GetValue(CreateCommandProperty);
        set => SetValue(CreateCommandProperty, value);
    }
    public ICommand UpdateCommand
    {
        get => (ICommand)GetValue(UpdateCommandProperty);
        set => SetValue(UpdateCommandProperty, value);
    }
    public ICommand RemoveCommand
    {
        get => (ICommand)GetValue(RemoveCommandProperty);
        set => SetValue(RemoveCommandProperty, value);
    }
    public ICollection<Model> Collection
    {
        get => (ICollection<Model>)GetValue(CollectionProperty);
        set => SetValue(CollectionProperty, value);
    }
    public bool CanCreate
    {
        get => (bool)GetValue(CanCreateProperty);
        set => SetValue(CanCreateProperty, value);
    }
    public bool CanSelect
    {
        get => (bool)GetValue(CanSelectProperty);
        set => SetValue(CanSelectProperty, value);
    }
    public IEnumerable<Model> EntitySource
    {
        get => (IEnumerable<Model>)GetValue(EntitySourceProperty);
        set => SetValue(EntitySourceProperty, value);
    }
    public Type[] Models
    {
        get => (Type[])GetValue(ModelsProperty);
        set => SetValue(ModelsProperty, value);
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

    public ListEditor()
    {
        InitializeComponent();
    }
}
