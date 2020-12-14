using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TheXDS.Ganymede.Component;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Math;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;
using TheXDS.MCART.UI;
using TheXDS.MCART.ViewModel;

namespace TheXDS.Ganymede.Mvvm
{
    /// <summary>
    /// Procura servicios de UI para instancias de <see cref="PageViewModel"/>,
    /// implementando algunos de los servicios por medio del paradigma MVVM.
    /// </summary>
    public class MvvmServiceBroker : NotifyPropertyChanged, IUiServiceBroker, IUiHostService, IUiSiblingControl, IUiDialogService
    {
        private int? _Progress;
        private string? _DialogIcon;
        private string? _Message;
        private string? _title;
        private bool _closeable = true;
        private Color? _accentColor;
        private IEnumerable<Launcher>? _actions;
        private MvvmContent _Content;
        private bool _modal;

        /// <summary>
        /// Obtiene la instancia cliente del servicio de UI.
        /// </summary>
        public PageViewModel Guest { get; }

        /// <summary>
        /// Obtiene el Host de la página cliente de servicios.
        /// </summary>
        public HostViewModel HostVm { get; }

        /// <summary>
        /// Obtiene el comando a ejecutar para cerrar esta página.
        /// </summary>
        public ICommand CloseCommand { get; }

        /// <summary>
        /// Obtiene o establece el valor Content.
        /// </summary>
        /// <value>El valor de Content.</value>
        public MvvmContent ContentSelection
        {
            get => _Content;
            set => Change(ref _Content, value);
        }

        /// <summary>
        /// Obtiene o establece el valor Progress.
        /// </summary>
        /// <value>El valor de Progress.</value>
        public double Progress
        {
            get => _Progress ?? double.NaN;
            set
            {
                Change(ref _Progress, value.IsValid() ? (int?)value : null);
            }
        }

        /// <summary>
        /// Obtiene o establece el valor DialogIcon.
        /// </summary>
        /// <value>El valor de DialogIcon.</value>
        public string? DialogIcon
        {
            get => _DialogIcon;
            set => Change(ref _DialogIcon, value);
        }

        /// <summary>
        /// Obtiene o establece el valor Message.
        /// </summary>
        /// <value>El valor de Message.</value>
        public string? MessageText
        {
            get => _Message;
            set => Change(ref _Message, value);
        }

        /// <summary>
        /// Obtiene o establece el valor Actions.
        /// </summary>
        /// <value>El valor de MyProperty.</value>
        public IEnumerable<Launcher> Actions
        {
            get => _actions ?? Array.Empty<Launcher>();
            private set => Change(ref _actions, value);
        }

        /// <summary>
        /// Obtiene o establece el valor IsBusy.
        /// </summary>
        /// <value>El valor de IsBusy.</value>
        public bool IsBusy => ContentSelection == MvvmContent.Progress;

        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="MvvmServiceBroker"/>.
        /// </summary>
        /// <param name="guest">Página cliente del servicio de UI.</param>
        /// <param name="host">
        /// Host de la página cliente del servicio de UI.
        /// </param>
        public MvvmServiceBroker(PageViewModel guest, HostViewModel host)
        {
            RegisterPropertyChangeBroadcast(nameof(ContentSelection), nameof(IsBusy));
            Guest = guest;
            HostVm = host;
            CloseCommand = new ObservingCommand(this, ((IUiServiceBroker)this).VisualHost.Close)
                .ListensToCanExecute(() => ((IUiServiceBroker)this).Properties.Closeable);
        }

        /// <summary>
        /// Permite establecer el valor de reporte de progreso en esta
        /// instancia.
        /// </summary>
        /// <param name="progress">
        /// Progreso a reportar.
        /// </param>
        protected virtual void ReportProgress(ProgressInfo progress)
        {
            Progress = progress.Progress ?? double.NaN;
            MessageText = progress.Status;
        }

        /// <summary>
        /// Establece el títlo de la página.
        /// </summary>
        /// <param name="value">
        /// Título a establecer en la página.
        /// </param>
        protected void SetTitle(string value) => ((IUiPropertyDescriptor)this).Title = value;

        /// <summary>
        /// Establece un valor que indica si la página puede ser cerrada.
        /// </summary>
        /// <param name="value">
        /// Valor a establecer que define el la posibilidad de cerrar la
        /// página.
        /// </param>
        protected void SetCloseable(bool value) => ((IUiPropertyDescriptor)this).Closeable = value;

        /// <summary>
        /// Establece el color a utilizar para decorar la página.
        /// </summary>
        /// <param name="value">
        /// Color a utilizar para decorar la página.
        /// </param>
        protected void SetAccentColor(Color? value) => ((IUiPropertyDescriptor)this).AccentColor = value;
        protected void SetModal(bool value) => ((IUiPropertyDescriptor)this).Modal = value;

        /// <inheritdoc/>
        public Color? AccentColor
        {
            get => _accentColor;
            set => Change(ref _accentColor, value);
        }

        /// <inheritdoc/>
        public bool Closeable
        { 
            get => _closeable;
            set => Change(ref _closeable, value);
        }

        /// <inheritdoc/>
        public string Title
        { 
            get => _title ?? string.Empty;
            set => Change(ref _title, value);
        }

        /// <inheritdoc/>
        public bool Modal
        {
            get => _modal;
            set => Change(ref _modal, value);
        }

        IUiDialogService IUiServiceBroker.Dialogs => this;

        IUiHostService IUiServiceBroker.VisualHost => this;

        IUiPropertyDescriptor IUiServiceBroker.Properties => this;

        IUiSiblingControl IUiServiceBroker.Siblings => this;

        void IUiHostControl.Close() => ((IUiSiblingControl)this).Close(Guest);
        bool IUiSiblingControl.Close(PageViewModel page)
        {
            var retval = HostVm.Pages.Contains(page);
            HostVm.ClosePage(page);
            return retval;
        }
        async Task<int> IUiDialogService.Message(string message, params string[] options)
        {
            var result = new TaskCompletionSource<int>();
            var actions = new List<Launcher>();

            MessageText = message;
            ContentSelection = MvvmContent.Message;

            for (var j = 0; j < options.Length; j++)
            {
                int jj = j;
                actions.Add(new Launcher(options[j], () => result.SetResult(jj)));
            }
            Actions = actions;
            var r = await result.Task;
            ContentSelection = MvvmContent.Default;
            return r;
        }
        async Task<bool> IUiSiblingControl.OpenAsync(PageViewModel page)
        {
            try
            {
                await HostVm.AddPage(page);
                return true;
            }
            catch
            {
                return false;
            }
        }
        async Task IUiHostService.RunBusyAsync(Func<IProgress<ProgressInfo>, Task> task)
        {
            ContentSelection = MvvmContent.Progress;
            var progress = new Progress<ProgressInfo>(ReportProgress);
            await task(progress);
            ContentSelection = MvvmContent.Default;
        }
        async Task<T> IUiHostService.RunBusyAsync<T>(Func<IProgress<ProgressInfo>, Task<T>> task)
        {
            ContentSelection = MvvmContent.Progress;
            var progress = new Progress<ProgressInfo>(ReportProgress);
            var r = await task(progress);
            ContentSelection = MvvmContent.Default;
            return r;
        }

        void IUiHostControl.Activate()
        {
            throw new NotImplementedException();
        }

        void IUiHostControl.Deactivate()
        {
            throw new NotImplementedException();
        }
    }
}
