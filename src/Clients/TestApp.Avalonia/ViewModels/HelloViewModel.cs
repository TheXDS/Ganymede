using System;
using System.Threading;
using System.Windows.Input;
using ReactiveUI;
using TheXDS.Ganymede.ViewModels.Base;

namespace TheXDS.Ganymede.ViewModels;

public class HelloViewModel : NavigatableViewModel
{
    private string _username;
    
    public string Username
    {
        get => _username;
        set => Change(ref _username, value);
    }
    
    public ICommand LogoutCommand { get; }
    
    public ICommand GreetCommand { get; }

    public ICommand AskCommand { get; }
    
    public ICommand OperationCommand { get; }
    
    public ICommand StringInputCommand { get; }
    
    public ICommand IntegerInputCommand { get; }
    
    public ICommand DecimalInputCommand { get; }
    
    public ICommand DateTimeInputCommand { get; }
    
    public ICommand BooleanInputCommand { get; }

    public HelloViewModel()
    {
        _username = String.Empty;
        LogoutCommand = CreateHardNavigateCommand<LoginViewModel>();
        GreetCommand = ReactiveCommand.Create(OnGreet);
        AskCommand = ReactiveCommand.Create(OnAsk);
        OperationCommand = ReactiveCommand.Create(OnOperation);
        StringInputCommand = ReactiveCommand.Create(OnInput);
        IntegerInputCommand = ReactiveCommand.Create(() => OnInputRange(1000));
        DecimalInputCommand = ReactiveCommand.Create(OnInput<decimal>);
        DateTimeInputCommand = ReactiveCommand.Create(() => OnInputRange(DateTime.Now + TimeSpan.FromDays(1000)));
        BooleanInputCommand = ReactiveCommand.Create(OnInput<bool>);
    }

    private async void OnGreet()
    {
        await DialogService.Message($"Hello! You logged in as {Username}");
    }

    private async void OnAsk()
    {
        if (await DialogService.Ask("Do you love me?"))
        {
            await DialogService.Message("I love you too.");
        }
        else
        {
            await DialogService.Warning("That was not nice.");
            await DialogService.Error("It's a shame you don't love me. I hope I crash.");
        }
    }

    private async void OnOperation()
    {
        if (await DialogService.RunOperation("Testing...", (ct, p) =>
            {
                p.Report("This op will take about 10 secs.");
                for (int j = 0; j < 100; j++)
                {
                    Thread.Sleep(100);
                    if (ct.IsCancellationRequested) break;
                    p.Report(j);
                }
            }))
        {
            await DialogService.Message("Operation completed!");
        }
        else
        {
            await DialogService.Error("Operation has been cancelled.");
        }
    }

    private async void OnInput<T>() where T : struct, IComparable<T>
    {
        var result = await DialogService.GetInputValue<T>("Input Test", "Please enter a value.");
        if (result)
        {
            await DialogService.Message($"Got \"{result.Result}\" back.");
        }
        else
        {
            await DialogService.Error("User didn't input anything.");
        }
    }
    
    private async void OnInput()
    {
        var result = await DialogService.GetInputText("Input Test", "Please enter a value.");
        if (result)
        {
            await DialogService.Message($"Got \"{result.Result}\" back.");
        }
        else
        {
            await DialogService.Error("User didn't input anything.");
        }
    }
    
    private async void OnInputRange<T>(T max) where T : struct, IComparable<T>
    {
        var result = await DialogService.GetInputRange("Input Test", "Please enter a value.",default, max);
        if (result)
        {
            await DialogService.Message($"Got range \"{result.Result!.Value.Min} to {result.Result!.Value.Max}\" back.");
        }
        else
        {
            await DialogService.Error("User didn't input anything.");
        }
    }
}