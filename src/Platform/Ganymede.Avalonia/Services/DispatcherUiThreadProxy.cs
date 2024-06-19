using Avalonia.Threading;

namespace TheXDS.Ganymede.Services;

/// <summary>
/// Implements a <see cref="IUiThreadProxy"/> that invokes methods on the UI
/// thread by accesing it via the <see cref="Dispatcher.UIThread"/> property.
/// </summary>
public class DispatcherUiThreadProxy : IUiThreadProxy
{
    void IUiThreadProxy.Invoke(Action action)
    {
        Dispatcher.UIThread.Invoke(action);
    }

    T IUiThreadProxy.Invoke<T>(Func<T> func)
    {
        return Dispatcher.UIThread.Invoke(func);
    }
}