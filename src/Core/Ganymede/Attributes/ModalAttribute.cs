using System;

namespace TheXDS.Ganymede.Attributes
{
    /// <summary>
    /// Marca una página para presentarse de forma modal.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ModalAttribute : Attribute
    {
    }
}
