namespace NetFabric.Numerics.Tensors.Operators;

public readonly struct Atan2Operator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, IFloatingPointIeee754<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => T.Atan2(x, y);

    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct Atan2PiOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, IFloatingPointIeee754<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => T.Atan2Pi(x, y);

    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct BitDecrementOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, IFloatingPointIeee754<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.BitDecrement(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct BitIncrementOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, IFloatingPointIeee754<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.BitIncrement(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct FusedMultiplyAddOperator<T>
    : ITernaryOperator<T, T, T, T>
    where T : struct, IFloatingPointIeee754<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y, T z)
        => T.FusedMultiplyAdd(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y, ref readonly Vector<T> z)
        => (x * y) + z;
}

public readonly struct Ieee754RemainderOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, IFloatingPointIeee754<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => T.Ieee754Remainder(x, y);

    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct ILogBOperator<T>
    : IUnaryOperator<T, int>
    where T : struct, IFloatingPointIeee754<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Invoke(T x)
        => T.ILogB(x);

    public static Vector<int> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<int>>();
}

public readonly struct LerpOperator<T>
    : ITernaryOperator<T, T, T, T>
    where T : struct, IFloatingPointIeee754<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y, T z)
        => T.Lerp(x, y, z);

    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y, ref readonly Vector<T> z)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct ReciprocalEstimateOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, IFloatingPointIeee754<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.ReciprocalEstimate(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct ReciprocalSqrtEstimateOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, IFloatingPointIeee754<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.ReciprocalSqrtEstimate(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}