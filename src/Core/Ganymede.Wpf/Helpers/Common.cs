using System;
using System.Windows;

namespace TheXDS.Ganymede.Helpers;

/// <summary>
/// Includes a set of generic helpers available througout Ganymede.
/// </summary>
public static class Common
{
    /// <summary>
    /// Helper that redirects object creation to the UI thread.
    /// </summary>
    /// <typeparam name="TObj">Type of object to return.</typeparam>
    /// <param name="create">Object creation callback.</param>
    /// <returns>
    /// A new instance of <typeparamref name="TObj"/> where the owner thread is
    /// the UI thread.
    /// </returns>
    public static TObj UiInvoke<TObj>(Func<TObj> create) => Application.Current.Dispatcher.Invoke(create);

    /// <summary>
    /// Helper that redirects execution of an action to the UI thread.
    /// </summary>
    /// <param name="action">Callback to execute.</param>
    public static void UiInvoke(Action action) => Application.Current.Dispatcher.Invoke(action);
}
