namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static (T MinMagnitude, T MaxMagnitude) MinMaxMagnitude<T>(ReadOnlySpan<T> left)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Tensor.AggregatePropagateNaN2<T, MinMagnitudeAggregationOperator<T>, MaxMagnitudeAggregationOperator<T>>(left);
}