namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    /// <summary>
    /// Returns the first element in the source span that is less than or equal to the specified value.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the span.</typeparam>
    /// <param name="source">The source span.</param>
    /// <param name="value">The value to compare against.</param>
    /// <returns>The first element that is less than or equal to the specified value, or null if no such element is found.</returns>
    public static T? FirstLessThanOrEqual<T>(ReadOnlySpan<T> source, T value)
        where T : struct, IComparisonOperators<T, T, bool>
        => Tensor.First<T, LessThanOrEqualAnyOperator<T>>(source, value);

    /// <summary>
    /// Returns the index of the first element in the source span that is less than or equal to the specified value.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the span.</typeparam>
    /// <param name="source">The source span.</param>
    /// <param name="value">The value to compare against.</param>
    /// <returns>The index of the first element that is less than or equal to the specified value, or -1 if no such element is found.</returns>
    public static int IndexOfFirstLessThanOrEqual<T>(ReadOnlySpan<T> source, T value)
        where T : struct, IComparisonOperators<T, T, bool>
        => Tensor.IndexOfFirst<T, LessThanOrEqualAnyOperator<T>>(source, value);

    /// <summary>
    /// Compares each element in the source span with the specified value and writes the result to the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The source span.</param>
    /// <param name="y">The value to compare against.</param>
    /// <param name="destination">The destination span.</param>
    public static void LessThanOrEqual<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, IComparisonOperators<T, T, bool>, IMultiplicativeIdentity<T, T>
        => Tensor.Apply<T, LessThanOrEqualOperator<T>>(x, y, destination);

    /// <summary>
    /// Compares each element in the source span with the specified tuple of values and writes the result to the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The source span.</param>
    /// <param name="y">The tuple of values to compare against.</param>
    /// <param name="destination">The destination span.</param>
    public static void LessThanOrEqual<T>(ReadOnlySpan<T> x, (T, T) y, Span<T> destination)
        where T : struct, IComparisonOperators<T, T, bool>, IMultiplicativeIdentity<T, T>
        => Tensor.Apply<T, LessThanOrEqualOperator<T>>(x, y, destination);

    /// <summary>
    /// Compares each element in the source span with the specified tuple of values and writes the result to the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The source span.</param>
    /// <param name="y">The tuple of values to compare against.</param>
    /// <param name="destination">The destination span.</param>
    public static void LessThanOrEqual<T>(ReadOnlySpan<T> x, (T, T, T) y, Span<T> destination)
        where T : struct, IComparisonOperators<T, T, bool>, IMultiplicativeIdentity<T, T>
        => Tensor.Apply<T, LessThanOrEqualOperator<T>>(x, y, destination);

    /// <summary>
    /// Compares each element in the source spans and writes the result to the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The first source span.</param>
    /// <param name="y">The second source span.</param>
    /// <param name="destination">The destination span.</param>
    public static void LessThanOrEqual<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> destination)
        where T : struct, IComparisonOperators<T, T, bool>, IMultiplicativeIdentity<T, T>
        => Tensor.Apply<T, LessThanOrEqualOperator<T>>(x, y, destination);

    // int

    /// <summary>
    /// Compares each element in the source span with the specified value and writes the result to the destination span.
    /// </summary>
    /// <param name="x">The source span.</param>
    /// <param name="y">The value to compare against.</param>
    /// <param name="destination">The destination span.</param>
    public static void LessThanOrEqual(ReadOnlySpan<int> x, int y, Span<int> destination)
        => Tensor.Apply<int, LessThanOrEqualInt32Operator>(x, y, destination);

    /// <summary>
    /// Compares each element in the source span with the specified tuple of values and writes the result to the destination span.
    /// </summary>
    /// <param name="x">The source span.</param>
    /// <param name="y">The tuple of values to compare against.</param>
    /// <param name="destination">The destination span.</param>
    public static void LessThanOrEqual(ReadOnlySpan<int> x, (int, int) y, Span<int> destination)
        => Tensor.Apply<int, LessThanOrEqualInt32Operator>(x, y, destination);

    /// <summary>
    /// Compares each element in the source span with the specified tuple of values and writes the result to the destination span.
    /// </summary>
    /// <param name="x">The source span.</param>
    /// <param name="y">The tuple of values to compare against.</param>
    /// <param name="destination">The destination span.</param>
    public static void LessThanOrEqual(ReadOnlySpan<int> x, (int, int, int) y, Span<int> destination)
        => Tensor.Apply<int, LessThanOrEqualInt32Operator>(x, y, destination);

    /// <summary>
    /// Compares each element in the source spans and writes the result to the destination span.
    /// </summary>
    /// <param name="x">The first source span.</param>
    /// <param name="y">The second source span.</param>
    /// <param name="destination">The destination span.</param>
    public static void LessThanOrEqual(ReadOnlySpan<int> x, ReadOnlySpan<int> y, Span<int> destination)
        => Tensor.Apply<int, LessThanOrEqualInt32Operator>(x, y, destination);

    // long

    /// <summary>
    /// Compares each element in the source span with the specified value and writes the result to the destination span.
    /// </summary>
    /// <param name="x">The source span.</param>
    /// <param name="y">The value to compare against.</param>
    /// <param name="destination">The destination span.</param>
    public static void LessThanOrEqual(ReadOnlySpan<long> x, long y, Span<long> destination)
        => Tensor.Apply<long, LessThanOrEqualInt64Operator>(x, y, destination);

    /// <summary>
    /// Compares each element in the source span with the specified tuple of values and writes the result to the destination span.
    /// </summary>
    /// <param name="x">The source span.</param>
    /// <param name="y">The tuple of values to compare against.</param>
    /// <param name="destination">The destination span.</param>
    public static void LessThanOrEqual(ReadOnlySpan<long> x, (long, long) y, Span<long> destination)
        => Tensor.Apply<long, LessThanOrEqualInt64Operator>(x, y, destination);

    /// <summary>
    /// Compares each element in the source span with the specified tuple of values and writes the result to the destination span.
    /// </summary>
    /// <param name="x">The source span.</param>
    /// <param name="y">The tuple of values to compare against.</param>
    /// <param name="destination">The destination span.</param>
    public static void LessThanOrEqual(ReadOnlySpan<long> x, (long, long, long) y, Span<long> destination)
        => Tensor.Apply<long, LessThanOrEqualInt64Operator>(x, y, destination);

    /// <summary>
    /// Compares each element in the source spans and writes the result to the destination span.
    /// </summary>
    /// <param name="x">The first source span.</param>
    /// <param name="y">The second source span.</param>
    /// <param name="destination">The destination span.</param>
    public static void LessThanOrEqual(ReadOnlySpan<long> x, ReadOnlySpan<long> y, Span<long> destination)
        => Tensor.Apply<long, LessThanOrEqualInt64Operator>(x, y, destination);

    // float

    /// <summary>
    /// Compares each element in the source span with the specified value and writes the result to the destination span.
    /// </summary>
    /// <param name="x">The source span.</param>
    /// <param name="y">The value to compare against.</param>
    /// <param name="destination">The destination span.</param>
    public static void LessThanOrEqual(ReadOnlySpan<float> x, float y, Span<int> destination)
        => Tensor.Apply<float, float, int, LessThanOrEqualSingleOperator>(x, y, destination);

    /// <summary>
    /// Compares each element in the source span with the specified tuple of values and writes the result to the destination span.
    /// </summary>
    /// <param name="x">The source span.</param>
    /// <param name="y">The tuple of values to compare against.</param>
    /// <param name="destination">The destination span.</param>
    public static void LessThanOrEqual(ReadOnlySpan<float> x, (float, float) y, Span<int> destination)
        => Tensor.Apply<float, float, int, LessThanOrEqualSingleOperator>(x, y, destination);

    /// <summary>
    /// Compares each element in the source span with the specified tuple of values and writes the result to the destination span.
    /// </summary>
    /// <param name="x">The source span.</param>
    /// <param name="y">The tuple of values to compare against.</param>
    /// <param name="destination">The destination span.</param>
    public static void LessThanOrEqual(ReadOnlySpan<float> x, (float, float, float) y, Span<int> destination)
        => Tensor.Apply<float, float, int, LessThanOrEqualSingleOperator>(x, y, destination);

    /// <summary>
    /// Compares each element in the source spans and writes the result to the destination span.
    /// </summary>
    /// <param name="x">The first source span.</param>
    /// <param name="y">The second source span.</param>
    /// <param name="destination">The destination span.</param>
    public static void LessThanOrEqual(ReadOnlySpan<float> x, ReadOnlySpan<float> y, Span<int> destination)
        => Tensor.Apply<float, float, int, LessThanOrEqualSingleOperator>(x, y, destination);

    // double

    /// <summary>
    /// Compares each element in the source span with the specified value and writes the result to the destination span.
    /// </summary>
    /// <param name="x">The source span.</param>
    /// <param name="y">The value to compare against.</param>
    /// <param name="destination">The destination span.</param>
    public static void LessThanOrEqual(ReadOnlySpan<double> x, double y, Span<long> destination)
        => Tensor.Apply<double, double, long, LessThanOrEqualDoubleOperator>(x, y, destination);

    /// <summary>
    /// Compares each element in the source span with the specified tuple of values and writes the result to the destination span.
    /// </summary>
    /// <param name="x">The source span.</param>
    /// <param name="y">The tuple of values to compare against.</param>
    /// <param name="destination">The destination span.</param>
    public static void LessThanOrEqual(ReadOnlySpan<double> x, (double, double) y, Span<long> destination)
        => Tensor.Apply<double, double, long, LessThanOrEqualDoubleOperator>(x, y, destination);

    /// <summary>
    /// Compares each element in the source span with the specified tuple of values and writes the result to the destination span.
    /// </summary>
    /// <param name="x">The source span.</param>
    /// <param name="y">The tuple of values to compare against.</param>
    /// <param name="destination">The destination span.</param>
    public static void LessThanOrEqual(ReadOnlySpan<double> x, (double, double, double) y, Span<long> destination)
        => Tensor.Apply<double, double, long, LessThanOrEqualDoubleOperator>(x, y, destination);

    /// <summary>
    /// Compares each element in the source spans and writes the result to the destination span.
    /// </summary>
    /// <param name="x">The first source span.</param>
    /// <param name="y">The second source span.</param>
    /// <param name="destination">The destination span.</param>
    public static void LessThanOrEqual(ReadOnlySpan<double> x, ReadOnlySpan<double> y, Span<long> destination)
        => Tensor.Apply<double, double, long, LessThanOrEqualDoubleOperator>(x, y, destination);
}