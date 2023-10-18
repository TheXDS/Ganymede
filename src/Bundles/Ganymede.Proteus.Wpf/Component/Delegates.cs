﻿using System.Windows;
using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Component;

/// <summary>
/// Defines a chainable method that applies transformations to a generated
/// control that may include replacing it altogether.
/// </summary>
/// <param name="control">Control instance to process.</param>
/// <param name="description">Property description information.</param>
/// <returns>
/// Either the same instance as <paramref name="control"/> after it being
/// processed and/or transformed, or a new <see cref="FrameworkElement"/> that
/// will substitute the generated control, which may or may not contain the
/// original control in the chain.
/// </returns>
public delegate FrameworkElement ControlTransform(FrameworkElement control, IPropertyDescription description);

/// <summary>
/// Defines a method to execute when determining if the described property
/// should be generated by the <see cref="CrudEditorVisualBuilder"/>.
/// </summary>
/// <param name="description">Property description information.</param>
/// <param name="viewModelContext">
/// Context data of the editor ViewModel.
/// </param>
/// <returns>
/// <see langword="true"/> if the described property should be skipped on the,
/// specified ViewModel context, <see langword="false"/> otherwise.
/// </returns>
public delegate bool CrudGenSkipCheck(IPropertyDescription description, CrudEditorViewModel viewModelContext);