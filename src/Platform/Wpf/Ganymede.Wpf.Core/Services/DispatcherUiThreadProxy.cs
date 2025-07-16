namespace TheXDS.Ganymede.Services;

/// <summary>
/// Implements a <see cref="IUiThreadProxy"/> that uses the application
/// dispatcher to execute methods on the UI thread.
/// </summary>
public class DispatcherUiThreadProxy : IUiThreadProxy
{
    void IUiThreadProxy.Invoke(Action action)
    {
        Application.Current.Dispatcher.Invoke(action);
    }

    T IUiThreadProxy.Invoke<T>(Func<T> func)
    {
        return Application.Current.Dispatcher.Invoke(func);
    }
}
