namespace NetFabric.Numerics;

public static partial class Tensor
{
    /// <summary>
    /// Squares each element in the source span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The source span.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="ArgumentException">Thrown when the source and destination spans have different lengths.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the type <typeparamref name="T"/> does not implement the <see cref="IMultiplyOperators{T, T, T}"/> interface.</exception> 
    public static void Square<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Apply<T, SquareOperator<T>>(left, destination);
}