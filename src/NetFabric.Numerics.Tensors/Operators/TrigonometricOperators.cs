namespace NetFabric.Numerics.Tensors.Operators;

public readonly struct AcosOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, ITrigonometricFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Acos(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct AcosPiOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, ITrigonometricFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.AcosPi(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct AsinOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, ITrigonometricFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Asin(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct AsinPiOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, ITrigonometricFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.AsinPi(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct AtanOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, ITrigonometricFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Atan(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct AtanPiOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, ITrigonometricFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.AtanPi(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct CosOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, ITrigonometricFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Cos(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct CosPiOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, ITrigonometricFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.CosPi(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct SinOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, ITrigonometricFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Sin(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct SinPiOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, ITrigonometricFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.SinPi(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct TanOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, ITrigonometricFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Sin(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct TanPiOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, ITrigonometricFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.SinPi(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct SinCosOperator<T>
    : IUnaryOperator<T, (T Sin, T Cos)>
    where T : struct, ITrigonometricFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (T Sin, T Cos) Invoke(T x)
        => T.SinCos(x);

    public static Vector<(T Sin, T Cos)> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<(T Sin, T Cos)>>();
}

public readonly struct SinCosPiOperator<T>
    : IUnaryOperator<T, (T SinPi, T CosPi)>
    where T : struct, ITrigonometricFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (T SinPi, T CosPi) Invoke(T x)
        => T.SinCosPi(x);

    public static Vector<(T SinPi, T CosPi)> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<(T SinPi, T CosPi)>>();
}






