using TheXDS.Ganymede.Component;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Mvvm
{
    /// <summary>
    /// Fábrica de servicios que crea un proveedor que utiliza MVVM para
    /// brindar ciertos servicios de UI.
    /// </summary>
    public class MvvmServiceBrokerFactory : IUiServiceBrokerFactory
    {
        /// <inheritdoc/>
        public IUiServiceBroker Create(PageViewModel page, HostViewModel host)
        {
            return new MvvmServiceBroker(page, host);
        }
    }
}
