using TheXDS.MCART.Types;

namespace TheXDS.Ganymede.Component
{
    /// <summary>
    /// Incluye una serie de extensiones que simplifican el acceso a la API de
    /// configuración de contenedores visuales.
    /// </summary>
    public static class UiPropertyDescriptorExtensions
    {
        /// <summary>
        /// Hace que el contenedor visual se muestre de manera modal.
        /// </summary>
        /// <param name="d">
        /// Descriptor de propiedades del contenedor visual.
        /// </param>
        public static void MakeModal(this IUiPropertyDescriptor d) => d.Modal = true;

        /// <summary>
        /// Quita el color de acento para el contenedor visual.
        /// </summary>
        /// <param name="d">
        /// Descriptor de propiedades del contenedor visual.
        /// </param>
        public static void RemoveAccentColor(this IUiPropertyDescriptor d) => d.SetAccentColor(null);

        /// <summary>
        /// Establece el color a mostrar en el contenedor visual.
        /// </summary>
        /// <param name="d">
        /// Descriptor de propiedades del contenedor visual.
        /// </param>
        /// <param name="color">
        /// Color a establecer para el contenedor visual.
        /// </param>
        public static void SetAccentColor(this IUiPropertyDescriptor d, Color? color) => d.AccentColor = color;

        /// <summary>
        /// Establece un valor que indica si el contenedor visual debe poder 
        /// ser cerrado.
        /// </summary>
        /// <param name="d">
        /// Descriptor de propiedades del contenedor visual.
        /// </param>
        /// <param name="value">
        /// <see langword="true"/> para permitir al contenedor visual ser
        /// cerrado, <see langword="false"/> en caso contrario.
        /// </param>
        public static void SetCloseable(this IUiPropertyDescriptor d, bool value) => d.Closeable = value;

        /// <summary>
        /// Establece el título a mostrar en el contenedor visual.
        /// </summary>
        /// <param name="d">
        /// Descriptor de propiedades del contenedor visual.
        /// </param>
        /// <param name="title">
        /// Título a mostrar en el contenedor visual.
        /// </param>
        public static void SetTitle(this IUiPropertyDescriptor d, string title) => d.Title = title;
    }
}