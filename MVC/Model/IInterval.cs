namespace Shared.Utils
{
    public interface IInterval<T>
    {
        /// <summary>
        /// Valor inicial do intervalo
        /// </summary>
        T Start { get; }
        /// <summary>
        /// Valor final do intervalo
        /// </summary>
        T End { get; }

        /// <summary>
        /// Se o intervalo contém o valor
        /// </summary>
        bool Includes(T value);
        /// <summary>
        /// Se o intervalo contém o valor
        /// </summary>
        bool Includes(IInterval<T> interval);
        /// <summary>
        /// Se o intervalo está a antes do valor
        /// </summary>
        bool Before(T value);
        /// <summary>
        /// Se o intervalo está a antes do valor
        /// </summary>
        bool Before(IInterval<T> interval);
        /// <summary>
        /// Se o intervalo está depois do valor
        /// </summary>
        bool After(T value);
        /// <summary>
        /// Se o intervalo está depois do valor
        /// </summary>
        bool After(IInterval<T> interval);
    }
}