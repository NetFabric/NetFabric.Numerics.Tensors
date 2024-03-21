namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static int IndexOfMax<T>(ReadOnlySpan<T> source)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Tensor.IndexOfAggregate<T, MaxAggregationOperator<T>>(source);

    public static int IndexOfMaxSum<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Tensor.IndexOfAggregate<T, SumOperator<T>, MaxAggregationOperator<T>>(left, right);

    /// <summary>
    /// Gets the maximum value of a <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source span that must implement the <see cref="INumber{T}"/> and <see cref="IMinMaxValue{T}"/> interfaces.</typeparam>
    /// <param name="source">The span of elements to get the maximum value.</param>
    /// <returns>The maximum value of the source span.</returns>
    /// <remarks>This methods follows the IEEE 754 standard for floating-point arithmetic, it returns NaN if any of the elements is NaN.</remarks>
    public static T Max<T>(ReadOnlySpan<T> source)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Tensor.AggregatePropagateNaN<T, MaxAggregationOperator<T>>(source);

    public static void Max<T>(ReadOnlySpan<T> left, T right, Span<T> destination)
        where T : struct, INumber<T>, IMinMaxValue<T> 
        => Tensor.Apply<T, MaxPropagateNaNOperator<T>>(left, right, destination);

    public static void Max<T>(ReadOnlySpan<T> left, (T, T) right, Span<T> destination)
        where T : struct, INumber<T>, IMinMaxValue<T> 
        => Tensor.Apply<T, MaxPropagateNaNOperator<T>>(left, right, destination);

    public static void Max<T>(ReadOnlySpan<T> left, (T, T, T) right, Span<T> destination)
        where T : struct, INumber<T>, IMinMaxValue<T> 
        => Tensor.Apply<T, MaxPropagateNaNOperator<T>>(left, right, destination);

    public static void Max<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, INumber<T>, IMinMaxValue<T> 
        => Tensor.Apply<T, MaxPropagateNaNOperator<T>>(left, right, destination);
}