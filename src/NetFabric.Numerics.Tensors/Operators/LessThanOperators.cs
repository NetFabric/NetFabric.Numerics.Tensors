using NetFabric.Numerics.Tensors;

namespace NetFabric.Numerics;

readonly struct LessThanOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, IComparisonOperators<T, T, bool>
{
    public static T Invoke(T x, T y)
        =>  x < y ? AllBitsSet<T>.Value : default!;

    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => Vector.LessThan(x, y);
}

readonly struct LessThanInt32Operator
    : IBinaryOperator<int, int, int>
{
    public static int Invoke(int x, int y)
        => x < y ? -1 : 0;

    public static Vector<int> Invoke(ref readonly Vector<int> x, ref readonly Vector<int> y)
        => Vector.LessThan(x, y);
}

readonly struct LessThanInt64Operator
    : IBinaryOperator<long, long, long>
{
    public static long Invoke(long x, long y)
        => x < y ? -1L : 0L;

    public static Vector<long> Invoke(ref readonly Vector<long> x, ref readonly Vector<long> y)
        => Vector.LessThan(x, y);
}

readonly struct LessThanSingleOperator
    : IBinaryOperator<float, float, int>
{
    public static int Invoke(float x, float y)
        => x < y ? -1 : 0;

    public static Vector<int> Invoke(ref readonly Vector<float> x, ref readonly Vector<float> y)
        => Vector.LessThan(x, y);
}

readonly struct LessThanDoubleOperator
    : IBinaryOperator<double, double, long>
{
    public static long Invoke(double x, double y)
        => x < y ? -1L : 0L;

    public static Vector<long> Invoke(ref readonly Vector<double> x, ref readonly Vector<double> y)
        => Vector.LessThan(x, y);
}
