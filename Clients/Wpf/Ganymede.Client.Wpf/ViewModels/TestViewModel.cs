using System.Net.PeerToPeer;
using System.Threading.Tasks;
using System.Windows.Input;
using TheXDS.MCART.ViewModel;

namespace TheXDS.Ganymede.ViewModels
{
    /// <summary>
    /// ViewModel de prueba de generación de UI.
    /// </summary>
    public class TestViewModel : PageViewModel
    {
        private static int _count;
        private string _name = "usuario";
        private int _numberOne;
        private int _numberTwo;
        private int _result;

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="TestViewModel"/>.
        /// </summary>
        public TestViewModel()
        {
            _count++;
            Title = $"Prueba # {_count}";
            AccentColor = MCART.Resources.Colors.Pick();
            SumCommand = new SimpleCommand(OnSum);
            OkTkxByeCommand = new SimpleCommand(Close);
            BusyOpCommand = new SimpleCommand(OnBusyOp);
        }

        /// <summary>
        /// Expone de manera pública el valor
        /// <see cref="PageViewModel.Closeable"/>.
        /// </summary>
        /// <value>El valor de <see cref="PageViewModel.Closeable"/>.</value>
        public bool CloseableToggle
        {
            get => Closeable;
            set => Closeable = value;
        }

        /// <summary>
        /// Obtiene o establece el valor Name.
        /// </summary>
        /// <value>El valor de Name.</value>
        public string Name
        {
            get => _name;
            set => Change(ref _name, value);
        }

        /// <summary>
        /// Obtiene o establece el valor NumberOne.
        /// </summary>
        /// <value>El valor de NumberOne.</value>
        public int NumberOne
        {
            get => _numberOne;
            set => Change(ref _numberOne, value);
        }

        /// <summary>
        /// Obtiene o establece el valor NumberTwo.
        /// </summary>
        /// <value>El valor de NumberTwo.</value>
        public int NumberTwo
        {
            get => _numberTwo;
            set => Change(ref _numberTwo, value);
        }

        /// <summary>
        /// Obtiene o establece el valor Result.
        /// </summary>
        /// <value>El valor de Result.</value>
        public int Result
        {
            get => _result;
            private set => Change(ref _result, value);
        }

        /// <summary>
        ///     Obtiene el comando relacionado a la acción BusyOp.
        /// </summary>
        /// <returns>El comando BusyOp.</returns>
        public ICommand BusyOpCommand { get; }

        /// <summary>
        /// Obtiene el comando relacionado a la acción Sum.
        /// </summary>
        /// <returns>El comando Sum.</returns>
        public ICommand SumCommand { get; }

        /// <summary>
        /// Okay, Thanks, Bye.
        /// </summary>
        public ICommand OkTkxByeCommand { get; }

        private void OnSum()
        {
            Result = NumberOne + NumberTwo;
        }


        private async void OnBusyOp()
        {
            IsBusy = true;
            await Task.Delay(5000);
            IsBusy = false;
        }
    }
}