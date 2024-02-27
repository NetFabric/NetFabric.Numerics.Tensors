namespace NetFabric.Numerics.Tensors.Operators;

public readonly struct AbsOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, INumberBase<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Abs(x);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Vector.Abs(x);
}