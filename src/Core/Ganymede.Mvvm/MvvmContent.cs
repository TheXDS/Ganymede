namespace TheXDS.Ganymede.Mvvm
{
    /// <summary>
    /// Enumera los distintos contenidos que pueden ser mostrados por un host
    /// visual utilizando MVVM.
    /// </summary>
    public enum MvvmContent
    {
        /// <summary>
        /// Contenido predeterminado. Se muestra la página.
        /// </summary>
        Default,

        /// <summary>
        /// Presentación de mensajes de diálogo interactivos.
        /// </summary>
        Message,

        /// <summary>
        /// Presentación de un bloque de progreso.
        /// </summary>
        Progress
    }
}