namespace NetFabric.Numerics;

public readonly struct SquareRootOperator<T>
    : IUnaryOperator<T>
    where T : struct, IRootFunctions<T>
{
    public static T Invoke(T x)
        => T.Sqrt(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Vector.SquareRoot(x);
}