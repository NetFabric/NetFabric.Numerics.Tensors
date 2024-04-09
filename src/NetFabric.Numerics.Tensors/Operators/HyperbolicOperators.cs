namespace NetFabric.Numerics.Tensors.Operators;

public readonly struct AcoshOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, IHyperbolicFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Acosh(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct AsinhOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, IHyperbolicFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Asinh(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct AtanhOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, IHyperbolicFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Atanh(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct CoshOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, IHyperbolicFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Cosh(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct SinhOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, IHyperbolicFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Sinh(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct TanhOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, IHyperbolicFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Tanh(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}