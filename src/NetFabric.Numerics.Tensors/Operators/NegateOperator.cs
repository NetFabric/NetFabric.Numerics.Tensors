namespace NetFabric.Numerics;

readonly struct NegateOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, IUnaryNegationOperators<T, T>
{
    public static T Invoke(T x)
        => -x;

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => -x;
}