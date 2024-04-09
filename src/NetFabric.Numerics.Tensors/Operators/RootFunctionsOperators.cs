namespace NetFabric.Numerics.Tensors.Operators;

public readonly struct CbrtOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, IRootFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Cbrt(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct HypotOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, IRootFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => T.Hypot(x, y);

    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct RootNOperator<T>
    : IBinaryScalarOperator<T, int, T>
    where T : struct, IRootFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, int y)
        => T.RootN(x, y);

    public static Vector<T> Invoke(ref readonly Vector<T> x, int y)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct SqrtOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, IRootFunctions<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Sqrt(x);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Vector.SquareRoot(x);
}