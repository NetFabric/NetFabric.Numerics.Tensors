namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    /// <summary>
    /// Computes the inverse hyperbolic cosine of each element in the input span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="source">The input span.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="source"/> and <paramref name="destination"/> have different lengths.</exception>
    public static void Acosh<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, IHyperbolicFunctions<T>
        => Tensor.Apply<T, AcoshOperator<T>>(source, destination);

    /// <summary>
    /// Computes the inverse hyperbolic sine of each element in the input span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="source">The input span.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="source"/> and <paramref name="destination"/> have different lengths.</exception>
    public static void Asinh<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, IHyperbolicFunctions<T>
        => Tensor.Apply<T, AsinhOperator<T>>(source, destination);

    /// <summary>
    /// Computes the inverse hyperbolic tangent of each element in the input span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="source">The input span.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="source"/> and <paramref name="destination"/> have different lengths.</exception>
    public static void Atanh<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, IHyperbolicFunctions<T>
        => Tensor.Apply<T, AtanhOperator<T>>(source, destination);

    /// <summary>
    /// Computes the hyperbolic cosine of each element in the input span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="source">The input span.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="source"/> and <paramref name="destination"/> have different lengths.</exception>
    public static void Cosh<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, IHyperbolicFunctions<T>
        => Tensor.Apply<T, CoshOperator<T>>(source, destination);

    /// <summary>
    /// Computes the hyperbolic sine of each element in the input span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="source">The input span.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="source"/> and <paramref name="destination"/> have different lengths.</exception>
    public static void Sinh<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, IHyperbolicFunctions<T>
        => Tensor.Apply<T, SinhOperator<T>>(source, destination);

    /// <summary>
    /// Computes the hyperbolic tangent of each element in the input span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="source">The input span.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="source"/> and <paramref name="destination"/> have different lengths.</exception>
    public static void Tanh<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, IHyperbolicFunctions<T>
        => Tensor.Apply<T, TanhOperator<T>>(source, destination);
}