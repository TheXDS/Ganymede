using Avalonia.Controls;

namespace TheXDS.Ganymede.Views;

/// <summary>
/// Special debugging View that gets automatically resolved on Ganymede upon
/// failure to resolve an appropriate view for a ViewModel.
/// </summary>
public partial class GanymedeNavErrorFallbackView : UserControl
{
    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="GanymedeNavErrorFallbackView"/> class.
    /// </summary>
    public GanymedeNavErrorFallbackView()
    {
        InitializeComponent();
    }
}