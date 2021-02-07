namespace TheXDS.Ganymede.Component
{
    /// <summary>
    /// Define una serie de miembros a implementar por un tipo que permita 
    /// establecer un contexto de datos para utilizar internamente.
    /// </summary>
    public interface IDataContext
    {
        /// <summary>
        /// Obtiene o establece el contexto de datos utilizado por esta
        /// instancia.
        /// </summary>
        object? DataContext { get; set; }
    }
}