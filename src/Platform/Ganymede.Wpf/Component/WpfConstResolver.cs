using System;
using System.Windows;
using TheXDS.Ganymede.Types.Base;
using TheXDS.MCART.Exceptions;

namespace TheXDS.Ganymede.Component;

/// <summary>
/// Implements a <see cref="IVisualResolver{TVisual}"/> that resolves 
/// </summary>
public class WpfConstResolver : IVisualResolver<FrameworkElement>
{
    private Type? view;

    public Type? View
    {
        get => view;
        set
        {
            if (value is not null && !value.IsAssignableTo(typeof(FrameworkElement)))
            { 
                throw new InvalidTypeException();
            }
            view = value;
        }
    }

    FrameworkElement? IVisualResolver<FrameworkElement>.Resolve(IViewModel viewModel)
    {
        return View is not null ? (FrameworkElement)Activator.CreateInstance(View)! : null;
    }
}
