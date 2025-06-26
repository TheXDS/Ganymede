using System.Windows.Controls.Primitives;
using System.Windows.Data;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a dialog template builder that can generate an item selection
/// Combo box.
/// </summary>
public class ComboSelectionDialogTemplateBuilder : IDialogTemplateBuilder
{
    FrameworkElement? IDialogTemplateBuilder.Build(IDialogViewModel viewModel)
    {
        var control = new ComboBox()
        {
            DisplayMemberPath = nameof(INameable.Name)
        };
        control.SetBinding(ItemsControl.ItemsSourceProperty, new Binding(nameof(SelectionDialogViewModel<object>.Options)));
        control.SetBinding(Selector.SelectedItemProperty, new Binding(nameof(SelectionDialogViewModel<object>.Value)));
        return control;
    }

    bool IDialogTemplateBuilder.CanBuild(IDialogViewModel viewModel)
    {
        return viewModel.GetType().Implements(typeof(SelectionDialogViewModel<>));
    }
}