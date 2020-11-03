using System;
using TheXDS.MCART.Exceptions;
using TheXDS.Ganymede.ViewModels;

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
    public abstract class FallbackVisualResolver<TVisual> : IVisualResolver<TVisual> where TVisual : notnull
    {
        /// <summary>
        /// <see cref="IVisualResolver{T}"/> para el cual esta instancia es una
        /// envoltura segura.
        /// </summary>
        public IVisualResolver<TVisual> Resolver { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="FallbackVisualResolver{T}"/>.
        /// </summary>
        /// <param name="resolver">
        /// <see cref="IVisualResolver{T}"/> para el cual esta instancia es una
        /// envoltura segura.
        /// </param>
        public FallbackVisualResolver(IVisualResolver<TVisual> resolver)
        {
            Resolver = resolver;
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
                return FallbackResolve(viewModel, ex) ?? throw new InvalidReturnValueException((Func<PageViewModel, Exception, TVisual>)FallbackResolve);
            }
        }

        /// <summary>
        /// Inicia la resolución del contenido visual en caso de un error.
        /// </summary>
        /// <param name="viewModel">
        /// <see cref="PageViewModel"/> que no ha podido ser resuelto.
        /// </param>
        /// <param name="ex">
        /// Excepción producida durante la resolución normal del contenedor
        /// visual.
        /// </param>
        /// <returns>
        /// Un contenido visual resuelto a partir de la excepción producida.
        /// </returns>
        protected abstract TVisual FallbackResolve(PageViewModel viewModel, Exception ex);
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
        public FallbackVisualResolver(IVisualResolver<TVisual> resolver) : base(resolver)
        {
        }

        /// <inheritdoc/>
        protected override TVisual FallbackResolve(PageViewModel viewModel, Exception ex)
        {
            return new TFallback();
        }
    }
}