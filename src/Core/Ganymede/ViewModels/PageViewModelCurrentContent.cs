namespace TheXDS.Ganymede.ViewModels
{
    /// <summary>
    /// Enumera distintos tipos de contenido alternativo predefinido que 
    /// puede ser presentado en el host visual de un 
    /// <see cref="PageViewModel"/>.
    /// </summary>
    public enum PageViewModelCurrentContent : byte
    {
        /// <summary>
        /// Contenido predeterminado. Se presenta el contenido de la página.
        /// </summary>
        Defualt,
        /// <summary>
        /// Overlay para presentar mensajes genéricos.
        /// </summary>
        Message,
        /// <summary>
        /// Overlay para diálogos de entrada.
        /// </summary>
        Input,
        /// <summary>
        /// Overlay para estados de progreso.
        /// </summary>
        BusyOp
    }
}