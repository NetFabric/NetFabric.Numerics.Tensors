namespace NetFabric.Numerics.Tensors.Operators;

public readonly struct NegateOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, IUnaryNegationOperators<T, T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T value)
        => -value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> value)
        => -value;
}

public readonly struct CheckedNegateOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, IUnaryNegationOperators<T, T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T value)
        => checked(-value);

    public static Vector<T> Invoke(ref readonly Vector<T> value)
        => Throw.InvalidOperationException<Vector<T>>();
}