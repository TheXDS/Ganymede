using TheXDS.Ganymede.ViewModels;
using System.Diagnostics.CodeAnalysis;

namespace TheXDS.Ganymede.Component
{
    /// <summary>
    /// Define una serie de miembros a implementar por un tipo que permita
    /// resolver un contenedor visual a utilizar para alojar a un
    /// <see cref="PageViewModel"/>.
    /// </summary>
    public interface IVisualResolver
    {
        /// <summary>
        /// Resuelve el contenedor visual a utilizar para alojar al 
        /// <see cref="PageViewModel"/> especificado.
        /// </summary>
        /// <param name="viewModel">
        /// <see cref="PageViewModel"/> que va a alojarse.
        /// </param>
        /// <returns>
        /// Un contenedor visual para el <see cref="PageViewModel"/>
        /// especificado.
        /// </returns>
        object ResolveVisual(PageViewModel viewModel);        
    }

    /// <summary>
    /// Define una serie de miembros a implementar por un tipo que permita
    /// resolver un contenedor visual fuertemente tipeado a utilizar para
    /// alojar a un <see cref="PageViewModel"/>.
    /// </summary>
    public interface IVisualResolver<T> : IVisualResolver where T : notnull
    {
        /// <summary>
        /// Resuelve el contenedor visual a utilizar para alojar al 
        /// <see cref="PageViewModel"/> especificado.
        /// </summary>
        /// <param name="viewModel">
        /// <see cref="PageViewModel"/> que va a alojarse.
        /// </param>
        /// <returns>
        /// Un contenedor visual fuertemente tipeado para el
        /// <see cref="PageViewModel"/> especificado.
        /// </returns>
        new T ResolveVisual(PageViewModel viewModel);

        /// <summary>
        /// Intenta resolver un contenedor visual a utilizar para alojar al
        /// <see cref="PageViewModel"/> especificado.
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
        bool TryResolveVisual(PageViewModel viewModel, [NotNullWhen(true)] out T? visual)
        {
            try
            {
                visual = ResolveVisual(viewModel);
                return true;
            }
            catch
            {
                visual = default;
                return false;
            }
        }

        /// <inheritdoc/>
        object IVisualResolver.ResolveVisual(PageViewModel viewModel) => ResolveVisual(viewModel)!;
    }
}