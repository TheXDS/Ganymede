using System;
using System.Threading.Tasks;
using TheXDS.Ganymede.Attributes;
using TheXDS.Ganymede.Component;
using TheXDS.Ganymede.Exceptions;
using TheXDS.Ganymede.Resources;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.ViewModel;
using St = TheXDS.Ganymede.Resources.Strings;

namespace TheXDS.Ganymede.ViewModels
{
    /// <summary>
    /// ViewModel que describe una página visual.
    /// </summary>
    public class PageViewModel : ViewModelBase
    {
        private IUiServiceBroker? _ui = null;

        /// <summary>
        /// Obtiene una referencia al proveedor de servicios de UI al cual este ViewModel tiene acceso.
        /// </summary>
        public IUiServiceBroker UiServices
        {
            get => _ui ?? throw Errors.UiHostAccess;
            internal set => _ui = value;
        }

        /// <summary>
        /// Permite establecer propiedades básicas de UI de la página, como ser
        /// el título, estado, etc.
        /// </summary>
        /// <param name="host">
        /// Configurador del host visual que alojará a la página.
        /// </param>
        /// <param name="progress">
        /// Objeto que permite reportar el progreso de la operación.
        /// </param>
        /// <remarks>Considere comprobar si <paramref name="host"/> es
        /// <see langword="null"/> cada vez que desee configurar una propiedad
        /// del host visual, ya que existe la posibilidad de que el usuario
        /// decida cerrar la página antes de completar las inicializaciones. Si
        /// no realiza las comprobaciones, podría producirse un
        /// <see cref="UiHostAccessException"/>.
        /// </remarks>
        protected internal virtual Task InitializeAsync(IUiHostControl host, IProgress<ProgressInfo> progress)
        {
            host.Title = this.GetAttr<TitleAttribute>()?.Title ?? St.UntitledPage;
            host.AccentColor = this.GetAttr<AccentColorAttribute>()?.Value;
            host.Closeable = this.GetAttr<CloseableAttribute>()?.Closeable ?? true;
            host.Modal = this.HasAttr<ModalAttribute>();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Construye un nuevo <see cref="SimpleCommand"/> que ejecutará un
        /// método de forma asíncrona.
        /// </summary>
        /// <param name="action">
        /// Acción a ejecutar. No es necesario que la misma sea asíncrona
        /// (declarada utilizando <see langword="async"/>)
        /// </param>
        /// <returns>
        /// Una nueva instancia de la clase <see cref="SimpleCommand"/> que
        /// ejecutará la acción en un contexto asíncrono.
        /// </returns>
        protected SimpleCommand BuildBusyCommand(Action<IProgress<ProgressInfo>> action)
        {
            return new SimpleCommand(() => UiServices.VisualHost.RunBusyAsync(action));
        }

        /// <summary>
        /// Construye un nuevo <see cref="SimpleCommand"/> que ejecutará un
        /// método de forma asíncrona.
        /// </summary>
        /// <param name="task">Tarea a ejecutar.</param>
        /// <returns>
        /// Una nueva instancia de la clase <see cref="SimpleCommand"/> que
        /// ejecutará la acción en un contexto asíncrono.
        /// </returns>
        protected SimpleCommand BuildBusyCommand(Func<IProgress<ProgressInfo>, Task> task)
        {
            return new SimpleCommand(() => UiServices.VisualHost.RunBusyAsync(task));
        }

        /// <summary>
        /// Construye un nuevo <see cref="SimpleCommand"/> que ejecutará un
        /// método de forma asíncrona.
        /// </summary>
        /// <param name="task">
        /// Tarea a ejecutar. Considere proveer de un mecanismo para reportar
        /// el progreso de la operación.
        /// </param>
        /// <returns>
        /// Una nueva instancia de la clase <see cref="SimpleCommand"/> que
        /// ejecutará la acción en un contexto asíncrono.
        /// </returns>
        protected SimpleCommand BuildBusyCommand(Func<Task> task)
        {
            return new SimpleCommand(() => UiServices.VisualHost.RunBusyAsync(p =>
            {
                p.Report(ProgressInfo.Unknwon);
                return task();
            }));
        }

        /// <summary>
        /// Construye un nuevo <see cref="SimpleCommand"/> que ejecutará un
        /// método de forma asíncrona.
        /// </summary>
        /// <param name="action">
        /// Acción a ejecutar. Considere proveer de un mecanismo para reportar
        /// el progreso de la operación.
        /// </param>
        /// <returns>
        /// Una nueva instancia de la clase <see cref="SimpleCommand"/> que
        /// ejecutará la acción en un contexto asíncrono.
        /// </returns>
        protected SimpleCommand BuildBusyCommand(Action action)
        {
            return new SimpleCommand(() => UiServices.VisualHost.RunBusyAsync(p =>
            {
                p.Report(ProgressInfo.Unknwon);
                action();
            }));
        }
    }
}