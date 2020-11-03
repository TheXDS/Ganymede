namespace TheXDS.Ganymede.Interactivity
{
    /// <summary>
    /// Describe una serie de miembros a implementar por un tipo que permita
    /// escribir entradas de bitácora que describen eventos ocurridos dentro de
    /// la aplicación.
    /// </summary>
    public interface ILogTarget
    {
        /// <summary>
        /// Escribe una entrada de bitácora en el objetivo.
        /// </summary>
        /// <param name="message">Mensaje a escribir en la bitácora.</param>
        void Log(string message);
    }
}
