namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static void Negate<T>(T[] value, T[] destination)
        where T : struct, IUnaryNegationOperators<T, T>
        => Tensor.Apply<T, NegateOperator<T>>(value, destination);

    public static void Negate<T>(ReadOnlyMemory<T> value, Memory<T> destination)
        where T : struct, IUnaryNegationOperators<T, T>
        => Tensor.Apply<T, NegateOperator<T>>(value, destination);

    /// <summary>
    /// Negates the elements in the source span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="value">The source span.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="ArgumentException">Thrown when the source and destination spans have different lengths.</exception>
    public static void Negate<T>(ReadOnlySpan<T> value, Span<T> destination)
        where T : struct, IUnaryNegationOperators<T, T>
        => Tensor.Apply<T, NegateOperator<T>>(value, destination);

    public static void CheckedNegate<T>(T[] value, T[] destination)
        where T : struct, IUnaryNegationOperators<T, T>
        => Tensor.Apply<T, CheckedNegateOperator<T>>(value, destination);

    public static void CheckedNegate<T>(ReadOnlyMemory<T> value, Memory<T> destination)
        where T : struct, IUnaryNegationOperators<T, T>
        => Tensor.Apply<T, CheckedNegateOperator<T>>(value, destination);


    /// <summary>
    /// Negates the elements in the source span and stores the result in the destination span, performing checked arithmetic.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="value">The source span.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="ArgumentException">Thrown when the source and destination spans have different lengths.</exception>
    /// <exception cref="OverflowException">Thrown when the negation operation results in an overflow.</exception>
    public static void CheckedNegate<T>(ReadOnlySpan<T> value, Span<T> destination)
        where T : struct, IUnaryNegationOperators<T, T>
        => Tensor.Apply<T, CheckedNegateOperator<T>>(value, destination);
}