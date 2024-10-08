using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
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
    StyledElement? IDialogTemplateBuilder.Build(IDialogViewModel viewModel) => new ComboBox()
    {
        DisplayMemberBinding = new Binding(nameof(INameable.Name), BindingMode.OneWay),
        [!ItemsControl.ItemsSourceProperty] = new Binding(nameof(SelectionDialogViewModel<object>.Options), BindingMode.OneWay),
        [!SelectingItemsControl.SelectedItemProperty] = new Binding(nameof(SelectionDialogViewModel<object>.Value))
    };

    bool IDialogTemplateBuilder.CanBuild(IDialogViewModel viewModel)
    {
        return viewModel.GetType().Implements(typeof(SelectionDialogViewModel<>));
    }
}