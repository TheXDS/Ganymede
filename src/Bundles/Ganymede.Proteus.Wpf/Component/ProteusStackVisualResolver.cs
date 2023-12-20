using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Ganymede.CrudGen.Mappings;
using TheXDS.Ganymede.CrudGen.Mappings.Base;
using TheXDS.Ganymede.Types.Base;
using TheXDS.Ganymede.ValueConverters;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.Ganymede.Component;

/// <summary>
/// Implements a <see cref="IVisualResolver{TVisual}"/> that dynamically
/// generates views for Proteus ViewModels.
/// </summary>
public class ProteusStackVisualResolver : IVisualResolver<FrameworkElement>, IViewModelToViewRegistry<FrameworkElement>
{
    private readonly IVisualResolver<FrameworkElement>[] _resolvers;
    private readonly DictionaryVisualResolver<FrameworkElement> _dict;

    /// <summary>
    /// Gets a static reference to the settings to use when generating editor
    /// ViewModels.
    /// </summary>
    public static CrudEditorVisualBuilderSettings EditorSettings { get; }

    /// <summary>
    /// Inicializa la clase <see cref="ProteusStackVisualResolver"/>
    /// </summary>
    static ProteusStackVisualResolver()
    {
        EditorSettings = new()
        {
            ControlTransformations =
            {
                ProcessReadOnly,
                ProcessNullable,
                AddDefaultMargin
            },
            SkipCkecks = 
            {
                // Skip the ID property if it's inferred that the entity being edited already exists.
                (d, v) => d.Property.Name == "Id" && v.Context.UpdatingExisting,
                // Skip if the property is explicitly hidden from the editor.
                (d, _) => d.HideFromEditor,
                // Skip on inferred parent/child relationship.
                (d, v) => d is ISingleObjectPropertyDescription && d.Property.PropertyType.IsAssignableFrom(v.Context.ParentModel),
            },
        };
        EditorSettings.Mappings.AddRange(ReflectionHelpers.FindAllObjects<ICrudMapping>());
    }

    private static FrameworkElement ProcessNullable(FrameworkElement control, IPropertyDescription description)
    {
        if (description is INullablePropertyDescription d && d.Nullable)
        {
            var chk = new CheckBox()
            {
                IsChecked = true,
                Content = control,
                IsThreeState = true,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Stretch
            };
            control.SetBinding(UIElement.IsEnabledProperty, new Binding(nameof(CheckBox.IsChecked)) { Source = chk, Converter = new NullableBoolToBoolConverter() });
            chk.Click += (s, e) =>
            {
                if (ReferenceEquals(s, chk) && chk.DataContext is CrudEditorViewModel vm)
                {
                    switch (chk.IsChecked)
                    {
                        /* Fix: As implemented, a CheckBox will listen to some keystrokes
                         * if not set as three-state, which might interfere with inner
                         * controls, especially TextBoxes. Having a three-state checkbox
                         * that should just accept two states is not what we want, so we
                         * just set it to false if it's on the third state (null).
                         * 
                         * Ask me how I know.
                         */
                        case null: chk.IsChecked = false; break;
                        case false: description.Property.SetDefault(vm.Entity); break;
                    }
                }
            };
            return chk;
        }
        return control;
    }

    private static FrameworkElement AddDefaultMargin(FrameworkElement element, IPropertyDescription _)
    {
        element.Margin = new Thickness(5);
        return element;
    }

    private static FrameworkElement ProcessReadOnly(FrameworkElement element, IPropertyDescription d)
    {
        return d.ReadOnly ? new ReadOnlyMapping().CreateControl(d) : element;
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ProteusStackVisualResolver"/> class.
    /// </summary>
    public ProteusStackVisualResolver()
    {
        _resolvers = new IVisualResolver<FrameworkElement>[]
        {
            new ProteusHostVisualResolver(),
            new CrudPageVisualBuilder(),
            new CrudDetailsVisualBuilder(),
            new CrudEditorVisualBuilder(EditorSettings),
            _dict = new DictionaryVisualResolver<FrameworkElement>(),
            new ConventionVisualResolver<FrameworkElement>(),
        };
    }

    /// <summary>
    /// Gets a reference to an internal <see cref="IVisualResolver{TVisual}"/>
    /// that is part of the resolution stack of this instance, which allows for
    /// dictionary ViewModel-to-View mapping.
    /// </summary>
    public IViewModelToViewRegistry<FrameworkElement> ViewMappings => _dict;

    /// <inheritdoc/>
    public FrameworkElement? Resolve(IViewModel viewModel)
    {
        return _resolvers.Select(p => p.Resolve(viewModel)).FirstOrDefault(p => p is not null);
    }

    void IViewModelToViewRegistry<FrameworkElement>.Register<TViewModel, TVisual>()
    {
        ((IViewModelToViewRegistry<FrameworkElement>)_dict).Register<TViewModel, TVisual>();
    }
}