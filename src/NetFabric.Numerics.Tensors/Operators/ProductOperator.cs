namespace NetFabric.Numerics.Tensors.Operators;

public readonly struct ProductOperator<T>
    : IAggregationOperator<T, T>
    where T : struct, IMultiplicativeIdentity<T, T>, IMultiplyOperators<T, T, T>
{
    public static T Seed 
        => T.MultiplicativeIdentity;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => x * y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => x * y;
}