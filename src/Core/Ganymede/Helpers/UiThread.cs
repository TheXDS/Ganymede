using TheXDS.Ganymede.Services;

namespace TheXDS.Ganymede.Helpers;

/// <summary>
/// Helper class that allows a program to specify the UI thread to be used when
/// executing thread-sensitive operations, like instancing and/or accessing UI
/// elements.
/// </summary>
public static class UiThread
{
    private static IUiThreadProxy? _instance;

    /// <summary>
    /// Sets the <see cref="IUiThreadProxy"/> to be used when attempting to run
    /// a method on the UI thread.
    /// </summary>
    /// <param name="instance">
    /// <see cref="IUiThreadProxy"/> to be used. If set to
    /// <see langword="null"/>, this static class will not queue execution of
    /// the methods on the UI thread, and will instead run them in the current
    /// thread.
    /// </param>
    public static void SetProxy(IUiThreadProxy? instance)
    {
        _instance = instance;
    }

    /// <summary>
    /// Invokes an action that does not return a value.
    /// </summary>
    /// <param name="action">Action to execute.</param>
    public static void Invoke(Action action)
    {
        if (_instance != null)
        {
            _instance.Invoke(action);
        }
        else
        {
            action();
        }
    }

    /// <summary>
    /// Invokes a function that returns a value.
    /// </summary>
    /// <typeparam name="T">Type of value to be returned.</typeparam>
    /// <param name="func">Function to execute.</param>
    /// <returns>The result of the function.</returns>
    public static T Invoke<T>(Func<T> func)
    {
        return _instance is not null ? _instance.Invoke(func) : func();
    }
}
