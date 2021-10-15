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
        private IEnumerable<NamedObject<Enum>>? _InputEnums;
        private Type? _dataType;
        private object? _inputValue;
        private ICommand? _AcceptInputCommand;
        private ICommand? _CancelInputCommand;

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


        private string? _MessageTitle;

        /// <summary>
        /// Obtiene o establece el valor MessageTitle.
        /// </summary>
        /// <value>El valor de MessageTitle.</value>
        public string? MessageTitle
        {
            get => _MessageTitle;
            set => Change(ref _MessageTitle, value);
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
        public IEnumerable<Launcher>? Actions
        {
            get => _actions ?? Array.Empty<Launcher>();
            private set => Change(ref _actions, value);
        }

        /// <summary>
        /// Obtiene un comando que acepta la entrada en el modo de diálogo de
        /// entrada.
        /// </summary>
        public ICommand? AcceptInputCommand
        {
            get => _AcceptInputCommand;
            set => Change(ref _AcceptInputCommand, value);
        }

        /// <summary>
        /// Obtiene un comando que cancela la entrada en el modo de diálogo de
        /// entrada.
        /// </summary>
        public ICommand? CancelInputCommand
        {
            get => _CancelInputCommand;
            set => Change(ref _CancelInputCommand, value);
        }

        /// <summary>
        /// Enumeración auxiliar que permite obtener una colección de valores
        /// de enumeración disponibles para seleccionar.
        /// </summary>
        public IEnumerable<NamedObject<Enum>>? InputEnums
        {
            get => _InputEnums;
            set => Change(ref _InputEnums, value);
        }

        /// <summary>
        /// Obtiene o establece el valor DataType.
        /// </summary>
        /// <value>El valor de DataType.</value>
        public Type? DataType
        {
            get => _dataType;
            set => Change(ref _dataType, value);
        }

        /// <summary>
        /// Obtiene o establece el valor InputValue.
        /// </summary>
        /// <value>El valor de InputValue.</value>
        public object? InputValue
        {
            get => _inputValue;
            set => Change(ref _inputValue, value);
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
                .ListensToCanExecute(() => ((IUiServiceBroker)this).VisualHost.Closeable);
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

        /// <summary>
        /// Establece un valor que marca la página activa como modal.
        /// </summary>
        /// <param name="value">
        /// <see langword="true"/> para indicar que la página será presentada
        /// de forma modal, <see langword="false"/> en caso contrario.
        /// </param>
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

        IUiSiblingControl IUiServiceBroker.Siblings => this;

        void IUiHostControl.Close() => ((IUiSiblingControl)this).Close(Guest);
        bool IUiSiblingControl.Close(PageViewModel page)
        {
            bool retval = HostVm.Pages.Contains(page);
            HostVm.ClosePage(page);
            return retval;
        }

        async Task<int> IUiDialogService.Message(MessageDialogTemplate dialogTemplate)
        {
            TaskCompletionSource<int>? result = new();
            List<Launcher>? actions = new();

            MessageTitle = dialogTemplate.Title;
            MessageText = dialogTemplate.Message;
            ContentSelection = MvvmContent.Message;
            DialogIcon = dialogTemplate.Type switch
            {
                MessageDialogType.Information => "ℹ",
                MessageDialogType.Warning => "⚠",
                MessageDialogType.Error => "❌",
                MessageDialogType.Critical => "🔥",
                MessageDialogType.Question => "❓",
                MessageDialogType.Input => "✏",
                _ => string.Empty
            };

            for (int j = 0; j < dialogTemplate.Options.Length; j++)
            {
                int jj = j;
                actions.Add(new Launcher(dialogTemplate.Options[j], () => result.TrySetResult(jj)));
            }
            Actions = actions;
            int r = await result.Task;
            Actions = null;
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

        /// <inheritdoc/>
        public async Task<T?> Get<T>(string prompt, T? @default) where T : notnull
        {
            TaskCompletionSource<T?>? result = new();
            MessageText = prompt;
            DataType = typeof(T);
            InputEnums = typeof(T).IsEnum ? typeof(T).ToNamedEnum() : null;
            InputValue = @default;
            ContentSelection = MvvmContent.Entry;
            AcceptInputCommand = new SimpleCommand(() => result.TrySetResult((T?)InputValue));
            CancelInputCommand = new SimpleCommand(() => result.TrySetResult(@default));
            T? r = await result.Task;
            AcceptInputCommand = null;
            CancelInputCommand = null;
            ContentSelection = MvvmContent.Default;
            return r;
        }

        async Task IUiHostService.RunBusyAsync(Func<IProgress<ProgressInfo>, Task> task)
        {
            ContentSelection = MvvmContent.Progress;
            Progress<ProgressInfo>? progress = new(ReportProgress);
            await task(progress);
            ContentSelection = MvvmContent.Default;
        }

        async Task<T> IUiHostService.RunBusyAsync<T>(Func<IProgress<ProgressInfo>, Task<T>> task)
        {
            ContentSelection = MvvmContent.Progress;
            Progress<ProgressInfo>? progress = new(ReportProgress);
            T? r = await task(progress);
            ContentSelection = MvvmContent.Default;
            return r;
        }

        void IUiHostControl.Activate()
        {
            HostVm.ActivePage = Guest;
        }

        void IUiHostControl.Deactivate()
        {
            throw new NotImplementedException();
        }
    }
}
