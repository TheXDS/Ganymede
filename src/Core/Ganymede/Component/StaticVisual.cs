using System;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Component
{
    /// <summary>
    /// Implementación de <see cref="IVisualResolver{T}"/> que siempre devuelve
    /// un mismo contenedor visual para todas las instancias de la clase
    /// <see cref="PageViewModel"/> sin ejecutar una resolución.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de contenedor visual a implementar.
    /// </typeparam>
    public class StaticVisual<T> : IVisualResolver<T> where T : notnull
    {
        private readonly Func<T> _staticVisualFunc;

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="StaticVisual{T}"/>.
        /// </summary>
        /// <param name="staticVisualFunc"></param>
        public StaticVisual(Func<T> staticVisualFunc)
        {
            _staticVisualFunc = staticVisualFunc;
        }

        /// <summary>
        /// Devuelve siempre un contenedor visual especificado a la hora de 
        /// construir esta instancia.
        /// </summary>
        /// <param name="viewModel">
        /// <see cref="PageViewModel"/> que va a alojarse.
        /// </param>
        /// <returns
        /// >Una instancia preestablecida de tipo <typeparamref name="T"/>.
        /// </returns>
        public T ResolveVisual(PageViewModel viewModel)
        {
            return _staticVisualFunc();
        }

        /// <summary>
        /// Obtiene un nuevo <see cref="StaticVisual{T}"/> para un tipo
        /// <typeparamref name="TVisual"/> instanciable sin parámetros.
        /// </summary>
        /// <typeparam name="TVisual">
        /// Tipo de contenedor visual a implementar.
        /// </typeparam>
        /// <returns>
        /// Un nuevo <see cref="StaticVisual{T}"/> que devovlerá nuevas
        /// instancias del tipo <typeparamref name="TVisual"/>.
        /// </returns>
        public static StaticVisual<TVisual> Build<TVisual>() where TVisual : notnull, new()
        {
            return new StaticVisual<TVisual>(() => new TVisual());
        }
    }
}