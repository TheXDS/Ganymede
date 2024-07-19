using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a dialog template builder that can generate an item selection
/// Combo box.
/// </summary>
public class ComboSelectionDialogTemplateBuilder : IDialogTemplateBuilder<SelectionDialogViewModel>
{
    /// <inheritdoc/>
    public StyledElement? Build(SelectionDialogViewModel viewModel) => new ComboBox()
    {
        [!ItemsControl.ItemsSourceProperty] = new Binding(nameof(viewModel.Options)),
        [!SelectingItemsControl.SelectedItemProperty] = new Binding(nameof(viewModel.Value))
    };
}