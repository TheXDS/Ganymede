using Avalonia;
using Avalonia.Controls;
using System.Globalization;
using TheXDS.Ganymede.Component;
using TheXDS.Ganymede.Types.Base;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.ValueConverters.Base;

namespace TheXDS.Ganymede.ValueConverters;

public sealed partial class DialogVisualConverter : IOneWayValueConverter<DialogViewModel?, StyledElement?>, IVisualResolver<StyledElement>
{
    /// <inheritdoc/>
    public StyledElement? Convert(DialogViewModel? value, object? parameter, CultureInfo? culture)
    {
        if (value is null) return null;
        var result = GetBuilder(value)?.Build(value);
        if (result is not null)
        {
            result.Initialized += DialogContent_Loaded;
        }
        return result;
    }

    private void DialogContent_Loaded(object? sender, EventArgs e)
    {
        if (sender is Control c)
        {
            c.Focus();
            c.Initialized -= DialogContent_Loaded;
        }
    }

    StyledElement? IVisualResolver<StyledElement>.Resolve(IViewModel viewModel)
    {
        return viewModel is DialogViewModel vm
            ? Convert(vm, null, CultureInfo.InvariantCulture)
            : null;
    }
}
