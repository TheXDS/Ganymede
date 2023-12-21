using Avalonia.Controls;
using TheXDS.Ganymede.ViewModels.Dialogs;

namespace TheXDS.Ganymede.Component.Templating;

public interface IDialogTemplateBuilder<in T> : IDialogTemplateBuilder where T : DialogViewModelBase
{
    Control? Build(T viewModel);
    bool IDialogTemplateBuilder.CanBuild(DialogViewModelBase vm) => vm is T;
    Control? IDialogTemplateBuilder.Build(DialogViewModelBase vm) => Build((T)vm);
}