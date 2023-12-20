using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Services.Configuration;
using TheXDS.Ivie.Services;
using TheXDS.ServicePool.Triton;
using TheXDS.Triton.Services.Base;
using Sp = TheXDS.ServicePool.ServicePool;

namespace TheXDS.Ivie.Properties;

public static class AppInitSteps
{
    private static IEnumerable<string> GetPlugins(Configuration? config)
    {
        return Directory.EnumerateFiles(
            config?.PluginsPath ??
            Environment.ProcessPath ??
            ".").Where(p => p.EndsWith(".dll"));
    }

    private static IEnumerable<Func<IProgress<ProgressReport>, ITritonService, Task>> GetDbInitializationSteps()
    {
        yield return DbInitSteps.EnsureUsersExist;
    }

    public static Task LoadComponents(IProgress<ProgressReport> progress, Sp pool)
    {
        progress.Report("Loading components...");
        var config = pool.Resolve<Configuration>();

        foreach (var j in GetPlugins(config))
        {
            try
            {
                 System.Reflection.Assembly.Load(j);
            }
            catch { }
        }
        return Task.CompletedTask;
    }

    public static async Task ConfigureDataConnection(IProgress<ProgressReport> progress, Sp pool)
    {
        progress.Report("Initializing data services...");
        var config = pool.Resolve<Configuration>();
        var j = pool.DiscoverAll<ITritonServiceConfigurator>(false);
        if (await Task.Run(() => config is not null ? j.FirstOrDefault(p => p.ClassId == config.DataProvider) : j.FirstOrDefault()) is { } configurator)
        {
            configurator.Configure(pool.UseTriton());
        }
    }

    public static async Task InitializeDb(IProgress<ProgressReport> progress, Sp pool)
    {
        if (pool.Resolve<IIvieService>() is { } svc)
        {
            foreach (var step in GetDbInitializationSteps())
            {
                await step.Invoke(progress, svc);
            }
        }
    }
}