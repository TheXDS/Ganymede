using System;
using TheXDS.Ganymede.Exceptions;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types.Extensions;
using St = TheXDS.Ganymede.Resources.ErrorStrings;

namespace TheXDS.Ganymede.Resources
{
    /// <summary>
    /// Contiene errores que pueden ser lanzados por Ganymede.
    /// </summary>
    public static class Errors
    {
        /// <summary>
        /// Obtiene una nueva instancia de la excepción
        /// <see cref="UiHostAccessException"/>.
        /// </summary>
        public static Exception UiHostAccess => new UiHostAccessException();

        /// <summary>
        /// Obtiene una excepción que se produce cuando no se ha podido
        /// localizar un contenedor visual para el <see cref="PageViewModel"/>
        /// especificado.
        /// </summary>
        /// <param name="vm">
        /// <see cref="PageViewModel"/> para el cual no se pudo encontrar un 
        /// contenedor visual.
        /// </param>
        /// <returns>
        /// Una nueva instancia de la clase <see cref="MissingTypeException"/>
        /// con un mensaje que describe la excepción ocurrida.
        /// </returns>
        public static Exception VisualHostNotFound(PageViewModel vm)
        {
            return new MissingTypeException(string.Format(St.VisualHostNotFound,
                vm.UiServices.VisualHost.Title.OrNull() ??
                vm.GetType().NameOf()));
        }

        /// <summary>
        /// Obtiene una excepción que se produce cuando un tipo no implementa
        /// de un tipo de contenedor visual válido.
        /// </summary>
        /// <param name="type">Tipo que ha producido la excepción.</param>
        /// <returns>
        /// Una nueva instancia de la clase <see cref="InvalidTypeException"/>
        /// con un mensaje que describe la excepción ocurrida.
        /// </returns>
        public static Exception InvalidViewTypeException(Type type)
        {
            return new InvalidTypeException(St.InvalidViewType, type);
        }
    }
}
