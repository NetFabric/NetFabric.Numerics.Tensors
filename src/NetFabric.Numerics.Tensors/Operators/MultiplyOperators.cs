namespace NetFabric.Numerics.Tensors.Operators;

public readonly struct MultiplyOperator<T>
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

public readonly struct CheckedMultiplyOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, IMultiplyOperators<T, T, T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => checked(x * y);

    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct MultiplyScalarOperator<T>
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


