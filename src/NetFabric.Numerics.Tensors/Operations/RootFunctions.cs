namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    /// <summary>
    /// Calculates the cube root of each element in the source span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The source span.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="ArgumentException">Thrown when the source and destination spans have different lengths.</exception>
    public static void Cbrt<T>(ReadOnlySpan<T> x, Span<T> destination)
        where T : struct, IRootFunctions<T>
        => Tensor.Apply<T, CbrtOperator<T>>(x, destination);

    /// <summary>
    /// Calculates the hypotenuse of each pair of corresponding elements in the source spans and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The first source span.</param>
    /// <param name="y">The second source span.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="ArgumentException">Thrown when the source and destination spans have different lengths.</exception>
    public static void Hypot<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> destination)
        where T : struct, IRootFunctions<T>
        => Tensor.Apply<T, HypotOperator<T>>(x, y, destination);

    /// <summary>
    /// Calculates the n-th root of each element in the source span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The source span.</param>
    /// <param name="n">The exponent.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="ArgumentException">Thrown when the source and destination spans have different lengths.</exception>
    public static void RootN<T>(ReadOnlySpan<T> x, int n, Span<T> destination)
        where T : struct, IRootFunctions<T>
        => Tensor.ApplyScalar<T, int, T, RootNOperator<T>>(x, n, destination);

    /// <summary>
    /// Calculates the square root of each element in the source span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The source span.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="ArgumentException">Thrown when the source and destination spans have different lengths.</exception>
    public static void Sqrt<T>(ReadOnlySpan<T> x, Span<T> destination)
        where T : struct, IRootFunctions<T>
        => Tensor.Apply<T, SqrtOperator<T>>(x, destination);
}