namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    /// <summary>
    /// Multiplies each element of the input span by a scalar value and divides by another scalar value,
    /// storing the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The input span.</param>
    /// <param name="y">The scalar value to multiply the elements of <paramref name="x"/> by.</param>
    /// <param name="z">The scalar value to divide the elements of <paramref name="x"/> by.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the lengths of the spans are not equal.</exception>
    public static void MultiplyDivide<T>(ReadOnlySpan<T> x, T y, T z, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>, IDivisionOperators<T, T, T>
        => Tensor.Apply<T, MultiplyDivideOperator<T>>(x, y, z, destination);

    /// <summary>
    /// Multiplies each element of the input span by a scalar tuple and divides by another scalar tuple,
    /// storing the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The input span.</param>
    /// <param name="y">The tuple values to multiply the elements of <paramref name="x"/> by.</param>
    /// <param name="z">The tuple values to divide the elements of <paramref name="x"/> by.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the lengths of the spans are not equal.</exception>
    public static void MultiplyDivide<T>(ReadOnlySpan<T> x, (T, T) y, (T, T) z, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>, IDivisionOperators<T, T, T>
        => Tensor.Apply<T, MultiplyDivideOperator<T>>(x, y, z, destination);

    /// <summary>
    /// Multiplies each element of the input span by a scalar tuple and divides by another scalar tuple,
    /// storing the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The input span.</param>
    /// <param name="y">The tuple values to multiply the elements of <paramref name="x"/> by.</param>
    /// <param name="z">The tuple values to divide the elements of <paramref name="x"/> by.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the lengths of the spans are not equal.</exception>
    public static void MultiplyDivide<T>(ReadOnlySpan<T> x, (T, T, T) y, (T, T, T) z, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>, IDivisionOperators<T, T, T>
        => Tensor.Apply<T, MultiplyDivideOperator<T>>(x, y, z, destination);

    /// <summary>
    /// Multiplies each element of the input span by a scalar tuple,
    /// and divides each element by the corresponding element in the <paramref name="z"/> span,
    /// storing the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The input span.</param>
    /// <param name="y">The scalar value to multiply the elements of <paramref name="x"/> by.</param>
    /// <param name="z">The span containing the values to divide the elements of <paramref name="x"/> by.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the lengths of the spans are not equal.</exception>
    public static void MultiplyDivide<T>(ReadOnlySpan<T> x, T y, ReadOnlySpan<T> z, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>, IDivisionOperators<T, T, T>
        => Tensor.Apply<T, MultiplyDivideOperator<T>>(x, y, z, destination);

    /// <summary>
    /// Multiplies each element of the input span by a scalar tuple,
    /// and divides each element by the corresponding element in the <paramref name="z"/> span,
    /// storing the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The input span.</param>
    /// <param name="y">The tuple values to multiply the elements of <paramref name="x"/> by.</param>
    /// <param name="z">The span containing the values to divide the elements of <paramref name="x"/> by.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the lengths of the spans are not equal.</exception>
    public static void MultiplyDivide<T>(ReadOnlySpan<T> x, (T, T) y, ReadOnlySpan<T> z, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>, IDivisionOperators<T, T, T>
        => Tensor.Apply<T, MultiplyDivideOperator<T>>(x, y, z, destination);

    /// <summary>
    /// Multiplies each element of the input span by a scalar tuple,
    /// and divides each element by the corresponding element in another span,
    /// storing the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The input span.</param>
    /// <param name="y">The tuple values to multiply the elements of <paramref name="x"/> by.</param>
    /// <param name="z">The span containing the values to divide the elements of <paramref name="x"/> by.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the lengths of the spans are not equal.</exception>
    public static void MultiplyDivide<T>(ReadOnlySpan<T> x, (T, T, T) y, ReadOnlySpan<T> z, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>, IDivisionOperators<T, T, T>
        => Tensor.Apply<T, MultiplyDivideOperator<T>>(x, y, z, destination);

    /// <summary>
    /// Multiplies each element of the input spans together,
    /// and divides each element by a scalar value,
    /// storing the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The first input span.</param>
    /// <param name="y">The second input span.</param>
    /// <param name="z">The scalar value to divide the elements by.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the lengths of the spans are not equal.</exception>
    public static void MultiplyDivide<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, T z, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>, IDivisionOperators<T, T, T>
        => Tensor.Apply<T, MultiplyDivideOperator<T>>(x, y, z, destination);

    /// <summary>
    /// Multiplies each element of the input spans together,
    /// and divides each element by a scalar tuple,
    /// storing the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The first input span.</param>
    /// <param name="y">The second input span.</param>
    /// <param name="z">The tuple values to divide the elements by.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the lengths of the spans are not equal.</exception>
    public static void MultiplyDivide<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, (T, T) z, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>, IDivisionOperators<T, T, T>
        => Tensor.Apply<T, MultiplyDivideOperator<T>>(x, y, z, destination);

    /// <summary>
    /// Multiplies each element of the input spans together,
    /// and divides each element by a scalar tuple,
    /// storing the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The first input span.</param>
    /// <param name="y">The second input span.</param>
    /// <param name="z">The tuple values to divide the elements by.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the lengths of the spans are not equal.</exception>
    public static void MultiplyDivide<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, (T, T, T) z, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>, IDivisionOperators<T, T, T>
        => Tensor.Apply<T, MultiplyDivideOperator<T>>(x, y, z, destination);

    /// <summary>
    /// Multiplies each element of two input spans and divides each element of a third input span by the corresponding elements,
    /// storing the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The first input span.</param>
    /// <param name="y">The second input span.</param>
    /// <param name="z">The third input span.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the lengths of the spans are not equal.</exception>
    public static void MultiplyDivide<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, ReadOnlySpan<T> z, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>, IDivisionOperators<T, T, T>
        => Tensor.Apply<T, MultiplyDivideOperator<T>>(x, y, z, destination);
}