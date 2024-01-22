namespace NetFabric.Numerics;

readonly struct MaxOperator<T>
    : IBinaryOperator<T, T>
    where T : struct, INumberBase<T>
{
    public static T Invoke(T x, T y)
        => T.MaxMagnitude(x, y);

    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => Vector.Max(x, y);
}