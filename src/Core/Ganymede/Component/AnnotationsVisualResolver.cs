using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TheXDS.Ganymede.Attributes;
using TheXDS.Ganymede.Resources;
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
            return (AnnotationsVisualResolver<T>.GetViewType(viewModel) ?? throw Errors.VisualHostNotFound(viewModel)).New<T>();
        }

        /// <inheritdoc/>
        public bool TryResolveVisual(PageViewModel viewModel, [MaybeNullWhen(false)] out T visual)
        {
            var t = AnnotationsVisualResolver<T>.GetViewType(viewModel);
            if (t is not null)
            {
                visual = t.New<T>();
                return true;
            }
            else
            {
                visual = default;
                return false;
            }
        }

        private static Type? GetViewType(PageViewModel vm)
        {
            var t = vm.GetType();
            foreach (var j in Objects.GetTypes<T>(true))
            {
                if (j.GetAttr<PageViewModelAttribute>()?.ViewType == t) return j;
            }
            return vm.GetAttr<ViewAttribute>()?.ViewType is { } tt && tt.Implements<T>() && tt.IsInstantiable() ? tt : null;
        }
    }
}