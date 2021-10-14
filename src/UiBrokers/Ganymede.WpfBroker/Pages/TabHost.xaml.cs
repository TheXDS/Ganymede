using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TheXDS.Ganymede.ViewModels;
using TheXDS.Ganymede.WpfBroker.Widgets;

namespace TheXDS.Ganymede.Pages
{
    /// <summary>
    /// Lógica de interacción para TabHost.xaml
    /// </summary>
    public partial class TabHost : TabItem
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="TabHost"/>.
        /// </summary>
        /// <param name="viewModel">
        /// <see cref="PageViewModel"/> que controlará la lógica interactiva de
        /// la página.
        /// </param>
        /// <param name="content">Página visual que contiene los controles y
        /// widgets por medio de los cuales el usuario intectuará con la
        /// aplicación.
        /// </param>
        public TabHost(PageViewModel viewModel, Page content)
        {
            InitializeComponent();
            DataContext = viewModel;
            Content = new UiPageHost()
            {
                Page = content,
            };
            content.DataContext = DataContext;
        }

        /// <summary>
        /// Obtiene una referencia a la propiedad de dependencia <see cref="TabBackground"/>.
        /// </summary>
        public static readonly DependencyProperty TabBackgroundProperty =
            DependencyProperty.Register(nameof(TabBackground), typeof(Brush), typeof(TabHost), new FrameworkPropertyMetadata(Brushes.Gray, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Obtiene o establece un color de fondo a utilizar en el control
        /// visual a mostrarse dentro del encabezado de un 
        /// <see cref="TabHostControl"/>.
        /// </summary>
        public Brush TabBackground
        {
            get => (Brush)GetValue(TabBackgroundProperty);
            set => SetValue(TabBackgroundProperty, value);
        }
    }
}
