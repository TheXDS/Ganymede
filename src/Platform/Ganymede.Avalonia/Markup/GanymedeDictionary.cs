using Avalonia.Controls;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;
using System.Reflection;
using TheXDS.Ganymede.Controls;
using TheXDS.Ganymede.Helpers;

namespace TheXDS.Ganymede.Markup;

/// <summary>
/// Implements a <see cref="Styles"/> collection that is used to initialize
/// Ganymede, as well as provide the required styles for Ganymede to work
/// properly.
/// </summary>
public sealed partial class GanymedeDictionary : Styles
{
    private static readonly string? _assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

    /// <summary>
    /// Initializes a new instance of the <see cref="GanymedeDictionary"/> class.
    /// </summary>
    public GanymedeDictionary()
    {
        GanymedeInitializer.Initialize();
        Add(GetStyle<NavigationHost>());
        Add(GetStyle<BusyContainer>());
        Add(GetStyle<BusyIndicator>());
    }

    private static StyleInclude GetStyle<TControl>() where TControl : Control
    {
        return GetStyle(typeof(TControl).Name);
    }

    private static StyleInclude GetStyle(string controlName)
    {
        return new StyleInclude((Uri?)null) { Source = new Uri($"avares://{_assemblyName}/Controls/{controlName}.axaml") };
    }
}
