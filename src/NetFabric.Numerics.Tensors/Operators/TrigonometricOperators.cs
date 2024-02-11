namespace NetFabric.Numerics.Tensors;

readonly struct AcosOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, ITrigonometricFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Acos(x);

#pragma warning disable IDE0060 // Remove unused parameter
    public static Vector<T> Invoke(ref readonly Vector<T> x)
#pragma warning restore IDE0060 // Remove unused parameter
        => Throw.InvalidOperationException<Vector<T>>();
}

readonly struct AcosPiOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, ITrigonometricFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.AcosPi(x);

#pragma warning disable IDE0060 // Remove unused parameter
    public static Vector<T> Invoke(ref readonly Vector<T> x)
#pragma warning restore IDE0060 // Remove unused parameter
        => Throw.InvalidOperationException<Vector<T>>();
}

readonly struct AsinOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, ITrigonometricFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Asin(x);

#pragma warning disable IDE0060 // Remove unused parameter
    public static Vector<T> Invoke(ref readonly Vector<T> x)
#pragma warning restore IDE0060 // Remove unused parameter
        => Throw.InvalidOperationException<Vector<T>>();
}

readonly struct AsinPiOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, ITrigonometricFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.AsinPi(x);

#pragma warning disable IDE0060 // Remove unused parameter
    public static Vector<T> Invoke(ref readonly Vector<T> x)
#pragma warning restore IDE0060 // Remove unused parameter
        => Throw.InvalidOperationException<Vector<T>>();
}

readonly struct AtanOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, ITrigonometricFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Atan(x);

#pragma warning disable IDE0060 // Remove unused parameter
    public static Vector<T> Invoke(ref readonly Vector<T> x)
#pragma warning restore IDE0060 // Remove unused parameter
        => Throw.InvalidOperationException<Vector<T>>();
}

readonly struct AtanPiOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, ITrigonometricFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.AtanPi(x);

#pragma warning disable IDE0060 // Remove unused parameter
    public static Vector<T> Invoke(ref readonly Vector<T> x)
#pragma warning restore IDE0060 // Remove unused parameter
        => Throw.InvalidOperationException<Vector<T>>();
}

readonly struct CosOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, ITrigonometricFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Cos(x);

#pragma warning disable IDE0060 // Remove unused parameter
    public static Vector<T> Invoke(ref readonly Vector<T> x)
#pragma warning restore IDE0060 // Remove unused parameter
        => Throw.InvalidOperationException<Vector<T>>();
}

readonly struct CosPiOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, ITrigonometricFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.CosPi(x);

#pragma warning disable IDE0060 // Remove unused parameter
    public static Vector<T> Invoke(ref readonly Vector<T> x)
#pragma warning restore IDE0060 // Remove unused parameter
        => Throw.InvalidOperationException<Vector<T>>();
}

readonly struct SinOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, ITrigonometricFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Sin(x);

#pragma warning disable IDE0060 // Remove unused parameter
    public static Vector<T> Invoke(ref readonly Vector<T> x)
#pragma warning restore IDE0060 // Remove unused parameter
        => Throw.InvalidOperationException<Vector<T>>();
}

readonly struct SinPiOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, ITrigonometricFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.SinPi(x);

#pragma warning disable IDE0060 // Remove unused parameter
    public static Vector<T> Invoke(ref readonly Vector<T> x)
#pragma warning restore IDE0060 // Remove unused parameter
        => Throw.InvalidOperationException<Vector<T>>();
}

readonly struct TanOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, ITrigonometricFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Sin(x);

#pragma warning disable IDE0060 // Remove unused parameter
    public static Vector<T> Invoke(ref readonly Vector<T> x)
#pragma warning restore IDE0060 // Remove unused parameter
        => Throw.InvalidOperationException<Vector<T>>();
}

readonly struct TanPiOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, ITrigonometricFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.SinPi(x);

#pragma warning disable IDE0060 // Remove unused parameter
    public static Vector<T> Invoke(ref readonly Vector<T> x)
#pragma warning restore IDE0060 // Remove unused parameter
        => Throw.InvalidOperationException<Vector<T>>();
}

readonly struct SinCosOperator<T>
    : IUnaryOperator<T, (T Sin, T Cos)>
    where T : struct, ITrigonometricFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (T Sin, T Cos) Invoke(T x)
        => T.SinCos(x);

#pragma warning disable IDE0060 // Remove unused parameter
    public static Vector<(T Sin, T Cos)> Invoke(ref readonly Vector<T> x)
#pragma warning restore IDE0060 // Remove unused parameter
        => Throw.InvalidOperationException<Vector<(T Sin, T Cos)>>();
}

readonly struct SinCosPiOperator<T>
    : IUnaryOperator<T, (T SinPi, T CosPi)>
    where T : struct, ITrigonometricFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (T SinPi, T CosPi) Invoke(T x)
        => T.SinCosPi(x);

#pragma warning disable IDE0060 // Remove unused parameter
    public static Vector<(T SinPi, T CosPi)> Invoke(ref readonly Vector<T> x)
#pragma warning restore IDE0060 // Remove unused parameter
        => Throw.InvalidOperationException<Vector<(T SinPi, T CosPi)>>();
}






