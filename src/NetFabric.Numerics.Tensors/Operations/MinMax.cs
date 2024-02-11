namespace NetFabric.Numerics.Tensors;

public static partial class Tensor
{
    public static (T Min, T Max) MinMax<T>(ReadOnlySpan<T> left)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => AggregatePropagateNaN2<T, MinOperator<T>, MaxOperator<T>>(left);
}