namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    /// <summary>
    /// Performs a bitwise right arithmetic shift of the elements in the source span by the specified count and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source and destination spans.</typeparam>
    /// <param name="value">The source span.</param>
    /// <param name="count">The number of bits to shift the elements by.</param>
    /// <param name="destination">The destination span.</param>
    /// <remarks>
    /// This method performs a bitwise right arithmetic shift of each element in the source span by the specified count and stores the result in the corresponding element of the destination span.
    /// The shift is performed on the binary representation of the elements.
    /// </remarks>
    public static void ShiftRightArithmetic<T>(ReadOnlySpan<T> value, int count, Span<T> destination)
        where T : struct, IShiftOperators<T, int, T>
        => ShiftRightArithmetic<T, T>(value, count, destination);

    /// <summary>
    /// Performs a bitwise right arithmetic shift of the elements in the source span by the specified count and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source span.</typeparam>
    /// <typeparam name="TResult">The type of the elements in the destination span.</typeparam>
    /// <param name="value">The source span.</param>
    /// <param name="count">The number of bits to shift the elements by.</param>
    /// <param name="destination">The destination span.</param>
    /// <remarks>
    /// This method performs a bitwise right arithmetic shift of each element in the source span by the specified count and stores the result in the corresponding element of the destination span.
    /// The shift is performed on the binary representation of the elements.
    /// </remarks>
    public static void ShiftRightArithmetic<T, TResult>(ReadOnlySpan<T> value, int count, Span<TResult> destination)
        where T : struct, IShiftOperators<T, int, TResult>
        where TResult : struct
        => Tensor.ApplyScalar<T, int, TResult, ShiftRightArithmeticOperator<T, TResult>>(value, count, destination);

    /// <summary>
    /// Performs a bitwise right arithmetic shift of the elements in the source span by the specified count and stores the result in the destination span.
    /// </summary>
    /// <param name="value">The source span.</param>
    /// <param name="count">The number of bits to shift the elements by.</param>
    /// <param name="destination">The destination span.</param>
    /// <remarks>
    /// This method performs a bitwise right arithmetic shift of each element in the source span by the specified count and stores the result in the corresponding element of the destination span.
    /// The shift is performed on the binary representation of the elements.
    /// </remarks>
    public static void ShiftRightArithmetic(ReadOnlySpan<sbyte> value, int count, Span<sbyte> destination)
        => Tensor.ApplyScalar<sbyte, int, sbyte, ShiftRightArithmeticSByteOperator>(value, count, destination);

    /// <summary>
    /// Performs a bitwise right arithmetic shift of the elements in the source span by the specified count and stores the result in the destination span.
    /// </summary>
    /// <param name="value">The source span.</param>
    /// <param name="count">The number of bits to shift the elements by.</param>
    /// <param name="destination">The destination span.</param>
    /// <remarks>
    /// This method performs a bitwise right arithmetic shift of each element in the source span by the specified count and stores the result in the corresponding element of the destination span.
    /// The shift is performed on the binary representation of the elements.
    /// </remarks>
    public static void ShiftRightArithmetic(ReadOnlySpan<short> value, int count, Span<short> destination)
        => Tensor.ApplyScalar<short, int, short, ShiftRightArithmeticInt16Operator>(value, count, destination);

    /// <summary>
    /// Performs a bitwise right arithmetic shift of the elements in the source span by the specified count and stores the result in the destination span.
    /// </summary>
    /// <param name="value">The source span.</param>
    /// <param name="count">The number of bits to shift the elements by.</param>
    /// <param name="destination">The destination span.</param>
    /// <remarks>
    /// This method performs a bitwise right arithmetic shift of each element in the source span by the specified count and stores the result in the corresponding element of the destination span.
    /// The shift is performed on the binary representation of the elements.
    /// </remarks>
    public static void ShiftRightArithmetic(ReadOnlySpan<int> value, int count, Span<int> destination)
        => Tensor.ApplyScalar<int, int, int, ShiftRightArithmeticInt32Operator>(value, count, destination);

    /// <summary>
    /// Performs a bitwise right arithmetic shift of the elements in the source span by the specified count and stores the result in the destination span.
    /// </summary>
    /// <param name="value">The source span.</param>
    /// <param name="count">The number of bits to shift the elements by.</param>
    /// <param name="destination">The destination span.</param>
    /// <remarks>
    /// This method performs a bitwise right arithmetic shift of each element in the source span by the specified count and stores the result in the corresponding element of the destination span.
    /// The shift is performed on the binary representation of the elements.
    /// </remarks>
    public static void ShiftRightArithmetic(ReadOnlySpan<long> value, int count, Span<long> destination)
        => Tensor.ApplyScalar<long, int, long, ShiftRightArithmeticInt64Operator>(value, count, destination);

    /// <summary>
    /// Performs a bitwise right arithmetic shift of the elements in the source span by the specified count and stores the result in the destination span.
    /// </summary>
    /// <param name="value">The source span.</param>
    /// <param name="count">The number of bits to shift the elements by.</param>
    /// <param name="destination">The destination span.</param>
    /// <remarks>
    /// This method performs a bitwise right arithmetic shift of each element in the source span by the specified count and stores the result in the corresponding element of the destination span.
    /// The shift is performed on the binary representation of the elements.
    /// </remarks>
    public static void ShiftRightArithmetic(ReadOnlySpan<IntPtr> value, int count, Span<IntPtr> destination)
        => Tensor.ApplyScalar<IntPtr, int, IntPtr, ShiftRightArithmeticIntPtrOperator>(value, count, destination);
}