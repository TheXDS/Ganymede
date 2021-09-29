namespace TheXDS.Ganymede.Component
{
    /// <summary>
    /// Enumera los posibles tipos de mensaje de diálogo.
    /// </summary>
    public enum MessageDialogType
    {
        /// <summary>
        /// Mensaje genérico.
        /// </summary>
        Message,
        /// <summary>
        /// Mensaje informativo.
        /// </summary>
        Information,
        /// <summary>
        /// Advertencia.
        /// </summary>
        Warning,
        /// <summary>
        /// Error.
        /// </summary>
        Error,
        /// <summary>
        /// Error crítico.
        /// </summary>
        Critical,
        /// <summary>
        /// Pregunta.
        /// </summary>
        Question,
        /// <summary>
        /// Cuadro de entrada.
        /// </summary>
        Input
    }
}