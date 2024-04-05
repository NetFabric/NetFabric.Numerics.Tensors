namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    /// <summary>
    /// Computes the arc tangent of the element-wise quotient of two spans with matching dimensions.
    /// </summary>
    /// <typeparam name="T">The type of the numbers.</typeparam>
    /// <param name="x">The dividend.</param>
    /// <param name="y">The divisor.</param>
    /// <param name="destination">The span to store the results.</param>
    /// <exception cref="ArgumentException">Thrown when the destination span is not large enough to store the results.</exception>
    public static void Atan2<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> destination)
        where T : struct, IFloatingPointIeee754<T>
        => Tensor.Apply<T, Atan2Operator<T>>(x, y, destination);

    /// <summary>
    /// Computes the arc tangent of the quotient of the elements of a span and a constant.
    /// </summary>
    /// <typeparam name="T">The type of the numbers.</typeparam>
    /// <param name="x">The dividend.</param>
    /// <param name="y">The divisor.</param>
    /// <param name="destination">The span to store the results.</param>
    /// <exception cref="ArgumentException">Thrown when the destination span is not large enough to store the results.</exception>
    public static void Atan2<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, IFloatingPointIeee754<T>
        => Tensor.Apply<T, Atan2Operator<T>>(x, y, destination);

    /// <summary>
    /// Computes the arc tangent of the element-wise quotient of two spans with matching dimensions, multiplied by π.
    /// </summary>
    /// <typeparam name="T">The type of the numbers.</typeparam>
    /// <param name="x">The dividend.</param>
    /// <param name="y">The divisor.</param>
    /// <param name="destination">The span to store the results.</param>
    /// <exception cref="ArgumentException">Thrown when the destination span is not large enough to store the results.</exception>
    public static void Atan2Pi<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> destination)
        where T : struct, IFloatingPointIeee754<T>
        => Tensor.Apply<T, Atan2PiOperator<T>>(x, y, destination);

    /// <summary>
    /// Computes the arc tangent of the quotient of a specified number and a constant, multiplied by π.
    /// </summary>
    /// <typeparam name="T">The type of the numbers.</typeparam>
    /// <param name="x">The dividend.</param>
    /// <param name="y">The divisor.</param>
    /// <param name="destination">The span to store the results.</param>
    /// <exception cref="ArgumentException">Thrown when the destination span is not large enough to store the results.</exception>
    public static void Atan2Pi<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, IFloatingPointIeee754<T>
        => Tensor.Apply<T, Atan2PiOperator<T>>(x, y, destination);

    /// <summary>
    /// Determines the minimum value that is greater than each element within a tensor.
    /// </summary>
    /// <typeparam name="T">The type of the numbers.</typeparam>
    /// <param name="x">The span of numbers.</param>
    /// <param name="destination">The span to store the results.</param>
    /// <exception cref="ArgumentException">Thrown when the destination span is not large enough to store the results.</exception>
    public static void BitDecrement<T>(ReadOnlySpan<T> x, Span<T> destination)
        where T : struct, IFloatingPointIeee754<T>
        => Tensor.Apply<T, BitDecrementOperator<T>>(x, destination);

    /// <summary>
    /// Determines the maximum value that is less than each element within a tensor.
    /// </summary>
    /// <typeparam name="T">The type of the numbers.</typeparam>
    /// <param name="x">The span of numbers.</param>
    /// <param name="destination">The span to store the results.</param>
    /// <exception cref="ArgumentException">Thrown when the destination span is not large enough to store the results.</exception>
    public static void BitIncrement<T>(ReadOnlySpan<T> x, Span<T> destination)
        where T : struct, IFloatingPointIeee754<T>
        => Tensor.Apply<T, BitIncrementOperator<T>>(x, destination);

    /// <summary>
    /// Computes the remainder of element divided number by a constant.
    /// </summary>
    /// <typeparam name="T">The type of the numbers.</typeparam>
    /// <param name="x">The dividend.</param>
    /// <param name="y">The divisor.</param>
    /// <param name="destination">The span to store the results.</param>
    /// <exception cref="ArgumentException">Thrown when the destination span is not large enough to store the results.</exception>
    public static void Ieee754Remainder<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, IFloatingPointIeee754<T>
        => Tensor.Apply<T, Ieee754RemainderOperator<T>>(x, y, destination);

    /// <summary>
    /// Calculates the remainder of the element-wise division of two spans of same size.
    /// </summary>
    /// <typeparam name="T">The type of the numbers.</typeparam>
    /// <param name="x">The dividend.</param>
    /// <param name="y">The divisor.</param>
    /// <param name="destination">The span to store the results.</param>
    /// <exception cref="ArgumentException">Thrown when the destination span is not large enough to store the results.</exception>
    public static void Ieee754Remainder<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> destination)
        where T : struct, IFloatingPointIeee754<T>
        => Tensor.Apply<T, Ieee754RemainderOperator<T>>(x, y, destination);

    /// <summary>
    /// Computes the integer logarithm of each element of a span.
    /// </summary>
    /// <typeparam name="T">The type of the numbers.</typeparam>
    /// <param name="x">The numbers.</param>
    /// <param name="destination">The span to store the results.</param>
    /// <exception cref="ArgumentException">Thrown when the destination span is not large enough to store the results.</exception>
    public static void ILogB<T>(ReadOnlySpan<T> x, Span<int> destination)
        where T : struct, IFloatingPointIeee754<T>
        => Tensor.Apply<T, int, ILogBOperator<T>>(x, destination);

    /// <summary>
    /// Performs a element-wise linear interpolation of two spans and the weighting factor on a third span.
    /// </summary>
    /// <typeparam name="T">The type of the numbers.</typeparam>
    /// <param name="value1">The first value.</param>
    /// <param name="value2">The second value.</param>
    /// <param name="amount">The weighting factor.</param>
    /// <param name="destination">The span to store the results.</param>
    /// <exception cref="ArgumentException">Thrown when the destination span is not large enough to store the results.</exception>
    public static void Lerp<T>(ReadOnlySpan<T> value1, ReadOnlySpan<T> value2, ReadOnlySpan<T> amount, Span<T> destination)
        where T : struct, IFloatingPointIeee754<T>
        => Tensor.Apply<T, LerpOperator<T>>(value1, value2, amount, destination);

    /// <summary>
    /// Performs a linear interpolation between the elements of a span and a constant using a specified weighting factor.
    /// </summary>
    /// <typeparam name="T">The type of the numbers.</typeparam>
    /// <param name="value1">The first value.</param>
    /// <param name="value2">The second value.</param>
    /// <param name="amount">The weighting factor.</param>
    /// <param name="destination">The span to store the results.</param>
    /// <exception cref="ArgumentException">Thrown when the destination span is not large enough to store the results.</exception>
    public static void Lerp<T>(ReadOnlySpan<T> value1, T value2, ReadOnlySpan<T> amount, Span<T> destination)
        where T : struct, IFloatingPointIeee754<T>
        => Tensor.Apply<T, LerpOperator<T>>(value1, value2, amount, destination);

    /// <summary>
    /// Performs a element-wise linear interpolation of two spans of same size using a specified weighting factor.
    /// </summary>
    /// <typeparam name="T">The type of the numbers.</typeparam>
    /// <param name="value1">The first value.</param>
    /// <param name="value2">The second value.</param>
    /// <param name="amount">The weighting factor.</param>
    /// <param name="destination">The span to store the results.</param>
    /// <exception cref="ArgumentException">Thrown when the destination span is not large enough to store the results.</exception>
    public static void Lerp<T>(ReadOnlySpan<T> value1, ReadOnlySpan<T> value2, T amount, Span<T> destination)
        where T : struct, IFloatingPointIeee754<T>
        => Tensor.Apply<T, LerpOperator<T>>(value1, value2, amount, destination);

    /// <summary>
    /// Performs a linear interpolation between the elements of a span and a constant using a constant weighting factor.
    /// </summary>
    /// <typeparam name="T">The type of the numbers.</typeparam>
    /// <param name="value1">The first value.</param>
    /// <param name="value2">The second value.</param>
    /// <param name="amount">The weighting factor.</param>
    /// <param name="destination">The span to store the results.</param>
    /// <exception cref="ArgumentException">Thrown when the destination span is not large enough to store the results.</exception>
    public static void Lerp<T>(ReadOnlySpan<T> value1, T value2, T amount, Span<T> destination)
        where T : struct, IFloatingPointIeee754<T>
        => Tensor.Apply<T, LerpOperator<T>>(value1, value2, amount, destination);

    /// <summary>
    /// Computes the product of the elements of a span and its base-radix raised to the specified power.
    /// </summary>
    /// <typeparam name="T">The type of the numbers.</typeparam>
    /// <param name="x">The span of numbers.</param>
    /// <param name="n">The power of two.</param>
    /// <param name="destination">The span to store the results.</param>
    /// <exception cref="ArgumentException">Thrown when the destination span is not large enough to store the results.</exception>
    public static void ScaleB<T>(ReadOnlySpan<T> x, int n, Span<T> destination)
        where T : struct, IFloatingPointIeee754<T>
        => Tensor.ApplyScalar<T, T, T, MultiplyScalarOperator<T>>(x, T.CreateChecked(float.Pow(2, n)), destination);
}