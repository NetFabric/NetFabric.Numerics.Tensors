namespace NetFabric.Numerics;

readonly struct ProductOperator<T>
    : IAggregationOperator<T, T>
    where T : struct, IMultiplicativeIdentity<T, T>, IMultiplyOperators<T, T, T>
{
    public static T Identity 
        => T.MultiplicativeIdentity;

    public static T Invoke(T x, T y)
        => x * y;

    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => x * y;
}