using Avalonia.Controls;
using TheXDS.Ganymede.ViewModels.Dialogs;

namespace TheXDS.Ganymede.Component.Templating;

public interface IDialogTemplateBuilder
{
    bool CanBuild(DialogViewModelBase viewModel);
    Control? Build(DialogViewModelBase viewModel);
}