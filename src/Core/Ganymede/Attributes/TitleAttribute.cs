using System;
using TheXDS.MCART.Attributes;

namespace TheXDS.Ganymede.Attributes
{
    /// <summary>
    /// Establece un título a utilizar al inicializar la página.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class TitleAttribute : Attribute, IValueAttribute<string>
    {
        /// <summary>
        /// Obtiene el título a establecer en la página.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Establece un título a utilizar al inicializar la página.
        /// </summary>
        /// <param name="title">
        /// Título de la página.
        /// </param>
        public TitleAttribute(string title)
        {
            Title = title;
        }

        string IValueAttribute<string>.Value => Title;
    }
}
