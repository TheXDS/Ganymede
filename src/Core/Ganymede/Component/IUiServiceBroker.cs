namespace TheXDS.Ganymede.Component
{
    /// <summary>
    /// Define una serie de miembros a implementar por un tipo que exponga 
    /// distintos servicios básicos de UI y de interacción con el usuario final
    /// de la aplicación.
    /// </summary>
    public interface IUiServiceBroker
    {
        /// <summary>
        /// Expone servicios de cuadro de diálogo.
        /// </summary>
        IUiDialogService Dialogs { get; }

        /// <summary>
        /// Expone servicios de ejecución de tareas en segundo plano para el
        /// <see cref="ViewModels.PageViewModel"/> activo.
        /// </summary>
        IUiHostService VisualHost { get; }

        /// <summary>
        /// Expone servicios de control para otras páginas abiertas dentro de
        /// la aplicación.
        /// </summary>
        IUiSiblingControl Siblings { get; }
    }
}