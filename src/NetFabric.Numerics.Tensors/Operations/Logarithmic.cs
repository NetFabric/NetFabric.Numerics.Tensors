namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    /// <summary>
    /// Computes the logarithm of each element in the <paramref name="source"/> span and stores the result in the <paramref name="destination"/> span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="source">The input span.</param>
    /// <param name="destination">The output span.</param>
    /// <exception cref="ArgumentException">Thrown when the input and output spans have different lengths.</exception>
    public static void Log<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, ILogarithmicFunctions<T>
        => Tensor.Apply<T, LogOperator<T>>(source, destination);

    /// <summary>
    /// Computes the logarithm of each element in the <paramref name="source"/> span with the specified <paramref name="newBase"/> and stores the result in the <paramref name="destination"/> span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="source">The input span.</param>
    /// <param name="newBase">The base of the logarithm.</param>
    /// <param name="destination">The output span.</param>
    /// <exception cref="ArgumentException">Thrown when the input and output spans have different lengths.</exception>
    public static void Log<T>(ReadOnlySpan<T> source, T newBase, Span<T> destination)
        where T : struct, ILogarithmicFunctions<T>
        => Tensor.ApplyScalar<T, T, T, LogBaseOperator<T>>(source, newBase, destination);

    /// <summary>
    /// Computes the natural logarithm of each element in the <paramref name="source"/> span plus one and stores the result in the <paramref name="destination"/> span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="source">The input span.</param>
    /// <param name="destination">The output span.</param>
    /// <exception cref="ArgumentException">Thrown when the input and output spans have different lengths.</exception>
    public static void LogP1<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, ILogarithmicFunctions<T>
        => Tensor.Apply<T, LogP1Operator<T>>(source, destination);

    /// <summary>
    /// Computes the base-2 logarithm of each element in the <paramref name="source"/> span and stores the result in the <paramref name="destination"/> span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="source">The input span.</param>
    /// <param name="destination">The output span.</param>
    /// <exception cref="ArgumentException">Thrown when the input and output spans have different lengths.</exception>
    public static void Log2<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, ILogarithmicFunctions<T>
        => Tensor.Apply<T, Log2Operator<T>>(source, destination);

    /// <summary>
    /// Computes the base-2 logarithm of each element in the <paramref name="source"/> span plus one and stores the result in the <paramref name="destination"/> span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="source">The input span.</param>
    /// <param name="destination">The output span.</param>
    /// <exception cref="ArgumentException">Thrown when the input and output spans have different lengths.</exception>
    public static void Log2P1<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, ILogarithmicFunctions<T>
        => Tensor.Apply<T, Log2P1Operator<T>>(source, destination);

    /// <summary>
    /// Computes the base-10 logarithm of each element in the <paramref name="source"/> span and stores the result in the <paramref name="destination"/> span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="source">The input span.</param>
    /// <param name="destination">The output span.</param>
    /// <exception cref="ArgumentException">Thrown when the input and output spans have different lengths.</exception>
    public static void Log10<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, ILogarithmicFunctions<T>
        => Tensor.Apply<T, Log10Operator<T>>(source, destination);

    /// <summary>
    /// Computes the base-10 logarithm of each element in the <paramref name="source"/> span plus one and stores the result in the <paramref name="destination"/> span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="source">The input span.</param>
    /// <param name="destination">The output span.</param>
    /// <exception cref="ArgumentException">Thrown when the input and output spans have different lengths.</exception>
    public static void Log10P1<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, ILogarithmicFunctions<T>
        => Tensor.Apply<T, Log10P1Operator<T>>(source, destination);
}