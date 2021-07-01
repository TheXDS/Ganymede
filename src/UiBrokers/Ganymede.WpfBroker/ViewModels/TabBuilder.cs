using System.Windows;
using System.Windows.Controls;
using TheXDS.Ganymede.Component;
using TheXDS.Ganymede.Pages;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.WpfBroker.ViewModels
{
    /// <summary>
    /// Constructor visual que genera páginas en pestañas utilizando controles
    /// <see cref="TabHost"/> como el host primario y <see cref="Page"/> como
    /// contenido visual.
    /// </summary>
    public class TabBuilder : IVisualBuilder<TabHost>
    {
        private readonly IVisualResolver<Page> _resolver;

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="TabBuilder"/>, especificando el
        /// <see cref="IVisualResolver{T}"/> fuertemente tipeado a utilizar
        /// para obtener páginas a partir de un <see cref="PageViewModel"/>.
        /// </summary>
        /// <param name="resolver">
        /// <see cref="IVisualResolver{T}"/> fuertemente tipeado a utilizar
        /// para obtener páginas a partir de un <see cref="PageViewModel"/>.
        /// </param>
        public TabBuilder(IVisualResolver<Page> resolver)
        {
            _resolver = resolver;
        }

        /// <inheritdoc/>
        public TabHost Build(PageViewModel viewModel)
        {
            return Application.Current.Dispatcher.Invoke(() =>
                new TabHost(viewModel, _resolver.ResolveVisual(viewModel)));
        }
    }
}