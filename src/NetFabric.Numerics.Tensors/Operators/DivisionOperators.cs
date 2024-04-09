namespace NetFabric.Numerics.Tensors.Operators;

public readonly struct DivideOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, IDivisionOperators<T, T, T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => x / y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => x / y;
}

public readonly struct CheckedDivideOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, IDivisionOperators<T, T, T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => checked(x / y);

    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => Throw.InvalidOperationException<Vector<T>>();
}
