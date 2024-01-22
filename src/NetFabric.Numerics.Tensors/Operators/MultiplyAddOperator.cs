namespace NetFabric.Numerics;

readonly struct MultiplyAddOperator<T>
    : ITernaryOperator<T, T>
    where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
{
    public static T Invoke(T x, T y, T z)
        => (x * y) + z;

    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y, ref readonly Vector<T> z)
        => (x * y) + z;
}