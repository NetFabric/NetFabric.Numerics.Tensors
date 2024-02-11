namespace NetFabric.Numerics.Tensors;

readonly struct DecrementOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, IDecrementOperators<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => --x;

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}

readonly struct CheckedDecrementOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, IDecrementOperators<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => checked(--x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}