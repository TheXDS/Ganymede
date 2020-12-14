using System;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Attributes
{
    /// <summary>
    /// Atributo que permite especificar una vista a utilizar para el <see cref="PageViewModel"/> especificado.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ViewAttribute : Attribute
    {
        /// <summary>
        /// Obtiene el tipo de contenedor visual definido para el
        /// <see cref="PageViewModel"/>.
        /// </summary>
        public Type ViewType { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="ViewAttribute"/>.
        /// </summary>
        /// <param name="viewType">
        /// Tipo de contenedor visual a utilizar para este 
        /// <see cref="PageViewModel"/>.</param>
        public ViewAttribute(Type viewType)
        {
            ViewType = viewType;
        }
    }
}
