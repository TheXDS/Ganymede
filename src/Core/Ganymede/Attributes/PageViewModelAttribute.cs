using System;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.Ganymede.Attributes
{
    /// <summary>
    /// Atributo que permite especificar un <see cref="PageViewModel"/> para el
    /// cual el objeto es un contenedor visual.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class PageViewModelAttribute : Attribute
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
        /// <param name="vmType">
        /// Tipo de <see cref="PageViewModel"/> para el cual el objeto es un
        /// contenedor visual.
        /// </param>
        public PageViewModelAttribute(Type vmType)
        {
            if (!vmType.Implements<PageViewModel>()) throw new InvalidTypeException();
            ViewType = vmType;
        }
    }
}
