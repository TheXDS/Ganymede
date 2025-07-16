using System.Globalization;
using System.Windows.Input;
using TheXDS.Ganymede.Component;
using TheXDS.Ganymede.Types.Base;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.ValueConverters.Base;

namespace TheXDS.Ganymede.ValueConverters;

public sealed partial class DialogVisualConverter : IOneWayValueConverter<DialogViewModel, FrameworkElement?>, IVisualResolver<FrameworkElement>
{
    /// <inheritdoc/>
    public FrameworkElement? Convert(DialogViewModel value, object? parameter, CultureInfo? culture)
    {
        var result = GetBuilder(value)?.Build(value);
        if (result is not null)
        {
            result.Loaded += DialogContent_Loaded;
        }
        return result;
    }

    private void DialogContent_Loaded(object sender, RoutedEventArgs e)
    {
        FrameworkElement control = (FrameworkElement)sender;
        control.Focus();
        Keyboard.Focus(control);
        control.Loaded -= DialogContent_Loaded;
    }

    FrameworkElement? IVisualResolver<FrameworkElement>.Resolve(IViewModel viewModel)
    {
        return viewModel is DialogViewModel vm
            ? Convert(vm, null, CultureInfo.InvariantCulture)
            : null;
    }
}
