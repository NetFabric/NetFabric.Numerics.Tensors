namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    /// <summary>
    /// Returns the first element in the source tensor that is greater than or equal to the specified value.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the tensor.</typeparam>
    /// <param name="source">The source tensor.</param>
    /// <param name="value">The value to compare against.</param>
    /// <returns>The first element in the source tensor that is greater than or equal to the specified value.</returns>
    public static T? FirstGreaterThanOrEqual<T>(ReadOnlySpan<T> source, T value)
        where T : struct, IComparisonOperators<T, T, bool>
        => Tensor.First<T, GreaterThanOrEqualAnyOperator<T>>(source, value);

    /// <summary>
    /// Returns the index of the first element in the source tensor that is greater than or equal to the specified value.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the tensor.</typeparam>
    /// <param name="source">The source tensor.</param>
    /// <param name="value">The value to compare against.</param>
    /// <returns>The index of the first element in the source tensor that is greater than or equal to the specified value.</returns>
    public static int IndexOfFirstGreaterThanOrEqual<T>(ReadOnlySpan<T> source, T value)
        where T : struct, IComparisonOperators<T, T, bool>
        => Tensor.IndexOfFirst<T, GreaterThanOrEqualAnyOperator<T>>(source, value);

    /// <summary>
    /// Applies the greater than or equal comparison operation element-wise on the source tensor and stores the result in the destination tensor.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the tensors.</typeparam>
    /// <param name="x">The source tensor.</param>
    /// <param name="y">The value to compare against.</param>
    /// <param name="destination">The destination tensor to store the result.</param>
    public static void GreaterThanOrEqual<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, IComparisonOperators<T, T, bool>, IMultiplicativeIdentity<T, T>
        => Tensor.Apply<T, GreaterThanOrEqualOperator<T>>(x, y, destination);

    /// <summary>
    /// Applies the greater than or equal comparison operation element-wise on the source tensor and a tuple of values, and stores the result in the destination tensor.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the tensors.</typeparam>
    /// <param name="x">The source tensor.</param>
    /// <param name="y">The tuple of values to compare against.</param>
    /// <param name="destination">The destination tensor to store the result.</param>
    public static void GreaterThanOrEqual<T>(ReadOnlySpan<T> x, (T, T) y, Span<T> destination)
        where T : struct, IComparisonOperators<T, T, bool>, IMultiplicativeIdentity<T, T>
        => Tensor.Apply<T, GreaterThanOrEqualOperator<T>>(x, y, destination);

    /// <summary>
    /// Applies the greater than or equal comparison operation element-wise on the source tensor and a tuple of values, and stores the result in the destination tensor.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the tensors.</typeparam>
    /// <param name="x">The source tensor.</param>
    /// <param name="y">The tuple of values to compare against.</param>
    /// <param name="destination">The destination tensor to store the result.</param>
    public static void GreaterThanOrEqual<T>(ReadOnlySpan<T> x, (T, T, T) y, Span<T> destination)
        where T : struct, IComparisonOperators<T, T, bool>, IMultiplicativeIdentity<T, T>
        => Tensor.Apply<T, GreaterThanOrEqualOperator<T>>(x, y, destination);

    /// <summary>
    /// Applies the greater than or equal comparison operation element-wise on the source tensors and stores the result in the destination tensor.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the tensors.</typeparam>
    /// <param name="x">The first source tensor.</param>
    /// <param name="y">The second source tensor.</param>
    /// <param name="destination">The destination tensor to store the result.</param>
    public static void GreaterThanOrEqual<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> destination)
        where T : struct, IComparisonOperators<T, T, bool>, IMultiplicativeIdentity<T, T>
        => Tensor.Apply<T, GreaterThanOrEqualOperator<T>>(x, y, destination);

    // int

    /// <summary>
    /// Applies the greater than or equal comparison operation element-wise on the source tensor and stores the result in the destination tensor.
    /// </summary>
    /// <param name="x">The source tensor.</param>
    /// <param name="y">The value to compare against.</param>
    /// <param name="destination">The destination tensor to store the result.</param>
    public static void GreaterThanOrEqual(ReadOnlySpan<int> x, int y, Span<int> destination)
        => Tensor.Apply<int, GreaterThanOrEqualInt32Operator>(x, y, destination);

    /// <summary>
    /// Applies the greater than or equal comparison operation element-wise on the source tensor and a tuple of values, and stores the result in the destination tensor.
    /// </summary>
    /// <param name="x">The source tensor.</param>
    /// <param name="y">The tuple of values to compare against.</param>
    /// <param name="destination">The destination tensor to store the result.</param>
    public static void GreaterThanOrEqual(ReadOnlySpan<int> x, (int, int) y, Span<int> destination)
        => Tensor.Apply<int, GreaterThanOrEqualInt32Operator>(x, y, destination);

    /// <summary>
    /// Applies the greater than or equal comparison operation element-wise on the source tensor and a tuple of values, and stores the result in the destination tensor.
    /// </summary>
    /// <param name="x">The source tensor.</param>
    /// <param name="y">The tuple of values to compare against.</param>
    /// <param name="destination">The destination tensor to store the result.</param>
    public static void GreaterThanOrEqual(ReadOnlySpan<int> x, (int, int, int) y, Span<int> destination)
        => Tensor.Apply<int, GreaterThanOrEqualInt32Operator>(x, y, destination);

    /// <summary>
    /// Applies the greater than or equal comparison operation element-wise on the source tensors and stores the result in the destination tensor.
    /// </summary>
    /// <param name="x">The first source tensor.</param>
    /// <param name="y">The second source tensor.</param>
    /// <param name="destination">The destination tensor to store the result.</param>
    public static void GreaterThanOrEqual(ReadOnlySpan<int> x, ReadOnlySpan<int> y, Span<int> destination)
        => Tensor.Apply<int, GreaterThanOrEqualInt32Operator>(x, y, destination);

    // long

    /// <summary>
    /// Applies the greater than or equal comparison operation element-wise on the source tensor and stores the result in the destination tensor.
    /// </summary>
    /// <param name="x">The source tensor.</param>
    /// <param name="y">The value to compare against.</param>
    /// <param name="destination">The destination tensor to store the result.</param>
    public static void GreaterThanOrEqual(ReadOnlySpan<long> x, long y, Span<long> destination)
        => Tensor.Apply<long, GreaterThanOrEqualInt64Operator>(x, y, destination);

    /// <summary>
    /// Applies the greater than or equal comparison operation element-wise on the source tensor and a tuple of values, and stores the result in the destination tensor.
    /// </summary>
    /// <param name="x">The source tensor.</param>
    /// <param name="y">The tuple of values to compare against.</param>
    /// <param name="destination">The destination tensor to store the result.</param>
    public static void GreaterThanOrEqual(ReadOnlySpan<long> x, (long, long) y, Span<long> destination)
        => Tensor.Apply<long, GreaterThanOrEqualInt64Operator>(x, y, destination);

    /// <summary>
    /// Applies the greater than or equal comparison operation element-wise on the source tensor and a tuple of values, and stores the result in the destination tensor.
    /// </summary>
    /// <param name="x">The source tensor.</param>
    /// <param name="y">The tuple of values to compare against.</param>
    /// <param name="destination">The destination tensor to store the result.</param>
    public static void GreaterThanOrEqual(ReadOnlySpan<long> x, (long, long, long) y, Span<long> destination)
        => Tensor.Apply<long, GreaterThanOrEqualInt64Operator>(x, y, destination);

    /// <summary>
    /// Applies the greater than or equal comparison operation element-wise on the source tensors and stores the result in the destination tensor.
    /// </summary>
    /// <param name="x">The first source tensor.</param>
    /// <param name="y">The second source tensor.</param>
    /// <param name="destination">The destination tensor to store the result.</param>
    public static void GreaterThanOrEqual(ReadOnlySpan<long> x, ReadOnlySpan<long> y, Span<long> destination)
        => Tensor.Apply<long, GreaterThanOrEqualInt64Operator>(x, y, destination);

    // float

    /// <summary>
    /// Applies the greater than or equal comparison operation element-wise on the source tensor and stores the result in the destination tensor.
    /// </summary>
    /// <param name="x">The source tensor.</param>
    /// <param name="y">The value to compare against.</param>
    /// <param name="destination">The destination tensor to store the result.</param>
    public static void GreaterThanOrEqual(ReadOnlySpan<float> x, float y, Span<int> destination)
        => Tensor.Apply<float, float, int, GreaterThanOrEqualSingleOperator>(x, y, destination);

    /// <summary>
    /// Applies the greater than or equal comparison operation element-wise on the source tensor and a tuple of values, and stores the result in the destination tensor.
    /// </summary>
    /// <param name="x">The source tensor.</param>
    /// <param name="y">The tuple of values to compare against.</param>
    /// <param name="destination">The destination tensor to store the result.</param>
    public static void GreaterThanOrEqual(ReadOnlySpan<float> x, (float, float) y, Span<int> destination)
        => Tensor.Apply<float, float, int, GreaterThanOrEqualSingleOperator>(x, y, destination);

    /// <summary>
    /// Applies the greater than or equal comparison operation element-wise on the source tensor and a tuple of values, and stores the result in the destination tensor.
    /// </summary>
    /// <param name="x">The source tensor.</param>
    /// <param name="y">The tuple of values to compare against.</param>
    /// <param name="destination">The destination tensor to store the result.</param>
    public static void GreaterThanOrEqual(ReadOnlySpan<float> x, (float, float, float) y, Span<int> destination)
        => Tensor.Apply<float, float, int, GreaterThanOrEqualSingleOperator>(x, y, destination);

    /// <summary>
    /// Applies the greater than or equal comparison operation element-wise on the source tensors and stores the result in the destination tensor.
    /// </summary>
    /// <param name="x">The first source tensor.</param>
    /// <param name="y">The second source tensor.</param>
    /// <param name="destination">The destination tensor to store the result.</param>
    public static void GreaterThanOrEqual(ReadOnlySpan<float> x, ReadOnlySpan<float> y, Span<int> destination)
        => Tensor.Apply<float, float, int, GreaterThanOrEqualSingleOperator>(x, y, destination);

    // double

    /// <summary>
    /// Applies the greater than or equal comparison operation element-wise on the source tensor and stores the result in the destination tensor.
    /// </summary>
    /// <param name="x">The source tensor.</param>
    /// <param name="y">The value to compare against.</param>
    /// <param name="destination">The destination tensor to store the result.</param>
    public static void GreaterThanOrEqual(ReadOnlySpan<double> x, double y, Span<long> destination)
        => Tensor.Apply<double, double, long, GreaterThanOrEqualDoubleOperator>(x, y, destination);

    /// <summary>
    /// Applies the greater than or equal comparison operation element-wise on the source tensor and a tuple of values, and stores the result in the destination tensor.
    /// </summary>
    /// <param name="x">The source tensor.</param>
    /// <param name="y">The tuple of values to compare against.</param>
    /// <param name="destination">The destination tensor to store the result.</param>
    public static void GreaterThanOrEqual(ReadOnlySpan<double> x, (double, double) y, Span<long> destination)
        => Tensor.Apply<double, double, long, GreaterThanOrEqualDoubleOperator>(x, y, destination);

    /// <summary>
    /// Applies the greater than or equal comparison operation element-wise on the source tensor and a tuple of values, and stores the result in the destination tensor.
    /// </summary>
    /// <param name="x">The source tensor.</param>
    /// <param name="y">The tuple of values to compare against.</param>
    /// <param name="destination">The destination tensor to store the result.</param>
    public static void GreaterThanOrEqual(ReadOnlySpan<double> x, (double, double, double) y, Span<long> destination)
        => Tensor.Apply<double, double, long, GreaterThanOrEqualDoubleOperator>(x, y, destination);

    /// <summary>
    /// Applies the greater than or equal comparison operation element-wise on the source tensors and stores the result in the destination tensor.
    /// </summary>
    /// <param name="x">The first source tensor.</param>
    /// <param name="y">The second source tensor.</param>
    /// <param name="destination">The destination tensor to store the result.</param>
    public static void GreaterThanOrEqual(ReadOnlySpan<double> x, ReadOnlySpan<double> y, Span<long> destination)
        => Tensor.Apply<double, double, long, GreaterThanOrEqualDoubleOperator>(x, y, destination);
}