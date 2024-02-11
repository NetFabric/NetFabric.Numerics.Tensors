namespace NetFabric.Numerics.Tensors;

readonly struct LogOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, ILogarithmicFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Log(x);

#pragma warning disable IDE0060 // Remove unused parameter
    public static Vector<T> Invoke(ref readonly Vector<T> x)
#pragma warning restore IDE0060 // Remove unused parameter
        => Throw.InvalidOperationException<Vector<T>>();
}

readonly struct LogBaseOperator<T>
    : IBinaryScalarOperator<T, T, T>
    where T : struct, ILogarithmicFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T newBase)
        => T.Log(x, newBase);

#pragma warning disable IDE0060 // Remove unused parameter
    public static Vector<T> Invoke(ref readonly Vector<T> x, T newBase)
#pragma warning restore IDE0060 // Remove unused parameter
        => Throw.InvalidOperationException<Vector<T>>();
}

readonly struct LogP1Operator<T>
    : IUnaryOperator<T, T>
    where T : struct, ILogarithmicFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.LogP1(x);

#pragma warning disable IDE0060 // Remove unused parameter
    public static Vector<T> Invoke(ref readonly Vector<T> x)
#pragma warning restore IDE0060 // Remove unused parameter
        => Throw.InvalidOperationException<Vector<T>>();
}

readonly struct Log2Operator<T>
    : IUnaryOperator<T, T>
    where T : struct, ILogarithmicFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Log2(x);

#pragma warning disable IDE0060 // Remove unused parameter
    public static Vector<T> Invoke(ref readonly Vector<T> x)
#pragma warning restore IDE0060 // Remove unused parameter
        => Throw.InvalidOperationException<Vector<T>>();
}

readonly struct Log2P1Operator<T>
    : IUnaryOperator<T, T>
    where T : struct, ILogarithmicFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Log2P1(x);

#pragma warning disable IDE0060 // Remove unused parameter
    public static Vector<T> Invoke(ref readonly Vector<T> x)
#pragma warning restore IDE0060 // Remove unused parameter
        => Throw.InvalidOperationException<Vector<T>>();
}

readonly struct Log10Operator<T>
    : IUnaryOperator<T, T>
    where T : struct, ILogarithmicFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Log10(x);

#pragma warning disable IDE0060 // Remove unused parameter
    public static Vector<T> Invoke(ref readonly Vector<T> x)
#pragma warning restore IDE0060 // Remove unused parameter
        => Throw.InvalidOperationException<Vector<T>>();
}

readonly struct Log10P1Operator<T>
    : IUnaryOperator<T, T>
    where T : struct, ILogarithmicFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Log10P1(x);

#pragma warning disable IDE0060 // Remove unused parameter
    public static Vector<T> Invoke(ref readonly Vector<T> x)
#pragma warning restore IDE0060 // Remove unused parameter
        => Throw.InvalidOperationException<Vector<T>>();
}

