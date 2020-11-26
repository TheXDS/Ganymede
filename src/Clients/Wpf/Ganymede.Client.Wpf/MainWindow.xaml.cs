using System.Windows;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Events;
using TheXDS.MCART.Math;
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
            _vm.PageAdded += MainWindowViewModel_PageAdded;
            _vm.PageClosing += MainWindowViewModel_PageClosing;
            _vm.PageClosed += MainWindowViewModel_PageClosed;
        }


        private int _lastActivePage;

        private void MainWindowViewModel_PageClosing(object? sender, CancelValueEventArgs<PageViewModel> e)
        {
            _lastActivePage = _tabRoot.SelectedIndex;
        }
        private void MainWindowViewModel_PageClosed(object? sender, ValueEventArgs<PageViewModel> e)
        {
            if (_tabRoot.Items.Count > 0) _tabRoot.SelectedIndex = _lastActivePage;
        }

        private void MainWindowViewModel_PageAdded(object? sender, ValueEventArgs<PageViewModel> e)
        {
            _tabRoot.SelectedIndex = _vm.Pages.FindIndexOf(e.Value);
        }
    }
}
