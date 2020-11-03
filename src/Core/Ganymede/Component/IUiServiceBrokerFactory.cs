using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Component
{
    /// <summary>
    /// Define una fábrica de servicios de UI que genera un 
    /// <see cref="IUiServiceBroker"/> para la instancia de 
    /// <see cref="PageViewModel"/> especificada.
    /// </summary>
    public interface IUiServiceBrokerFactory
    {
        /// <summary>
        /// Crea un nuevo <see cref="IUiServiceBroker"/> que brindará servicios
        /// de UI al <see cref="PageViewModel"/> especificado.
        /// </summary>
        /// <param name="page">
        /// Página a la cual brindar servicios de UI.
        /// </param>
        /// <param name="host">
        /// Host que alberga al <see cref="PageViewModel"/> especificado. Puede
        /// (no necesariamente) ser quien provee directamente los servicios de UI.
        /// </param>
        /// <returns></returns>
        IUiServiceBroker Create(PageViewModel page, HostViewModel host);
    }
}