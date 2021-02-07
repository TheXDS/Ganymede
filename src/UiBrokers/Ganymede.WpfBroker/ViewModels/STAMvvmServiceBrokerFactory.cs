using System.Windows;
using TheXDS.Ganymede.Component;
using TheXDS.Ganymede.Mvvm;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.WpfBroker.ViewModels
{
    /// <summary>
    /// Fábrica de proveedor de servicios de UI que enlaza todas las acciones
    /// de UI al hilo de UI de la aplicación.
    /// </summary>
    public class STAMvvmServiceBrokerFactory : IUiServiceBrokerFactory
    {
        /// <inheritdoc/>
        public IUiServiceBroker Create(PageViewModel page, HostViewModel host)
        {
            return new STAMvvmServiceBroker(page, host, Application.Current.Dispatcher.Invoke);
        }
    }
}
