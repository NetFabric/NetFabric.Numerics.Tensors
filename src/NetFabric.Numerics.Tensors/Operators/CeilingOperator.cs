namespace NetFabric.Numerics;

public readonly struct CeilingSingleOperator
    : IUnaryOperator<float>
{
    public static float Invoke(float x)
        => float.Ceiling(x);

    public static Vector<float> Invoke(ref readonly Vector<float> x)
        => Vector.Ceiling(x);
}

public readonly struct CeilingDoubleOperator
    : IUnaryOperator<double>
{
    public static double Invoke(double x)
        => double.Ceiling(x);

    public static Vector<double> Invoke(ref readonly Vector<double> x)
        => Vector.Ceiling(x);
}