using System;
using TheXDS.MCART.Exceptions;
using TheXDS.Ganymede.ViewModels;
using System.Diagnostics.CodeAnalysis;

namespace TheXDS.Ganymede.Component
{
    /// <summary>
    /// <see cref="IVisualResolver{T}"/> que permite obtener un contenedor
    /// visual predeterminado en caso de no poder resolver el 
    /// <see cref="PageViewModel"/> especificado.
    /// </summary>
    /// <typeparam name="TVisual">
    /// Tipo de contenedor visual a implementar.
    /// </typeparam>
    public class FallbackVisualResolver<TVisual> : IVisualResolver<TVisual> where TVisual : notnull
    {
        /// <summary>
        /// <see cref="IVisualResolver{T}"/> para el cual esta instancia es una
        /// envoltura segura.
        /// </summary>
        public IVisualResolver<TVisual> Resolver { get; }

        /// <summary>
        /// Función que genera un contenedor visual en vaso de un error al
        /// intentar resolver un contenedor visual para un
        /// <see cref="PageViewModel"/>.
        /// </summary>
        public Func<PageViewModel, Exception, TVisual> FallbackResolver { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="FallbackVisualResolver{T}"/>.
        /// </summary>
        /// <param name="resolver">
        /// <see cref="IVisualResolver{T}"/> para el cual esta instancia es una
        /// envoltura segura.
        /// </param>
        /// <param name="fallbackResolver">
        /// Función que genera un contenedor visual en vaso de un error al
        /// intentar resolver un contenedor visual para un
        /// <see cref="PageViewModel"/>.
        /// </param>
        public FallbackVisualResolver(IVisualResolver<TVisual> resolver, Func<PageViewModel, Exception, TVisual> fallbackResolver)
        {
            Resolver = resolver;
            FallbackResolver = fallbackResolver;
        }

        /// <inheritdoc/>
        public virtual TVisual ResolveVisual(PageViewModel viewModel)
        {
            try
            {
                return Resolver.ResolveVisual(viewModel);
            }
            catch (Exception ex)
            {
                return FallbackResolver(viewModel, ex) ?? throw new InvalidReturnValueException(FallbackResolver);
            }
        }

        /// <summary>
        /// Intenta resolver un contenedor visual utilizando el 
        /// <see cref="IVisualResolver{T}"/> subyacente.
        /// </summary>
        /// <param name="viewModel">
        /// <see cref="PageViewModel"/> que va a alojarse.
        /// </param>
        /// <param name="visual">
        /// Contenedor visual para el <see cref="PageViewModel"/> especificado.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si el contenedor visual pudo ser resuelto
        /// por esta instancia, <see langword="false"/> en caso contrario.
        /// </returns>
        public bool TryResolveVisual(PageViewModel viewModel, [NotNullWhen(true)] out TVisual? visual)
        {
            return Resolver.TryResolveVisual(viewModel, out visual);
        }
    }

    /// <summary>
    /// <see cref="IVisualResolver{T}"/> que permite obtener un contenedor
    /// visual predeterminado en caso de no poder resolver el 
    /// <see cref="PageViewModel"/> especificado.
    /// </summary>
    /// <typeparam name="TVisual">
    /// Tipo de contenedor visual a implementar.
    /// </typeparam>
    /// <typeparam name="TFallback">
    /// Tipo de contenedor visual predeterminado a obtener cuando el contenedor
    /// visual no pueda ser resuelto para un <see cref="PageViewModel"/>.
    /// </typeparam>
    public class FallbackVisualResolver<TVisual, TFallback> : FallbackVisualResolver<TVisual> where TVisual : notnull where TFallback : notnull, TVisual, new()
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="FallbackVisualResolver{TVisual, TFallback}"/>.
        /// </summary>
        /// <param name="resolver">
        /// <see cref="IVisualResolver{T}"/> para el cual esta instancia es una
        /// envoltura segura.
        /// </param>
        public FallbackVisualResolver(IVisualResolver<TVisual> resolver) : base(resolver, BuildFallback)
        {
        }

        private static TVisual BuildFallback(PageViewModel vm, Exception ex)
        {
            TFallback f = new();
            if (f is IDataContext dc) dc.DataContext = vm;
            return f;
        }
    }
}