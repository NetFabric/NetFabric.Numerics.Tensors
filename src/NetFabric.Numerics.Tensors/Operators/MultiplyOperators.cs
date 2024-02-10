namespace NetFabric.Numerics;

readonly struct MultiplyOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, IMultiplyOperators<T, T, T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => x * y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => x * y;
}

readonly struct CheckedMultiplyOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, IMultiplyOperators<T, T, T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => checked(x * y);

#pragma warning disable IDE0060 // Remove unused parameter
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
#pragma warning restore IDE0060 // Remove unused parameter
        => Throw.InvalidOperationException<Vector<T>>();
}

readonly struct MultiplyScalarOperator<T>
    : IBinaryScalarOperator<T, T, T>
    where T : struct, IMultiplyOperators<T, T, T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => x * y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x, T y)
        => x * y;
}


