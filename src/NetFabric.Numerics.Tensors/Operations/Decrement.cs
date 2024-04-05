namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    /// <summary>
    /// Decrements each element in the <paramref name="source"/> span and stores the result in the <paramref name="destination"/> span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="source">The input span.</param>
    /// <param name="destination">The output span.</param>
    /// <exception cref="System.ArgumentException">Thrown when the <paramref name="source"/> and <paramref name="destination"/> spans have different lengths.</exception>
    public static void Decrement<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, IDecrementOperators<T>
        => Tensor.Apply<T, DecrementOperator<T>>(source, destination);

    /// <summary>
    /// Decrements each element in the <paramref name="source"/> span and stores the result in the <paramref name="destination"/> span. 
    /// Checks for overflow and throws an exception if it occurs.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="source">The input span.</param>
    /// <param name="destination">The output span.</param>
    /// <exception cref="System.ArgumentException">Thrown when the <paramref name="source"/> and <paramref name="destination"/> spans have different lengths.</exception>
    /// <exception cref="System.OverflowException">Thrown when an overflow occurs during the decrement operation.</exception>
    public static void CheckedDecrement<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, IDecrementOperators<T>
        => Tensor.Apply<T, CheckedDecrementOperator<T>>(source, destination);
}