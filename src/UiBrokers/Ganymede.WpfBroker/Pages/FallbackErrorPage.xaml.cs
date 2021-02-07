using System;
using System.Windows.Controls;

namespace TheXDS.Ganymede.WpfBroker.Pages
{
    /// <summary>
    /// Lógica de interacción para FallbackErrorPage.xaml
    /// </summary>
    public partial class FallbackErrorPage : Page
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="FallbackErrorPage"/>.
        /// </summary>
        public FallbackErrorPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="FallbackErrorPage"/>.
        /// </summary>
        /// <param name="ex">Excepción generada.</param>
        public FallbackErrorPage(Exception ex) : this()
        {
            Title = ex.GetType().Name;
            Message = ex.Message;
        }

        /// <summary>
        /// Obtiene o establece el mensaje a mostrar directamente en esta
        /// página.
        /// </summary>
        public string Message
        {
            get => LblMessage.Text;
            init => LblMessage.Text = value;
        }
    }
}
