namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    /// <summary>
    /// Computes the maximum sum of corresponding elements in two spans.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The first span.</param>
    /// <param name="right">The second span.</param>
    /// <returns>The maximum sum of corresponding elements in the spans.</returns>
    /// <remarks>
    /// This methods follows the IEEE 754 standard for floating-point arithmetic, it returns NaN if the transformation and aggregation of any of the elements result in NaN.
    /// </remarks>
    public static T MaxSum<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Tensor.Aggregate<T, SumOperator<T>, MaxAggregationOperator<T>>(left, right);

    /// <summary>
    /// Computes the maximum sum of corresponding elements in two spans.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The first span.</param>
    /// <param name="right">The second span.</param>
    /// <returns>The number of elements involved in the maximum sum of corresponding elements in the spans.</returns>
    /// <remarks>This methods does not propagate NaN.</remarks>
    public static T MaxSumNumber<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Tensor.AggregateNumber<T, SumOperator<T>, MaxNumberAggregationOperator<T>>(left, right);

    /// <summary>
    /// Computes the index of the maximum sum of corresponding elements in two spans.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The first span.</param>
    /// <param name="right">The second span.</param>
    /// <returns>The index of the maximum sum of corresponding elements in the spans.</returns>
    /// <remarks>
    /// This methods follows the IEEE 754 standard for floating-point arithmetic, it returns NaN if the transformation and aggregation of any of the elements result in NaN.
    /// </remarks>
    public static int IndexOfMaxSum<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Tensor.IndexOfAggregate<T, SumOperator<T>, MaxAggregationOperator<T>>(left, right);

    /// <summary>
    /// Computes the index of the maximum sum of corresponding elements in two spans.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The first span.</param>
    /// <param name="right">The second span.</param>
    /// <returns>The number of elements involved in the maximum sum of corresponding elements in the spans.</returns>
    /// <remarks>This methods does not propagate NaN.</remarks>
    public static int IndexOfMaxSumNumber<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Tensor.IndexOfAggregateNumber<T, SumOperator<T>, MaxNumberAggregationOperator<T>>(left, right);
}