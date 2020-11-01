using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TheXDS.MCART.Component;
using TheXDS.MCART.Events;
using TheXDS.MCART.Types.Extensions;
using TheXDS.MCART.ViewModel;
using TheXDS.Ganymede.Component;

namespace TheXDS.Ganymede.ViewModels
{
    /// <summary>
    /// Clase base para un ViewModel que contenga páginas.
    /// </summary>
    public class HostViewModel : ViewModelBase
    {
        private protected readonly ObservableCollection<PageViewModel> _pages = new ObservableCollection<PageViewModel>();
        
        /// <summary>
        /// Se produce cuando se ha agregado una página a la colección de
        /// páginas de este host.
        /// </summary>
        public event EventHandler<ValueEventArgs<PageViewModel>>? PageAdded;
        
        /// <summary>
        /// Se produce cuando se ha cerrado una página en la colección de
        /// páginas de este host.
        /// </summary>
        public event EventHandler<ValueEventArgs<PageViewModel>>? PageClosed;

        /// <summary>
        /// Enumera las páginas abiertas activas de esta instancia.
        /// </summary>
        public IEnumerable<PageViewModel> Pages => _pages;

        /// <summary>
        /// Agrega una página a esta instancia.
        /// </summary>
        /// <param name="page">
        /// Página a agregar.
        /// </param>
        public virtual void AddPage(PageViewModel page)
        {
            page.PushInto(_pages).Host = this;
            PageAdded?.Invoke(this,page);
        }

        /// <summary>
        /// Cierra una página activa en esta instancia.
        /// </summary>
        /// <param name="page">
        /// Página a cerrar.
        /// </param>
        public virtual void ClosePage(PageViewModel page)
        {
            page.Host = null;
            _pages.Remove(page);
            PageClosed?.Invoke(this,page);
        }
    }

    /// <summary>
    /// Clase base para un ViewModel que contenga páginas y que las
    /// presente en contenedores visuales fuertemente tipeados.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de contenedor visual. Debe implementar
    /// <see cref="ICloseable"/>.
    /// </typeparam>
    public class HostViewModel<T> : HostViewModel where T : notnull
    {
        private readonly IVisualBuilder<T> _visualBuilder;

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="HostViewModel{T}"/>.
        /// </summary>
        /// <param name="visualBuilder">
        /// Constructor de contenedores visuales a utilizar para presentar
        /// las páginas.
        /// </param>
        public HostViewModel(IVisualBuilder<T> visualBuilder)
        {
            _visualBuilder = visualBuilder;
        }

        /// <summary>
        /// Agrega una página a esta instancia.
        /// </summary>
        /// <param name="page">
        /// Página a agregar.
        /// </param>
        public override void AddPage(PageViewModel page)
        {
            base.AddPage(page);
            Notify(nameof(Children));
        }

        /// <summary>
        /// Cierra una página activa en esta instancia.
        /// </summary>
        /// <param name="page">
        /// Página a cerrar.
        /// </param>
        public override void ClosePage(PageViewModel page)
        {
            base.ClosePage(page);
            Notify(nameof(Children));
        }

        /// <summary>
        /// Enumera los contenedores visuales de los
        /// <see cref="PageViewModel"/> abiertos dentro de esta instancia.
        /// </summary>
        public IEnumerable<T> Children => _pages.Select(_visualBuilder.Build);
    }
}