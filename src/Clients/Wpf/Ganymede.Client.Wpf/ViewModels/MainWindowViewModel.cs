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
    public class MainWindowViewModel : HostViewModel<TabHost>
    {
        public MainWindowViewModel() : base(CreateBuilder(), new MvvmServiceBrokerFactory())
        {
            Task.WhenAll(new[]
            {
                Task.Run(() => AddPage(new TestViewModel()))
            });            
        }

        private static IVisualBuilder<TabHost> CreateBuilder()
        {
            var r = new DictionaryVisualResolver<Page>();
            r.RegisterVisual<TestViewModel, TestPage>();
            return new TabBuilder(new TestFvr(r));
        }
    }

    /// <summary>
    /// <see cref="FallbackVisualResolver{TVisual}"/> que genera una página de
    /// error cuando no se puede resolver el contenedor visual de un
    /// <see cref="PageViewModel"/>.
    /// </summary>
    public class TestFvr : FallbackVisualResolver<Page>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="TestFvr"/>.
        /// </summary>
        /// <param name="resolver">
        /// <see cref="IVisualResolver{T}"/> a envolver en un generador con
        /// Fallback.
        /// </param>
        public TestFvr(IVisualResolver<Page> resolver) : base(resolver)
        {
        }

        /// <inheritdoc/>
        protected override Page FallbackResolve(PageViewModel viewModel, Exception ex)
        {
            return new FallbackErrorPage { Message = $"{ex.GetType().Name}{ex.Message.OrNull(": {0}")}" };
        }
    }
}
