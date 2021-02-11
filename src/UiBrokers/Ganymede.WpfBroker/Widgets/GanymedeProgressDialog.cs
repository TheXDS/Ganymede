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

    /// <summary>
    /// Control que define el estado visual predeterminado a mostrar cuando un
    /// <see cref="UiPageHost"/> se encuentra en un estado que muestra un
    /// cuadro de introducción de datos.
    /// </summary>
    public class GanymedeInputDialog : Control
    {
        static GanymedeInputDialog()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GanymedeInputDialog), new FrameworkPropertyMetadata(typeof(GanymedeInputDialog)));
        }
    }

    /// <summary>
    /// Control auxiliar que permite establecer un overlay a aplicar dentro de un <see cref="GanymedeInputDialog"/> que define el control de edición a presentar.
    /// </summary>
    public class GanymedeInputDialogOverlay : Control
    {
        static GanymedeInputDialogOverlay()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GanymedeInputDialogOverlay), new FrameworkPropertyMetadata(typeof(GanymedeInputDialogOverlay)));
        }
    }
}
