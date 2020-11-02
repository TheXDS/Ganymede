using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Types;

namespace TheXDS.Ganymede.Component
{
    /// <summary>
    /// Define una serie de miembros a implementar por un tipo que exponga
    /// funciones de configuración de la presentación de la UI para una
    /// instancia de la clase <see cref="PageViewModel"/>.
    /// </summary>
    public interface IUiConfigurator
    {
        /// <summary>
        /// Establece el título de la página.
        /// </summary>
        /// <param name="value">Título de la página.</param>
        void SetTitle(string value);

        /// <summary>
        /// Establece un valor que configura la posibilidad de cerrar una
        /// página.
        /// </summary>
        /// <param name="value">
        /// Valor que representa la posibilidad de cerrar la página.
        /// <see langword="true"/> para permitir el cierre de la página,
        /// <see langword="false"/> en caso que el cierre de la página deba
        /// deshabilitarse.
        /// </param>
        void SetCloseable(bool value);

        /// <summary>
        /// Establece un color de adorno para la interfaz de la página.
        /// </summary>
        /// <param name="value">
        /// Color a utilizar para decorar los elementos de la página.
        /// </param>
        void SetAccentColor(Color? value);
    }
}