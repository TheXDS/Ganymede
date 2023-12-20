using TheXDS.MCART.Types.Extensions;
using TheXDS.ServicePool.Triton;

namespace TheXDS.Ganymede.Services.Configuration;

/// <summary>
/// Defines a set of members to be implemented by a type that allows for Tritón
/// configuration.
/// </summary>
public interface ITritonServiceConfigurator
{
    /// <summary>
    /// Gets a name to be displayed on the UI for this configurator instance.
    /// </summary>
    string DisplayName
    {
        get
        {
            var str = string.Join(null, GetType().Name.SplitByCase());
            return str[0].ToString().ToUpper() + str[1..].ToLower();
        }
    }

    /// <summary>
    /// Gets a <see cref="Guid"/> that uniquely identifies this configurator
    /// type.
    /// </summary>
    Guid ClassId => new(System.Text.Encoding.UTF8.GetBytes(GetType().Name).Concat(new byte[16]).ToArray()[..16]);

    /// <summary>
    /// Executes configuration steps on the <see cref="ITritonConfigurable"/>
    /// instance.
    /// </summary>
    /// <param name="configurable">
    /// Object instance used to configure Triton.
    /// </param>
    void Configure(ITritonConfigurable configurable);
}
