namespace NetFabric.Numerics.Tensors.Operators;

public readonly struct ModulusOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, IModulusOperators<T, T, T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => x % y;

    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => Throw.InvalidOperationException<Vector<T>>();
}