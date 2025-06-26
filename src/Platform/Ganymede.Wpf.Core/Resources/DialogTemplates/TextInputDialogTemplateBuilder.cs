using System.Windows.Data;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a dialog template builder that can generate text input controls.
/// </summary>
public class TextInputDialogTemplateBuilder<T> : IDialogTemplateBuilder<TextInputDialogViewModel> where T : TextBox, new()
{
    /// <inheritdoc/>
    public virtual FrameworkElement Build(TextInputDialogViewModel viewModel)
    {
        var control = new T();
        control.SetBinding(TextBox.TextProperty, new Binding(nameof(viewModel.Value)) { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
        control.SetBinding(TextBox.MaxLengthProperty, new Binding(nameof(viewModel.MaxLength)));
        return control;
    }
}
