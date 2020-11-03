using St = TheXDS.Ganymede.Resources.Strings;

namespace TheXDS.Ganymede.Interactivity
{
    /// <summary>
    /// Describe una serie de miembros a implementar por un tipo que permita
    /// presentar mensajes de estado.
    /// </summary>
    public interface IMessageTarget
    {
        /// <summary>
        /// Muestra un mensaje.
        /// </summary>
        /// <param name="title">Título del mensaje.</param>
        /// <param name="message">Mensaje a mostrar.</param>
        void Message(string title, string message);

        /// <summary>
        /// Muestra un mensaje.
        /// </summary>
        /// <param name="message">Mensaje a mostrar.</param>
        void Message(string message) => Message(St.Message, message);

        /// <summary>
        /// Muestra un mensaje de información.
        /// </summary>
        /// <param name="message">Mensaje a mostrar.</param>
        void Info(string message) => Message(St.Info, message);

        /// <summary>
        /// Muestra un mensaje de advertencia.
        /// </summary>
        /// <param name="message">Mensaje a mostrar.</param>
        void Warning(string message) => Message(St.Warning, message);

        /// <summary>
        /// Muestra un mensaje de detención.
        /// </summary>
        /// <param name="message">Mensaje a mostrar.</param>
        void Stop(string message) => Message(St.Stop, message);

        /// <summary>
        /// Muestra un mensaje de error.
        /// </summary>
        /// <param name="message">Mensaje a mostrar.</param>
        void Error(string message) => Message(St.Error, message);

        /// <summary>
        /// Muestra un mensaje de error crítico.
        /// </summary>
        /// <param name="message">Mensaje a mostrar.</param>
        void Critical(string message) => Message(St.Critical, message);
    }
}
