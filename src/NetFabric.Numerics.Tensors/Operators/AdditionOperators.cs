namespace NetFabric.Numerics.Tensors.Operators;

public readonly struct AddOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, IAdditionOperators<T, T, T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => x + y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => x + y;
}

public readonly struct CheckedAddOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, IAdditionOperators<T, T, T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => checked(x + y);

    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y) 
        => Throw.InvalidOperationException<Vector<T>>();
}