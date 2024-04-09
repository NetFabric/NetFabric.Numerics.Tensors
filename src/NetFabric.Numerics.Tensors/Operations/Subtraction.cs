namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    /// <summary>
    /// Subtracts a scalar value from each element in the source span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The source span.</param>
    /// <param name="right">The scalar value to subtract.</param>
    /// <param name="destination">The destination span to store the result.</param>
    public static void Subtract<T>(ReadOnlySpan<T> left, T right, Span<T> destination)
        where T : struct, ISubtractionOperators<T, T, T>
        => Tensor.Apply<T, SubtractOperator<T>>(left, right, destination);

    /// <summary>
    /// Subtracts a tuple of scalar values from each element in the source span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The source span.</param>
    /// <param name="right">The tuple of scalar values to subtract.</param>
    /// <param name="destination">The destination span to store the result.</param>
    public static void Subtract<T>(ReadOnlySpan<T> left, (T, T) right, Span<T> destination)
        where T : struct, ISubtractionOperators<T, T, T>
        => Tensor.Apply<T, SubtractOperator<T>>(left, right, destination);

    /// <summary>
    /// Subtracts a tuple of scalar values from each element in the source span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The source span.</param>
    /// <param name="right">The tuple of scalar values to subtract.</param>
    /// <param name="destination">The destination span to store the result.</param>
    public static void Subtract<T>(ReadOnlySpan<T> left, (T, T, T) right, Span<T> destination)
        where T : struct, ISubtractionOperators<T, T, T>
        => Tensor.Apply<T, SubtractOperator<T>>(left, right, destination);

    /// <summary>
    /// Subtracts corresponding elements in the left and right spans and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The left source span.</param>
    /// <param name="right">The right source span.</param>
    /// <param name="destination">The destination span to store the result.</param>
    public static void Subtract<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, ISubtractionOperators<T, T, T>
        => Tensor.Apply<T, SubtractOperator<T>>(left, right, destination);

    /// <summary>
    /// Subtracts a scalar value from each element in the source span and stores the result in the destination span, performing checked arithmetic.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The source span.</param>
    /// <param name="right">The scalar value to subtract.</param>
    /// <param name="destination">The destination span to store the result.</param>
    public static void CheckedSubtract<T>(ReadOnlySpan<T> left, T right, Span<T> destination)
        where T : struct, ISubtractionOperators<T, T, T>
        => Tensor.Apply<T, CheckedSubtractOperator<T>>(left, right, destination);

    /// <summary>
    /// Subtracts a tuple of scalar values from each element in the source span and stores the result in the destination span, performing checked arithmetic.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The source span.</param>
    /// <param name="right">The tuple of scalar values to subtract.</param>
    /// <param name="destination">The destination span to store the result.</param>
    public static void CheckedSubtract<T>(ReadOnlySpan<T> left, (T, T) right, Span<T> destination)
        where T : struct, ISubtractionOperators<T, T, T>
        => Tensor.Apply<T, CheckedSubtractOperator<T>>(left, right, destination);

    /// <summary>
    /// Subtracts a tuple of scalar values from each element in the source span and stores the result in the destination span, performing checked arithmetic.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The source span.</param>
    /// <param name="right">The tuple of scalar values to subtract.</param>
    /// <param name="destination">The destination span to store the result.</param>
    public static void CheckedSubtract<T>(ReadOnlySpan<T> left, (T, T, T) right, Span<T> destination)
        where T : struct, ISubtractionOperators<T, T, T>
        => Tensor.Apply<T, CheckedSubtractOperator<T>>(left, right, destination);

    /// <summary>
    /// Subtracts corresponding elements in the left and right spans and stores the result in the destination span, performing checked arithmetic.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The left source span.</param>
    /// <param name="right">The right source span.</param>
    /// <param name="destination">The destination span to store the result.</param>
    public static void CheckedSubtract<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, ISubtractionOperators<T, T, T>
        => Tensor.Apply<T, CheckedSubtractOperator<T>>(left, right, destination);
}