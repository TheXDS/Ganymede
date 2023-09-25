using Avalonia.ReactiveUI;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Views;

public partial class HelloView : ReactiveUserControl<HelloViewModel>
{
    public HelloView()
    {
        InitializeComponent();
    }
}