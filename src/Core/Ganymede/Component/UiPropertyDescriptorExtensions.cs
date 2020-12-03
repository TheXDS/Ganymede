using TheXDS.MCART.Types;

namespace TheXDS.Ganymede.Component
{
    public static class UiPropertyDescriptorExtensions
    {
        public static void MakeModal(this IUiPropertyDescriptor d) => d.Modal = true;
        public static void RemoveAccentColor(this IUiPropertyDescriptor d) => d.SetAccentColor(null);
        public static void SetAccentColor(this IUiPropertyDescriptor d, Color? color) => d.AccentColor = color;
        public static void SetCloseable(this IUiPropertyDescriptor d, bool value) => d.Closeable = value;
        public static void SetTitle(this IUiPropertyDescriptor d, string title) => d.Title = title;
    }
}