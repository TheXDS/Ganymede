using System;
using System.Threading;
using System.Windows.Input;
using TheXDS.Ganymede.Component;
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
        private readonly int _pgnum;

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="TestViewModel"/>.
        /// </summary>
        public TestViewModel()
        {
            _pgnum = ++_count;
            SumCommand = new SimpleCommand(OnSum);
            OkTkxByeCommand = new SimpleCommand(() => Host.Close(this));
            BusyOpCommand = new SimpleCommand(async () => await Host.RunBusyAsync(OnBusyOp));
            SaluteCommand = new SimpleCommand(OnSalute);
            SpawnSiblingCommand = new SimpleCommand(OnSpawnSibling);
        }

        /// <inheritdoc/>
        protected override void UiInit(IUiConfigurator host, IProgress<ProgressInfo> progress)
        {
            host.SetTitle($"Prueba # {_pgnum}");            
            Thread.Sleep(3000);
            host.SetAccentColor(MCART.Resources.Colors.Pick());
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

        /// <summary>
        /// Comando que muestra un saludo al usuario.
        /// </summary>
        public ICommand SaluteCommand { get; }

        /// <summary>
        /// Comando que abrirá una página hermana.
        /// </summary>
        public ICommand SpawnSiblingCommand { get; }

        private void OnSum()
        {
            Result = NumberOne + NumberTwo;
        }

        private void OnBusyOp(IProgress<ProgressInfo> progress)
        {
            progress.Report(new ProgressInfo("Esperando 3 segundos..."));
            Thread.Sleep(3000);
            for (var j = 0; j <=100; j++)
            {
                Thread.Sleep(50);
                progress.Report(new ProgressInfo(j, $"Paso {(j / 20) + 1} de 5"));
            }
        }

        private void OnSpawnSibling()
        {
            Host.OpenAsync<TestViewModel>();
        }

        private async void OnSalute()
        {
            switch (await Host.AskYnc("Saludar?"))
            {
                case true:
                    await Host.Message($"Hello {Name}!");
                    break;
                case false:
                    await Host.Message($"Goodbye {Name}");
                    break;                
            }
        }
    }
}