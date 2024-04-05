namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    /// <summary>
    /// Increments each element in the <paramref name="left"/> span and stores the result in the <paramref name="destination"/> span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The input span.</param>
    /// <param name="destination">The output span.</param>
    /// <exception cref="ArgumentException">Thrown when the lengths of the input and output spans are not equal.</exception>
    public static void Increment<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, IIncrementOperators<T>
        => Tensor.Apply<T, IncrementOperator<T>>(left, destination);

    /// <summary>
    /// Increments each element in the <paramref name="left"/> span and stores the result in the <paramref name="destination"/> span, performing overflow checking.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The input span.</param>
    /// <param name="destination">The output span.</param>
    /// <exception cref="ArgumentException">Thrown when the lengths of the input and output spans are not equal.</exception>
    /// <exception cref="OverflowException">Thrown when the increment operation results in an overflow.</exception>
    public static void CheckedIncrement<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, IIncrementOperators<T>
        => Tensor.Apply<T, CheckedIncrementOperator<T>>(left, destination);
}