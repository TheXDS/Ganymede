using System.Runtime.CompilerServices;
using ReactiveUI;

namespace TheXDS.Ganymede.ViewModels.Base;

/// <summary>
/// Base class for all Ganymede ViewModels. Implements NPC forwarding support,
/// and a short-named, slightly more convenient method for changing a field in
/// NPC properties compared to the one provided by ReactiveUI
/// (<see cref="IReactiveObjectExtensions.RaiseAndSetIfChanged{TObj, TRet}"/>).
/// </summary>
/// <remarks>
/// Implementation loosely based on a subset of TheXDS's MCART ViewModelBase
/// implementation as found at:
/// https://github.com/TheXDS/MCART/blob/master/src/Lib/MCART.Mvvm/Types/Base/ViewModelBase.cs
/// </remarks>
public abstract class ViewModelBase : ReactiveObject
{
    private static IDictionary<string, ICollection<string>> _npcBroadcast = new Dictionary<string, ICollection<string>>();

    /// <summary>
    /// Registers the notification of additional properties when a notification
    /// for the specified property name is fired.
    /// </summary>
    /// <param name="sourcepropertyName">Property name to watch for.</param>
    /// <param name="targetProperties">
    /// Additional properties to fire NPC events for.
    /// </param>
    protected static void RegisterNpcBroadcast(string sourcepropertyName, params string[] targetProperties)
    {
        RegisterNpcBroadcast(sourcepropertyName, targetProperties.AsEnumerable());
    }
    
    /// <summary>
    /// Registers the notification of additional properties when a notification
    /// for the specified property name is fired.
    /// </summary>
    /// <param name="sourcepropertyName">Property name to watch for.</param>
    /// <param name="targetProperties">
    /// Additional properties to fire NPC events for.
    /// </param>
    protected static void RegisterNpcBroadcast(string sourcepropertyName, IEnumerable<string> targetProperties)
    {
        if (_npcBroadcast.TryGetValue(sourcepropertyName, out var c))
        {
            foreach (var j in targetProperties) c.Add(j);
        }
        else _npcBroadcast.Add(sourcepropertyName, new List<string>(targetProperties));
    }
    
    /// <summary>
    /// Changes the value of a NPC property backing field, firing the
    /// corresponding property change notification events.
    /// </summary>
    /// <param name="field">Backing field of the property.</param>
    /// <param name="value">New value to assign the backing field to.</param>
    /// <param name="propertyName">
    /// Normally omitted. Name of the property that is changing its value.
    /// </param>
    /// <typeparam name="T">Property/backing field/value type.</typeparam>
    /// <returns>
    /// <see langword="true"/> if the property has changed value,
    /// <see langword="false"/> otherwise.
    /// </returns>
    protected bool Change<T>(ref T field, T value, [CallerMemberName] string propertyName = null!)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        string[] npc = _npcBroadcast.TryGetValue(propertyName, out var c) ? c.ToArray() : Array.Empty<string>();
        
        this.RaisePropertyChanging(propertyName);
        foreach (var j in npc) this.RaisePropertyChanging(j);
        field = value;
        this.RaisePropertyChanged(propertyName);
        foreach (var j in npc) this.RaisePropertyChanged(j);
        return true;
    }
}