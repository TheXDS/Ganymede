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
    public enum MvvmContent
    {
        Default,
        Message,
        Progress
    }

    /// <summary>
    /// Procura servicios de UI para instancias de <see cref="PageViewModel"/>,
    /// implementando algunos de los servicios por medio del paradigma MVVM.
    /// </summary>
    public class MvvmServiceBroker : NotifyPropertyChanged, IUiServiceBroker
    {
        private int? _Progress;
        private string? _DialogIcon;
        private string? _Message;
        private string? _title;
        private bool _closeable = true;
        private Color? _accentColor;
        private IEnumerable<Launcher>? _actions;
        private MvvmContent _Content;


        /// <summary>
        /// Obtiene la instancia cliente del servicio de UI.
        /// </summary>
        public PageViewModel Guest { get; }

        /// <summary>
        /// Obtiene el Host de la página cliente de servicios.
        /// </summary>
        public HostViewModel Host { get; }

        /// <summary>
        /// Obtiene el comando a ejecutar para cerrar esta página.
        /// </summary>
        public ICommand CloseCommand { get; }

        /// <summary>
        /// Obtiene o establece el título de la página.
        /// </summary>
        /// <value>El título de la página.</value>
        public string Title
        {
            get => _title ?? string.Empty;
            private set => Change(ref _title, value);
        }

        /// <summary>
        /// Obtiene o establece un valor que indica si esta página puede ser
        /// cerrada.
        /// </summary>
        /// <value>
        /// <see langword="true"/> para indicar que la página puede ser
        /// cerrada, <see langword="false"/> en caso contrario.
        /// </value>
        public bool Closeable
        {
            get => _closeable;
            private set => Change(ref _closeable, value);
        }

        /// <summary>
        /// Obtiene o establece un color decorativo a utilizar para la página.
        /// </summary>
        /// <value>El color decorativo a utilizar.</value>
        public Color? AccentColor
        {
            get => _accentColor;
            private set => Change(ref _accentColor, value);
        }

        /// <summary>
        ///     Obtiene o establece el valor IsBusy.
        /// </summary>
        /// <value>El valor de IsBusy.</value>
        public bool IsBusy
        {
            get => ContentSelection == MvvmContent.Progress;
        }

        /// <summary>
        ///     Obtiene o establece el valor Content.
        /// </summary>
        /// <value>El valor de Content.</value>
        public MvvmContent ContentSelection
        {
            get => _Content;
            set => Change(ref _Content, value);
        }

        /// <summary>
        ///     Obtiene o establece el valor Progress.
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
        ///     Obtiene o establece el valor DialogIcon.
        /// </summary>
        /// <value>El valor de DialogIcon.</value>
        public string? DialogIcon
        {
            get => _DialogIcon;
            set => Change(ref _DialogIcon, value);
        }

        /// <summary>
        ///     Obtiene o establece el valor Message.
        /// </summary>
        /// <value>El valor de Message.</value>
        public string? MessageText
        {
            get => _Message;
            set => Change(ref _Message, value);
        }

        /// <summary>
        ///     Obtiene o establece el valor MyProperty.
        /// </summary>
        /// <value>El valor de MyProperty.</value>
        public IEnumerable<Launcher> Actions
        {
            get => _actions ?? Array.Empty<Launcher>();
            private set => Change(ref _actions, value);
        }

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
            Host = host;
            CloseCommand = new ObservingCommand(this, () => Host.ClosePage(Guest))
                .ListensToCanExecute(() => Closeable);
        }

        /// <inheritdoc/>
        public bool Close(PageViewModel page)
        {
            var retval = Host.Pages.Contains(page);
            Host.ClosePage(page);
            return retval;
        }

        /// <inheritdoc/>
        public async Task<bool> OpenAsync(PageViewModel page)
        {
            try
            {
                await Host.AddPage(page);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public async Task<int> Message(string message, params string[] options)
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


        /// <inheritdoc/>
        public async Task RunBusyAsync(Action<IProgress<ProgressInfo>> action)
        {
            ContentSelection = MvvmContent.Progress;            
            var progress = new Progress<ProgressInfo>(ReportProgress);
            await Task.Run(() => action(progress));
            ContentSelection = MvvmContent.Default;
        }

        /// <inheritdoc/>
        public async Task<T> RunBusyAsync<T>(Func<IProgress<ProgressInfo>, T> function)
        {
            ContentSelection = MvvmContent.Progress;
            var progress = new Progress<ProgressInfo>(ReportProgress);
            var r = await Task.Run(() => function(progress));
            ContentSelection = MvvmContent.Default;
            return r;
        }

        private void ReportProgress(ProgressInfo progress)
        {
            Progress = progress.Progress ?? double.NaN;
        }

        /// <inheritdoc/>
        public void SetTitle(string value)
        {
            Title = value;
        }

        /// <inheritdoc/>
        public void SetCloseable(bool value)
        {
            Closeable = value;
        }

        /// <inheritdoc/>
        public void SetAccentColor(Color? value)
        {
            AccentColor = value;
        }
    }

    public class MvvmServiceBrokerFactory : IUiServiceBrokerFactory
    {
        public IUiServiceBroker Create(PageViewModel page, HostViewModel host)
        {
            return new MvvmServiceBroker(page, host);
        }
    }
}
