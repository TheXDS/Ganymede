using System.Windows;
using TheXDS.MCART.Math;
using TheXDS.MCART.Types;
using MC = System.Windows.Media.Color;

namespace TheXDS.Ganymede.Client
{
    /// <summary>
    /// Representa un color con transparencia ajustable.
    /// </summary>
    public class TransparentColor : DependencyObject
    {
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="BaseColor"/>.
        /// </summary>
        public static readonly DependencyProperty BaseColorProperty = 
            DependencyProperty.Register(nameof(BaseColor), typeof(Color?), typeof(TransparentColor), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Identifica a la propiedad de dependencia
        /// <see cref="Transparency"/>.
        /// </summary>
        public static readonly DependencyProperty TransparencyProperty = 
            DependencyProperty.Register(nameof(Transparency), typeof(float), typeof(TransparentColor), new FrameworkPropertyMetadata(1f, (o, d) => { }, (d, v) => ((float)v).Clamp(0f, 1f)));

        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="UseAlpha"/>.
        /// </summary>
        public static readonly DependencyProperty UseAlphaProperty =
            DependencyProperty.Register(nameof(UseAlpha), typeof(bool), typeof(TransparentColor), new FrameworkPropertyMetadata(true));

        /// <summary>
        /// Obtiene o establece el color base a devolver.
        /// </summary>
        public Color? BaseColor
        {
            get => (Color?)GetValue(BaseColorProperty);
            set => SetValue(BaseColorProperty, value);
        }

        /// <summary>
        /// Obtiene o establece el nivel de transparencia a devolver para el color base.
        /// </summary>
        public float Transparency
        {
            get { return (float)GetValue(TransparencyProperty); }
            set { SetValue(TransparencyProperty, value); }
        }

        /// <summary>
        /// Obtiene o establece un valor que indica si se tomará en cuenta el Alpha del color base para calcular la transparencia.
        /// </summary>
        public bool UseAlpha
        {
            get => (bool)GetValue(UseAlphaProperty);
            set => SetValue(UseAlphaProperty, value);
        }

        /// <summary>
        /// Convierte implícitamente un <see cref="TransparentColor"/> en un <see cref="MC"/>.
        /// </summary>
        /// <param name="color"></param>
        public static implicit operator MC(TransparentColor color)
        {
            var bc = color.BaseColor;
            
            return new MC()
            {
                ScA = color.Transparency * (color.UseAlpha ? bc?.ScA ?? 1f : 1f),
                ScR = bc?.ScR ?? 0f,
                ScG = bc?.ScG ?? 0f,
                ScB = bc?.ScB ?? 0f,
            };
        }
    }
}
