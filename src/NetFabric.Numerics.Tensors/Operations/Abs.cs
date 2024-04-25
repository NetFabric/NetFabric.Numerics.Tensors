namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static void Abs<T>(T[] source, T[] destination)
        where T : struct, INumberBase<T>
        => Tensor.Apply<T, AbsOperator<T>>(source, destination);

    public static void Abs<T>(ReadOnlyMemory<T> source, Memory<T> destination)
        where T : struct, INumberBase<T>
        => Tensor.Apply<T, AbsOperator<T>>(source, destination);

    /// <summary>
    /// Computes the absolute value of each element in the source span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="source">The source span.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="ArgumentException">Thrown when the source and destination spans have different lengths.</exception>
    public static void Abs<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, INumberBase<T>
        => Tensor.Apply<T, AbsOperator<T>>(source, destination);
}