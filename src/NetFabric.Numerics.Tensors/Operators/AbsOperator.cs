namespace NetFabric.Numerics;

public readonly struct AbsOperator<T>
    : IUnaryOperator<T>
    where T : struct, INumberBase<T>
{
    public static T Invoke(T x)
        => T.Abs(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Vector.Abs(x);
}