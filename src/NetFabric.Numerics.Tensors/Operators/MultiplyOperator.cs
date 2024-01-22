namespace NetFabric.Numerics;

readonly struct MultiplyOperator<T>
    : IBinaryOperator<T, T>
    where T : struct, IMultiplyOperators<T, T, T>
{
    public static T Invoke(T x, T y)
        => x * y;

    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => x * y;
}