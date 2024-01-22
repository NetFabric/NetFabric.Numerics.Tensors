namespace NetFabric.Numerics;

readonly struct SquareOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, IMultiplyOperators<T, T, T>
{
    public static T Invoke(T x)
        => x * x;

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => x * x;
}