namespace NetFabric.Numerics.Tensors.Operators;

public readonly struct LogOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, ILogarithmicFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Log(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct LogBaseOperator<T>
    : IBinaryScalarOperator<T, T, T>
    where T : struct, ILogarithmicFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T newBase)
        => T.Log(x, newBase);

    public static Vector<T> Invoke(ref readonly Vector<T> x, T newBase)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct LogP1Operator<T>
    : IUnaryOperator<T, T>
    where T : struct, ILogarithmicFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.LogP1(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct Log2Operator<T>
    : IUnaryOperator<T, T>
    where T : struct, ILogarithmicFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Log2(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct Log2P1Operator<T>
    : IUnaryOperator<T, T>
    where T : struct, ILogarithmicFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Log2P1(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct Log10Operator<T>
    : IUnaryOperator<T, T>
    where T : struct, ILogarithmicFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Log10(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct Log10P1Operator<T>
    : IUnaryOperator<T, T>
    where T : struct, ILogarithmicFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Log10P1(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}

