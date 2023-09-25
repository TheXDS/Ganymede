namespace TheXDS.Ganymede.CrudGen;

/// <summary>
/// Defines a set of members to be implemented by a type that can generate UI
/// elements to be used as the View in a framework that supports the MVVM
/// pattern.
/// </summary>
/// <typeparam name="TVisual">
/// Type of visual elements in the UI framework used by the client application.
/// </typeparam>
public interface ICrudBuilder<TVisual>
{
    /// <summary>
    /// Builds an editor View.
    /// </summary>
    /// <param name="description">
    /// Model description for which to build the editor.
    /// </param>
    /// <returns>
    /// A visual element that supports binding and can be presented in the
    /// App's UI.
    /// </returns>
    TVisual BuildEditor(ICrudDescriptor description);

    /// <summary>
    /// Builds a details panel.
    /// </summary>
    /// <param name="description">
    /// Model description for which to build the details panel.
    /// </param>
    /// <returns>
    /// A visual element that supports binding and can be presented in the
    /// App's UI.
    /// </returns>
    TVisual BuildDetailsPanel(ICrudDescriptor description);
}
