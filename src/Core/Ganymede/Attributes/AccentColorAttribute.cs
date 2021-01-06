using System;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Types;

namespace TheXDS.Ganymede.Attributes
{
    /// <summary>
    /// Establece el color de acento a utilizar al inicializar la página.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class AccentColorAttribute : Attribute, IValueAttribute<Color>
    {
        /// <summary>
        /// Establece el color de acento a utilizar al inicializar la página.
        /// </summary>
        /// <param name="color">
        /// Nombre del color, o cadena que describe los valores en formato 
        /// #[AA]RRGGBB a utilizar para describir el color.
        /// </param>
        public AccentColorAttribute(string color)
        {
            Value = Color.Parse(color);
        }

        /// <summary>
        /// Establece el color de acento a utilizar al inicializar la página.
        /// </summary>
        /// <param name="r">Nivel del componente rojo.</param>
        /// <param name="g">Nivel del componente verde.</param>
        /// <param name="b">Nivel del componente azul.</param>
        public AccentColorAttribute(in byte r, in byte g, in byte b)
        {
            Value = new Color(r, g, b);
        }

        /// <summary>
        /// Establece el color de acento a utilizar al inicializar la página.
        /// </summary>
        /// <param name="r">Nivel del componente rojo.</param>
        /// <param name="g">Nivel del componente verde.</param>
        /// <param name="b">Nivel del componente azul.</param>
        /// <param name="a">Nivel del componente alpha.</param>
        public AccentColorAttribute(in byte r, in byte g, in byte b, in byte a)
        {
            Value = new Color(r, g, b, a);
        }

        /// <summary>
        /// Establece el color de acento a utilizar al inicializar la página.
        /// </summary>
        /// <param name="r">Nivel del componente rojo.</param>
        /// <param name="g">Nivel del componente verde.</param>
        /// <param name="b">Nivel del componente azul.</param>
        public AccentColorAttribute(in float r, in float g, in float b)
        {
            Value = new Color(r, g, b);
        }

        /// <summary>
        /// Establece el color de acento a utilizar al inicializar la página.
        /// </summary>
        /// <param name="r">Nivel del componente rojo.</param>
        /// <param name="g">Nivel del componente verde.</param>
        /// <param name="b">Nivel del componente azul.</param>
        /// <param name="a">Nivel del componente alpha.</param>
        public AccentColorAttribute(in float r, in float g, in float b, in float a)
        {
            Value = new Color(r, g, b, a);
        }

        /// <summary>
        /// Obtiene el color de acento a utilizar al inicializar la página.
        /// </summary>
        public Color Value { get; }
    }
}
