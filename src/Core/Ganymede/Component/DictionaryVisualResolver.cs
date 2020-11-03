using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Types.Extensions;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Component
{
    /// <summary>
    /// Implementa un <see cref="IVisualResolver"/> que contiene un
    /// diccionario que mapea un <see cref="PageViewModel"/> por su tipo
    /// con un contenedor visual de tipo <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de contenedor visual a implementar.
    /// </typeparam>
    public class DictionaryVisualResolver<T> : IVisualResolver<T> where T : notnull
    {
        private readonly Dictionary<Type, Type> _mappings = new Dictionary<Type, Type>();

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
        /// <exception cref="KeyNotFoundException">
        /// Se produce si se intenta resolver el contenedor visual para un
        /// tipo de <see cref="PageViewModel"/> que no ha sido registrado.
        /// </exception>
        [DebuggerNonUserCode]
        public T ResolveVisual(PageViewModel viewModel)
        {            
            return ResolveVisual(viewModel.GetType());
        }

        /// <summary>
        /// Resuelve el contenedor visual a utilizar para alojar a un 
        /// <see cref="PageViewModel"/> del tipo especificado.
        /// </summary>
        /// <typeparam name="TViewModel">
        /// Tipo de <see cref="PageViewModel"/> que va a alojarse.
        /// </typeparam>
        /// <returns>
        /// Un contenedor visual fuertemente tipeado para el
        /// <see cref="PageViewModel"/> especificado.
        /// </returns>
        /// <exception cref="KeyNotFoundException">
        /// Se produce si se intenta resolver el contenedor visual para un
        /// tipo de <see cref="PageViewModel"/> que no ha sido registrado.
        /// </exception>
        [DebuggerNonUserCode]
        public T ResolveVisual<TViewModel>() where TViewModel : PageViewModel
        {
            return ResolveVisual(typeof(TViewModel));
        }

        /// <summary>
        /// Resuelve el contenedor visual a utilizar para alojar al 
        /// <see cref="PageViewModel"/> especificado.
        /// </summary>
        /// <param name="vmType">
        /// Tipo <see cref="PageViewModel"/> que va a alojarse.
        /// </param>
        /// <returns>
        /// Un contenedor visual fuertemente tipeado para el
        /// <see cref="PageViewModel"/> especificado.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se produce si se intenta resolver el contenedor visual para un
        /// valor nulo.
        /// </exception>
        /// <exception cref="KeyNotFoundException">
        /// Se produce si se intenta resolver el contenedor visual para un
        /// tipo de <see cref="PageViewModel"/> que no ha sido registrado.
        /// </exception>
        [DebuggerNonUserCode, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T ResolveVisual(Type vmType)
        {
            return _mappings[vmType].New<T>();
        }

        /// <summary>
        /// Registra la resolución de un <see cref="PageViewModel"/> a un tipo
        /// de contenedor visual a utilizar para presentarlo.
        /// </summary>
        /// <typeparam name="TViewModel">
        /// Tipo de <see cref="PageViewModel"/> que va a alojarse.
        /// </typeparam>
        /// <typeparam name="TVisual">
        /// Tipo de contenedor visual a utilizar para mostrar el <see cref="PageViewModel"/> a registrar.
        /// </typeparam>
        public void RegisterVisual<TViewModel, TVisual>() where TViewModel : PageViewModel where TVisual : notnull, T, new()
        {
            _mappings.Add(typeof(TViewModel), typeof(TVisual));
        }
    }
}