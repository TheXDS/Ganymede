using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.Ganymede.Component
{
    /// <summary>
    /// Implementa un <see cref="IVisualResolver"/> que resolverá un contenedor
    /// visual basado en el nombre de tipo de un <see cref="PageViewModel"/>.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de contenedor visual a implementar.
    /// </typeparam>
    public class ConventionVisualResolver<T> : IVisualResolver<T> where T : notnull
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
        /// <exception cref="ArgumentNullException">
        /// Se produce si se intenta resolver el contenedor visual para un
        /// valor nulo.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Se produce si no es posible resolver un contenedor visual para el
        /// <see cref="PageViewModel"/> especificado.
        /// </exception>
        public T ResolveVisual(PageViewModel viewModel)
        {
            string? name = GetName(viewModel);
            return Objects.GetTypes<T>(true).First(p => p.Name == name).New<T>();
        }

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
        public bool TryResolveVisual(PageViewModel viewModel, [MaybeNullWhen(false)] out T visual)
        {
            string? name = GetName(viewModel);
            visual = Objects.GetTypes<T>(true).FirstOrDefault(p => p.Name == name) is Type t ? t.New<T>() : default;
            return visual is { };
        }

        private static string GetName(PageViewModel viewModel)
        {
            return (viewModel ?? throw new ArgumentNullException(nameof(viewModel)))
                .GetType().Name.Replace("ViewModel", typeof(T).Name);
        }
    }
}