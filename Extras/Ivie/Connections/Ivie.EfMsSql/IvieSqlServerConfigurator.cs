using Microsoft.EntityFrameworkCore;
using TheXDS.Ivie.Models;
using TheXDS.Ivie.Services.Configuration;

namespace TheXDS.Ivie;

/// <summary>
/// Implements a configuration object for Ivie with a database in memory.
/// </summary>
public class IvieSqlServerConfigurator : IvieEfConfiguratorBase
{
    /// <inheritdoc/>
    protected override void ConfigureContext(DbContextOptionsBuilder<IvieContext> builder)
    {
        builder.UseSqlServer("Ivie");
    }
}