namespace NetFabric.Numerics;

readonly struct AddOperator<T>
    : IBinaryOperator<T, T>
    where T : struct, IAdditionOperators<T, T, T>
{
    public static T Invoke(T x, T y)
        => x + y;

    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => x + y;
}