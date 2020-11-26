using System;
using System.Threading.Tasks;
using TheXDS.Ganymede.Component;
using TheXDS.Ganymede.Resources;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.ViewModel;
using St = TheXDS.Ganymede.Resources.Strings;

namespace TheXDS.Ganymede.ViewModels
{
    /// <summary>
    /// ViewModel que describe una página visual.
    /// </summary>
    public class PageViewModel : NotifyPropertyChanged, IViewModel
    {
        private IUiServiceBroker? _host = null;

        /// <summary>
        /// Obtiene una referencia al proveedor de servicios de UI al cual este ViewModel tiene acceso.
        /// </summary>
        public IUiServiceBroker Host
        {
            get => _host ?? throw Errors.UiHostAccess;
            internal set => _host = value;
        }

        /// <summary>
        /// Permite establecer propiedades básicas de UI de la página, como ser el título, estado, etc.
        /// </summary>
        /// <param name="host"></param>
        /// <param name="progress">
        /// Objeto que permite reportar el progreso de la operación.
        /// </param>
        protected internal virtual void UiInit(IUiConfigurator host, IProgress<ProgressInfo> progress)
        {
            host.SetTitle(St.UntitledPage);
        }

        /// <summary>
        /// Construye un nuevo <see cref="SimpleCommand"/> que ejecutará un
        /// método de forma asíncrona.
        /// </summary>
        /// <param name="action">
        /// Acción a ejecutar. No es necesario que la misma sea asíncrona (no
        /// debe declararse utilizando <see langword="async"/>)
        /// </param>
        /// <returns>
        /// Una nueva instancia de la clase <see cref="SimpleCommand"/> que
        /// ejecutará la acción en un contexto asíncrono.
        /// </returns>
        protected SimpleCommand BuildBusyCommand(Action<IProgress<ProgressInfo>> action)
        {
            return BuildBusyCommand(() => Host.RunBusyAsync(action));
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
            return new SimpleCommand(async () => await task());
        }
    }
}