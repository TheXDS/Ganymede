﻿using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using TheXDS.Ganymede.Component;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.ViewModel;

namespace TheXDS.Proteus.Slim.ViewModels
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
            BusyOpCommand = BuildBusyCommand(OnBusyOp);
            SaluteCommand = new SimpleCommand(OnSalute);
            SpawnSiblingCommand = new SimpleCommand(OnSpawnSibling);
        }

        /// <inheritdoc/>
        protected override async Task InitializeAsync(IUiHostControl host, IProgress<ProgressInfo> progress)
        {
            OkTkxByeCommand = new SimpleCommand(UiServices.VisualHost.Close);
            host.Closeable = false;
            host.Title = $"Cargando...";

            await Task.Delay(3000);

            host.Title = $"Prueba # {_pgnum}";
            host.AccentColor = MCART.Resources.Colors.Pick();
            host.Closeable = true;
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
        public ICommand OkTkxByeCommand { get; private set; }

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

        private async void OnSpawnSibling()
        {
            await UiServices.Siblings.OpenAsync<TestViewModel>();
        }

        private async void OnSalute()
        {
            switch (await UiServices.Dialogs.AskYnc("Saludar?"))
            {
                case true:
                    await UiServices.Dialogs.Message($"Hello {Name}!");
                    break;
                case false:
                    await UiServices.Dialogs.Message($"Goodbye {Name}");
                    break;                
            }
        }
    }
}