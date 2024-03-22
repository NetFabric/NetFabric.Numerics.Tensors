namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static T MaxSum<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Tensor.Aggregate<T, SumOperator<T>, MaxAggregationOperator<T>>(left, right);

    public static T MaxSumNumber<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Tensor.AggregateNumber<T, SumOperator<T>, MaxNumberAggregationOperator<T>>(left, right);

    public static int IndexOfMaxSum<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Tensor.IndexOfAggregate<T, SumOperator<T>, MaxAggregationOperator<T>>(left, right);

    public static int IndexOfMaxSumNumber<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Tensor.IndexOfAggregateNumber<T, SumOperator<T>, MaxNumberAggregationOperator<T>>(left, right);
}