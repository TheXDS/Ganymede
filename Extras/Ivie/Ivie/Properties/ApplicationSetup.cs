using TheXDS.Ganymede.Models;
using Sp = TheXDS.ServicePool.ServicePool;

namespace TheXDS.Ivie.Properties;

/// <summary>
/// Contains a series of members to be used for configuring Ivie, Tritón and
/// the underlying database.
/// </summary>
public static class ApplicationSetup
{
    /// <summary>
    /// Enumerates all the steps required to initialize the application.
    /// </summary>
    /// <returns>
    /// A collection of all the steps required to initialize the application.
    /// </returns>
    public static IEnumerable<Func<IProgress<ProgressReport>, Sp, Task>> GetInitializationSteps()
    {
        yield return AppInitSteps.LoadComponents;
        yield return AppInitSteps.ConfigureDataConnection;
        yield return AppInitSteps.InitializeDb;
    }
}
