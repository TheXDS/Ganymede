using System;
using TheXDS.Ganymede.Component;
using TheXDS.Ganymede.Resources;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.ViewModel;
using St = TheXDS.Ganymede.Resources.Strings;

namespace TheXDS.Ganymede.ViewModels
{
    public struct ProgressInfo
    {
        public int? Progress { get; }
        public string? Status { get; }

        public ProgressInfo(int progress, string status)
        {
            Progress = progress;
            Status = status;
        }

        public ProgressInfo(int progress)
        {
            Progress = progress;
            Status = null;
        }

        public ProgressInfo(string status)
        {
            Progress = null;
            Status = status;

        }

        public static ProgressInfo Indeterminate => new ProgressInfo();
    }

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
    }
}