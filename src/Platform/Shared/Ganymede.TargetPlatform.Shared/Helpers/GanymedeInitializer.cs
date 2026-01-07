using TheXDS.Ganymede.Services;

namespace TheXDS.Ganymede.Helpers;

/// <summary>
/// Initializes the Ganymede library.
/// </summary>
public static partial class GanymedeInitializer
{
    private static bool _initialized = false;

    /// <summary>
    /// Initializes the Ganymede library.
    /// </summary>
    public static void Initialize()
    {
        if (_initialized) return;
        _initialized = true;
        UiThread.SetProxy(new DispatcherUiThreadProxy());
        InitializeDialogVisualConverter();
    }

    private static partial void InitializeDialogVisualConverter();
}