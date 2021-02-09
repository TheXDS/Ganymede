using System.Windows;
using System.Windows.Controls;

namespace TheXDS.Ganymede.WpfBroker.Widgets
{
    /// <summary>
    /// Control que define el estado visual predeterminado a mostrar cuando un
    /// <see cref="UiPageHost"/> se encuentra en un estado que muestra el
    /// progreso de una operación.
    /// </summary>
    public class GanymedeProgressDialog : Control
    {
        static GanymedeProgressDialog()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GanymedeProgressDialog), new FrameworkPropertyMetadata(typeof(GanymedeProgressDialog)));
        }
    }
}
