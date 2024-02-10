namespace NetFabric.Numerics;

readonly struct IdentityOperator<T>
    : IUnaryOperator<T, T>
    where T : struct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => x;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => x;
}