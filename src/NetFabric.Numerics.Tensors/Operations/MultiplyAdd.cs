namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    /// <summary>
    /// Multiplies each element of the input span by a scalar value and adds another scalar value, storing the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The input span.</param>
    /// <param name="y">The scalar value to multiply each element of the input span by.</param>
    /// <param name="z">The scalar value to add to each element of the input span after multiplication.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the input spans and the destination span have different lengths.</exception>
    public static void MultiplyAdd<T>(ReadOnlySpan<T> x, T y, T z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, MultiplyAddOperator<T>>(x, y, z, destination);

    /// <summary>
    /// Multiplies each element of the input span by a tuple of scalar values and adds another tuple of scalar values, storing the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The input span.</param>
    /// <param name="y">The tuple of scalar values to multiply each element of the input span by.</param>
    /// <param name="z">The tuple of scalar values to add to each element of the input span after multiplication.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the input spans and the destination span have different lengths.</exception>
    public static void MultiplyAdd<T>(ReadOnlySpan<T> x, (T, T) y, (T, T) z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, MultiplyAddOperator<T>>(x, y, z, destination);

    /// <summary>
    /// Multiplies each element of the input span by a tuple of scalar values and adds another tuple of scalar values, storing the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The input span.</param>
    /// <param name="y">The tuple of scalar values to multiply each element of the input span by.</param>
    /// <param name="z">The tuple of scalar values to add to each element of the input span after multiplication.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the input spans and the destination span have different lengths.</exception>
    public static void MultiplyAdd<T>(ReadOnlySpan<T> x, (T, T, T) y, (T, T, T) z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, MultiplyAddOperator<T>>(x, y, z, destination);

    /// <summary>
    /// Multiplies each element of the input span by a scalar value and adds the corresponding element from another span, storing the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The input span.</param>
    /// <param name="y">The scalar value to multiply each element of the input span by.</param>
    /// <param name="z">The span containing the values to add to each element of the input span after multiplication.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the input spans and the destination span have different lengths.</exception>
    public static void MultiplyAdd<T>(ReadOnlySpan<T> x, T y, ReadOnlySpan<T> z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, MultiplyAddOperator<T>>(x, y, z, destination);

    /// <summary>
    /// Multiplies each element of the input span by a tuple of scalar values and adds the corresponding element from another span, storing the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The input span.</param>
    /// <param name="y">The tuple of scalar values to multiply each element of the input span by.</param>
    /// <param name="z">The span containing the values to add to each element of the input span after multiplication.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the input spans and the destination span have different lengths.</exception>
    public static void MultiplyAdd<T>(ReadOnlySpan<T> x, (T, T) y, ReadOnlySpan<T> z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, MultiplyAddOperator<T>>(x, y, z, destination);

    /// <summary>
    /// Multiplies each element of the input span by a tuple of scalar values and adds the corresponding element from another span, storing the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The input span.</param>
    /// <param name="y">The tuple of scalar values to multiply each element of the input span by.</param>
    /// <param name="z">The span containing the values to add to each element of the input span after multiplication.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the input spans and the destination span have different lengths.</exception>
    public static void MultiplyAdd<T>(ReadOnlySpan<T> x, (T, T, T) y, ReadOnlySpan<T> z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, MultiplyAddOperator<T>>(x, y, z, destination);

    /// <summary>
    /// Multiplies each element of the input span by a scalar value and adds the corresponding elements from two other spans, storing the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The input span.</param>
    /// <param name="y">The span containing the values to multiply each element of the input span by.</param>
    /// <param name="z">The scalar value to add to each element of the input span after multiplication.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the input spans and the destination span have different lengths.</exception>
    public static void MultiplyAdd<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, T z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, MultiplyAddOperator<T>>(x, y, z, destination);

    /// <summary>
    /// Multiplies each element of the input span by the corresponding elements from two other spans and adds a tuple of scalar values, storing the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The input span.</param>
    /// <param name="y">The span containing the values to multiply each element of the input span by.</param>
    /// <param name="z">The tuple of scalar values to add to each element of the input span after multiplication.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the input spans and the destination span have different lengths.</exception>
    public static void MultiplyAdd<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, (T, T) z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, MultiplyAddOperator<T>>(x, y, z, destination);

    /// <summary>
    /// Multiplies each element of the input span by the corresponding elements from two other spans and adds a tuple of scalar values, storing the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The input span.</param>
    /// <param name="y">The span containing the values to multiply each element of the input span by.</param>
    /// <param name="z">The tuple of scalar values to add to each element of the input span after multiplication.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the input spans and the destination span have different lengths.</exception>
    public static void MultiplyAdd<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, (T, T, T) z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, MultiplyAddOperator<T>>(x, y, z, destination);

    /// <summary>
    /// Multiplies each element of the input span by the corresponding elements from two other spans and adds the corresponding elements from a third span, storing the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The input span.</param>
    /// <param name="y">The span containing the values to multiply each element of the input span by.</param>
    /// <param name="z">The span containing the values to add to each element of the input span after multiplication.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the input spans and the destination span have different lengths.</exception>
    public static void MultiplyAdd<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, ReadOnlySpan<T> z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, MultiplyAddOperator<T>>(x, y, z, destination);
}