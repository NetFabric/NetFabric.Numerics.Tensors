namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    /// <summary>
    /// Multiplies each element of the input span by a scalar value, storing the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The span containing the left operands.</param>
    /// <param name="right">The right operand.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the <paramref name="left"/> and <paramref name="destination"/> spans have different lengths.</exception>
    public static void Multiply<T>(ReadOnlySpan<T> left, T right, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, MultiplyOperator<T>>(left, right, destination);

    /// <summary>
    /// Multiplies each element of the input span by a scalar tuple, storing the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The span containing the left operands.</param>
    /// <param name="right">The right operand as a tuple.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the <paramref name="left"/> and <paramref name="destination"/> spans have different lengths.</exception>
    public static void Multiply<T>(ReadOnlySpan<T> left, (T, T) right, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, MultiplyOperator<T>>(left, right, destination);

    /// <summary>
    /// Multiplies each element of the input span by a scalar tuple, storing the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The span containing the left operands.</param>
    /// <param name="right">The right operand as a tuple.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the <paramref name="left"/> and <paramref name="destination"/> spans have different lengths.</exception>
    public static void Multiply<T>(ReadOnlySpan<T> left, (T, T, T) right, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, MultiplyOperator<T>>(left, right, destination);

    /// <summary>
    /// Multiplies each element of the input span by the corresponding element of a scalar span, storing the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The span containing the left operands.</param>
    /// <param name="right">The span containing the right operands.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the <paramref name="left"/>, <paramref name="right"/>, and <paramref name="destination"/> spans have different lengths.</exception>
    public static void Multiply<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, MultiplyOperator<T>>(left, right, destination);

    /// <summary>
    /// Multiplies each element of the input span by a scalar value, storing the result in the destination span, performing checked arithmetic.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The span containing the left operands.</param>
    /// <param name="right">The right operand.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the <paramref name="left"/> and <paramref name="destination"/> spans have different lengths.</exception>
    /// <exception cref="OverflowException">Thrown when the multiplication overflows.</exception>
    public static void CheckedMultiply<T>(ReadOnlySpan<T> left, T right, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, CheckedMultiplyOperator<T>>(left, right, destination);

    /// <summary>
    /// Multiplies each element of the input span by a scalar tuple, storing the result in the destination span, performing checked arithmetic.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The span containing the left operands.</param>
    /// <param name="right">The right operand as a tuple.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the <paramref name="left"/> and <paramref name="destination"/> spans have different lengths.</exception>
    /// <exception cref="OverflowException">Thrown when the multiplication overflows.</exception>
    public static void CheckedMultiply<T>(ReadOnlySpan<T> left, (T, T) right, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, CheckedMultiplyOperator<T>>(left, right, destination);

    /// <summary>
    /// Multiplies each element of the input span by a scalar tuple, storing the result in the destination span, performing checked arithmetic.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The span containing the left operands.</param>
    /// <param name="right">The right operand as a tuple.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the <paramref name="left"/> and <paramref name="destination"/> spans have different lengths.</exception>
    /// <exception cref="OverflowException">Thrown when the multiplication overflows.</exception>
    public static void CheckedMultiply<T>(ReadOnlySpan<T> left, (T, T, T) right, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, CheckedMultiplyOperator<T>>(left, right, destination);

    /// <summary>
    /// Multiplies each element of the input span by the corresponding element of a scalar span, storing the result in the destination span, performing checked arithmetic.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The span containing the left operands.</param>
    /// <param name="right">The span containing the right operands.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the <paramref name="left"/>, <paramref name="right"/>, and <paramref name="destination"/> spans have different lengths.</exception>
    /// <exception cref="OverflowException">Thrown when the multiplication overflows.</exception>
    public static void CheckedMultiply<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, CheckedMultiplyOperator<T>>(left, right, destination);
}