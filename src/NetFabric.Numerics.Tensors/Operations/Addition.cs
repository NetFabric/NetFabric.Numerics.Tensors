namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    /// <summary>
    /// Adds a scalar value to each element in the source tensor and stores the result in the destination tensor.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The span containing the elements to add.</param>
    /// <param name="right">The scalar value to add to each element.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the source and destination spans have different lengths.</exception>
    public static void Add<T>(ReadOnlySpan<T> left, T right, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
        => Tensor.Apply<T, AddOperator<T>>(left, right, destination);

    /// <summary>
    /// Adds a tuple of two values to each element in the left span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The span containing the elements to add.</param>
    /// <param name="right">The tuple of two values to add to each element.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the source and destination spans have different lengths.</exception>
    public static void Add<T>(ReadOnlySpan<T> left, (T, T) right, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
        => Tensor.Apply<T, AddOperator<T>>(left, right, destination);

    /// <summary>
    /// Adds a tuple of three values to each element in the left span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The span containing the elements to add.</param>
    /// <param name="right">The tuple of three values to add to each element.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the source and destination spans have different lengths.</exception>
    public static void Add<T>(ReadOnlySpan<T> left, (T, T, T) right, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
        => Tensor.Apply<T, AddOperator<T>>(left, right, destination);

    public static void Add<T>(T[] left, T[] right, T[] destination)
        where T : struct, IAdditionOperators<T, T, T>
        => Tensor.Apply<T, AddOperator<T>>(left, right, destination);

    public static void Add<T>(ReadOnlyMemory<T> left, ReadOnlyMemory<T> right, Memory<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
        => Tensor.Apply<T, AddOperator<T>>(left, right, destination);

    /// <summary>
    /// Performs element-wise addition of two tensors with matching dimensions.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The span containing the left elements to add.</param>
    /// <param name="right">The span containing the right elements to add.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the source and destination spans have different lengths.</exception>
    public static void Add<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
        => Tensor.Apply<T, AddOperator<T>>(left, right, destination);

    /// <summary>
    /// Adds a scalar value to each element in the left span and stores the result in the destination span, performing checked addition.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The span containing the elements to add.</param>
    /// <param name="right">The scalar value to add to each element.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the source and destination spans have different lengths.</exception>
    /// <exception cref="OverflowException">Thrown when the addition operation results in an overflow.</exception>
    public static void CheckedAdd<T>(ReadOnlySpan<T> left, T right, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
        => Tensor.Apply<T, CheckedAddOperator<T>>(left, right, destination);

    /// <summary>
    /// Adds a tuple of two values to each element in the left span and stores the result in the destination span, performing checked addition.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The span containing the elements to add.</param>
    /// <param name="right">The tuple of two values to add to each element.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the source and destination spans have different lengths.</exception>
    /// <exception cref="OverflowException">Thrown when the addition operation results in an overflow.</exception>
    public static void CheckedAdd<T>(ReadOnlySpan<T> left, (T, T) right, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
        => Tensor.Apply<T, CheckedAddOperator<T>>(left, right, destination);

    /// <summary>
    /// Adds a tuple of three values to each element in the left span and stores the result in the destination span, performing checked addition.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The span containing the elements to add.</param>
    /// <param name="right">The tuple of three values to add to each element.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the source and destination spans have different lengths.</exception>
    /// <exception cref="OverflowException">Thrown when the addition operation results in an overflow.</exception>
    public static void CheckedAdd<T>(ReadOnlySpan<T> left, (T, T, T) right, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
        => Tensor.Apply<T, CheckedAddOperator<T>>(left, right, destination);

    /// <summary>
    /// Performs element-wise addition of two tensors with matching dimensions.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The span containing the left elements to add.</param>
    /// <param name="right">The span containing the right elements to add.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the source and destination spans have different lengths.</exception>
    /// <exception cref="OverflowException">Thrown when the addition operation results in an overflow.</exception>
    public static void CheckedAdd<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
        => Tensor.Apply<T, CheckedAddOperator<T>>(left, right, destination);
}