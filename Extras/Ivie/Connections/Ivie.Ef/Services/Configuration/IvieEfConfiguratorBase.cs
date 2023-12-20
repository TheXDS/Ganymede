using TheXDS.Ivie.Models;
using TheXDS.Proteus.Services.Configuration;
using TheXDS.Triton.Services;

namespace TheXDS.Ivie.Services.Configuration;

/// <summary>
/// Implements an abstract configuration object for Ivie.
/// </summary>
public abstract class IvieEfConfiguratorBase : TritonEfConfiguratorBase<IvieService, IvieContext>
{
    /// <inheritdoc/>
    protected override IvieService CreateService(IMiddlewareConfigurator middlewareConfigurator, EfCoreTransFactory<IvieContext> transactionFactory)
    {
        return new IvieService(middlewareConfigurator, transactionFactory);
    }
}
