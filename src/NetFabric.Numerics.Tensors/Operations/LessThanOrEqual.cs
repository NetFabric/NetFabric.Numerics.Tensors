namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void LessThanOrEqual<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, INumberBase<T>, IComparisonOperators<T, T, bool>
        => Apply<T, LessThanOrEqualOperator<T>>(x, y, destination);

    public static void LessThanOrEqual<T>(ReadOnlySpan<T> x, ValueTuple<T, T> y, Span<T> destination)
        where T : struct, INumberBase<T>, IComparisonOperators<T, T, bool>
        => Apply<T, LessThanOrEqualOperator<T>>(x, y, destination);

    public static void LessThanOrEqual<T>(ReadOnlySpan<T> x, ValueTuple<T, T, T> y, Span<T> destination)
        where T : struct, INumberBase<T>, IComparisonOperators<T, T, bool>
        => Apply<T, LessThanOrEqualOperator<T>>(x, y, destination);

    public static void LessThanOrEqual<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> destination)
        where T : struct, INumberBase<T>, IComparisonOperators<T, T, bool>
        => Apply<T, LessThanOrEqualOperator<T>>(x, y, destination);

    // int

    public static void LessThanOrEqual(ReadOnlySpan<int> x, int y, Span<int> destination)
        => Apply<int, LessThanOrEqualInt32Operator>(x, y, destination);

    public static void LessThanOrEqual(ReadOnlySpan<int> x, ValueTuple<int, int> y, Span<int> destination)
        => Apply<int, LessThanOrEqualInt32Operator>(x, y, destination);

    public static void LessThanOrEqual(ReadOnlySpan<int> x, ValueTuple<int, int, int> y, Span<int> destination)
        => Apply<int, LessThanOrEqualInt32Operator>(x, y, destination);

   public static void LessThanOrEqual(ReadOnlySpan<int> x, ReadOnlySpan<int> y, Span<int> destination)
        => Apply<int, LessThanOrEqualInt32Operator>(x, y, destination);

    // long

    public static void LessThanOrEqual(ReadOnlySpan<long> x, long y, Span<long> destination)
        => Apply<long, LessThanOrEqualInt64Operator>(x, y, destination);

    public static void LessThanOrEqual(ReadOnlySpan<long> x, ValueTuple<long, long> y, Span<long> destination)
        => Apply<long, LessThanOrEqualInt64Operator>(x, y, destination);

    public static void LessThanOrEqual(ReadOnlySpan<long> x, ValueTuple<long, long, long> y, Span<long> destination)
        => Apply<long, LessThanOrEqualInt64Operator>(x, y, destination);

    public static void LessThanOrEqual(ReadOnlySpan<long> x, ReadOnlySpan<long> y, Span<long> destination)
        => Apply<long, LessThanOrEqualInt64Operator>(x, y, destination);

    // float

    public static void LessThanOrEqual(ReadOnlySpan<float> x, float y, Span<int> destination)
        => Apply<float, float, int, LessThanOrEqualSingleOperator>(x, y, destination);

    public static void LessThanOrEqual(ReadOnlySpan<float> x, ValueTuple<float, float> y, Span<int> destination)
        => Apply<float, float, int, LessThanOrEqualSingleOperator>(x, y, destination);

    public static void LessThanOrEqual(ReadOnlySpan<float> x, ValueTuple<float, float, float> y, Span<int> destination)
        => Apply<float, float, int, LessThanOrEqualSingleOperator>(x, y, destination);

    public static void LessThanOrEqual(ReadOnlySpan<float> x, ReadOnlySpan<float> y, Span<int> destination)
        => Apply<float, float, int, LessThanOrEqualSingleOperator>(x, y, destination);

    // double

    public static void LessThanOrEqual(ReadOnlySpan<double> x, double y, Span<long> destination)
        => Apply<double, double, long, LessThanOrEqualDoubleOperator>(x, y, destination);

    public static void LessThanOrEqual(ReadOnlySpan<double> x, ValueTuple<double, double> y, Span<long> destination)
        => Apply<double, double, long, LessThanOrEqualDoubleOperator>(x, y, destination);

    public static void LessThanOrEqual(ReadOnlySpan<double> x, ValueTuple<double, double, double> y, Span<long> destination)
        => Apply<double, double, long, LessThanOrEqualDoubleOperator>(x, y, destination);

    public static void LessThanOrEqual(ReadOnlySpan<double> x, ReadOnlySpan<double> y, Span<long> destination)
        => Apply<double, double, long, LessThanOrEqualDoubleOperator>(x, y, destination);

}