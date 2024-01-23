namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void GreaterThanOrEqual<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, INumberBase<T>, IComparisonOperators<T, T, bool>
        => Apply<T, GreaterThanOrEqualOperator<T>>(x, y, destination);

    public static void GreaterThanOrEqual<T>(ReadOnlySpan<T> x, ValueTuple<T, T> y, Span<T> destination)
        where T : struct, INumberBase<T>, IComparisonOperators<T, T, bool>
        => Apply<T, GreaterThanOrEqualOperator<T>>(x, y, destination);

    public static void GreaterThanOrEqual<T>(ReadOnlySpan<T> x, ValueTuple<T, T, T> y, Span<T> destination)
        where T : struct, INumberBase<T>, IComparisonOperators<T, T, bool>
        => Apply<T, GreaterThanOrEqualOperator<T>>(x, y, destination);

    public static void GreaterThanOrEqual<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> destination)
        where T : struct, INumberBase<T>, IComparisonOperators<T, T, bool>
        => Apply<T, GreaterThanOrEqualOperator<T>>(x, y, destination);

    // int

    public static void GreaterThanOrEqual(ReadOnlySpan<int> x, int y, Span<int> destination)
        => Apply<int, GreaterThanOrEqualInt32Operator>(x, y, destination);

    public static void GreaterThanOrEqual(ReadOnlySpan<int> x, ValueTuple<int, int> y, Span<int> destination)
        => Apply<int, GreaterThanOrEqualInt32Operator>(x, y, destination);

    public static void GreaterThanOrEqual(ReadOnlySpan<int> x, ValueTuple<int, int, int> y, Span<int> destination)
        => Apply<int, GreaterThanOrEqualInt32Operator>(x, y, destination);

   public static void GreaterThanOrEqual(ReadOnlySpan<int> x, ReadOnlySpan<int> y, Span<int> destination)
        => Apply<int, GreaterThanOrEqualInt32Operator>(x, y, destination);

    // long

    public static void GreaterThanOrEqual(ReadOnlySpan<long> x, long y, Span<long> destination)
        => Apply<long, GreaterThanOrEqualInt64Operator>(x, y, destination);

    public static void GreaterThanOrEqual(ReadOnlySpan<long> x, ValueTuple<long, long> y, Span<long> destination)
        => Apply<long, GreaterThanOrEqualInt64Operator>(x, y, destination);

    public static void GreaterThanOrEqual(ReadOnlySpan<long> x, ValueTuple<long, long, long> y, Span<long> destination)
        => Apply<long, GreaterThanOrEqualInt64Operator>(x, y, destination);

    public static void GreaterThanOrEqual(ReadOnlySpan<long> x, ReadOnlySpan<long> y, Span<long> destination)
        => Apply<long, GreaterThanOrEqualInt64Operator>(x, y, destination);

    // float

    public static void GreaterThanOrEqual(ReadOnlySpan<float> x, float y, Span<int> destination)
        => Apply<float, float, int, GreaterThanOrEqualSingleOperator>(x, y, destination);

    public static void GreaterThanOrEqual(ReadOnlySpan<float> x, ValueTuple<float, float> y, Span<int> destination)
        => Apply<float, float, int, GreaterThanOrEqualSingleOperator>(x, y, destination);

    public static void GreaterThanOrEqual(ReadOnlySpan<float> x, ValueTuple<float, float, float> y, Span<int> destination)
        => Apply<float, float, int, GreaterThanOrEqualSingleOperator>(x, y, destination);

    public static void GreaterThanOrEqual(ReadOnlySpan<float> x, ReadOnlySpan<float> y, Span<int> destination)
        => Apply<float, float, int, GreaterThanOrEqualSingleOperator>(x, y, destination);

    // double

    public static void GreaterThanOrEqual(ReadOnlySpan<double> x, double y, Span<long> destination)
        => Apply<double, double, long, GreaterThanOrEqualDoubleOperator>(x, y, destination);

    public static void GreaterThanOrEqual(ReadOnlySpan<double> x, ValueTuple<double, double> y, Span<long> destination)
        => Apply<double, double, long, GreaterThanOrEqualDoubleOperator>(x, y, destination);

    public static void GreaterThanOrEqual(ReadOnlySpan<double> x, ValueTuple<double, double, double> y, Span<long> destination)
        => Apply<double, double, long, GreaterThanOrEqualDoubleOperator>(x, y, destination);

    public static void GreaterThanOrEqual(ReadOnlySpan<double> x, ReadOnlySpan<double> y, Span<long> destination)
        => Apply<double, double, long, GreaterThanOrEqualDoubleOperator>(x, y, destination);

}