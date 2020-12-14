using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TheXDS.Ganymede.Component;
using TheXDS.MCART.Component;
using TheXDS.MCART.Events;
using TheXDS.MCART.Types.Extensions;
using TheXDS.MCART.ViewModel;

namespace TheXDS.Ganymede.ViewModels
{
    /// <summary>
    /// Clase base para un ViewModel que contenga páginas.
    /// </summary>
    public class HostViewModel : ViewModelBase
    {
        private protected readonly ObservableCollection<PageViewModel> _pages = new ObservableCollection<PageViewModel>();
        private readonly IUiServiceBrokerFactory _serviceFactory;
        
        /// <summary>
        /// Se produce cuando se ha agregado una página a la colección de
        /// páginas de este host.
        /// </summary>
        public event EventHandler<ValueEventArgs<PageViewModel>>? PageAdded;

        /// <summary>
        /// Se produce cuando una página ha sido agregada e inicializada, y
        /// está lista para interactuar.
        /// </summary>
        public event EventHandler<ValueEventArgs<PageViewModel>>? PageReady;

        /// <summary>
        /// Se produce cuando se ha cerrado una página en la colección de
        /// páginas de este host.
        /// </summary>
        public event EventHandler<ValueEventArgs<PageViewModel>>? PageClosed;

        /// <summary>
        /// Se produce cuando se agregará una página a la colección de
        /// páginas de este host.
        /// </summary>
        public event EventHandler<CancelValueEventArgs<PageViewModel>>? PageAdding;

        /// <summary>
        /// Se produce cuando se cerrará una página en la colección de
        /// páginas de este host.
        /// </summary>
        public event EventHandler<CancelValueEventArgs<PageViewModel>>? PageClosing;

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
        public virtual async Task<bool> AddPage(PageViewModel page)
        {
            var r = PushPage(page);
            if (r) await InitPageAsync(page).ConfigureAwait(false);
            return r;
        }

        /// <summary>
        /// Cierra una página activa en esta instancia.
        /// </summary>
        /// <param name="page">
        /// Página a cerrar.
        /// </param>
        public virtual void ClosePage(PageViewModel page)
        {
            if (CancelEv(PageClosing, page)) return;
            page.UiServices = null!;
            _pages.Remove(page);
            PageClosed?.Invoke(this, page);
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="HostViewModel"/>.
        /// </summary>
        /// <param name="serviceFactory">
        /// Fábrica de proveedor de servicios de UI a utilizar al agregar un
        /// <see cref="PageViewModel"/> a este Host.
        /// </param>
        public HostViewModel(IUiServiceBrokerFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        /// <summary>
        /// Ejecuta la inicialización de la página de forma asíncrona.
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        protected async Task InitPageAsync(PageViewModel page)
        {
            await page.UiServices.VisualHost.RunBusyAsync(p => page.InitializeAsync(page.UiServices.VisualHost, p)).ConfigureAwait(false);
            PageReady?.Invoke(this, page);
        }

        /// <summary>
        /// Envía la página a la colección de páginas abiertas.
        /// </summary>
        /// <param name="page">
        /// Página a agregar.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si la página se agregó correctamente a la
        /// colección de páginas abiertas, <see langword="false"/> en caso
        /// contrario.
        /// </returns>
        protected bool PushPage(PageViewModel page)
        {
            if (CancelEv(PageAdding, page)) return false;
            page.PushInto(_pages).UiServices = _serviceFactory.Create(page, this);
            PageAdded?.Invoke(this, page);
            return true;
        }

        private bool CancelEv(EventHandler<CancelValueEventArgs<PageViewModel>>? handler, PageViewModel page)
        {
            var ev = new CancelValueEventArgs<PageViewModel>(page);
            handler?.Invoke(this, ev);
            return ev.Cancel;
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
        private readonly Dictionary<PageViewModel, T> _visuals = new Dictionary<PageViewModel, T>();
        private readonly IVisualBuilder<T> _visualBuilder;

        /// <summary>
        /// Se produce cuando un contenedor visual ha sido agregado a la 
        /// colección de visuales abiertos.
        /// </summary>
        public event EventHandler<ValueEventArgs<T>>? VisualAdded;

        /// <summary>
        /// Se produce cuando un contenedor visual es quitado de la colección
        /// de visuales abiertos.
        /// </summary>
        public event EventHandler<ValueEventArgs<T>>? VisualRemoved;

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="HostViewModel{T}"/>.
        /// </summary>
        /// <param name="visualBuilder">
        /// Constructor de contenedores visuales a utilizar para presentar
        /// las páginas.
        /// </param>
        /// <param name="serviceFactory">
        /// Fábrica de proveedor de servicios de UI a utilizar al instanciar un
        /// <see cref="PageViewModel"/>.
        /// </param>
        public HostViewModel(IVisualBuilder<T> visualBuilder, IUiServiceBrokerFactory serviceFactory) : base(serviceFactory)
        {
            _visualBuilder = visualBuilder;
        }

        /// <summary>
        /// Agrega una página a esta instancia.
        /// </summary>
        /// <param name="page">
        /// Página a agregar.
        /// </param>
        public override async Task<bool> AddPage(PageViewModel page)
        {
            T v;
            if (!PushPage(page)) return false;
            _visuals.Add(page, v = _visualBuilder.Build(page));
            Notify(nameof(Visuals));
            VisualAdded?.Invoke(this, new ValueEventArgs<T>(v));
            await InitPageAsync(page);
            return true;
        }

        /// <summary>
        /// Cierra una página activa en esta instancia.
        /// </summary>
        /// <param name="page">
        /// Página a cerrar.
        /// </param>
        public override void ClosePage(PageViewModel page)
        {
            T v;
            base.ClosePage(page);
            v = _visuals[page];
            _visuals.Remove(page);
            Notify(nameof(Visuals));
            VisualRemoved?.Invoke(this, new ValueEventArgs<T>(v));
        }

        /// <summary>
        /// Enumera los contenedores visuales de los
        /// <see cref="PageViewModel"/> abiertos dentro de esta instancia.
        /// </summary>
        public virtual IEnumerable<T> Visuals => _visuals.Select(p => p.Value);
    }
}