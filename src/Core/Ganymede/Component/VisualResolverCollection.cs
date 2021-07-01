using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TheXDS.Ganymede.Resources;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Component
{
    /// <summary>
    /// Colección de <see cref="IVisualResolver{T}"/> que permite obtener un 
    /// contenedor visual desde el primer <see cref="IVisualResolver{T}"/>
    /// capaz de resolver el <see cref="PageViewModel"/> especificado.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de contenedor visual a implementar.
    /// </typeparam>
    public class VisualResolverCollection<T> : List<IVisualResolver<T>>, IVisualResolver<T> where T : notnull
    {
        /// <inheritdoc/>
        public T ResolveVisual(PageViewModel viewModel)
        {
            foreach (IVisualResolver<T>? j in this)
            {
                if (j.TryResolveVisual(viewModel, out T? v)) return v;
            }
            throw Errors.VisualHostNotFound(viewModel);
        }

        /// <inheritdoc/>
        public bool TryResolveVisual(PageViewModel viewModel, [NotNullWhen(true)] out T? visual)
        {
            foreach (IVisualResolver<T>? j in this)
            {
                if (j.TryResolveVisual(viewModel, out visual)) return true;
            }
            visual = default;
            return false;
        }
    }
}