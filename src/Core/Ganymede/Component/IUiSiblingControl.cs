using TheXDS.Ganymede.ViewModels;
using System.Threading.Tasks;

namespace TheXDS.Ganymede.Component
{
    /// <summary>
    /// Define una serie de miembros a implementar por un tipo que exponga
    /// funciones de control para otras páginas hermanas abiertas.
    /// </summary>
    public interface IUiSiblingControl
    {
        /// <summary>
        /// Solicita el cierre de una página.
        /// </summary>
        /// <param name="page">Página a cerrar.</param>
        /// <returns>
        /// <see langword="true"/> si este método ha cerrado la página 
        /// especificada o si la misma no esta abaierta, 
        /// <see langword="false"/> en caso que la página no se haya cerrado.
        /// </returns>
        bool Close(PageViewModel page);

        /// <summary>
        /// Solicita la apertura de una página.
        /// </summary>
        /// <param name="page">Página a abrir o activar.</param>
        /// <returns>
        /// <see langword="true"/> si la página solicitada ha sido abierta 
        /// exitosamente o si la misma ya se encontraba abierta, 
        /// <see langword="false"/> en caso contrario.
        /// </returns>
        Task<bool> OpenAsync(PageViewModel page);

        /// <summary>
        /// Solicita la apertura de una nueva página.
        /// </summary>
        /// <typeparam name="TPage">
        /// Tipo de la página a instanciar y abrir.
        /// </typeparam>
        /// <returns>
        /// <see langword="true"/> si la página solicitada ha sido abierta 
        /// exitosamente o si la misma ya se encontraba abierta, 
        /// <see langword="false"/> en caso contrario.
        /// </returns>
        Task<bool> OpenAsync<TPage>() where TPage : PageViewModel, new() => OpenAsync(new TPage());
    }
}