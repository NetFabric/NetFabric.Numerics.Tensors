namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static T Min<T>(ReadOnlySpan<T> left)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Tensor.AggregatePropagateNaN<T, MinAggregationOperator<T>>(left);

    public static void Min<T>(ReadOnlySpan<T> left, T right, Span<T> destination)
        where T : struct, INumber<T>, IMinMaxValue<T> 
        => Tensor.Apply<T, MinPropagateNaNOperator<T>>(left, right, destination);

    public static void Min<T>(ReadOnlySpan<T> left, ValueTuple<T, T> right, Span<T> destination)
        where T : struct, INumber<T>, IMinMaxValue<T> 
        => Tensor.Apply<T, MinPropagateNaNOperator<T>>(left, right, destination);

    public static void Min<T>(ReadOnlySpan<T> left, ValueTuple<T, T, T> right, Span<T> destination)
        where T : struct, INumber<T>, IMinMaxValue<T> 
        => Tensor.Apply<T, MinPropagateNaNOperator<T>>(left, right, destination);

    public static void Min<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, INumber<T>, IMinMaxValue<T> 
        => Tensor.Apply<T, MinPropagateNaNOperator<T>>(left, right, destination);

}