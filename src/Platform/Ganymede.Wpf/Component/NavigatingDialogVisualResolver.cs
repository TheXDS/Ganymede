using TheXDS.Ganymede.Views.Dialogs;

namespace TheXDS.Ganymede.Component;

/// <summary>
/// Implements a <see cref="IVisualResolver{TVisual}"/> tailored to resolve
/// dialog-specific ViewModels.
/// </summary>
public class NavigatingDialogVisualResolver : ConstVisualResolver<DialogView>;
