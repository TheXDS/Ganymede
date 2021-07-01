using TheXDS.Ganymede.ViewModels;

namespace TheXDS.Ganymede.Component
{
    /// <summary>
    /// Define una serie de miembros a implementar por un tipo que permita
    /// construir hosts visuales para un <see cref="PageViewModel"/>.
    /// </summary>
    public interface IVisualBuilder
    {
        /// <summary>
        /// Construye un host visual para el <see cref="PageViewModel"/>
        /// especificado.
        /// </summary>
        /// <param name="viewModel">
        /// <see cref="PageViewModel"/> para el cual construir un host
        /// visual.
        /// </param>
        /// <returns>
        /// Un host visual para el <see cref="PageViewModel"/>
        /// especificado.
        /// </returns>
        object Build(PageViewModel viewModel);
    }

    /// <summary>
    /// Define una serie de miembros a implementar por un tipo que permita
    /// construir hosts visuales fuertemente tipeados para un
    /// <see cref="PageViewModel"/>.
    /// </summary>
    public interface IVisualBuilder<out T> : IVisualBuilder where T : notnull
    {
        /// <summary>
        /// Construye un host visual para el <see cref="PageViewModel"/>
        /// especificado.
        /// </summary>
        /// <param name="viewModel">
        /// <see cref="PageViewModel"/> para el cual construir un host
        /// visual.
        /// </param>
        /// <returns>
        /// Un host visual para el <see cref="PageViewModel"/>
        /// especificado.
        /// </returns>
        new T Build(PageViewModel viewModel);

        object IVisualBuilder.Build(PageViewModel viewModel) => Build(viewModel);
    }
}