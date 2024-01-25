namespace NetFabric.Numerics;

readonly struct GreaterThanOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, IComparisonOperators<T, T, bool>, IMultiplicativeIdentity<T, T>
{
    public static T Invoke(T x, T y)
        =>  x > y 
            ? Vector<T>.IsSupported 
                ? AllBitsSet<T>.Value 
                : T.MultiplicativeIdentity
            : default!;

    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => Vector.GreaterThan(x, y);
}

readonly struct GreaterThanInt32Operator
    : IBinaryOperator<int, int, int>
{
    public static int Invoke(int x, int y)
        => x > y ? -1 : 0;

    public static Vector<int> Invoke(ref readonly Vector<int> x, ref readonly Vector<int> y)
        => Vector.GreaterThan(x, y);
}

readonly struct GreaterThanInt64Operator
    : IBinaryOperator<long, long, long>
{
    public static long Invoke(long x, long y)
        => x > y ? -1 : 0;

    public static Vector<long> Invoke(ref readonly Vector<long> x, ref readonly Vector<long> y)
        => Vector.GreaterThan(x, y);
}

readonly struct GreaterThanSingleOperator
    : IBinaryOperator<float, float, int>
{
    public static int Invoke(float x, float y)
        => x > y ? -1 : 0;

    public static Vector<int> Invoke(ref readonly Vector<float> x, ref readonly Vector<float> y)
        => Vector.GreaterThan(x, y);
}

readonly struct GreaterThanDoubleOperator
    : IBinaryOperator<double, double, long>
{
    public static long Invoke(double x, double y)
        => x > y ? -1 : 0;

    public static Vector<long> Invoke(ref readonly Vector<double> x, ref readonly Vector<double> y)
        => Vector.GreaterThan(x, y);
}

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

readonly struct LessThanOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, IComparisonOperators<T, T, bool>, IMultiplicativeIdentity<T, T>
{
    public static T Invoke(T x, T y)
        =>  x < y 
            ? Vector<T>.IsSupported 
                ? AllBitsSet<T>.Value 
                : T.MultiplicativeIdentity
            : default!;

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

readonly struct LessThanOrEqualOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, IComparisonOperators<T, T, bool>, IMultiplicativeIdentity<T, T>
{
    public static T Invoke(T x, T y)
        =>  x <= y 
            ? Vector<T>.IsSupported 
                ? AllBitsSet<T>.Value 
                : T.MultiplicativeIdentity
            : default!;

    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => Vector.LessThanOrEqual(x, y);
}

readonly struct LessThanOrEqualInt32Operator
    : IBinaryOperator<int, int, int>
{
    public static int Invoke(int x, int y)
        => x <= y ? -1 : 0;

    public static Vector<int> Invoke(ref readonly Vector<int> x, ref readonly Vector<int> y)
        => Vector.LessThanOrEqual(x, y);
}

readonly struct LessThanOrEqualInt64Operator
    : IBinaryOperator<long, long, long>
{
    public static long Invoke(long x, long y)
        => x <= y ? -1 : 0;

    public static Vector<long> Invoke(ref readonly Vector<long> x, ref readonly Vector<long> y)
        => Vector.LessThanOrEqual(x, y);
}

readonly struct LessThanOrEqualSingleOperator
    : IBinaryOperator<float, float, int>
{
    public static int Invoke(float x, float y)
        => x <= y ? -1 : 0;

    public static Vector<int> Invoke(ref readonly Vector<float> x, ref readonly Vector<float> y)
        => Vector.LessThanOrEqual(x, y);
}

readonly struct LessThanOrEqualDoubleOperator
    : IBinaryOperator<double, double, long>
{
    public static long Invoke(double x, double y)
        => x <= y ? -1 : 0;

    public static Vector<long> Invoke(ref readonly Vector<double> x, ref readonly Vector<double> y)
        => Vector.LessThanOrEqual(x, y);
}
