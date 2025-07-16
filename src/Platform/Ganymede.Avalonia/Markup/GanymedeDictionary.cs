using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;
using System.Reflection;
using TheXDS.Ganymede.Helpers;

namespace TheXDS.Ganymede.Markup;

/// <summary>
/// Implements a <see cref="Styles"/> collection that is used to initialize
/// Ganymede, as well as provide the required styles for Ganymede to work
/// properly.
/// </summary>
public sealed partial class GanymedeDictionary : Styles
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GanymedeDictionary"/> class.
    /// </summary>
    public GanymedeDictionary()
    {
        GanymedeInitializer.Initialize();
        var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
        Add(new StyleInclude((Uri?)null) { Source = new Uri($"avares://{assemblyName}/Controls/NavigationHost.axaml") });
        Add(new StyleInclude((Uri?)null) { Source = new Uri($"avares://{assemblyName}/Controls/BusyContainer.axaml") });
        Add(new StyleInclude((Uri?)null) { Source = new Uri($"avares://{assemblyName}/Controls/BusyIndicator.axaml") });
    }
}