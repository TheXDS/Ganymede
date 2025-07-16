using System.Windows.Data;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Types.Extensions;
using Wpf.Ui.Controls;
using St = TheXDS.Ganymede.Resources.Strings.Common;
using TextBox = Wpf.Ui.Controls.TextBox;
using PasswordBox = Wpf.Ui.Controls.PasswordBox;

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
        var usr = new TextBox()
        { 
            Icon = new SymbolIcon(SymbolRegular.Person20),
            PlaceholderText = St.User, PlaceholderEnabled = true
        };
        var pwd = new PasswordBox()
        { 
            Icon = new SymbolIcon(SymbolRegular.Password20),
            PlaceholderEnabled = true, PlaceholderText = St.Password,
            Margin = new Thickness(0, 10, 0, 0)
        };
        pwd.PasswordChanged += (_, _) =>
        {
            viewModel.Password?.Dispose();
            viewModel.Password = pwd.Password.ToSecureString();
        };
        usr.SetBinding(System.Windows.Controls.TextBox.TextProperty, new Binding(nameof(viewModel.User)));
        return new StackPanel() {  Children = { usr, pwd } };
    }
}
