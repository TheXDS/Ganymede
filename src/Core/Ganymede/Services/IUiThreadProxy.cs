namespace TheXDS.Ganymede.Services;

/// <summary>
/// Defines a set of members to be implemented by a type that provides of
/// methods to invoke operations on the UI thread.
/// </summary>
public interface IUiThreadProxy
{
    /// <summary>
    /// Invokes a method that does not return a value.
    /// </summary>
    /// <param name="action">Method to execute.</param>
    void Invoke(Action action);

    /// <summary>
    /// Invokes a method that returns a value.
    /// </summary>
    /// <typeparam name="T">Type of value to be returned.</typeparam>
    /// <param name="func">Method to execute.</param>
    /// <returns>The result of the method call.</returns>
    T Invoke<T>(Func<T> func);
}
