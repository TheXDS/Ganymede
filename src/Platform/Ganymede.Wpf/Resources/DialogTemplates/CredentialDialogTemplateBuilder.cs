using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TheXDS.Ganymede.Controls;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ViewModels;
using St = TheXDS.Ganymede.Resources.Strings.Common;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a dialog template builder that can generate credential input
/// controls.
/// </summary>
public class CredentialDialogTemplateBuilder : IDialogTemplateBuilder<CredentialInputDialogViewModel>
{
    /// <inheritdoc/>
    public FrameworkElement? Build(CredentialInputDialogViewModel viewModel)
    {
        var usr = new TextBoxEx() { Icon = "👤", Label = St.User };
        var pwd = new PasswordBox() { Margin = new Thickness(0, 10, 0, 0) };
        pwd.PasswordChanged += (_, _) =>
        {
            viewModel.Password?.Dispose();
            viewModel.Password = pwd.SecurePassword;
        };
        usr.SetBinding(TextBox.TextProperty, new Binding(nameof(viewModel.User)));
        return new StackPanel() {  Children = { usr, pwd } };
    }
}