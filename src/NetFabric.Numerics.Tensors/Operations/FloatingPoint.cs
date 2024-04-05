namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    /// <summary>
    /// Computes the largest integer less than or equal to each element of a specified tensor.
    /// </summary>
    /// <typeparam name="T">The type of the tensor elements.</typeparam>
    /// <param name="source">The tensor to compute the floor of.</param>
    /// <param name="destination">The tensor to store the result in.</param>
    /// <remarks>
    /// This method applies the floor operation to each element of the <paramref name="source"/> tensor
    /// and stores the result in the <paramref name="destination"/> tensor.
    /// </remarks>
    public static void Floor<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, IFloatingPoint<T>
        => Tensor.Apply<T, FloorOperator<T>>(source, destination);

    /// <summary>
    /// Computes the largest integer less than or equal to each element of a specified tensor of single-precision floating-point numbers.
    /// </summary>
    /// <param name="source">The tensor to compute the floor of.</param>
    /// <param name="destination">The tensor to store the result in.</param>
    /// <remarks>
    /// This method applies the floor operation to each element of the <paramref name="source"/> tensor
    /// and stores the result in the <paramref name="destination"/> tensor.
    /// </remarks>
    public static void Floor(ReadOnlySpan<float> source, Span<float> destination)
        => Tensor.Apply<float, FloorSingleOperator>(source, destination);

    /// <summary>
    /// Computes the largest integer less than or equal to each element of a specified tensor of double-precision floating-point numbers.
    /// </summary>
    /// <param name="source">The tensor to compute the floor of.</param>
    /// <param name="destination">The tensor to store the result in.</param>
    /// <remarks>
    /// This method applies the floor operation to each element of the <paramref name="source"/> tensor
    /// and stores the result in the <paramref name="destination"/> tensor.
    /// </remarks>
    public static void Floor(ReadOnlySpan<double> source, Span<double> destination)
        => Tensor.Apply<double, FloorDoubleOperator>(source, destination);

    /// <summary>
    /// Computes the smallest integer greater than or equal to each element of a specified tensor.
    /// </summary>
    /// <typeparam name="T">The type of the tensor elements.</typeparam>
    /// <param name="source">The tensor to compute the ceiling of.</param>
    /// <param name="destination">The tensor to store the result in.</param>
    public static void Ceiling<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, IFloatingPoint<T>
        => Tensor.Apply<T, CeilingOperator<T>>(source, destination);

    /// <summary>
    /// Computes the smallest integer greater than or equal to each element of a specified tensor of single-precision floating-point numbers.
    /// </summary>
    /// <param name="source">The tensor to compute the ceiling of.</param>
    /// <param name="destination">The tensor to store the result in.</param>
    public static void Ceiling(ReadOnlySpan<float> source, Span<float> destination)
        => Tensor.Apply<float, CeilingSingleOperator>(source, destination);

    /// <summary>
    /// Computes the smallest integer greater than or equal to each element of a specified tensor of double-precision floating-point numbers.
    /// </summary>
    /// <param name="source">The tensor to compute the ceiling of.</param>
    /// <param name="destination">The tensor to store the result in.</param>
    public static void Ceiling(ReadOnlySpan<double> source, Span<double> destination)
        => Tensor.Apply<double, CeilingDoubleOperator>(source, destination);

    /// <summary>
    /// Rounds each element of a specified tensor to the nearest integral value.
    /// </summary>
    /// <typeparam name="T">The type of the tensor elements.</typeparam>
    /// <param name="source">The tensor to round.</param>
    /// <param name="destination">The tensor to store the result in.</param>
    public static void Round<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, IFloatingPoint<T>
        => Tensor.Apply<T, RoundOperator<T>>(source, destination);

    /// <summary>
    /// Rounds each element of a specified tensor to a specified number of fractional digits.
    /// </summary>
    /// <typeparam name="T">The type of the tensor elements.</typeparam>
    /// <param name="source">The tensor to round.</param>
    /// <param name="digits">The number of fractional digits to round to.</param>
    /// <param name="destination">The tensor to store the result in.</param>
    public static void Round<T>(ReadOnlySpan<T> source, int digits, Span<T> destination)
        where T : struct, IFloatingPoint<T>
        => Tensor.ApplyScalar<T, int, T, RoundDigitsOperator<T>>(source, digits, destination);

    /// <summary>
    /// Rounds each element of a specified tensor to the nearest integral value, using the specified rounding convention.
    /// </summary>
    /// <typeparam name="T">The type of the tensor elements.</typeparam>
    /// <param name="source">The tensor to round.</param>
    /// <param name="mode">The rounding convention to use.</param>
    /// <param name="destination">The tensor to store the result in.</param>
    public static void Round<T>(ReadOnlySpan<T> source, MidpointRounding mode, Span<T> destination)
        where T : struct, IFloatingPoint<T>
        => Tensor.ApplyScalar<T, MidpointRounding, T, RoundModeOperator<T>>(source, mode, destination);

    /// <summary>
    /// Rounds each element of a specified tensor to a specified number of fractional digits, using the specified rounding convention.
    /// </summary>
    /// <typeparam name="T">The type of the tensor elements.</typeparam>
    /// <param name="source">The tensor to round.</param>
    /// <param name="digits">The number of fractional digits to round to.</param>
    /// <param name="mode">The rounding convention to use.</param>
    /// <param name="destination">The tensor to store the result in.</param>
    public static void Round<T>(ReadOnlySpan<T> source, int digits, MidpointRounding mode, Span<T> destination)
        where T : struct, IFloatingPoint<T>
        => Tensor.ApplyScalar<T, (int digits, MidpointRounding mode), T, RoundDigitModeOperator<T>>(source, (digits, mode), destination);

    /// <summary>
    /// Computes the integral part of each element of a specified tensor.
    /// </summary>
    /// <typeparam name="T">The type of the tensor elements.</typeparam>
    /// <param name="source">The tensor to truncate.</param>
    /// <param name="destination">The tensor to store the result in.</param>
    public static void Truncate<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, IFloatingPoint<T>
        => Tensor.Apply<T, TruncateOperator<T>>(source, destination);
}