using System.Diagnostics.CodeAnalysis;
using St = TheXDS.Ganymede.Resources.Strings;

namespace TheXDS.Ganymede.Interactivity
{
    /// <summary>
    /// Describe una serie de miembros a implementar por un tipo que permita
    /// obtener valores de forma interactiva.
    /// </summary>
    public interface IInputTarget
    {
        /// <summary>
        /// Realiza una pregunta al usuario, incluyendo las opciones "Sí", "No"
        /// y "Cancelar".
        /// </summary>
        /// <param name="title">Título del mensaje.</param>
        /// <param name="question">Pregunta a realizar.</param>
        /// <returns>
        /// <see langword="true"/> si el usuario ha contestado que "Sí" a la
        /// pregunta realizada, <see langword="false"/> si contesta que "No", o
        /// <see langword="null"/> si el usuario ha seleccionado "Cancelar".
        /// </returns>
        bool? AskOrNull(string title, string question);

        /// <summary>
        /// Realiza una pregunta al usuario, incluyendo las opciones "Sí" y
        /// "No".
        /// </summary>
        /// <param name="title">Título del mensaje.</param>
        /// <param name="question">Pregunta a realizar.</param>
        /// <param name="default">Valor predeterminado.</param>
        /// <returns>
        /// <see langword="true"/> si el usuario ha contestado que "Sí" a la
        /// pregunta realziada, <see langword="false"/> en caso contrario.
        /// </returns>
        bool Ask(string title, string question, bool @default) => AskOrNull(title, question) ?? @default;

        /// <summary>
        /// Realiza una pregunta al usuario, incluyendo las opciones "Sí" y
        /// "No".
        /// </summary>
        /// <param name="title">Título del mensaje.</param>
        /// <param name="question">Pregunta a realizar.</param>
        /// <returns>
        /// <see langword="true"/> si el usuario ha contestado que "Sí" a la
        /// pregunta realziada, <see langword="false"/> en caso contrario.
        /// </returns>
        bool Ask(string title, string question) => Ask(title, question, false);
        
        /// <summary>
        /// Realiza una pregunta al usuario, incluyendo las opciones "Sí" y
        /// "No".
        /// </summary>
        /// <param name="title">Título del mensaje.</param>
        /// <returns>
        /// <see langword="true"/> si el usuario ha contestado que "Sí" a la
        /// pregunta realziada, <see langword="false"/> en caso contrario.
        /// </returns>
        bool Ask(string title) => Ask(title, St.AreYouSureRunOp, false);

        /// <summary>
        /// Obtiene del usuario un valor del tipo especificado.
        /// </summary>
        /// <typeparam name="T">Tipo de valor a obtener.</typeparam>
        /// <param name="title">Título del diálogo.</param>
        /// <param name="question">Pregunta a realizar.</param>
        /// <param name="value">Valor de salida del método.</param>
        /// <returns>
        /// <see langword="true"/> si el usuario contesta la pregunta y provee 
        /// de un valor, <see langword="false"/> en caso contrario.
        /// </returns>
        bool Get<T>(string title, string question, [MaybeNullWhen(false)] out T value) => Get(title, question, default, out value);

        /// <summary>
        /// Obtiene del usuario un valor del tipo especificado.
        /// </summary>
        /// <typeparam name="T">Tipo de valor a obtener.</typeparam>
        /// <param name="title">Título del diálogo.</param>
        /// <param name="question">Pregunta a realizar.</param>
        /// <param name="default">Valor predeterminado inicial.</param>
        /// <param name="value">Valor de salida del método.</param>
        /// <returns>
        /// <see langword="true"/> si el usuario contesta la pregunta y provee 
        /// de un valor, <see langword="false"/> en caso contrario.
        /// </returns>
        bool Get<T>(string title, string question, [AllowNull]T @default, [MaybeNullWhen(false)] out T value)
        {
            value = @default;
            return Edit(title, question, ref value!);
        }

        /// <summary>
        /// Obtiene del usuario un valor del tipo especificado.
        /// </summary>
        /// <typeparam name="T">Tipo de valor a obtener.</typeparam>
        /// <param name="title">Título del diálogo.</param>
        /// <param name="question">Pregunta a realizar.</param>
        /// <param name="value">Valor de salida del método.</param>
        /// <returns>
        /// <see langword="true"/> si el usuario contesta la pregunta y provee 
        /// de un valor, <see langword="false"/> en caso contrario.
        /// </returns>
        bool Edit<T>(string title, string question, [MaybeNullWhen(false)] ref T value);
    }
}
