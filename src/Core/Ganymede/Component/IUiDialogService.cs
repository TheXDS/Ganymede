using TheXDS.MCART.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheXDS.MCART.UI;
using St = TheXDS.Ganymede.Resources.Strings;

namespace TheXDS.Ganymede.Component
{
    public interface IUiServiceBroker
    {
        IUiDialogService Dialogs { get; }
        IUiHostService Host { get; }
        IUiPropertyDescriptor Properties { get; }
        IUiSiblingControl Siblings { get; }
    }

    /// <summary>
    /// Define una serie de miembros a implementar por un tipo que provea de 
    /// servicios de diálogo de UI a una página.
    /// </summary>
    public interface IUiDialogService
    {
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
    }
}