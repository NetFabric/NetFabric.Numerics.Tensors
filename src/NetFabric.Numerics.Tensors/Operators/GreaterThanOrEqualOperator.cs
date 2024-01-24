namespace NetFabric.Numerics;

readonly struct GreaterThanOrEqualOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, IComparisonOperators<T, T, bool>, IMultiplicativeIdentity<T, T>
{
    public static T Invoke(T x, T y)
        =>  x >= y 
            ? Vector<T>.IsSupported 
                ? AllBitsSet<T>.Value 
                : T.MultiplicativeIdentity
            : default!;

    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => Vector.GreaterThanOrEqual(x, y);
}

readonly struct GreaterThanOrEqualInt32Operator
    : IBinaryOperator<int, int, int>
{
    public static int Invoke(int x, int y)
        => x >= y ? -1 : 0;

    public static Vector<int> Invoke(ref readonly Vector<int> x, ref readonly Vector<int> y)
        => Vector.GreaterThanOrEqual(x, y);
}

readonly struct GreaterThanOrEqualInt64Operator
    : IBinaryOperator<long, long, long>
{
    public static long Invoke(long x, long y)
        => x >= y ? -1 : 0;

    public static Vector<long> Invoke(ref readonly Vector<long> x, ref readonly Vector<long> y)
        => Vector.GreaterThanOrEqual(x, y);
}

readonly struct GreaterThanOrEqualSingleOperator
    : IBinaryOperator<float, float, int>
{
    public static int Invoke(float x, float y)
        => x >= y ? -1 : 0;

    public static Vector<int> Invoke(ref readonly Vector<float> x, ref readonly Vector<float> y)
        => Vector.GreaterThanOrEqual(x, y);
}

readonly struct GreaterThanOrEqualDoubleOperator
    : IBinaryOperator<double, double, long>
{
    public static long Invoke(double x, double y)
        => x >= y ? -1 : 0;

    public static Vector<long> Invoke(ref readonly Vector<double> x, ref readonly Vector<double> y)
        => Vector.GreaterThanOrEqual(x, y);
}
