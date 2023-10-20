using TheXDS.MCART.Component;

namespace TheXDS.Ganymede.ViewModels;

public class CustomTestDialogViewModel : AwaitableDialogViewModel
{
    private int _RingValue;
    private int _TimesRan;

    public int RingValue
    {
        get => _RingValue;
        set => Change(ref _RingValue, value);
    }

    public int TimesRan
    {
        get => _TimesRan;
        set => Change(ref _TimesRan, value);
    }

    public CustomTestDialogViewModel()
    {
        Icon = "⭕";
        IconBgColor = System.Drawing.Color.Magenta;
        Title = "Custom dialog";
        Message = "This is a custom dialog.";

        Interactions.Add(new(new SimpleCommand(OnRunRing), "Run"));
        Interactions.Add(new(new SimpleCommand(CloseDialog), "Close"));
    }

    private async Task OnRunRing()
    {
        IsBusy = true;
        for (; RingValue < 100; RingValue++) { await Task.Delay(50); }
        RingValue = 0;
        TimesRan++;
        IsBusy = false;
    }
}
