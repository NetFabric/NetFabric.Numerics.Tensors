namespace NetFabric.Numerics;

readonly struct XorOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, IBitwiseOperators<T, T, T>
{
    public static T Invoke(T x, T y)
        => x ^ y;

    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => Vector.Xor(x, y);
}