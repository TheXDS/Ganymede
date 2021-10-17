using System;
using System.Windows.Controls;
using TheXDS.Ganymede.Component;
using TheXDS.Ganymede.Pages;
using TheXDS.Ganymede.ViewModels;
using TheXDS.Ganymede.WpfBroker.Pages;

namespace TheXDS.Ganymede.WpfBroker.ViewModels
{
    /// <summary>
    /// ViewModel Host principal de la aplicación.
    /// </summary>
    public class MainHostViewModel : HostViewModel<TabHost>
    {
        /// <summary>
        /// Registro de páginas cuyos contenedores visuales se especifican por
        /// medio de un diccionario.
        /// </summary>
        public DictionaryVisualResolver<Page>? PageRegistry { get; }

        /// <summary>
        /// Inicializa una nueva isntancia de la clase
        /// <see cref="MainHostViewModel"/>.
        /// </summary>
        public MainHostViewModel() : this(CreateBuilder(out DictionaryVisualResolver<Page>? m))
        {
            PageRegistry = m;
        }

        /// <summary>
        /// Inicializa una nueva isntancia de la clase
        /// <see cref="MainHostViewModel"/>.
        /// </summary>
        /// <param name="builder">Contructor de visuales a utilizar.</param>
        protected MainHostViewModel(IVisualBuilder<TabHost> builder) : base(builder, new STAMvvmServiceBrokerFactory())
        {
        }

        private static IVisualBuilder<TabHost> CreateBuilder(out DictionaryVisualResolver<Page> dict)
        {
            VisualResolverCollection<Page>? c = new()
            {
                (dict = new DictionaryVisualResolver<Page>()),
                new AnnotationsVisualResolver<Page>(),
                new ConventionVisualResolver<Page>()
            };
            return new TabBuilder(new FallbackVisualResolver<Page>(c, BuildErrorPage));
        }

        private static Page BuildErrorPage(PageViewModel vm, Exception ex)
        {
            return new FallbackErrorPage(ex);
        }
    }
}
