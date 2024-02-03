namespace NetFabric.Numerics;

readonly struct MultiplyDivideOperator<T>
    : ITernaryOperator<T, T, T, T>
    where T : struct, IMultiplyOperators<T, T, T>, IDivisionOperators<T, T, T>
{
    public static T Invoke(T x, T y, T z)
        => (x * y) / z;

    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y, ref readonly Vector<T> z)
        => (x * y) / z;
}