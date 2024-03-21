namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static int IndexOfLessThan<T>(ReadOnlySpan<T> source, T value)
        where T : struct, IComparisonOperators<T, T, bool>
        => Tensor.IndexOfPredicate<T, LessThanAnyOperator<T>>(source, value);

    public static void LessThan<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, IComparisonOperators<T, T, bool>, IMultiplicativeIdentity<T, T>
        => Tensor.Apply<T, LessThanOperator<T>>(x, y, destination);

    public static void LessThan<T>(ReadOnlySpan<T> x, (T, T) y, Span<T> destination)
        where T : struct, IComparisonOperators<T, T, bool>, IMultiplicativeIdentity<T, T>
        => Tensor.Apply<T, LessThanOperator<T>>(x, y, destination);

    public static void LessThan<T>(ReadOnlySpan<T> x, (T, T, T) y, Span<T> destination)
        where T : struct, IComparisonOperators<T, T, bool>, IMultiplicativeIdentity<T, T>
        => Tensor.Apply<T, LessThanOperator<T>>(x, y, destination);

    public static void LessThan<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> destination)
        where T : struct, IComparisonOperators<T, T, bool>, IMultiplicativeIdentity<T, T>
        => Tensor.Apply<T, LessThanOperator<T>>(x, y, destination);

    // int

    public static void LessThan(ReadOnlySpan<int> x, int y, Span<int> destination)
        => Tensor.Apply<int, LessThanInt32Operator>(x, y, destination);

    public static void LessThan(ReadOnlySpan<int> x, (int, int) y, Span<int> destination)
        => Tensor.Apply<int, LessThanInt32Operator>(x, y, destination);

    public static void LessThan(ReadOnlySpan<int> x, (int, int, int) y, Span<int> destination)
        => Tensor.Apply<int, LessThanInt32Operator>(x, y, destination);

    public static void LessThan(ReadOnlySpan<int> x, ReadOnlySpan<int> y, Span<int> destination)
        => Tensor.Apply<int, LessThanInt32Operator>(x, y, destination);

    // long

    public static void LessThan(ReadOnlySpan<long> x, long y, Span<long> destination)
        => Tensor.Apply<long, LessThanInt64Operator>(x, y, destination);

    public static void LessThan(ReadOnlySpan<long> x, (long, long) y, Span<long> destination)
        => Tensor.Apply<long, LessThanInt64Operator>(x, y, destination);

    public static void LessThan(ReadOnlySpan<long> x, (long, long, long) y, Span<long> destination)
        => Tensor.Apply<long, LessThanInt64Operator>(x, y, destination);

    public static void LessThan(ReadOnlySpan<long> x, ReadOnlySpan<long> y, Span<long> destination)
        => Tensor.Apply<long, LessThanInt64Operator>(x, y, destination);

    // float

    public static void LessThan(ReadOnlySpan<float> x, float y, Span<int> destination)
        => Tensor.Apply<float, float, int, LessThanSingleOperator>(x, y, destination);

    public static void LessThan(ReadOnlySpan<float> x, (float, float) y, Span<int> destination)
        => Tensor.Apply<float, float, int, LessThanSingleOperator>(x, y, destination);

    public static void LessThan(ReadOnlySpan<float> x, (float, float, float) y, Span<int> destination)
        => Tensor.Apply<float, float, int, LessThanSingleOperator>(x, y, destination);

    public static void LessThan(ReadOnlySpan<float> x, ReadOnlySpan<float> y, Span<int> destination)
        => Tensor.Apply<float, float, int, LessThanSingleOperator>(x, y, destination);

    // double

    public static void LessThan(ReadOnlySpan<double> x, double y, Span<long> destination)
        => Tensor.Apply<double, double, long, LessThanDoubleOperator>(x, y, destination);

    public static void LessThan(ReadOnlySpan<double> x, (double, double) y, Span<long> destination)
        => Tensor.Apply<double, double, long, LessThanDoubleOperator>(x, y, destination);

    public static void LessThan(ReadOnlySpan<double> x, (double, double, double) y, Span<long> destination)
        => Tensor.Apply<double, double, long, LessThanDoubleOperator>(x, y, destination);

    public static void LessThan(ReadOnlySpan<double> x, ReadOnlySpan<double> y, Span<long> destination)
        => Tensor.Apply<double, double, long, LessThanDoubleOperator>(x, y, destination);

}