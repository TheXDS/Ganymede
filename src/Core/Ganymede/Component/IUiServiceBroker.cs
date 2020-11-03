using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Types;
using TheXDS.MCART.UI;
using St = TheXDS.Ganymede.Resources.Strings;

namespace TheXDS.Ganymede.Component
{
    /// <summary>
    /// Define una serie de miembros a implementar por un tipo que exponga
    /// servicios de UI a una página de Ganymede.
    /// </summary>
    public interface IUiServiceBroker : IUiConfigurator
    {
        /// <summary>
        /// Obtiene el título de la página.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Obtiene un valor que define si es posible cerrar la página.
        /// </summary>
        bool Closeable { get; }

        /// <summary>
        /// Obtiene una referencia al color de acento definido para la página.
        /// </summary>
        Color? AccentColor { get; }

        /// <summary>
        /// Solicita el cierre de una página.
        /// </summary>
        /// <param name="page"></param>
        /// <returns>
        /// <see langword="true"/> si este método ha cerrado la página 
        /// especificada o si la misma no esta abaierta, 
        /// <see langword="false"/> en caso que la página no se haya cerrado.
        /// </returns>
        bool Close(PageViewModel page);

        /// <summary>
        /// Solicita la apertura de una página.
        /// </summary>
        /// <param name="page">Página a abrir o activar.</param>
        /// <returns>
        /// <see langword="true"/> si la página solicitada ha sido abierta 
        /// exitosamente o si la misma ya se encontraba abierta, 
        /// <see langword="false"/> en caso contrario.
        /// </returns>
        Task<bool> OpenAsync(PageViewModel page);

        /// <summary>
        /// Solicita la apertura de una nueva página.
        /// </summary>
        /// <typeparam name="TPage">
        /// Tipo de la página a instanciar y abrir.
        /// </typeparam>
        /// <returns>
        /// <see langword="true"/> si la página solicitada ha sido abierta 
        /// exitosamente o si la misma ya se encontraba abierta, 
        /// <see langword="false"/> en caso contrario.
        /// </returns>
        Task<bool> OpenAsync<TPage>() where TPage : PageViewModel, new() => OpenAsync(new TPage());

        /// <summary>
        /// Envía un mensaje de diálogo al servicio de UI.
        /// </summary>
        /// <param name="message">Mensaje a desplegar.</param>
        /// <param name="options">Opciones de interacción.</param>
        /// <returns>
        /// El índice de base cero de la opción activada por el usuario al
        /// interactuar con el diálogo.
        /// </returns>
        Task<int> Message(string message, params string[] options);

        /// <summary>
        /// Permite selecionar un valor de una enumeración por medio del 
        /// servicio de UI.
        /// </summary>
        /// <typeparam name="T">
        /// Enumeración desde la cual obtener un valor.
        /// </typeparam>
        /// <param name="prompt">
        /// Mensaje a desplegar al usuario.
        /// </param>
        /// <returns>
        /// El valor de enumeración seleccionado por el usuario.
        /// </returns>
        async Task<T> Select<T>(string prompt) where T : struct, Enum
        {
            var v = NamedObject<T>.FromEnum().ToArray();
            var r = await Message(prompt, v.Select(p => p.Name).ToArray());
            return v[r].Value;
        }

        /// <summary>
        /// Envía un mensaje de diálogo al servicio de UI.
        /// </summary>
        /// <param name="message">Mensaje a desplegar.</param>
        /// <param name="options">
        /// Opciones de interacción. Se ejecutará el comando asociado a la
        /// opción activada por el usuario.
        /// </param>
        Task Message(string message, IList<Launcher> options)
        {
            return Task.Run(async () => options[await Message(message, options.Select(p => p.Name).ToArray())].Command.Execute(null));
        }

        /// <summary>
        /// Envía un mensaje de diálogo simple al servicio de UI.
        /// </summary>
        /// <param name="message">Mensaje a desplegar.</param>
        Task Message(string message) => Message(message, St.Ok);

        /// <summary>
        /// Envía un mensaje de diálogo con respuesta de "si/no" al servicio de UI.
        /// </summary>
        /// <param name="question">mensaje a desplegar al usuario.</param>
        /// <returns>
        /// <see langword="true"/> si el usuario ha activado la opción de "Sí",
        /// <see langword="false"/> en caso contrario.
        /// </returns>
        async Task<bool> AskYn(string question) => await Message(question, St.Yes, St.No) == 0;

        /// <summary>
        /// Envía un mensaje de diálogo con respuesta de "si/no/cancelar" al servicio de UI.
        /// </summary>
        /// <param name="question">mensaje a desplegar al usuario.</param>
        /// <returns>
        /// <see langword="true"/> si el usuario ha activado la opción de "Sí",
        /// <see langword="false"/> se el usuario ha activado la opción de
        /// "No", o <see langword="null"/> si el usuario ha activado la opción
        /// de "Cancelar"
        /// </returns>
        async Task<bool?> AskYnc(string question)
        {
            return await Message(question, St.Yes, St.No, St.Cancel) switch
            {
                0 => true,
                1 => false,
                _ => null
            };
        }

        /// <summary>
        /// Envía una acción como un trabajador de fondo al servicio de UI,
        /// permitiendo reportar el estado de la acción y cambiando el estado
        /// de presentación de la página a "Ocupado".
        /// </summary>
        /// <param name="progress">
        /// Objeto que permite reportar el progreso de una tarea.
        /// </param>
        /// <returns>
        /// Una tarea que puede utilizarse para monitorear la operación
        /// asíncrona.
        /// </returns>
        Task RunBusyAsync(Action<IProgress<ProgressInfo>> progress);

        /// <summary>
        /// Envía una función como un trabajador de fondo al servicio de UI,
        /// permitiendo reportar el estado de la función y cambiando el estado
        /// de presentación de la página a "Ocupado".
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de valor devuelto por la función.
        /// </typeparam>
        /// <param name="progress">
        /// Objeto que permite reportar el progreso de una tarea.
        /// </param>
        /// <returns>
        /// Una tarea con valor devuelto de tipo <typeparamref name="T"/> que
        /// puede utilizarse para monitorear la operación asíncrona.
        /// </returns>
        Task<T> RunBusyAsync<T>(Func<IProgress<ProgressInfo>,T> progress);
    }
}