using System.Collections.Generic;

namespace TheXDS.Ganymede.Component
{
    /// <summary>
    /// Define una serie de miembros a implementar por un tipo que permita
    /// cargar módulos de UI de Tritón.
    /// </summary>
    public interface IModuleSource
    {
        /// <summary>
        /// Enumera un conjunto de módulos a cargar por una aplicación de UI de
        /// Tritón.
        /// </summary>
        /// <returns>
        /// Una enumeración de módulos de UI cargados.
        /// </returns>
        IEnumerable<IModule> LoadModules();
    }
}