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