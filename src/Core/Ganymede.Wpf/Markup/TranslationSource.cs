using System;
using System.Windows.Markup;
using TheXDS.MCART.Types.Extensions;
using System.Resources;
using static System.Reflection.BindingFlags;
namespace TheXDS.Ganymede.Markup;

/// <summary>
/// Implements a markup extension that gets a translated string from the
/// specified resource class.
/// </summary>
public sealed class TranslationSource : MarkupExtension
{
    /// <summary>
    /// Gets or sets the resource class to get the resource strings from.
    /// </summary>
    public Type? ResourceClass { get; set; }

    /// <summary>
    /// Gets or sets the string ID to fetch from the resource class.
    /// </summary>
    public string? Id { get; set; }

    /// <inheritdoc/>
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        if (Id.IsEmpty()) { return string.Empty; }
        return ResourceClass?.GetProperty(Id, Static | Public)?.GetValue(null) as string
            ?? (ResourceClass?.GetProperty(nameof(ResourceManager), Static | Public)?.GetValue(null) is ResourceManager r
            ? r.GetString(Id) ?? Id: Id);
    }
}