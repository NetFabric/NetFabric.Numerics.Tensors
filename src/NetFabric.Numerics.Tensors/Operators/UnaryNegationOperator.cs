namespace NetFabric.Numerics.Tensors;

readonly struct NegateOperator<T>
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

readonly struct CheckedNegateOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, IUnaryNegationOperators<T, T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T value)
        => checked(-value);

#pragma warning disable IDE0060 // Remove unused parameter
    public static Vector<T> Invoke(ref readonly Vector<T> value)
#pragma warning restore IDE0060 // Remove unused parameter
        => Throw.InvalidOperationException<Vector<T>>();
}