using System.Windows;
using System.Windows.Controls;
using TheXDS.Ganymede.Services.Base;
using TheXDS.Triton.Models.Base;
using static TheXDS.Ganymede.Helpers.DependencyObjectHelpers;

namespace TheXDS.Ganymede.Controls;

/// <summary>
/// Control that allows for entity fetch, search and selection.
/// </summary>
public class EntitySelector : Control
{
    /// <summary>
    /// Identifies the <see cref="Provider"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ProviderProperty;

    /// <summary>
    /// Identifies the <see cref="SelectedEntity"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SelectedEntityProperty;

    static EntitySelector()
    {
        SetControlStyle<EntitySelector>(DefaultStyleKeyProperty);
        ProviderProperty = NewDp<IEntityProvider?, EntitySelector>(nameof(Provider));
        SelectedEntityProperty = NewDp2Way<Model?, EntitySelector>(nameof(SelectedEntity));
    }

    /// <summary>
    /// Gets or sets the <see cref="IEntityProvider"/> instance to use when
    /// presenting data.
    /// </summary>
    public IEntityProvider? Provider
    {
        get => (IEntityProvider?)GetValue(ProviderProperty);
        set => SetValue(ProviderProperty, value);
    }

    /// <summary>
    /// Gets or sets the currently selected entity.
    /// </summary>
    public Model? SelectedEntity
    {
        get => (Model?)GetValue(SelectedEntityProperty);
        set => SetValue(SelectedEntityProperty, value);
    }
}
