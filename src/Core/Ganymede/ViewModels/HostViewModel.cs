using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TheXDS.Ganymede.Component;
using TheXDS.MCART.Component;
using TheXDS.MCART.Events;
using TheXDS.MCART.Math;
using TheXDS.MCART.Types.Extensions;
using TheXDS.MCART.ViewModel;

namespace TheXDS.Ganymede.ViewModels
{
    /// <summary>
    /// Clase base para un ViewModel que contenga páginas.
    /// </summary>
    public class HostViewModel : ViewModelBase
    {
        private protected readonly ObservableCollection<PageViewModel> _pages = new();
        private readonly IUiServiceBrokerFactory _serviceFactory;
        private PageViewModel? _activePage;

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
        /// Obtiene o establece el índice de la página visual activa.
        /// </summary>
        public int ActiveIndex
        {
            get => Pages.FindIndexOf(ActivePage);
            set => SetActivePage(value);
        }

        /// <summary>
        ///     Obtiene o establece el valor ActivePage.
        /// </summary>
        /// <value>El valor de ActivePage.</value>
        public virtual PageViewModel? ActivePage
        {
            get => _activePage;
            set => Change(ref _activePage, value);
        }

        /// <summary>
        /// Agrega una página a esta instancia.
        /// </summary>
        /// <param name="page">
        /// Página a agregar.
        /// </param>
        public virtual async Task<bool> AddPage(PageViewModel page)
        {
            bool r = PushPage(page);
            if (r) await InitPageAsync(ActivePage = page).ConfigureAwait(false);
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
            int i = ActiveIndex;
            page.UiServices = null!;
            _pages.Remove(page);
            SetActivePage(i);
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
            RegisterPropertyChangeBroadcast(nameof(ActivePage), nameof(ActiveIndex));
        }

        /// <summary>
        /// Ejecuta la inicialización de la página de forma asíncrona.
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        protected async Task InitPageAsync(PageViewModel page)
        {
            await (page.UiServices?.VisualHost.RunBusyAsync(p => page.InitializeAsync(page.UiServices.VisualHost, p)) ?? Task.CompletedTask).ConfigureAwait(false);
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
            CancelValueEventArgs<PageViewModel>? ev = new(page);
            handler?.Invoke(this, ev);
            return ev.Cancel;
        }

        /// <summary>
        /// Establece el índice de la página activa.
        /// </summary>
        /// <param name="index">Índice de la págia activa.</param>
        protected void SetActivePage(int index)
        {
            if (index > -1 && Pages.Any())
            {
                ActivePage = Pages.ElementAt(index.Clamp(0, Pages.Count() - 1));
            }
            else
            {
                ActivePage = null;
            }
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
        private readonly Dictionary<PageViewModel, T> _visuals = new();
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
        /// Obtiene una referencia al constructor de contenedores visuales 
        /// activo para esta instancia.
        /// </summary>
        protected IVisualBuilder<T> VisualBuilder => _visualBuilder;

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
            RegisterPropertyChangeBroadcast(nameof(ActivePage), nameof(ActiveVisual));
        }

        /// <summary>
        /// Agrega una página a esta instancia.
        /// </summary>
        /// <param name="page">Página a agregar.</param>
        public override Task<bool> AddPage(PageViewModel page)
        {
            return AddVisualDirect(page, _visualBuilder.Build(page));
        }

        /// <summary>
        /// Agrega directamente un <see cref="PageViewModel"/> asociado al
        /// contenedor visual especificado.
        /// </summary>
        /// <param name="page">Página a agregar.</param>
        /// <param name="visual">Contenedor visual para la página.</param>
        protected async Task<bool> AddVisualDirect(PageViewModel page, T visual)
        {
            if (!PushPage(page)) return false;
            _visuals.Add(ActivePage = page, visual);
            Notify(nameof(Visuals));
            VisualAdded?.Invoke(this, new ValueEventArgs<T>(visual));
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
            int i = ActiveIndex;
            base.ClosePage(page);
            v = _visuals[page];
            _visuals.Remove(page);
            Notify(nameof(Visuals));
            VisualRemoved?.Invoke(this, new ValueEventArgs<T>(v));
            SetActivePage(i);
        }

        /// <summary>
        /// Enumera los contenedores visuales de los
        /// <see cref="PageViewModel"/> abiertos dentro de esta instancia.
        /// </summary>
        public virtual IEnumerable<T> Visuals
        {
            get
            {
                // HACK: Al implementar de este modo, se fuerza el orden de los objetos visuales.
                foreach (var j in Pages)
                {
                    yield return _visuals[j];
                }
            }
        }

        /// <summary>
        /// Obtiene una referencia al contenedor visual para la página activa.
        /// </summary>
        public T? ActiveVisual => ActivePage is not null && _visuals.TryGetValue(ActivePage, out T? t) ? t : default;
    }
}