using System.Diagnostics.CodeAnalysis;
using TheXDS.Ganymede.Attributes;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.Ganymede.Component
{
    /// <summary>
    /// Implementa un <see cref="IVisualResolver{T}"/> que obtendrá el tipo de
    /// contenedor visual a utilizar basado en los metadatos de anotación
    /// definidos en tiempo de compilación.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de contenedor visual a implementar.
    /// </typeparam>
    public class AnnotationsVisualResolver<T> : IVisualResolver<T> where T : notnull
    {
        /// <inheritdoc/>
        public T ResolveVisual(PageViewModel viewModel)
        {
            return viewModel.GetAttr<ViewAttribute>()!.ViewType.New<T>();
        }

        /// <inheritdoc/>
        public bool TryResolveVisual(PageViewModel viewModel, [MaybeNullWhen(false)] out T visual)
        {
            if (viewModel.HasAttr<ViewAttribute>(out var v) && v.ViewType.Implements<T>() && v.ViewType.IsInstantiable())
            {
                visual = v.ViewType.New<T>();
                return true;
            }
            else
            {
                visual = default;
                return false;
            }
        }
    }
}