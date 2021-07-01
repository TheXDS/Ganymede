using System.Threading.Tasks;
using TheXDS.Ganymede.Pages;
using TheXDS.Ganymede.ViewModels;
using TheXDS.Ganymede.WpfBroker.ViewModels;
using TheXDS.Proteus.Slim.Pages;

namespace TheXDS.Proteus.Slim.ViewModels
{
    /// <summary>
    /// ViewModel Host principal de la aplicación.
    /// </summary>
    public class MainWindowViewModel : MainHostViewModel
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="MainWindowViewModel"/>.
        /// </summary>
        public MainWindowViewModel()
        {
            Task.WhenAll(new[]
            {
                AddPage(new TestViewModel()),
                AddUiTestPage()
            });
        }

        private async Task AddUiTestPage()
        {
            PageViewModel? vm = new PageViewModel();
            TabHost? v = new TabHost(vm, new UiDemoPage());
            await AddVisualDirect(vm, v);
        }

        private double _NoiseOpacity = 0.025;
        private double _tintOpacity = 0.75;
        private string _title = "Proteus Slim";

        /// <summary>
        /// Obtiene o establece el valor de opacidad a aplicar a la capa de
        /// efectos que agrega ruido al fondo semitransparente de la ventana.
        /// </summary>
        public double NoiseOpacity
        {
            get => _NoiseOpacity;
            set => Change(ref _NoiseOpacity, value);
        }

        /// <summary>
        /// Obtiene o establece el valor de opacidad a aplicar a la capa de
        /// efectos que contiene el color base del fondo de la ventana.
        /// </summary>
        public double TintOpacity
        {
            get => _tintOpacity;
            set => Change(ref _tintOpacity, value);
        }

        /// <summary>
        /// Obtiene o establece el título de la ventana.
        /// </summary>
        public string Title
        {
            get => _title;
            set => Change(ref _title, value);
        }
    }
}
