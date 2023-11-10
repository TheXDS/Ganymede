using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Ganymede.CrudGen.Mappings.Base;

namespace TheXDS.Ganymede.CrudGen.Mappings;

/// <summary>
/// Implements a <see cref="ICrudMapping"/> that generates a warning upon
/// failure to find a suitable mapping instance for the property.
/// </summary>
internal class FallbackMapping : ICrudMapping
{
    /// <summary>
    /// Creates a new instance of the <see cref="FallbackMapping"/> class.
    /// </summary>
    /// <returns>
    /// A new instance of the <see cref="FallbackMapping"/> class.
    /// </returns>
    public static FallbackMapping Create() => new();

    /* This private constructor prevents automatic discovery and registration
     * of this mapping when using either type discovery or reflection, as this
     * is intended to be used internally by Proteus only when a property cannot
     * be mapped. */
    private FallbackMapping()
    {
    }

    /// <inheritdoc/>
    public bool CanMap(IPropertyDescription description) => true;

    /// <inheritdoc/>
    public FrameworkElement CreateControl(IPropertyDescription description)
    {
        return new TextBlock { 
            Text = $"⚠️ Unmapped: {description.Property.Name}",
            Foreground = Brushes.DarkRed,
            FontWeight = FontWeights.Bold
        };
    }
}
