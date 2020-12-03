using System;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Types;

namespace TheXDS.Ganymede.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class TitleAttribute : Attribute, IValueAttribute<string>
    {
        public string Title { get; }

        public TitleAttribute(string title)
        {
            Title = title;
        }

        string IValueAttribute<string>.Value => Title;
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class AccentColorAttribute : Attribute, IValueAttribute<Color>
    {
        public AccentColorAttribute(string color)
        {
            Value = Color.Parse(color);
        }

        public AccentColorAttribute(in byte r, in byte g, in byte b)
        {
            Value = new Color(r, g, b);
        }
        public AccentColorAttribute(in byte r, in byte g, in byte b, in byte a)
        {
            Value = new Color(r, g, b, a);
        }

        public AccentColorAttribute(in float r, in float g, in float b)
        {
            Value = new Color(r, g, b);
        }
        public AccentColorAttribute(in float r, in float g, in float b, in float a)
        {
            Value = new Color(r, g, b, a);
        }

        public Color Value { get; }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class CloseableAttribute : Attribute, IValueAttribute<bool>
    {
        public bool Closeable { get; }

        public CloseableAttribute(bool closeable)
        {
            Closeable = closeable;
        }

        bool IValueAttribute<bool>.Value => Closeable;
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ModalAttribute : Attribute
    {
    }
}
