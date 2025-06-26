using System.Reflection;
using System.Windows.Markup;
using TheXDS.Ganymede.Helpers;

namespace TheXDS.Ganymede.Markup;

/// <summary>
/// Implements a <see cref="ResourceDictionary"/> that is used to initialize
/// Ganymede, as well as provide the required dictionaries for Ganymede to work
/// properly.
/// </summary>
[Localizability(LocalizationCategory.Ignore)]
[Ambient]
[UsableDuringInitialization(true)]
public sealed class GanymedeDictionary : ResourceDictionary
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GanymedeDictionary"/> class.
    /// </summary>
    public GanymedeDictionary()
    {
        GanymedeInitializer.Initialize();
        Source = new Uri($"pack://application:,,,/{Assembly.GetExecutingAssembly().GetName().Name};component/Themes/Generic.xaml", UriKind.Absolute);
    }
}
