namespace NetFabric.Numerics.Tensors.Operators;

public readonly struct EqualsAllOperator<T>
    : IBinaryToScalarOperator<T, T, bool>
    where T : struct, IEqualityOperators<T, T, bool>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Invoke(T x, T y)
        => x == y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => Vector.EqualsAll(x, y);
}

public readonly struct EqualsAnyOperator<T>
    : IBinaryToScalarOperator<T, T, bool>
    where T : struct, IEqualityOperators<T, T, bool>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Invoke(T x, T y)
        => x == y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => Vector.EqualsAny(x, y);
}
