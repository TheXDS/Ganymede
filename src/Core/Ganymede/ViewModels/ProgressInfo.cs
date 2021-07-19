using System;

namespace TheXDS.Ganymede.ViewModels
{
    /// <summary>
    /// Representa el estado de progreso de una operación.
    /// </summary>
    public record ProgressInfo(int? Progress, string? Status)
    {
        /// <summary>
        /// Obtiene un <see cref="ProgressInfo"/> que representa una operación
        /// de progreso desconocido. Este campo es de solo lectura.
        /// </summary>
        public static readonly ProgressInfo Unknwon = new ProgressInfo();

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ProgressInfo"/>.
        /// </summary>
        /// <param name="progress">Valor de progreso de la operación.
        /// </param>
        public ProgressInfo(int progress) : this(progress, null) { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ProgressInfo"/> que representa un progreso 
        /// indeterminado.
        /// </summary>
        /// <param name="status">Mensaje de estado de la operación.</param>
        public ProgressInfo(string status) : this(null, status ?? throw new ArgumentNullException(nameof(status))) { }

        private ProgressInfo() : this(null, null) { }

        /// <summary>
        /// Convierte implícitamente un <see cref="string"/> en un
        /// <see cref="ProgressInfo"/> que representa un estado de progreso
        /// indeterminado con un mensaje.
        /// </summary>
        /// <param name="status">Mensaje de estado de la operación.</param>
        public static implicit operator ProgressInfo(string status) => new (null, status);
    }
}