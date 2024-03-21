namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static int IndexOfMinMagnitudeNumber<T>(ReadOnlySpan<T> source)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Tensor.IndexOfAggregate<T, MinMagnitudeAggregationOperator<T>>(source);

    /// <summary>
    /// Gets the minimum magnitude value of a <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source span that must implement the <see cref="INumber{T}"/> and <see cref="IMinMaxValue{T}"/> interfaces.</typeparam>
    /// <param name="source">The span of elements to get the minimum magnitude value.</param>
    /// <returns>The minimum magnitude value of the source span.</returns>
    /// <remarks>This methods follows the IEEE 754 standard for floating-point arithmetic, it returns NaN if any of the elements is NaN.</remarks>
    public static T MinMagnitude<T>(ReadOnlySpan<T> source)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Tensor.AggregatePropagateNaN<T, MinMagnitudeAggregationOperator<T>>(source);

    public static void MinMagnitude<T>(ReadOnlySpan<T> left, T right, Span<T> destination)
        where T : struct, INumber<T>, IMinMaxValue<T> 
        => Tensor.Apply<T, MinMagnitudePropagateNaNOperator<T>>(left, right, destination);

    public static void MinMagnitude<T>(ReadOnlySpan<T> left, (T, T) right, Span<T> destination)
        where T : struct, INumber<T>, IMinMaxValue<T> 
        => Tensor.Apply<T, MinMagnitudePropagateNaNOperator<T>>(left, right, destination);

    public static void MinMagnitude<T>(ReadOnlySpan<T> left, (T, T, T) right, Span<T> destination)
        where T : struct, INumber<T>, IMinMaxValue<T> 
        => Tensor.Apply<T, MinMagnitudePropagateNaNOperator<T>>(left, right, destination);

    public static void MinMagnitude<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, INumber<T>, IMinMaxValue<T> 
        => Tensor.Apply<T, MinMagnitudePropagateNaNOperator<T>>(left, right, destination);
}