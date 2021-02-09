using System.Windows;
using System.Windows.Controls;

namespace TheXDS.Ganymede.WpfBroker.Widgets
{
    /// <summary>
    /// Control que define el estado visual predeterminado a mostrar cuando un
    /// <see cref="UiPageHost"/> se encuentra en un estado que muestra un
    /// cuadro de diálogo con opciones de selección con botones.
    /// </summary>
    public class GanymedeMessageBoxDialog : Control
    {
        static GanymedeMessageBoxDialog()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GanymedeMessageBoxDialog), new FrameworkPropertyMetadata(typeof(GanymedeMessageBoxDialog)));
        }
    }
}
