using System.Linq;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.UI;

namespace TheXDS.Ganymede.Component
{
    /// <summary>
    /// Define una serie de miembros a implementar por un tipo que exponga un 
    /// módulo de UI completo a una aplicación cliente.
    /// </summary>
    public interface IModule : INameable
    {
        /// <summary>
        /// Enumera los grupos de interacciones (páginas, ventanas, pantallas)
        /// disponibles en el módulo.
        /// </summary>
        IGrouping<string, Launcher> Launchers { get; }
    }
}