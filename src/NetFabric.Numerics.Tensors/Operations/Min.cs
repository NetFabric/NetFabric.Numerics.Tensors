namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    /// <summary>
    /// Gets the minimum value of a <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source span that must implement the <see cref="INumber{T}"/> and <see cref="IMinMaxValue{T}"/> interfaces.</typeparam>
    /// <param name="source">The span of elements to get the minimum value.</param>
    /// <returns>The minimum value of the source span.</returns>
    /// <remarks>This methods follows the IEEE 754 standard for floating-point arithmetic, it returns NaN if any of the elements is NaN.</remarks>
    public static T Min<T>(ReadOnlySpan<T> source)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Tensor.Aggregate<T, MinAggregationOperator<T>>(source);

    /// <summary>
    /// Gets the minimum value of a <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source span that must implement the <see cref="INumber{T}"/> and <see cref="IMinMaxValue{T}"/> interfaces.</typeparam>
    /// <param name="source">The span of elements to get the minimum value.</param>
    /// <returns>The minimum value of the source span.</returns>
    /// <remarks>This methods does not propagate NaN.</remarks>
    public static T MinNumber<T>(ReadOnlySpan<T> source)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Tensor.AggregateNumber<T, MinNumberAggregationOperator<T>>(source);

    /// <summary>
    /// Gets the index of the first element that is the minimum value of a <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source span that must implement the <see cref="INumber{T}"/> and <see cref="IMinMaxValue{T}"/> interfaces.</typeparam>
    /// <param name="source">The span of elements to get the minimum value.</param>
    /// <returns>The index of the first element that is the minimum value of the source span.</returns>
    /// <remarks>This methods follows the IEEE 754 standard for floating-point arithmetic, it returns NaN if any of the elements is NaN.</remarks>
    public static int IndexOfMin<T>(ReadOnlySpan<T> source)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Tensor.IndexOfAggregate<T, MinAggregationOperator<T>>(source);

    /// <summary>
    /// Gets the index of the first element that is the minimum value of a <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source span that must implement the <see cref="INumber{T}"/> and <see cref="IMinMaxValue{T}"/> interfaces.</typeparam>
    /// <param name="source">The span of elements to get the minimum value.</param>
    /// <returns>The index of the first element that is the minimum value of the source span.</returns>
    /// <remarks>This methods does not propagate NaN.</remarks>
    public static int IndexOfMinNumber<T>(ReadOnlySpan<T> source)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Tensor.IndexOfAggregateNumber<T, MinNumberAggregationOperator<T>>(source);

    public static void Min<T>(ReadOnlySpan<T> left, T right, Span<T> destination)
        where T : struct, INumber<T>, IMinMaxValue<T> 
        => Tensor.Apply<T, MinOperator<T>>(left, right, destination);

    public static void Min<T>(ReadOnlySpan<T> left, (T, T) right, Span<T> destination)
        where T : struct, INumber<T>, IMinMaxValue<T> 
        => Tensor.Apply<T, MinOperator<T>>(left, right, destination);

    public static void Min<T>(ReadOnlySpan<T> left, (T, T, T) right, Span<T> destination)
        where T : struct, INumber<T>, IMinMaxValue<T> 
        => Tensor.Apply<T, MinOperator<T>>(left, right, destination);

    public static void Min<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, INumber<T>, IMinMaxValue<T> 
        => Tensor.Apply<T, MinOperator<T>>(left, right, destination);

}