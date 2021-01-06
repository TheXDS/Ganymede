using System;
using TheXDS.MCART.Attributes;

namespace TheXDS.Ganymede.Attributes
{
    /// <summary>
    /// Establece un valor que indica si la página podrá ser cerrada.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class CloseableAttribute : Attribute, IValueAttribute<bool>
    {
        /// <summary>
        /// Obtiene un valor que indica si la página podrá ser cerrada.
        /// </summary>
        public bool Closeable { get; }

        /// <summary>
        /// Establece un valor que indica si la página podrá ser cerrada.
        /// </summary>
        /// <param name="closeable">
        /// Valor que indica si la página podrá ser cerrada.
        /// </param>
        public CloseableAttribute(bool closeable)
        {
            Closeable = closeable;
        }

        bool IValueAttribute<bool>.Value => Closeable;
    }
}
