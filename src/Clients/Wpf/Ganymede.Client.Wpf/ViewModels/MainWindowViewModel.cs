using System;
using System.Windows.Controls;
using TheXDS.MCART.Types.Extensions;
using TheXDS.Ganymede.Pages;
using TheXDS.Ganymede.Component;
using TheXDS.Ganymede.ViewModels;
using TheXDS.Ganymede.Client.Pages;
using TheXDS.Ganymede.Mvvm;
using System.Threading.Tasks;

namespace TheXDS.Ganymede.Client.ViewModels
{
    /// <summary>
    /// ViewModel Host principal de la aplicación.
    /// </summary>
    public class MainWindowViewModel : HostViewModel<TabHost>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="MainWindowViewModel"/>.
        /// </summary>
        public MainWindowViewModel() : base(CreateBuilder(), new MvvmServiceBrokerFactory())
        {
            Task.WhenAll(new[]
            {
                Task.Run(() => AddPage(new TestViewModel()))
            });            
        }

        private static IVisualBuilder<TabHost> CreateBuilder()
        {
            var c = new VisualResolverCollection<Page>
            {
                new DictionaryVisualResolver<Page>().RegisterVisual<TestViewModel, TestPage>(),
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
