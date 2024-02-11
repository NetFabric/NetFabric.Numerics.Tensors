namespace NetFabric.Numerics.Tensors;

public static partial class Tensor
{
    public static void GreaterThan<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, IComparisonOperators<T, T, bool>, IMultiplicativeIdentity<T, T>
        => Apply<T, GreaterThanOperator<T>>(x, y, destination);

    public static void GreaterThan<T>(ReadOnlySpan<T> x, ValueTuple<T, T> y, Span<T> destination)
        where T : struct, IComparisonOperators<T, T, bool>, IMultiplicativeIdentity<T, T>
        => Apply<T, GreaterThanOperator<T>>(x, y, destination);

    public static void GreaterThan<T>(ReadOnlySpan<T> x, ValueTuple<T, T, T> y, Span<T> destination)
        where T : struct, IComparisonOperators<T, T, bool>, IMultiplicativeIdentity<T, T>
        => Apply<T, GreaterThanOperator<T>>(x, y, destination);

    public static void GreaterThan<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> destination)
        where T : struct, IComparisonOperators<T, T, bool>, IMultiplicativeIdentity<T, T>
        => Apply<T, GreaterThanOperator<T>>(x, y, destination);

    // int

    public static void GreaterThan(ReadOnlySpan<int> x, int y, Span<int> destination)
        => Apply<int, GreaterThanInt32Operator>(x, y, destination);

    public static void GreaterThan(ReadOnlySpan<int> x, ValueTuple<int, int> y, Span<int> destination)
        => Apply<int, GreaterThanInt32Operator>(x, y, destination);

    public static void GreaterThan(ReadOnlySpan<int> x, ValueTuple<int, int, int> y, Span<int> destination)
        => Apply<int, GreaterThanInt32Operator>(x, y, destination);

   public static void GreaterThan(ReadOnlySpan<int> x, ReadOnlySpan<int> y, Span<int> destination)
        => Apply<int, GreaterThanInt32Operator>(x, y, destination);

    // long

    public static void GreaterThan(ReadOnlySpan<long> x, long y, Span<long> destination)
        => Apply<long, GreaterThanInt64Operator>(x, y, destination);

    public static void GreaterThan(ReadOnlySpan<long> x, ValueTuple<long, long> y, Span<long> destination)
        => Apply<long, GreaterThanInt64Operator>(x, y, destination);

    public static void GreaterThan(ReadOnlySpan<long> x, ValueTuple<long, long, long> y, Span<long> destination)
        => Apply<long, GreaterThanInt64Operator>(x, y, destination);

    public static void GreaterThan(ReadOnlySpan<long> x, ReadOnlySpan<long> y, Span<long> destination)
        => Apply<long, GreaterThanInt64Operator>(x, y, destination);

    // float

    public static void GreaterThan(ReadOnlySpan<float> x, float y, Span<int> destination)
        => Apply<float, float, int, GreaterThanSingleOperator>(x, y, destination);

    public static void GreaterThan(ReadOnlySpan<float> x, ValueTuple<float, float> y, Span<int> destination)
        => Apply<float, float, int, GreaterThanSingleOperator>(x, y, destination);

    public static void GreaterThan(ReadOnlySpan<float> x, ValueTuple<float, float, float> y, Span<int> destination)
        => Apply<float, float, int, GreaterThanSingleOperator>(x, y, destination);

    public static void GreaterThan(ReadOnlySpan<float> x, ReadOnlySpan<float> y, Span<int> destination)
        => Apply<float, float, int, GreaterThanSingleOperator>(x, y, destination);

    // double

    public static void GreaterThan(ReadOnlySpan<double> x, double y, Span<long> destination)
        => Apply<double, double, long, GreaterThanDoubleOperator>(x, y, destination);

    public static void GreaterThan(ReadOnlySpan<double> x, ValueTuple<double, double> y, Span<long> destination)
        => Apply<double, double, long, GreaterThanDoubleOperator>(x, y, destination);

    public static void GreaterThan(ReadOnlySpan<double> x, ValueTuple<double, double, double> y, Span<long> destination)
        => Apply<double, double, long, GreaterThanDoubleOperator>(x, y, destination);

    public static void GreaterThan(ReadOnlySpan<double> x, ReadOnlySpan<double> y, Span<long> destination)
        => Apply<double, double, long, GreaterThanDoubleOperator>(x, y, destination);

}