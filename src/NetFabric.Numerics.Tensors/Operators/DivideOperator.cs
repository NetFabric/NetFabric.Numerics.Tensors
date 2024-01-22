namespace NetFabric.Numerics;

readonly struct DivideOperator<T>
    : IBinaryOperator<T, T>
    where T : struct, IDivisionOperators<T, T, T>
{
    public static T Invoke(T x, T y)
        => x / y;

    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => x / y;
}