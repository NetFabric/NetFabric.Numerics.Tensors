namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    /// <summary>
    /// Calculates the exponential function for each element in the input span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="source">The input span.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="System.ArgumentException">Thrown when the input and destination spans have different lengths.</exception>
    public static void Exp<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, IExponentialFunctions<T>
        => Tensor.Apply<T, ExpOperator<T>>(source, destination);

    /// <summary>
    /// Calculates the exponential minus one function for each element in the input span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="source">The input span.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="System.ArgumentException">Thrown when the input and destination spans have different lengths.</exception>
    public static void ExpM1<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, IExponentialFunctions<T>
        => Tensor.Apply<T, ExpM1Operator<T>>(source, destination);

    /// <summary>
    /// Calculates the base 2 exponential function for each element in the input span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="source">The input span.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="System.ArgumentException">Thrown when the input and destination spans have different lengths.</exception>
    public static void Exp2<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, IExponentialFunctions<T>
        => Tensor.Apply<T, Exp2Operator<T>>(source, destination);

    /// <summary>
    /// Calculates the base 2 exponential minus one function for each element in the input span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="source">The input span.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="System.ArgumentException">Thrown when the input and destination spans have different lengths.</exception>
    public static void Exp2M1<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, IExponentialFunctions<T>
        => Tensor.Apply<T, Exp2M1Operator<T>>(source, destination);

    /// <summary>
    /// Calculates the base 10 exponential function for each element in the input span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="source">The input span.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="System.ArgumentException">Thrown when the input and destination spans have different lengths.</exception>
    public static void Exp10<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, IExponentialFunctions<T>
        => Tensor.Apply<T, Exp10Operator<T>>(source, destination);

    /// <summary>
    /// Calculates the base 10 exponential minus one function for each element in the input span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="source">The input span.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="System.ArgumentException">Thrown when the input and destination spans have different lengths.</exception>
    public static void Exp10M1<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, IExponentialFunctions<T>
        => Tensor.Apply<T, Exp10M1Operator<T>>(source, destination);

    /// <summary>
    /// Calculates the sigmoid function for each element in the input span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="source">The input span.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="System.ArgumentException">Thrown when the input and destination spans have different lengths.</exception>
    public static void Sigmoid<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, IExponentialFunctions<T>
        => Tensor.Apply<T, SigmoidOperator<T>>(source, destination);
}