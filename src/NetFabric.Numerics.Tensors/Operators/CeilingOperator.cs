namespace NetFabric.Numerics;

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