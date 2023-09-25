//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using TheXDS.Ganymede.CrudGen.Mappings.Base;
//using TheXDS.MCART.Types.Base;
//using TheXDS.MCART.Types.Extensions;
//using TheXDS.Ganymede.CrudGen.Descriptions;
//using TheXDS.Ganymede.CrudGen.Mappings;
//using TheXDS.Triton.Models.Base;

//namespace TheXDS.Ganymede.CrudGen;

///// <summary>
///// Implements a <see cref="ICrudBuilder{TVisual}"/> for Windows
///// Presentation Framework.
///// </summary>
//public class CrudBuilder : ICrudBuilder<FrameworkElement>
//{
//    private static T UiCreate<T>(Func<T> create) => Application.Current.Dispatcher.Invoke(create);

//    private readonly List<ICrudMapping> _mappings = new()
//    {
//        new SimpleTextBoxMapping(),
//        new BoolCheckBoxMapping(),
//        new SimpleCrudMapping<DateTime, DatePicker>(DatePicker.SelectedDateProperty, OnCreateDatePicker),
//    };

//    private static void OnCreateDatePicker(DatePicker picker, PropertyInfo info, IPropertyDescription description)
//    {
//        if (description is not INumericPropertyDescription<DateTime> d) return;

//        picker.DisplayDateEnd = d.ValidRange?.Maximum;
//        picker.DisplayDateStart = d.ValidRange?.Minimum;

//    }

//    /// <inheritdoc/>
//    public FrameworkElement BuildDetailsPanel(ICrudDescriptor description)
//    {
//        var mapping = new ReadOnlyMapping();
//        return UiCreate(() =>
//        {
//            var pnl = new StackPanel
//            {
//                Children =
//                {
//                    CreateTitle(description),
//                    new Separator(),
//                }
//            };

//            foreach (var j in description.Description.PropertyDescriptions)
//            {
//                pnl.Children.Add(mapping.CreateControl(j.Key, j.Value.Description));
//            }
//            return pnl;
//        });
//    }

//    /// <inheritdoc/>
//    public FrameworkElement BuildEditor(ICrudDescriptor description)
//    {
//        return UiCreate(() =>
//        {
//            var pnl = new StackPanel();
//            var defaultMargin = new Thickness(5);
//            foreach (var j in description.Description.PropertyDescriptions)
//            {
//                var mapping = _mappings.FirstOrDefault(p => p.CanMap(j.Key, j.Value.Description)) ?? new FallbackMapping();
//                var ctrl = mapping.CreateControl(j.Key, j.Value.Description);
//                ctrl.Margin = defaultMargin;
//                pnl.Children.Add(ctrl);
//            }
//            return pnl;
//        });
//    }

//    private static Binding CreateTitleBinding(ICrudDescriptor descriptor)
//    {
//        return new Binding(GetTitlePath(descriptor))
//        {
//            StringFormat = " \"{0}\""
//        };
//    }

//    private static string GetTitlePath(ICrudDescriptor descriptor)
//    {
//        return descriptor.Description.FriendlyNameBindingPath.OrNull()
//            ?? (descriptor.Model.Implements<INameable>()
//            ? nameof(INameable.Name)
//            : nameof(Model<int>.Id));
//    }

//    private static TextBlock CreateTitle(ICrudDescriptor descriptor)
//    {
//        var i = new Run();
//        i.SetBinding(Run.TextProperty, CreateTitleBinding(descriptor));
//        var tb = new TextBlock
//        {
//            Margin = new Thickness(0, 30, 0, 0),
//            Style = Application.Current.TryFindResource("Title") as Style,
//            Inlines =
//            {
//                new Run(descriptor.Description.FriendlyName),
//                i
//            }
//        };
//        return tb;
//    }
//}
