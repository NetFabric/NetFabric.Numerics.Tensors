namespace NetFabric.Numerics.Tensors;

readonly struct MultiplyDivideOperator<T>
    : ITernaryOperator<T, T, T, T>
    where T : struct, IMultiplyOperators<T, T, T>, IDivisionOperators<T, T, T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y, T z)
        => (x * y) / z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y, ref readonly Vector<T> z)
        => (x * y) / z;
}