namespace NetFabric.Numerics;

readonly struct SquareOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, IMultiplyOperators<T, T, T>
{
    public static T Invoke(T x)
        => x * x;

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => x * x;
}

readonly struct CubeOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, IMultiplyOperators<T, T, T>
{
    public static T Invoke(T x)
        => x * x * x;

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => x * x * x;
}

readonly struct PowOperator<T>
    : IGenericBinaryOperator<T, T, T>
    where T : struct, IPowerFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    public static T Invoke(T x, T y)
        => T.Pow(x, y);

#pragma warning disable IDE0060 // Remove unused parameter
    public static Vector<T> Invoke(ref readonly Vector<T> x, T y)
#pragma warning restore IDE0060 // Remove unused parameter
        => Throw.InvalidOperationException<Vector<T>>();
}

