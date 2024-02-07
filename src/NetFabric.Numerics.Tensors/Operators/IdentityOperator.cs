namespace NetFabric.Numerics;

readonly struct IdentityOperator<T>
    : IUnaryOperator<T, T>
    where T : struct
{
    public static T Invoke(T x)
        => x;

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => x;
}