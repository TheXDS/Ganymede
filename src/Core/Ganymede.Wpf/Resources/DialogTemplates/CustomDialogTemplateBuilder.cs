using System.Windows;
using TheXDS.Ganymede.Component;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a dialog template builder that supports resolving visual
/// elements for custom dialogs.
/// </summary>
public class CustomDialogTemplateBuilder : IDialogTemplateBuilder<IAwaitableDialogViewModel>
{
    /// <inheritdoc/>
    public FrameworkElement? Build(IAwaitableDialogViewModel viewModel)
    {
        return new ConventionVisualResolver<FrameworkElement>().Resolve(viewModel);
    }
}