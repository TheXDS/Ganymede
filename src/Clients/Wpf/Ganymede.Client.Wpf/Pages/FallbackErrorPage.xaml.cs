using System.Windows.Controls;

namespace TheXDS.Ganymede.Client.Pages
{
    /// <summary>
    /// Lógica de interacción para FallbackErrorPage.xaml
    /// </summary>
    public partial class FallbackErrorPage : Page
    {
        public FallbackErrorPage()
        {
            InitializeComponent();
        }

        public string Message { get => LblMessage.Text; set => LblMessage.Text = value; }
    }
}
