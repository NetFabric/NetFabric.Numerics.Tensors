namespace NetFabric.Numerics;

public readonly struct FloorSingleOperator
    : IUnaryOperator<float>
{
    public static float Invoke(float x)
        => float.Floor(x);

    public static Vector<float> Invoke(ref readonly Vector<float> x)
        => Vector.Floor(x);
}

public readonly struct FloorDoubleOperator
    : IUnaryOperator<double>
{
    public static double Invoke(double x)
        => double.Floor(x);

    public static Vector<double> Invoke(ref readonly Vector<double> x)
        => Vector.Floor(x);
}