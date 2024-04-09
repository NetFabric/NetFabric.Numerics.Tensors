namespace NetFabric.Numerics.Tensors.Operators;

public readonly struct FloorOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, IFloatingPoint<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Floor(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct FloorSingleOperator
    : IUnaryOperator<float, float>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Invoke(float x)
        => float.Floor(x);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<float> Invoke(ref readonly Vector<float> x)
        => Vector.Floor(x);
}

public readonly struct FloorDoubleOperator
    : IUnaryOperator<double, double>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Invoke(double x)
        => double.Floor(x);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<double> Invoke(ref readonly Vector<double> x)
        => Vector.Floor(x);
}

public readonly struct CeilingOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, IFloatingPoint<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Ceiling(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct CeilingSingleOperator
    : IUnaryOperator<float, float>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Invoke(float x)
        => float.Ceiling(x);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<float> Invoke(ref readonly Vector<float> x)
        => Vector.Ceiling(x);
}

public readonly struct CeilingDoubleOperator
    : IUnaryOperator<double, double>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Invoke(double x)
        => double.Ceiling(x);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<double> Invoke(ref readonly Vector<double> x)
        => Vector.Ceiling(x);
}

public readonly struct RoundOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, IFloatingPoint<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Round(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct RoundDigitsOperator<T>
    : IBinaryScalarOperator<T, int, T>
    where T : struct, IFloatingPoint<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, int digits)
        => T.Round(x, digits);

    public static Vector<T> Invoke(ref readonly Vector<T> x, int digits)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct RoundModeOperator<T>
    : IBinaryScalarOperator<T, MidpointRounding, T>
    where T : struct, IFloatingPoint<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, MidpointRounding mode)
        => T.Round(x, mode);

    public static Vector<T> Invoke(ref readonly Vector<T> x, MidpointRounding mode)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct RoundDigitModeOperator<T>
    : IBinaryScalarOperator<T, (int digits, MidpointRounding mode), T>
    where T : struct, IFloatingPoint<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, (int digits, MidpointRounding mode) param)
        => T.Round(x, param.digits, param.mode);

    public static Vector<T> Invoke(ref readonly Vector<T> x, (int digits, MidpointRounding mode) param)
        => Throw.InvalidOperationException<Vector<T>>();
}

public readonly struct TruncateOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, IFloatingPoint<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Truncate(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}
