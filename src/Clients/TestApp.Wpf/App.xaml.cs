using System.Windows;
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
        ServicePool.CommonPool.RegisterNow(new TritonService(tc, tf));
    }
}
