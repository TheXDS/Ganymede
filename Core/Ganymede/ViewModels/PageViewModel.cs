using System.Windows.Input;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;
using TheXDS.MCART.ViewModel;
using static TheXDS.MCART.Types.Extensions.ObservingCommandExtensions;
using St = TheXDS.Ganymede.Resources.Strings;

namespace TheXDS.Ganymede.ViewModels
{
    /// <summary>
    /// ViewModel que describe una página visual.
    /// </summary>
    public class PageViewModel : ViewModelBase
    {
        private string? _title;
        private bool _closeable = true;
        private Color? _accentColor;

        internal HostViewModel? Host { get; set; }

        /// <summary>
        /// Obtiene o establece el título de la página.
        /// </summary>
        /// <value>El título de la página.</value>
        public string Title
        {
            get => _title ?? St.UntitledPage;
            protected set => Change(ref _title, value);
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
            protected set => Change(ref _closeable, value);
        }

        /// <summary>
        /// Obtiene o establece un color decorativo a utilizar para la página.
        /// </summary>
        /// <value>El color decorativo a utilizar.</value>
        public Color? AccentColor
        {
            get => _accentColor;
            protected set => Change(ref _accentColor, value);
        }

        /// <summary>
        /// Obtiene el comando a ejecutar para cerrar esta página.
        /// </summary>
        public ICommand CloseCommand { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PageViewModel"/>.
        /// </summary>
        public PageViewModel()
        {
            CloseCommand = new ObservingCommand(this, Close).ListensToCanExecute(() => Closeable);
        }

        /// <summary>
        /// Método que se ejecuta antes de cerrar una página.
        /// </summary>
        /// <param name="cancel">
        /// Parámetro que, cuando se establece en <see langword="true"/>, 
        /// permite cancelar la operación de cerrado de la página.
        /// </param>
        protected virtual void OnClosing(ref bool cancel) { }

        /// <summary>
        /// Método que se ejecuta luego de cerrar satisfactoriamente la
        /// página.
        /// </summary>
        protected virtual void OnClosed() { }

        /// <summary>
        /// Solicita el cierre de esta página.
        /// </summary>
        protected void Close()
        {
            var cancel = false;
            OnClosing(ref cancel);
            if (cancel) return;
            Host?.ClosePage(this);
            OnClosed();
        }
    }
}