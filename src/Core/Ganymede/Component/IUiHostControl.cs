namespace TheXDS.Ganymede.Component
{
    /// <summary>
    /// Define una serie de miembros a implemetar por un tipo que permita a una
    /// página controlar a su host visual.
    /// </summary>
    public interface IUiHostControl : IUiPropertyDescriptor
    {
        /// <summary>
        /// Indica al host visual que debe activarse u obtener el foco.
        /// </summary>
        void Activate();

        /// <summary>
        /// Solicita el cierre de la página actualmente alojada.
        /// </summary>
        void Close();

        /// <summary>
        /// Indica al host visual que debe desactivarse, ocultarse o perder el
        /// foco activo.
        /// </summary>
        void Deactivate();
    }
}