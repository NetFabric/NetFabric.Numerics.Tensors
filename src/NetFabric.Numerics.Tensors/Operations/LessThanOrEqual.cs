namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static T? FirstLessThanOrEqual<T>(ReadOnlySpan<T> source, T value)
        where T : struct, IComparisonOperators<T, T, bool>
        => Tensor.First<T, LessThanOrEqualAnyOperator<T>>(source, value);

    public static int IndexOfFirstLessThanOrEqual<T>(ReadOnlySpan<T> source, T value)
        where T : struct, IComparisonOperators<T, T, bool>
        => Tensor.IndexOfFirst<T, LessThanOrEqualAnyOperator<T>>(source, value);

    public static void LessThanOrEqual<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, IComparisonOperators<T, T, bool>, IMultiplicativeIdentity<T, T>
        => Tensor.Apply<T, LessThanOrEqualOperator<T>>(x, y, destination);

    public static void LessThanOrEqual<T>(ReadOnlySpan<T> x, (T, T) y, Span<T> destination)
        where T : struct, IComparisonOperators<T, T, bool>, IMultiplicativeIdentity<T, T>
        => Tensor.Apply<T, LessThanOrEqualOperator<T>>(x, y, destination);

    public static void LessThanOrEqual<T>(ReadOnlySpan<T> x, (T, T, T) y, Span<T> destination)
        where T : struct, IComparisonOperators<T, T, bool>, IMultiplicativeIdentity<T, T>
        => Tensor.Apply<T, LessThanOrEqualOperator<T>>(x, y, destination);

    public static void LessThanOrEqual<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> destination)
        where T : struct, IComparisonOperators<T, T, bool>, IMultiplicativeIdentity<T, T>
        => Tensor.Apply<T, LessThanOrEqualOperator<T>>(x, y, destination);

    // int

    public static void LessThanOrEqual(ReadOnlySpan<int> x, int y, Span<int> destination)
        => Tensor.Apply<int, LessThanOrEqualInt32Operator>(x, y, destination);

    public static void LessThanOrEqual(ReadOnlySpan<int> x, (int, int) y, Span<int> destination)
        => Tensor.Apply<int, LessThanOrEqualInt32Operator>(x, y, destination);

    public static void LessThanOrEqual(ReadOnlySpan<int> x, (int, int, int) y, Span<int> destination)
        => Tensor.Apply<int, LessThanOrEqualInt32Operator>(x, y, destination);

    public static void LessThanOrEqual(ReadOnlySpan<int> x, ReadOnlySpan<int> y, Span<int> destination)
        => Tensor.Apply<int, LessThanOrEqualInt32Operator>(x, y, destination);

    // long

    public static void LessThanOrEqual(ReadOnlySpan<long> x, long y, Span<long> destination)
        => Tensor.Apply<long, LessThanOrEqualInt64Operator>(x, y, destination);

    public static void LessThanOrEqual(ReadOnlySpan<long> x, (long, long) y, Span<long> destination)
        => Tensor.Apply<long, LessThanOrEqualInt64Operator>(x, y, destination);

    public static void LessThanOrEqual(ReadOnlySpan<long> x, (long, long, long) y, Span<long> destination)
        => Tensor.Apply<long, LessThanOrEqualInt64Operator>(x, y, destination);

    public static void LessThanOrEqual(ReadOnlySpan<long> x, ReadOnlySpan<long> y, Span<long> destination)
        => Tensor.Apply<long, LessThanOrEqualInt64Operator>(x, y, destination);

    // float

    public static void LessThanOrEqual(ReadOnlySpan<float> x, float y, Span<int> destination)
        => Tensor.Apply<float, float, int, LessThanOrEqualSingleOperator>(x, y, destination);

    public static void LessThanOrEqual(ReadOnlySpan<float> x, (float, float) y, Span<int> destination)
        => Tensor.Apply<float, float, int, LessThanOrEqualSingleOperator>(x, y, destination);

    public static void LessThanOrEqual(ReadOnlySpan<float> x, (float, float, float) y, Span<int> destination)
        => Tensor.Apply<float, float, int, LessThanOrEqualSingleOperator>(x, y, destination);

    public static void LessThanOrEqual(ReadOnlySpan<float> x, ReadOnlySpan<float> y, Span<int> destination)
        => Tensor.Apply<float, float, int, LessThanOrEqualSingleOperator>(x, y, destination);

    // double

    public static void LessThanOrEqual(ReadOnlySpan<double> x, double y, Span<long> destination)
        => Tensor.Apply<double, double, long, LessThanOrEqualDoubleOperator>(x, y, destination);

    public static void LessThanOrEqual(ReadOnlySpan<double> x, (double, double) y, Span<long> destination)
        => Tensor.Apply<double, double, long, LessThanOrEqualDoubleOperator>(x, y, destination);

    public static void LessThanOrEqual(ReadOnlySpan<double> x, (double, double, double) y, Span<long> destination)
        => Tensor.Apply<double, double, long, LessThanOrEqualDoubleOperator>(x, y, destination);

    public static void LessThanOrEqual(ReadOnlySpan<double> x, ReadOnlySpan<double> y, Span<long> destination)
        => Tensor.Apply<double, double, long, LessThanOrEqualDoubleOperator>(x, y, destination);

}