using TheXDS.Ganymede.Services;

namespace TheXDS.Ganymede.Helpers;

/// <summary>
/// Initializes the Ganymede library.
/// </summary>
public static partial class GanymedeInitializer
{
    /// <summary>
    /// Initializes the Ganymede library.
    /// </summary>
    public static void Initialize()
    {
        UiThread.SetProxy(new DispatcherUiThreadProxy());
        InitializeDialogVisualConverter();
    }

    private static partial void InitializeDialogVisualConverter();
}