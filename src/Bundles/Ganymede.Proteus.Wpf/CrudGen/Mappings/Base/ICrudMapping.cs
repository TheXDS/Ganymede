﻿using System.Windows;
using System.Windows.Data;
using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Triton.Models.Base;

namespace TheXDS.Ganymede.CrudGen.Mappings.Base;

/// <summary>
/// Defines a set of members to be implemented by any type that provides
/// mapping capabilities for generating controls to view/edit a property in a
/// generated editor view.
/// </summary>
public interface ICrudMapping
{
    /// <summary>
    /// Gets a value that indicates if the generated control requires its value
    /// to be set manually.
    /// </summary>
    /// <remarks>
    /// If this property returns <see langword="true"/>, the method
    /// <see cref="SetControlValue(FrameworkElement, Model, IPropertyDescription)"/>
    /// must be overriden.
    /// </remarks>
    bool MustSetValueManually => false;

    /// <summary>
    /// Tests if this instance could be used to generate a control for the
    /// given property/<see cref="IPropertyDescription"/> pair.
    /// </summary>
    /// <param name="description">
    /// <see cref="IPropertyDescription"/> to test.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if this instance can be used to generate a
    /// control to view/edit the given property, <see langword="false"/>
    /// otherwise.
    /// </returns>
    bool CanMap(IPropertyDescription description);

    /// <summary>
    /// Creates a control that can be presented visually to view/edit the
    /// specified property.
    /// </summary>
    /// <param name="description">
    /// Details about the specifics for control generation.
    /// </param>
    /// <returns>
    /// A <see cref="FrameworkElement"/> that can be presented visually on an
    /// edit View. The resulting control will use <see cref="Binding"/> for
    /// access to the target property.
    /// </returns>
    FrameworkElement CreateControl(IPropertyDescription description);

    /// <summary>
    /// When <see cref="MustSetValueManually"/> returns <see langword="true"/>,
    /// allows custom logic to be executed whenever a new editor ViewModel is
    /// invoked, and manually sets the control value from the entity being
    /// edited.
    /// </summary>
    /// <param name="control">
    /// Control onto which to set the entity's property value. It should be a
    /// control generated by this mapping.
    /// </param>
    /// <param name="entity">
    /// Entity from which to extract the property value.
    /// </param>
    /// <param name="description">
    /// Description context for the property for which the control was
    /// generated by this mapping.
    /// </param>
    void SetControlValue(FrameworkElement control, Model entity, IPropertyDescription description)
    {
    }
}
