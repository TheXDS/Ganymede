using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using TheXDS.Ganymede.Component;
using TheXDS.Ganymede.ViewModels;
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
    public class MvvmServiceBroker : NotifyPropertyChanged, IUiServiceBroker
    {
        private bool _IsBusy;
        private int? _Progress;
        private string? _DialogIcon;
        private string? _Message;
        private string? _title;
        private bool _closeable = true;
        private Color? _accentColor;
        private IEnumerable<Launcher>? _actions;

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
            get => _IsBusy;
            private set => Change(ref _IsBusy, value);
        }

        /// <summary>
        ///     Obtiene o establece el valor Progress.
        /// </summary>
        /// <value>El valor de Progress.</value>
        public int? Progress
        {
            get => _Progress;
            set => Change(ref _Progress, value);
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
            set => Change(ref _actions, value);
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
        public int Message(string message, params string[] options)
        {
            MessageText = message;
            return 0;
        }

        /// <inheritdoc/>
        public async Task RunBusyAsync(Action<IProgress<int?>> action)
        {
            IsBusy = true;
            var progress = new Progress<int?>(ReportProgress);
            await Task.Run(() => action(progress));
            IsBusy = false;
        }

        /// <inheritdoc/>
        public async Task<T> RunBusyAsync<T>(Func<IProgress<int?>, T> function)
        {
            IsBusy = true;
            var progress = new Progress<int?>(ReportProgress);
            var r = await Task.Run(() => function(progress));
            IsBusy = false;
            return r;
        }

        private void ReportProgress(int? value)
        {
            Progress = value;
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
