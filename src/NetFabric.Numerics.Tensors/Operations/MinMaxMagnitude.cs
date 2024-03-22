namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    /// <summary>
    /// Gets the minimum and maximum magnitude value of a <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source span that must implement the <see cref="INumber{T}"/> and <see cref="IMinMaxValue{T}"/> interfaces.</typeparam>
    /// <param name="source">The span of elements to get the minimum and maximum magnitude value.</param>
    /// <returns>The minimum and maximum magnitude value of the source span.</returns>
    /// <remarks>This methods follows the IEEE 754 standard for floating-point arithmetic, it returns NaN if any of the elements is NaN.</remarks>
    public static (T MinMagnitude, T MaxMagnitude) MinMaxMagnitude<T>(ReadOnlySpan<T> source)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Tensor.Aggregate2<T, MinMagnitudeNumberAggregationOperator<T>, MaxMagnitudeNumberAggregationOperator<T>>(source);
}