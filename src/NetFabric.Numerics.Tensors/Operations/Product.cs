namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static T Product<T>(ReadOnlySpan<T> source)
        where T : struct, IMultiplicativeIdentity<T, T>, IMultiplyOperators<T, T, T>
        => Aggregate<T, ProductOperator<T>>(source);

    public static ValueTuple<T, T> Product2D<T>(ReadOnlySpan<T> source)
        where T : struct, IMultiplicativeIdentity<T, T>, IMultiplyOperators<T, T, T>
        => Aggregate2D<T, ProductOperator<T>>(source);

    public static ValueTuple<T, T, T> Product3D<T>(ReadOnlySpan<T> source)
        where T : struct, IMultiplicativeIdentity<T, T>, IMultiplyOperators<T, T, T>
        => Aggregate3D<T, ProductOperator<T>>(source);

    public static ValueTuple<T, T, T, T> Product4D<T>(ReadOnlySpan<T> source)
        where T : struct, IMultiplicativeIdentity<T, T>, IMultiplyOperators<T, T, T>
        => Aggregate4D<T, ProductOperator<T>>(source);
}