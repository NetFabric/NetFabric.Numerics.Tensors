namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static T? FirstGreaterThan<T>(ReadOnlySpan<T> source, T value)
        where T : struct, IComparisonOperators<T, T, bool>
        => Tensor.First<T, GreaterThanAnyOperator<T>>(source, value);

    public static int IndexOfFirstGreaterThan<T>(ReadOnlySpan<T> source, T value)
        where T : struct, IComparisonOperators<T, T, bool>
        => Tensor.IndexOfFirst<T, GreaterThanAnyOperator<T>>(source, value);

    public static void GreaterThan<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, IComparisonOperators<T, T, bool>, IMultiplicativeIdentity<T, T>
        => Tensor.Apply<T, GreaterThanOperator<T>>(x, y, destination);

    public static void GreaterThan<T>(ReadOnlySpan<T> x, (T, T) y, Span<T> destination)
        where T : struct, IComparisonOperators<T, T, bool>, IMultiplicativeIdentity<T, T>
        => Tensor.Apply<T, GreaterThanOperator<T>>(x, y, destination);

    public static void GreaterThan<T>(ReadOnlySpan<T> x, (T, T, T) y, Span<T> destination)
        where T : struct, IComparisonOperators<T, T, bool>, IMultiplicativeIdentity<T, T>
        => Tensor.Apply<T, GreaterThanOperator<T>>(x, y, destination);

    public static void GreaterThan<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> destination)
        where T : struct, IComparisonOperators<T, T, bool>, IMultiplicativeIdentity<T, T>
        => Tensor.Apply<T, GreaterThanOperator<T>>(x, y, destination);

    // int

    public static void GreaterThan(ReadOnlySpan<int> x, int y, Span<int> destination)
        => Tensor.Apply<int, GreaterThanInt32Operator>(x, y, destination);

    public static void GreaterThan(ReadOnlySpan<int> x, (int, int) y, Span<int> destination)
        => Tensor.Apply<int, GreaterThanInt32Operator>(x, y, destination);

    public static void GreaterThan(ReadOnlySpan<int> x, (int, int, int) y, Span<int> destination)
        => Tensor.Apply<int, GreaterThanInt32Operator>(x, y, destination);

   public static void GreaterThan(ReadOnlySpan<int> x, ReadOnlySpan<int> y, Span<int> destination)
        => Tensor.Apply<int, GreaterThanInt32Operator>(x, y, destination);

    // long

    public static void GreaterThan(ReadOnlySpan<long> x, long y, Span<long> destination)
        => Tensor.Apply<long, GreaterThanInt64Operator>(x, y, destination);

    public static void GreaterThan(ReadOnlySpan<long> x, (long, long) y, Span<long> destination)
        => Tensor.Apply<long, GreaterThanInt64Operator>(x, y, destination);

    public static void GreaterThan(ReadOnlySpan<long> x, (long, long, long) y, Span<long> destination)
        => Tensor.Apply<long, GreaterThanInt64Operator>(x, y, destination);

    public static void GreaterThan(ReadOnlySpan<long> x, ReadOnlySpan<long> y, Span<long> destination)
        => Tensor.Apply<long, GreaterThanInt64Operator>(x, y, destination);

    // float

    public static void GreaterThan(ReadOnlySpan<float> x, float y, Span<int> destination)
        => Tensor.Apply<float, float, int, GreaterThanSingleOperator>(x, y, destination);

    public static void GreaterThan(ReadOnlySpan<float> x, (float, float) y, Span<int> destination)
        => Tensor.Apply<float, float, int, GreaterThanSingleOperator>(x, y, destination);

    public static void GreaterThan(ReadOnlySpan<float> x, (float, float, float) y, Span<int> destination)
        => Tensor.Apply<float, float, int, GreaterThanSingleOperator>(x, y, destination);

    public static void GreaterThan(ReadOnlySpan<float> x, ReadOnlySpan<float> y, Span<int> destination)
        => Tensor.Apply<float, float, int, GreaterThanSingleOperator>(x, y, destination);

    // double

    public static void GreaterThan(ReadOnlySpan<double> x, double y, Span<long> destination)
        => Tensor.Apply<double, double, long, GreaterThanDoubleOperator>(x, y, destination);

    public static void GreaterThan(ReadOnlySpan<double> x, (double, double) y, Span<long> destination)
        => Tensor.Apply<double, double, long, GreaterThanDoubleOperator>(x, y, destination);

    public static void GreaterThan(ReadOnlySpan<double> x, (double, double, double) y, Span<long> destination)
        => Tensor.Apply<double, double, long, GreaterThanDoubleOperator>(x, y, destination);

    public static void GreaterThan(ReadOnlySpan<double> x, ReadOnlySpan<double> y, Span<long> destination)
        => Tensor.Apply<double, double, long, GreaterThanDoubleOperator>(x, y, destination);

}