namespace NetFabric.Numerics.Tensors.Operators;

public readonly struct AddMultiplyOperator<T>
    : ITernaryOperator<T, T, T, T>
    where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y, T z)
        => (x + y) * z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y, ref readonly Vector<T> z)
        => (x + y) * z;
}