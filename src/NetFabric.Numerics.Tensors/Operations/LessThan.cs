namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void LessThan<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, INumberBase<T>, IComparisonOperators<T, T, bool>
        => Apply<T, LessThanOperator<T>>(x, y, destination);

    public static void LessThan<T>(ReadOnlySpan<T> x, ValueTuple<T, T> y, Span<T> destination)
        where T : struct, INumberBase<T>, IComparisonOperators<T, T, bool>
        => Apply<T, LessThanOperator<T>>(x, y, destination);

    public static void LessThan<T>(ReadOnlySpan<T> x, ValueTuple<T, T, T> y, Span<T> destination)
        where T : struct, INumberBase<T>, IComparisonOperators<T, T, bool>
        => Apply<T, LessThanOperator<T>>(x, y, destination);

    public static void LessThan<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> destination)
        where T : struct, INumberBase<T>, IComparisonOperators<T, T, bool>
        => Apply<T, LessThanOperator<T>>(x, y, destination);

    // int

    public static void LessThan(ReadOnlySpan<int> x, int y, Span<int> destination)
        => Apply<int, LessThanInt32Operator>(x, y, destination);

    public static void LessThan(ReadOnlySpan<int> x, ValueTuple<int, int> y, Span<int> destination)
        => Apply<int, LessThanInt32Operator>(x, y, destination);

    public static void LessThan(ReadOnlySpan<int> x, ValueTuple<int, int, int> y, Span<int> destination)
        => Apply<int, LessThanInt32Operator>(x, y, destination);

   public static void LessThan(ReadOnlySpan<int> x, ReadOnlySpan<int> y, Span<int> destination)
        => Apply<int, LessThanInt32Operator>(x, y, destination);

    // long

    public static void LessThan(ReadOnlySpan<long> x, long y, Span<long> destination)
        => Apply<long, LessThanInt64Operator>(x, y, destination);

    public static void LessThan(ReadOnlySpan<long> x, ValueTuple<long, long> y, Span<long> destination)
        => Apply<long, LessThanInt64Operator>(x, y, destination);

    public static void LessThan(ReadOnlySpan<long> x, ValueTuple<long, long, long> y, Span<long> destination)
        => Apply<long, LessThanInt64Operator>(x, y, destination);

    public static void LessThan(ReadOnlySpan<long> x, ReadOnlySpan<long> y, Span<long> destination)
        => Apply<long, LessThanInt64Operator>(x, y, destination);

    // float

    public static void LessThan(ReadOnlySpan<float> x, float y, Span<int> destination)
        => Apply<float, float, int, LessThanSingleOperator>(x, y, destination);

    public static void LessThan(ReadOnlySpan<float> x, ValueTuple<float, float> y, Span<int> destination)
        => Apply<float, float, int, LessThanSingleOperator>(x, y, destination);

    public static void LessThan(ReadOnlySpan<float> x, ValueTuple<float, float, float> y, Span<int> destination)
        => Apply<float, float, int, LessThanSingleOperator>(x, y, destination);

    public static void LessThan(ReadOnlySpan<float> x, ReadOnlySpan<float> y, Span<int> destination)
        => Apply<float, float, int, LessThanSingleOperator>(x, y, destination);

    // double

    public static void LessThan(ReadOnlySpan<double> x, double y, Span<long> destination)
        => Apply<double, double, long, LessThanDoubleOperator>(x, y, destination);

    public static void LessThan(ReadOnlySpan<double> x, ValueTuple<double, double> y, Span<long> destination)
        => Apply<double, double, long, LessThanDoubleOperator>(x, y, destination);

    public static void LessThan(ReadOnlySpan<double> x, ValueTuple<double, double, double> y, Span<long> destination)
        => Apply<double, double, long, LessThanDoubleOperator>(x, y, destination);

    public static void LessThan(ReadOnlySpan<double> x, ReadOnlySpan<double> y, Span<long> destination)
        => Apply<double, double, long, LessThanDoubleOperator>(x, y, destination);

}