using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TheXDS.Ganymede.CrudGen;
using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.MCART.ValueConverters.Base;

namespace TheXDS.Ganymede.ValueConverters;

/// <summary>
/// Converter that generates <see cref="GridView"/> objects to customize
/// <see cref="ListView"/> presentation.
/// </summary>
public class ListViewGridGenerator : IOneWayValueConverter<ICrudDescription?, ViewBase?>
{
    public ViewBase? Convert(ICrudDescription? value, object? parameter, CultureInfo? culture)
    {
        if (value is null) return null;
        var view = new GridView();
        foreach (var j in GetListDescriptions(value))
        {
            view.Columns.Add(CreateColumn(j));
        }
        return view;
    }

    private static IEnumerable<IPropertyDescription> GetListDescriptions(ICrudDescription description)
    {
        var props = description.ListViewProperties.ToArray();
        return description.PropertyDescriptions.Where(p => props.Contains(p.Key)).Select(p => p.Value.Description);
    }

    private static GridViewColumn CreateColumn(IPropertyDescription p)
    {
        return new GridViewColumn()
        {
            Header = p.Label,
            DisplayMemberBinding = new Binding(p.Property.Name)
        };
    }
}

public enum DescriptionCountVisibility : byte
{
    Single,
    Multiple
}

public class DescriptionCountVisibilityConverter : IOneWayValueConverter<ICrudDescription[], Visibility>
{
    public DescriptionCountVisibility VisibleIf { get; set; }

    public Visibility Convert(ICrudDescription[] value, object? parameter, CultureInfo? culture)
    {
        return VisibleIf == DescriptionCountVisibility.Single
            ? value.Length == 1 ? Visibility.Visible : Visibility.Collapsed
            : value.Length > 1 ? Visibility.Visible : Visibility.Collapsed;
    }
}