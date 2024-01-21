namespace NetFabric.Numerics;

public readonly struct MinOperator<T>
    : IBinaryOperator<T>
    where T : struct, INumberBase<T>
{
    public static T Invoke(T x, T y)
        => T.MinMagnitude(x, y);

    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => Vector.Min(x, y);
}