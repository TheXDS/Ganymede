using System.Windows;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.Services;
using TheXDS.ServicePool;
using TheXDS.Triton.InMemory.Services;
using TheXDS.Triton.Services;

namespace TestApp.Wpf;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    static App()
    {
        var tf = new InMemoryTransFactory();
        var tc = new TransactionConfiguration();
        var ui = new DispatcherUiThreadProxy();
        UiThread.SetProxy(ui);
        ServicePool.CommonPool.RegisterNow(ui);
        ServicePool.CommonPool.RegisterNow(new TritonService(tc, tf));
    }
}
