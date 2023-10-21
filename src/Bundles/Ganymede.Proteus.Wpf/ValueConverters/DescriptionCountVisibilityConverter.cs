using System.Globalization;
using System.Windows;
using TheXDS.Ganymede.CrudGen;
using TheXDS.MCART.ValueConverters.Base;

namespace TheXDS.Ganymede.ValueConverters;

/// <summary>
/// Converter that returns a vislibility value used to show/hide controls based
/// on the amount of CRUD descriptors present.
/// </summary>
public class DescriptionCountVisibilityConverter : IOneWayValueConverter<ICrudDescription[], Visibility>
{
    /// <summary>
    /// Gets or sets a value that indicates if the visilibity should be
    /// calculated for a collection of descriptions with either a single or
    /// multiple elements.
    /// </summary>
    public DescriptionCountVisibility VisibleIf { get; set; }

    /// <inheritdoc/>
    public Visibility Convert(ICrudDescription[] value, object? parameter, CultureInfo? culture)
    {
        return VisibleIf == DescriptionCountVisibility.Single
            ? value.Length == 1 ? Visibility.Visible : Visibility.Collapsed
            : value.Length > 1 ? Visibility.Visible : Visibility.Collapsed;
    }
}