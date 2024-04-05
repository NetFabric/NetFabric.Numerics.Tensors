namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    /// <summary>
    /// Returns the first element in the <paramref name="source"/> span that is greater than <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the span.</typeparam>
    /// <param name="source">The source span.</param>
    /// <param name="value">The value to compare against.</param>
    /// <returns>The first element greater than <paramref name="value"/>, or <c>null</c> if no such element is found.</returns>
    public static T? FirstGreaterThan<T>(ReadOnlySpan<T> source, T value)
        where T : struct, IComparisonOperators<T, T, bool>
        => Tensor.First<T, GreaterThanAnyOperator<T>>(source, value);

    /// <summary>
    /// Returns the index of the first element in the <paramref name="source"/> span that is greater than <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the span.</typeparam>
    /// <param name="source">The source span.</param>
    /// <param name="value">The value to compare against.</param>
    /// <returns>The index of the first element greater than <paramref name="value"/>, or -1 if no such element is found.</returns>
    public static int IndexOfFirstGreaterThan<T>(ReadOnlySpan<T> source, T value)
        where T : struct, IComparisonOperators<T, T, bool>
        => Tensor.IndexOfFirst<T, GreaterThanAnyOperator<T>>(source, value);

    /// <summary>
    /// Compares each element in the <paramref name="x"/> span with <paramref name="y"/> and stores the result in the <paramref name="destination"/> span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The first source span.</param>
    /// <param name="y">The value to compare against.</param>
    /// <param name="destination">The destination span to store the comparison result.</param>
    public static void GreaterThan<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, IComparisonOperators<T, T, bool>, IMultiplicativeIdentity<T, T>
        => Tensor.Apply<T, GreaterThanOperator<T>>(x, y, destination);

    /// <summary>
    /// Compares each element in the <paramref name="x"/> span with <paramref name="y"/> and stores the result in the <paramref name="destination"/> span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The first source span.</param>
    /// <param name="y">The value to compare against.</param>
    /// <param name="destination">The destination span to store the comparison result.</param>
    public static void GreaterThan<T>(ReadOnlySpan<T> x, (T, T) y, Span<T> destination)
        where T : struct, IComparisonOperators<T, T, bool>, IMultiplicativeIdentity<T, T>
        => Tensor.Apply<T, GreaterThanOperator<T>>(x, y, destination);

    /// <summary>
    /// Compares each element in the <paramref name="x"/> span with <paramref name="y"/> and stores the result in the <paramref name="destination"/> span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The first source span.</param>
    /// <param name="y">The value to compare against.</param>
    /// <param name="destination">The destination span to store the comparison result.</param>
    public static void GreaterThan<T>(ReadOnlySpan<T> x, (T, T, T) y, Span<T> destination)
        where T : struct, IComparisonOperators<T, T, bool>, IMultiplicativeIdentity<T, T>
        => Tensor.Apply<T, GreaterThanOperator<T>>(x, y, destination);

    /// <summary>
    /// Compares each element in the <paramref name="x"/> span with the corresponding element in the <paramref name="y"/> span and stores the result in the <paramref name="destination"/> span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The first source span.</param>
    /// <param name="y">The second source span.</param>
    /// <param name="destination">The destination span to store the comparison result.</param>
    public static void GreaterThan<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> destination)
        where T : struct, IComparisonOperators<T, T, bool>, IMultiplicativeIdentity<T, T>
        => Tensor.Apply<T, GreaterThanOperator<T>>(x, y, destination);

    // int

    /// <summary>
    /// Compares each element in the <paramref name="x"/> span with <paramref name="y"/> and stores the result in the <paramref name="destination"/> span.
    /// </summary>
    /// <param name="x">The first source span.</param>
    /// <param name="y">The value to compare against.</param>
    /// <param name="destination">The destination span to store the comparison result.</param>
    public static void GreaterThan(ReadOnlySpan<int> x, int y, Span<int> destination)
        => Tensor.Apply<int, GreaterThanInt32Operator>(x, y, destination);

    /// <summary>
    /// Compares each element in the <paramref name="x"/> span with <paramref name="y"/> and stores the result in the <paramref name="destination"/> span.
    /// </summary>
    /// <param name="x">The first source span.</param>
    /// <param name="y">The value to compare against.</param>
    /// <param name="destination">The destination span to store the comparison result.</param>
    public static void GreaterThan(ReadOnlySpan<int> x, (int, int) y, Span<int> destination)
        => Tensor.Apply<int, GreaterThanInt32Operator>(x, y, destination);

    /// <summary>
    /// Compares each element in the <paramref name="x"/> span with <paramref name="y"/> and stores the result in the <paramref name="destination"/> span.
    /// </summary>
    /// <param name="x">The first source span.</param>
    /// <param name="y">The value to compare against.</param>
    /// <param name="destination">The destination span to store the comparison result.</param>
    public static void GreaterThan(ReadOnlySpan<int> x, (int, int, int) y, Span<int> destination)
        => Tensor.Apply<int, GreaterThanInt32Operator>(x, y, destination);

    /// <summary>
    /// Compares each element in the <paramref name="x"/> span with the corresponding element in the <paramref name="y"/> span and stores the result in the <paramref name="destination"/> span.
    /// </summary>
    /// <param name="x">The first source span.</param>
    /// <param name="y">The second source span.</param>
    /// <param name="destination">The destination span to store the comparison result.</param>
    public static void GreaterThan(ReadOnlySpan<int> x, ReadOnlySpan<int> y, Span<int> destination)
        => Tensor.Apply<int, GreaterThanInt32Operator>(x, y, destination);

    // long

    /// <summary>
    /// Compares each element in the <paramref name="x"/> span with <paramref name="y"/> and stores the result in the <paramref name="destination"/> span.
    /// </summary>
    /// <param name="x">The first source span.</param>
    /// <param name="y">The value to compare against.</param>
    /// <param name="destination">The destination span to store the comparison result.</param>
    public static void GreaterThan(ReadOnlySpan<long> x, long y, Span<long> destination)
        => Tensor.Apply<long, GreaterThanInt64Operator>(x, y, destination);

    /// <summary>
    /// Compares each element in the <paramref name="x"/> span with <paramref name="y"/> and stores the result in the <paramref name="destination"/> span.
    /// </summary>
    /// <param name="x">The first source span.</param>
    /// <param name="y">The value to compare against.</param>
    /// <param name="destination">The destination span to store the comparison result.</param>
    public static void GreaterThan(ReadOnlySpan<long> x, (long, long) y, Span<long> destination)
        => Tensor.Apply<long, GreaterThanInt64Operator>(x, y, destination);

    /// <summary>
    /// Compares each element in the <paramref name="x"/> span with <paramref name="y"/> and stores the result in the <paramref name="destination"/> span.
    /// </summary>
    /// <param name="x">The first source span.</param>
    /// <param name="y">The value to compare against.</param>
    /// <param name="destination">The destination span to store the comparison result.</param>
    public static void GreaterThan(ReadOnlySpan<long> x, (long, long, long) y, Span<long> destination)
        => Tensor.Apply<long, GreaterThanInt64Operator>(x, y, destination);

    /// <summary>
    /// Compares each element in the <paramref name="x"/> span with the corresponding element in the <paramref name="y"/> span and stores the result in the <paramref name="destination"/> span.
    /// </summary>
    /// <param name="x">The first source span.</param>
    /// <param name="y">The second source span.</param>
    /// <param name="destination">The destination span to store the comparison result.</param>
    public static void GreaterThan(ReadOnlySpan<long> x, ReadOnlySpan<long> y, Span<long> destination)
        => Tensor.Apply<long, GreaterThanInt64Operator>(x, y, destination);

    // float

    /// <summary>
    /// Compares each element in the <paramref name="x"/> span with <paramref name="y"/> and stores the result in the <paramref name="destination"/> span.
    /// </summary>
    /// <param name="x">The first source span.</param>
    /// <param name="y">The value to compare against.</param>
    /// <param name="destination">The destination span to store the comparison result.</param>
    public static void GreaterThan(ReadOnlySpan<float> x, float y, Span<int> destination)
        => Tensor.Apply<float, float, int, GreaterThanSingleOperator>(x, y, destination);

    /// <summary>
    /// Compares each element in the <paramref name="x"/> span with <paramref name="y"/> and stores the result in the <paramref name="destination"/> span.
    /// </summary>
    /// <param name="x">The first source span.</param>
    /// <param name="y">The value to compare against.</param>
    /// <param name="destination">The destination span to store the comparison result.</param>
    public static void GreaterThan(ReadOnlySpan<float> x, (float, float) y, Span<int> destination)
        => Tensor.Apply<float, float, int, GreaterThanSingleOperator>(x, y, destination);

    /// <summary>
    /// Compares each element in the <paramref name="x"/> span with <paramref name="y"/> and stores the result in the <paramref name="destination"/> span.
    /// </summary>
    /// <param name="x">The first source span.</param>
    /// <param name="y">The value to compare against.</param>
    /// <param name="destination">The destination span to store the comparison result.</param>
    public static void GreaterThan(ReadOnlySpan<float> x, (float, float, float) y, Span<int> destination)
        => Tensor.Apply<float, float, int, GreaterThanSingleOperator>(x, y, destination);

    /// <summary>
    /// Compares each element in the <paramref name="x"/> span with the corresponding element in the <paramref name="y"/> span and stores the result in the <paramref name="destination"/> span.
    /// </summary>
    /// <param name="x">The first source span.</param>
    /// <param name="y">The second source span.</param>
    /// <param name="destination">The destination span to store the comparison result.</param>
    public static void GreaterThan(ReadOnlySpan<float> x, ReadOnlySpan<float> y, Span<int> destination)
        => Tensor.Apply<float, float, int, GreaterThanSingleOperator>(x, y, destination);

    // double

    /// <summary>
    /// Compares each element in the <paramref name="x"/> span with <paramref name="y"/> and stores the result in the <paramref name="destination"/> span.
    /// </summary>
    /// <param name="x">The first source span.</param>
    /// <param name="y">The value to compare against.</param>
    /// <param name="destination">The destination span to store the comparison result.</param>
    public static void GreaterThan(ReadOnlySpan<double> x, double y, Span<long> destination)
        => Tensor.Apply<double, double, long, GreaterThanDoubleOperator>(x, y, destination);

    /// <summary>
    /// Compares each element in the <paramref name="x"/> span with <paramref name="y"/> and stores the result in the <paramref name="destination"/> span.
    /// </summary>
    /// <param name="x">The first source span.</param>
    /// <param name="y">The value to compare against.</param>
    /// <param name="destination">The destination span to store the comparison result.</param>
    public static void GreaterThan(ReadOnlySpan<double> x, (double, double) y, Span<long> destination)
        => Tensor.Apply<double, double, long, GreaterThanDoubleOperator>(x, y, destination);

    /// <summary>
    /// Compares each element in the <paramref name="x"/> span with <paramref name="y"/> and stores the result in the <paramref name="destination"/> span.
    /// </summary>
    /// <param name="x">The first source span.</param>
    /// <param name="y">The value to compare against.</param>
    /// <param name="destination">The destination span to store the comparison result.</param>
    public static void GreaterThan(ReadOnlySpan<double> x, (double, double, double) y, Span<long> destination)
        => Tensor.Apply<double, double, long, GreaterThanDoubleOperator>(x, y, destination);

    /// <summary>
    /// Compares each element in the <paramref name="x"/> span with the corresponding element in the <paramref name="y"/> span and stores the result in the <paramref name="destination"/> span.
    /// </summary>
    /// <param name="x">The first source span.</param>
    /// <param name="y">The second source span.</param>
    /// <param name="destination">The destination span to store the comparison result.</param>
    public static void GreaterThan(ReadOnlySpan<double> x, ReadOnlySpan<double> y, Span<long> destination)
        => Tensor.Apply<double, double, long, GreaterThanDoubleOperator>(x, y, destination);
}