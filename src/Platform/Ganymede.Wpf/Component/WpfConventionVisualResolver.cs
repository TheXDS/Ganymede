using System.Windows;

namespace TheXDS.Ganymede.Component;

/// <summary>
/// Represents a <see cref="ConventionVisualResolver{TVisual}"/> that specifies
/// the type of visual elements to resolve as <see cref="FrameworkElement"/>.
/// </summary>
public class WpfConventionVisualResolver : ConventionVisualResolver<FrameworkElement>;