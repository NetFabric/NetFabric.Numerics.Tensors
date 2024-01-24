namespace NetFabric.Numerics;

readonly struct FloorOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, IFloatingPoint<T>
{
    public static bool IsVectorizable
        => false; 

    public static T Invoke(T x)
        => T.Floor(x);

#pragma warning disable IDE0060 // Remove unused parameter
    public static Vector<T> Invoke(ref readonly Vector<T> x)
#pragma warning restore IDE0060 // Remove unused parameter
        => Throw.InvalidOperationException<Vector<T>>();
}

readonly struct FloorSingleOperator
    : IUnaryOperator<float, float>
{
    public static float Invoke(float x)
        => float.Floor(x);

    public static Vector<float> Invoke(ref readonly Vector<float> x)
        => Vector.Floor(x);
}

readonly struct FloorDoubleOperator
    : IUnaryOperator<double, double>
{
    public static double Invoke(double x)
        => double.Floor(x);

    public static Vector<double> Invoke(ref readonly Vector<double> x)
        => Vector.Floor(x);
}

readonly struct CeilingOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, IFloatingPoint<T>
{
    public static bool IsVectorizable
        => false; 

    public static T Invoke(T x)
        => T.Ceiling(x);

#pragma warning disable IDE0060 // Remove unused parameter
    public static Vector<T> Invoke(ref readonly Vector<T> x)
#pragma warning restore IDE0060 // Remove unused parameter
        => Throw.InvalidOperationException<Vector<T>>();
}

readonly struct CeilingSingleOperator
    : IUnaryOperator<float, float>
{
    public static float Invoke(float x)
        => float.Ceiling(x);

    public static Vector<float> Invoke(ref readonly Vector<float> x)
        => Vector.Ceiling(x);
}

readonly struct CeilingDoubleOperator
    : IUnaryOperator<double, double>
{
    public static double Invoke(double x)
        => double.Ceiling(x);

    public static Vector<double> Invoke(ref readonly Vector<double> x)
        => Vector.Ceiling(x);
}

readonly struct RoundOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, IFloatingPoint<T>
{
    public static T Invoke(T x)
        => T.Round(x);

#pragma warning disable IDE0060 // Remove unused parameter
    public static Vector<T> Invoke(ref readonly Vector<T> x)
#pragma warning restore IDE0060 // Remove unused parameter
        => Throw.InvalidOperationException<Vector<T>>();
}

readonly struct RoundDigitsOperator<T>
    : IGenericBinaryOperator<T, int, T>
    where T : struct, IFloatingPoint<T>
{
    public static T Invoke(T x, int digits)
        => T.Round(x, digits);

#pragma warning disable IDE0060 // Remove unused parameter
    public static Vector<T> Invoke(ref readonly Vector<T> x, int digits)
#pragma warning restore IDE0060 // Remove unused parameter
        => Throw.InvalidOperationException<Vector<T>>();
}

readonly struct RoundModeOperator<T>
    : IGenericBinaryOperator<T, MidpointRounding, T>
    where T : struct, IFloatingPoint<T>
{
    public static T Invoke(T x, MidpointRounding mode)
        => T.Round(x, mode);

#pragma warning disable IDE0060 // Remove unused parameter
    public static Vector<T> Invoke(ref readonly Vector<T> x, MidpointRounding mode)
#pragma warning restore IDE0060 // Remove unused parameter
        => Throw.InvalidOperationException<Vector<T>>();
}

readonly struct RoundDigitModeOperator<T>
    : IGenericBinaryOperator<T, (int digits, MidpointRounding mode), T>
    where T : struct, IFloatingPoint<T>
{
    public static T Invoke(T x, (int digits, MidpointRounding mode) param)
        => T.Round(x, param.digits, param.mode);

#pragma warning disable IDE0060 // Remove unused parameter
    public static Vector<T> Invoke(ref readonly Vector<T> x, (int digits, MidpointRounding mode) param)
#pragma warning restore IDE0060 // Remove unused parameter
        => Throw.InvalidOperationException<Vector<T>>();
}

readonly struct TruncateOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, IFloatingPoint<T>
{
    public static T Invoke(T x)
        => T.Truncate(x);

#pragma warning disable IDE0060 // Remove unused parameter
    public static Vector<T> Invoke(ref readonly Vector<T> x)
#pragma warning restore IDE0060 // Remove unused parameter
        => Throw.InvalidOperationException<Vector<T>>();
}
