namespace NetFabric.Numerics.Tensors;

public static partial class Tensor
{
    public static T Max<T>(ReadOnlySpan<T> left)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => AggregatePropagateNaN<T, MaxOperator<T>>(left);

    public static void Max<T>(ReadOnlySpan<T> left, T right, Span<T> destination)
        where T : struct, INumber<T>, IMinMaxValue<T> 
        => Apply<T, MaxPropagateNaNOperator<T>>(left, right, destination);

    public static void Max<T>(ReadOnlySpan<T> left, ValueTuple<T, T> right, Span<T> destination)
        where T : struct, INumber<T>, IMinMaxValue<T> 
        => Apply<T, MaxPropagateNaNOperator<T>>(left, right, destination);

    public static void Max<T>(ReadOnlySpan<T> left, ValueTuple<T, T, T> right, Span<T> destination)
        where T : struct, INumber<T>, IMinMaxValue<T> 
        => Apply<T, MaxPropagateNaNOperator<T>>(left, right, destination);

    public static void Max<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, INumber<T>, IMinMaxValue<T> 
        => Apply<T, MaxPropagateNaNOperator<T>>(left, right, destination);
}