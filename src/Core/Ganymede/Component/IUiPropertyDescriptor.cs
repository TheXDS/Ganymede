using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Types;

namespace TheXDS.Ganymede.Component
{
    /// <summary>
    /// Define una serie de miembros a implementar por un tipo que exponga
    /// funciones de configuración de la presentación de la UI para una
    /// instancia de la clase <see cref="PageViewModel"/>.
    /// </summary>
    public interface IUiPropertyDescriptor
    {
        /// <summary>
        /// Establece un color de adorno para la interfaz de la página.
        /// </summary>
        /// <value>
        /// Un <see cref="Color"/> de acento para decorar la página, o
        /// <see langword="null"/> para no utilizar decoraciones
        /// personalizadas.
        /// </value>
        Color? AccentColor { get; set; }

        /// <summary>
        /// Establece un valor que configura la posibilidad de cerrar una
        /// página.
        /// </summary>
        /// <value>
        /// <see langword="true"/> para permitir el cierre de la página,
        /// <see langword="false"/> en caso que el cierre de la página deba
        /// deshabilitarse.
        /// </value>
        bool Closeable { get; set; }

        /// <summary>
        /// Establece un valor que indica que el host visual será presentado al usuario de manera modal.
        /// </summary>
        bool Modal { get; set; }

        /// <summary>
        /// Establece el título de la página.
        /// </summary>
        string Title { get; set; }
    }
}