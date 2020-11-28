using System.Windows;
using TheXDS.Ganymede.Pages;
using TheXDS.MCART.Events;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.Ganymede.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="MainWindow"/>.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _vm.VisualAdded += MainWindowViewModel_VisualAdded;
        }

        private void MainWindowViewModel_VisualAdded(object? sender, ValueEventArgs<TabHost> e)
        {
            int i;
            while ((i = _vm.Visuals.FindIndexOf(e.Value)) == -1)
            {
                System.Threading.Thread.Sleep(50);
            }
            _tabRoot.SelectedIndex = i;
        }
    }
}
