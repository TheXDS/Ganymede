using TheXDS.Ganymede.ViewModels;
using System;
using System.Threading.Tasks;

namespace TheXDS.Ganymede.Component
{
    /// <summary>
    /// Define una serie de miembros a implementar por un tipo que provea de
    /// servicios varios por parte del host visual.
    /// </summary>
    public interface IUiHostService : IUiHostControl
    {
        /// <summary>
        /// Envía una acción como un trabajador de fondo al servicio de UI,
        /// permitiendo reportar el estado de la acción y cambiando el estado
        /// de presentación de la página a "Ocupado".
        /// </summary>
        /// <param name="action">
        /// Acción a ejecutar.
        /// </param>
        /// <returns>
        /// Una tarea que puede utilizarse para monitorear la operación
        /// asíncrona.
        /// </returns>
        Task RunBusyAsync(Action<IProgress<ProgressInfo>> action)
        {
            return RunBusyAsync(p => Task.Run(() => action(p)));
        }

        /// <summary>
        /// Envía una función como un trabajador de fondo al servicio de UI,
        /// permitiendo reportar el estado de la función y cambiando el estado
        /// de presentación de la página a "Ocupado".
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de valor devuelto por la función.
        /// </typeparam>
        /// <param name="func">
        /// Función a ejecutar.
        /// </param>
        /// <returns>
        /// Una tarea con valor devuelto de tipo <typeparamref name="T"/> que
        /// puede utilizarse para monitorear la operación asíncrona.
        /// </returns>
        Task<T> RunBusyAsync<T>(Func<IProgress<ProgressInfo>, T> func)
        {
            return RunBusyAsync(p => Task.Run(() => func(p)));
        }

        /// <summary>
        /// Envía una función como un trabajador de fondo al servicio de UI,
        /// permitiendo reportar el estado de la función y cambiando el estado
        /// de presentación de la página a "Ocupado".
        /// </summary>
        /// <param name="task"> Tarea a ejecutar.</param>
        /// <returns>
        /// Una tarea que puede utilizarse para monitorear la operación
        /// asíncrona.
        /// </returns>
        Task RunBusyAsync(Func<IProgress<ProgressInfo>, Task> task);

        /// <summary>
        /// Envía una función como un trabajador de fondo al servicio de UI,
        /// permitiendo reportar el estado de la función y cambiando el estado
        /// de presentación de la página a "Ocupado".
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de valor devuelto por la función.
        /// </typeparam>
        /// <param name="task"> Tarea a ejecutar.</param>
        /// <returns>
        /// Una tarea con valor devuelto de tipo <typeparamref name="T"/> que
        /// puede utilizarse para monitorear la operación asíncrona.
        /// </returns>
        Task<T> RunBusyAsync<T>(Func<IProgress<ProgressInfo>, Task<T>> task);
    }
}