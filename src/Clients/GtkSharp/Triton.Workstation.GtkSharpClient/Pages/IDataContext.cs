namespace TheXDS.Triton.Workstation.GtkSharpClient.Pages
{
    /// <summary>
    /// Define una serie de miembros a implementar por un tipo que permita 
    /// establecer un contexto de datos para utilizar internamente.
    /// </summary>
    /// <remarks>
    /// Para C#9, esta interfaz es candidato a mudarse a la característica de
    /// "shapes".
    /// </remarks>
    public interface IDataContext
    {
        /// <summary>
        /// Obtiene o establece el contexto de datos utilizado por esta
        /// instancia.
        /// </summary>
        object? DataContext { get; set; }
    }
}