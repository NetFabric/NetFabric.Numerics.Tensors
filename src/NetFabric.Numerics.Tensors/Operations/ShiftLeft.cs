namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    /// <summary>
    /// Performs a bitwise left shift of the elements in the source span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source and destination spans.</typeparam>
    /// <param name="value">The source span.</param>
    /// <param name="count">The number of bits to shift each element.</param>
    /// <param name="destination">The destination span.</param>
    /// <remarks>
    /// This method performs a bitwise left shift of each element in the source span by the specified number of bits
    /// and stores the result in the corresponding position in the destination span.
    /// </remarks>
    public static void ShiftLeft<T>(ReadOnlySpan<T> value, int count, Span<T> destination)
        where T : struct, IShiftOperators<T, int, T>
        => ShiftLeft<T, T>(value, count, destination);

    /// <summary>
    /// Performs a bitwise left shift of the elements in the source span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source span.</typeparam>
    /// <typeparam name="TResult">The type of the elements in the destination span.</typeparam>
    /// <param name="value">The source span.</param>
    /// <param name="count">The number of bits to shift each element.</param>
    /// <param name="destination">The destination span.</param>
    /// <remarks>
    /// This method performs a bitwise left shift of each element in the source span by the specified number of bits
    /// and stores the result in the corresponding position in the destination span.
    /// </remarks>
    public static void ShiftLeft<T, TResult>(ReadOnlySpan<T> value, int count, Span<TResult> destination)
        where T : struct, IShiftOperators<T, int, TResult>
        where TResult : struct
        => Tensor.ApplyScalar<T, int, TResult, ShiftLeftOperator<T, TResult>>(value, count, destination);

    /// <summary>
    /// Performs a bitwise left shift of the elements in the source span and stores the result in the destination span.
    /// </summary>
    /// <param name="value">The source span.</param>
    /// <param name="count">The number of bits to shift each element.</param>
    /// <param name="destination">The destination span.</param>
    /// <remarks>
    /// This method performs a bitwise left shift of each element in the source span by the specified number of bits
    /// and stores the result in the corresponding position in the destination span.
    /// </remarks>
    public static void ShiftLeft(ReadOnlySpan<sbyte> value, int count, Span<sbyte> destination)
        => Tensor.ApplyScalar<sbyte, int, sbyte, ShiftLeftSByteOperator>(value, count, destination);

    /// <summary>
    /// Performs a bitwise left shift of the elements in the source span and stores the result in the destination span.
    /// </summary>
    /// <param name="value">The source span.</param>
    /// <param name="count">The number of bits to shift each element.</param>
    /// <param name="destination">The destination span.</param>
    /// <remarks>
    /// This method performs a bitwise left shift of each element in the source span by the specified number of bits
    /// and stores the result in the corresponding position in the destination span.
    /// </remarks>
    public static void ShiftLeft(ReadOnlySpan<ushort> value, int count, Span<ushort> destination)
        => Tensor.ApplyScalar<ushort, int, ushort, ShiftLeftUInt16Operator>(value, count, destination);

    /// <summary>
    /// Performs a bitwise left shift of the elements in the source span and stores the result in the destination span.
    /// </summary>
    /// <param name="value">The source span.</param>
    /// <param name="count">The number of bits to shift each element.</param>
    /// <param name="destination">The destination span.</param>
    /// <remarks>
    /// This method performs a bitwise left shift of each element in the source span by the specified number of bits
    /// and stores the result in the corresponding position in the destination span.
    /// </remarks>
    public static void ShiftLeft(ReadOnlySpan<uint> value, int count, Span<uint> destination)
        => Tensor.ApplyScalar<uint, int, uint, ShiftLeftUInt32Operator>(value, count, destination);

    /// <summary>
    /// Performs a bitwise left shift of the elements in the source span and stores the result in the destination span.
    /// </summary>
    /// <param name="value">The source span.</param>
    /// <param name="count">The number of bits to shift each element.</param>
    /// <param name="destination">The destination span.</param>
    /// <remarks>
    /// This method performs a bitwise left shift of each element in the source span by the specified number of bits
    /// and stores the result in the corresponding position in the destination span.
    /// </remarks>
    public static void ShiftLeft(ReadOnlySpan<ulong> value, int count, Span<ulong> destination)
        => Tensor.ApplyScalar<ulong, int, ulong, ShiftLeftUInt64Operator>(value, count, destination);

    /// <summary>
    /// Performs a bitwise left shift of the elements in the source span and stores the result in the destination span.
    /// </summary>
    /// <param name="value">The source span.</param>
    /// <param name="count">The number of bits to shift each element.</param>
    /// <param name="destination">The destination span.</param>
    /// <remarks>
    /// This method performs a bitwise left shift of each element in the source span by the specified number of bits
    /// and stores the result in the corresponding position in the destination span.
    /// </remarks>
    public static void ShiftLeft(ReadOnlySpan<UIntPtr> value, int count, Span<UIntPtr> destination)
        => Tensor.ApplyScalar<UIntPtr, int, UIntPtr, ShiftLeftUIntPtrOperator>(value, count, destination);

    /// <summary>
    /// Performs a bitwise left shift of the elements in the source span and stores the result in the destination span.
    /// </summary>
    /// <param name="value">The source span.</param>
    /// <param name="count">The number of bits to shift each element.</param>
    /// <param name="destination">The destination span.</param>
    /// <remarks>
    /// This method performs a bitwise left shift of each element in the source span by the specified number of bits
    /// and stores the result in the corresponding position in the destination span.
    /// </remarks>
    public static void ShiftLeft(ReadOnlySpan<byte> value, int count, Span<byte> destination)
        => Tensor.ApplyScalar<byte, int, byte, ShiftLeftByteOperator>(value, count, destination);

    /// <summary>
    /// Performs a bitwise left shift of the elements in the source span and stores the result in the destination span.
    /// </summary>
    /// <param name="value">The source span.</param>
    /// <param name="count">The number of bits to shift each element.</param>
    /// <param name="destination">The destination span.</param>
    /// <remarks>
    /// This method performs a bitwise left shift of each element in the source span by the specified number of bits
    /// and stores the result in the corresponding position in the destination span.
    /// </remarks>
    public static void ShiftLeft(ReadOnlySpan<short> value, int count, Span<short> destination)
        => Tensor.ApplyScalar<short, int, short, ShiftLeftInt16Operator>(value, count, destination);

    /// <summary>
    /// Performs a bitwise left shift of the elements in the source span and stores the result in the destination span.
    /// </summary>
    /// <param name="value">The source span.</param>
    /// <param name="count">The number of bits to shift each element.</param>
    /// <param name="destination">The destination span.</param>
    /// <remarks>
    /// This method performs a bitwise left shift of each element in the source span by the specified number of bits
    /// and stores the result in the corresponding position in the destination span.
    /// </remarks>
    public static void ShiftLeft(ReadOnlySpan<int> value, int count, Span<int> destination)
        => Tensor.ApplyScalar<int, int, int, ShiftLeftInt32Operator>(value, count, destination);

    /// <summary>
    /// Performs a bitwise left shift of the elements in the source span and stores the result in the destination span.
    /// </summary>
    /// <param name="value">The source span.</param>
    /// <param name="count">The number of bits to shift each element.</param>
    /// <param name="destination">The destination span.</param>
    /// <remarks>
    /// This method performs a bitwise left shift of each element in the source span by the specified number of bits
    /// and stores the result in the corresponding position in the destination span.
    /// </remarks>
    public static void ShiftLeft(ReadOnlySpan<long> value, int count, Span<long> destination)
        => Tensor.ApplyScalar<long, int, long, ShiftLeftInt64Operator>(value, count, destination);

    /// <summary>
    /// Performs a bitwise left shift of the elements in the source span and stores the result in the destination span.
    /// </summary>
    /// <param name="value">The source span.</param>
    /// <param name="count">The number of bits to shift each element.</param>
    /// <param name="destination">The destination span.</param>
    /// <remarks>
    /// This method performs a bitwise left shift of each element in the source span by the specified number of bits
    /// and stores the result in the corresponding position in the destination span.
    /// </remarks>
    public static void ShiftLeft(ReadOnlySpan<IntPtr> value, int count, Span<IntPtr> destination)
        => Tensor.ApplyScalar<IntPtr, int, IntPtr, ShiftLeftIntPtrOperator>(value, count, destination);
}