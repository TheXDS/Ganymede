namespace TheXDS.Ganymede.ViewModels.Dialogs;

/// <summary>
/// Implements a <see cref="DialogViewModelBase"/> for dialogs that lets a user
/// input data.
/// </summary>
public class RangeInputDialogViewModel<T> : InputDialogViewModelBase<T> where T : struct, IComparable<T>
{
    private T _min;
    private T _max;
    

    public T MinimumValue
    {
        get => _min;
        set => Change(ref _min, value);
    }
    
    public T MaximumValue
    {
        get => _max;
        set => Change(ref _max, value);
    }
}