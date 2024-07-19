using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using TheXDS.Ganymede.Resources.Strings;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.Ganymede.Resources.DialogTemplates;

/// <summary>
/// Implements a dialog template builder that can generate credential input
/// controls.
/// </summary>
public class CredentialDialogTemplateBuilder : IDialogTemplateBuilder<CredentialInputDialogViewModel>
{
    /// <inheritdoc/>
    public StyledElement? Build(CredentialInputDialogViewModel viewModel)
    {
        var usr = new TextBox()
        {
            Watermark = Common.User,
            [!TextBox.TextProperty] = new Binding(nameof(viewModel.User))
        };
        var pwd = new TextBox()
        { 
            Margin = new Thickness(0, 10, 0, 0),
            PasswordChar='·',
            Watermark = Common.Password            
        };
        pwd.TextChanged += (_, _) =>
        {
            viewModel.Password?.Dispose();
            viewModel.Password = (pwd.Text ?? string.Empty).ToSecureString();
        };
        return new StackPanel() { Children = { usr, pwd } };
    }
}