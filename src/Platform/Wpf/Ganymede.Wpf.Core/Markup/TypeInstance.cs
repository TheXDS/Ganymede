using System.Windows.Markup;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Helpers.ReflectionHelpers;

namespace TheXDS.Ganymede.Markup;

/// <summary>
/// Implements a markup extension that will find a public Type with the
/// specified name and instance it using its default parameterless constructor.
/// </summary>
public sealed class TypeInstance : MarkupExtension
{
    /// <summary>
    /// Gets or sets the Type name to search for.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <inheritdoc/>
    public override object? ProvideValue(IServiceProvider serviceProvider)
    {
        return PublicTypes().FirstOrDefault(p => p.Name == Name)?.New();
    }
}
