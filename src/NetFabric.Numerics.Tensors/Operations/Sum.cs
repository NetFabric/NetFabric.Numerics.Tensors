namespace NetFabric.Numerics.Tensors;

public static partial class Tensor
{
    public static T Sum<T>(ReadOnlySpan<T> source)
        where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
        => Aggregate<T, SumOperator<T>>(source);

    public static ValueTuple<T, T> Sum2D<T>(ReadOnlySpan<T> source)
        where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
        => Aggregate2D<T, SumOperator<T>>(source);

    public static ValueTuple<T, T, T> Sum3D<T>(ReadOnlySpan<T> source)
        where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
        => Aggregate3D<T, SumOperator<T>>(source);

    public static ValueTuple<T, T, T, T> Sum4D<T>(ReadOnlySpan<T> source)
        where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
        => Aggregate4D<T, SumOperator<T>>(source);
}