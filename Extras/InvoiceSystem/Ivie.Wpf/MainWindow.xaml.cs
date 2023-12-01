using TheXDS.Ganymede.Controls;
using TheXDS.Ivie.ViewModels;
using Sp = TheXDS.ServicePool.ServicePool;

namespace TheXDS.Ivie;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : AcrylicWindow
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow()
    {
        Sp.CommonPool.RegisterNow(this);
        InitializeComponent();
        vmHost.Navigator!.HomePage = new SplashViewModel();
    }
}
