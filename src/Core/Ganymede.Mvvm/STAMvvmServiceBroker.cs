using System;
using TheXDS.Ganymede.Component;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Types;

namespace TheXDS.Ganymede.Mvvm
{
    /// <summary>
    /// <see cref="MvvmServiceBroker"/> que permite ejecutar acciones que
    /// afectan a la UI en el hilo principal de la UI de la aplicación.
    /// </summary>
    public class STAMvvmServiceBroker : MvvmServiceBroker, IUiPropertyDescriptor
    {
        private readonly Action<Action> _uiInvoker;

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="STAMvvmServiceBroker"/>.
        /// </summary>
        /// <param name="guest">Página cliente del servicio de UI.</param>
        /// <param name="host">
        /// Host de la página cliente del servicio de UI.
        /// </param>
        /// <param name="uiInvoker">
        /// Delegado que permite ejecutar llamadas en el hilo de UI.
        /// </param>
        public STAMvvmServiceBroker(PageViewModel guest, HostViewModel host, Action<Action> uiInvoker) : base(guest, host)
        {
            _uiInvoker = uiInvoker;
        }

        string IUiPropertyDescriptor.Title
        { 
            get => Title;
            set => _uiInvoker(() => Title = value);
        }
        bool IUiPropertyDescriptor.Closeable 
        { 
            get => Closeable;
            set => _uiInvoker(() => Closeable = value);
        }
        Color? IUiPropertyDescriptor.AccentColor 
        { 
            get => AccentColor;
            set => _uiInvoker(() => AccentColor = value);
        }
        bool IUiPropertyDescriptor.Modal
        { 
            get => Modal;
            set => _uiInvoker(() => Modal = value);
        }

        /// <inheritdoc/>
        protected override void ReportProgress(ProgressInfo progress)
        {
            _uiInvoker(() => base.ReportProgress(progress));
        }
    }
}
