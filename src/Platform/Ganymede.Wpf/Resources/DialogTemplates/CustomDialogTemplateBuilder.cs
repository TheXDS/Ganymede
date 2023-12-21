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

    // Constructor made private to disallow reflection discovery on this type.
    private CustomDialogTemplateBuilder() { }

    /// <summary>
    /// Creates a new instance of the <see cref="CustomDialogTemplateBuilder"/>
    /// class.
    /// </summary>
    /// <returns>
    /// A new isntance of the <see cref="CustomDialogTemplateBuilder"/> class.
    /// </returns>
    public static CustomDialogTemplateBuilder Create() 
    { 
        return new CustomDialogTemplateBuilder();
    }
}