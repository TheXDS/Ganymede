using System.Windows;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.Properties;
using TheXDS.Ganymede.Services;
using TheXDS.ServicePool.Extensions;
using TheXDS.ServicePool.Triton;
using Sp = TheXDS.ServicePool.ServicePool;

namespace Ivie.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            UiThread.SetProxy(new DispatcherUiThreadProxy().RegisterInto(Sp.CommonPool));
            Sp.CommonPool.UseTriton(TritonConfiguration.Configure);
        }
    }
}
