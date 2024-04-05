namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    /// <summary>
    /// Divides each element in the <paramref name="left"/> span by the <paramref name="right"/> value and stores the result in the <paramref name="destination"/> span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The span containing the dividend values.</param>
    /// <param name="right">The divisor value.</param>
    /// <param name="destination">The span to store the division results.</param>
    /// <exception cref="System.DivideByZeroException">Thrown when <paramref name="right"/> is zero.</exception>
    public static void Divide<T>(ReadOnlySpan<T> left, T right, Span<T> destination)
        where T : struct, IDivisionOperators<T, T, T>
        => Tensor.Apply<T, DivideOperator<T>>(left, right, destination);

    /// <summary>
    /// Divides each element in the <paramref name="left"/> span by the corresponding element in the <paramref name="right"/> tuple and stores the result in the <paramref name="destination"/> span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans and tuple.</typeparam>
    /// <param name="left">The span containing the dividend values.</param>
    /// <param name="right">The tuple containing the divisor values.</param>
    /// <param name="destination">The span to store the division results.</param>
    /// <exception cref="System.DivideByZeroException">Thrown when any element in <paramref name="right"/> is zero.</exception>
    public static void Divide<T>(ReadOnlySpan<T> left, (T, T) right, Span<T> destination)
        where T : struct, IDivisionOperators<T, T, T>
        => Tensor.Apply<T, DivideOperator<T>>(left, right, destination);

    /// <summary>
    /// Divides each element in the <paramref name="left"/> span by the corresponding element in the <paramref name="right"/> tuple and stores the result in the <paramref name="destination"/> span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans and tuple.</typeparam>
    /// <param name="left">The span containing the dividend values.</param>
    /// <param name="right">The tuple containing the divisor values.</param>
    /// <param name="destination">The span to store the division results.</param>
    /// <exception cref="System.DivideByZeroException">Thrown when any element in <paramref name="right"/> is zero.</exception>
    public static void Divide<T>(ReadOnlySpan<T> left, (T, T, T) right, Span<T> destination)
        where T : struct, IDivisionOperators<T, T, T>
        => Tensor.Apply<T, DivideOperator<T>>(left, right, destination);

    /// <summary>
    /// Performs element-wise division of two tensors with matching dimensions.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The span containing the dividend values.</param>
    /// <param name="right">The span containing the divisor values.</param>
    /// <param name="destination">The span to store the division results.</param>
    /// <exception cref="System.DivideByZeroException">Thrown when any element in <paramref name="right"/> is zero.</exception>
    public static void Divide<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, IDivisionOperators<T, T, T>
        => Tensor.Apply<T, DivideOperator<T>>(left, right, destination);

    /// <summary>
    /// Divides each element in the <paramref name="left"/> span by the <paramref name="right"/> value and stores the result in the <paramref name="destination"/> span, performing checked division.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The span containing the dividend values.</param>
    /// <param name="right">The divisor value.</param>
    /// <param name="destination">The span to store the division results.</param>
    /// <exception cref="System.DivideByZeroException">Thrown when <paramref name="right"/> is zero.</exception>
    /// <exception cref="System.OverflowException">Thrown when the division result overflows the range of the type <typeparamref name="T"/>.</exception>
    public static void CheckedDivide<T>(ReadOnlySpan<T> left, T right, Span<T> destination)
        where T : struct, IDivisionOperators<T, T, T>
        => Tensor.Apply<T, CheckedDivideOperator<T>>(left, right, destination);

    /// <summary>
    /// Divides each element in the <paramref name="left"/> span by the corresponding element in the <paramref name="right"/> tuple and stores the result in the <paramref name="destination"/> span, performing checked division.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans and tuple.</typeparam>
    /// <param name="left">The span containing the dividend values.</param>
    /// <param name="right">The tuple containing the divisor values.</param>
    /// <param name="destination">The span to store the division results.</param>
    /// <exception cref="System.DivideByZeroException">Thrown when any element in <paramref name="right"/> is zero.</exception>
    /// <exception cref="System.OverflowException">Thrown when the division result overflows the range of the type <typeparamref name="T"/>.</exception>
    public static void CheckedDivide<T>(ReadOnlySpan<T> left, (T, T) right, Span<T> destination)
        where T : struct, IDivisionOperators<T, T, T>
        => Tensor.Apply<T, CheckedDivideOperator<T>>(left, right, destination);

    /// <summary>
    /// Divides each element in the <paramref name="left"/> span by the corresponding element in the <paramref name="right"/> tuple and stores the result in the <paramref name="destination"/> span, performing checked division.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans and tuple.</typeparam>
    /// <param name="left">The span containing the dividend values.</param>
    /// <param name="right">The tuple containing the divisor values.</param>
    /// <param name="destination">The span to store the division results.</param>
    /// <exception cref="System.DivideByZeroException">Thrown when any element in <paramref name="right"/> is zero.</exception>
    /// <exception cref="System.OverflowException">Thrown when the division result overflows the range of the type <typeparamref name="T"/>.</exception>
    public static void CheckedDivide<T>(ReadOnlySpan<T> left, (T, T, T) right, Span<T> destination)
        where T : struct, IDivisionOperators<T, T, T>
        => Tensor.Apply<T, CheckedDivideOperator<T>>(left, right, destination);

    /// <summary>
    /// Performs element-wise division of two tensors with matching dimensions.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The span containing the dividend values.</param>
    /// <param name="right">The span containing the divisor values.</param>
    /// <param name="destination">The span to store the division results.</param>
    /// <exception cref="System.DivideByZeroException">Thrown when any element in <paramref name="right"/> is zero.</exception>
    /// <exception cref="System.OverflowException">Thrown when the division result overflows the range of the type <typeparamref name="T"/>.</exception>
    public static void CheckedDivide<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, IDivisionOperators<T, T, T>
        => Tensor.Apply<T, CheckedDivideOperator<T>>(left, right, destination);
}