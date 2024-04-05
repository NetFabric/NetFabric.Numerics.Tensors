namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    /// <summary>
    /// Gets the maximum value of a <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source span that must implement the <see cref="INumber{T}"/> and <see cref="IMinMaxValue{T}"/> interfaces.</typeparam>
    /// <param name="source">The span of elements to get the maximum value.</param>
    /// <returns>The maximum value of the source span.</returns>
    /// <remarks>This methods follows the IEEE 754 standard for floating-point arithmetic, it returns NaN if any of the elements is NaN.</remarks>
    public static T Max<T>(ReadOnlySpan<T> source)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Tensor.Aggregate<T, MaxAggregationOperator<T>>(source);

    /// <summary>
    /// Gets the maximum value of a <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source span that must implement the <see cref="INumber{T}"/> and <see cref="IMinMaxValue{T}"/> interfaces.</typeparam>
    /// <param name="source">The span of elements to get the maximum value.</param>
    /// <returns>The maximum value of the source span.</returns>
    /// <remarks>This methods does not propagate NaN.</remarks>
    public static T MaxNumber<T>(ReadOnlySpan<T> source)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Tensor.AggregateNumber<T, MaxNumberAggregationOperator<T>>(source);

    /// <summary>
    /// Gets the index of the first element that is the maximum value of a <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source span that must implement the <see cref="INumber{T}"/> and <see cref="IMinMaxValue{T}"/> interfaces.</typeparam>
    /// <param name="source">The span of elements to get the maximum value.</param>
    /// <returns>The index of the first element that is the maximum value of the source span.</returns>
    /// <remarks>This methods follows the IEEE 754 standard for floating-point arithmetic, it returns NaN if any of the elements is NaN.</remarks>
    public static int IndexOfMax<T>(ReadOnlySpan<T> source)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Tensor.IndexOfAggregate<T, MaxAggregationOperator<T>>(source);

    /// <summary>
    /// Gets the index of the first element that is the maximum value of a <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source span that must implement the <see cref="INumber{T}"/> and <see cref="IMinMaxValue{T}"/> interfaces.</typeparam>
    /// <param name="source">The span of elements to get the maximum value.</param>
    /// <returns>The index of the first element that is the maximum value of the source span.</returns>
    /// <remarks>This methods does not propagate NaN.</remarks>
    public static int IndexOfMaxNumber<T>(ReadOnlySpan<T> source)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Tensor.IndexOfAggregateNumber<T, MaxNumberAggregationOperator<T>>(source);

    /// <summary>
    /// Applies the maximum operator to each element in the left span and a scalar value, storing the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source and destination spans that must implement the <see cref="INumber{T}"/> and <see cref="IMinMaxValue{T}"/> interfaces.</typeparam>
    /// <param name="left">The span of elements to apply the maximum operator to.</param>
    /// <param name="right">The scalar value to compare against each element in the left span.</param>
    /// <param name="destination">The span to store the result of the maximum operator.</param>
    public static void Max<T>(ReadOnlySpan<T> left, T right, Span<T> destination)
        where T : struct, INumber<T>, IMinMaxValue<T> 
        => Tensor.Apply<T, MaxOperator<T>>(left, right, destination);

    /// <summary>
    /// Applies the maximum operator to each element in the left span and a tuple of scalar values, storing the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source and destination spans that must implement the <see cref="INumber{T}"/> and <see cref="IMinMaxValue{T}"/> interfaces.</typeparam>
    /// <param name="left">The span of elements to apply the maximum operator to.</param>
    /// <param name="right">The tuple of scalar values to compare against each element in the left span.</param>
    /// <param name="destination">The span to store the result of the maximum operator.</param>
    public static void Max<T>(ReadOnlySpan<T> left, (T, T) right, Span<T> destination)
        where T : struct, INumber<T>, IMinMaxValue<T> 
        => Tensor.Apply<T, MaxOperator<T>>(left, right, destination);

    /// <summary>
    /// Applies the maximum operator to each element in the left span and a tuple of scalar values, storing the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source and destination spans that must implement the <see cref="INumber{T}"/> and <see cref="IMinMaxValue{T}"/> interfaces.</typeparam>
    /// <param name="left">The span of elements to apply the maximum operator to.</param>
    /// <param name="right">The tuple of scalar values to compare against each element in the left span.</param>
    /// <param name="destination">The span to store the result of the maximum operator.</param>
    public static void Max<T>(ReadOnlySpan<T> left, (T, T, T) right, Span<T> destination)
        where T : struct, INumber<T>, IMinMaxValue<T> 
        => Tensor.Apply<T, MaxOperator<T>>(left, right, destination);

    /// <summary>
    /// Applies the maximum operator to each element in the left span and the corresponding element in the right span, storing the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source and destination spans that must implement the <see cref="INumber{T}"/> and <see cref="IMinMaxValue{T}"/> interfaces.</typeparam>
    /// <param name="left">The span of elements to apply the maximum operator to.</param>
    /// <param name="right">The span of elements to compare against each element in the left span.</param>
    /// <param name="destination">The span to store the result of the maximum operator.</param>
    public static void Max<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, INumber<T>, IMinMaxValue<T> 
        => Tensor.Apply<T, MaxOperator<T>>(left, right, destination);
}