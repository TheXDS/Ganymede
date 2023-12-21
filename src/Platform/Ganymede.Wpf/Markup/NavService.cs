using System;
using System.Linq;
using System.Windows.Markup;
using TheXDS.Ganymede.Services;
using TheXDS.Ganymede.Types.Base;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Helpers.ReflectionHelpers;

namespace TheXDS.Ganymede.Markup;

/// <summary>
/// Markup extension that allows quick definition of a navigation service of
/// type <see cref="NavigationService{T}"/> with a specified home page.
/// </summary>
public sealed class NavService : MarkupExtension
{
    /// <summary>
    /// Gets or sets the <see cref="ViewModel"/> instance to be set as the home
    /// page for the new navigation service to be returned.
    /// </summary>
    public object? Home { get; set; }

    /// <inheritdoc/>
    public override object? ProvideValue(IServiceProvider serviceProvider)
    {
        return new NavigationService<ViewModel> { HomePage = GetViewModel() };
    }

    private ViewModel? GetViewModel()
    {
        return Home switch
        {
            ViewModel vm => vm,
            Type vmType when vmType.Implements<ViewModel>() => vmType.New<ViewModel>(),
            string vmName => GetByName(vmName),
            _ => null
        };
    }

    private static ViewModel? GetByName(string name)
    {
        return PublicTypes().FirstOrDefault(p => p.Name == name)?.New<ViewModel>();
    }
}
