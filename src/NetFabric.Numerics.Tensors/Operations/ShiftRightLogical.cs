namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    /// <summary>
    /// Performs a bitwise right logical shift of the elements in the source span by the specified count and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source and destination spans.</typeparam>
    /// <param name="value">The source span.</param>
    /// <param name="count">The number of bits to shift the elements by.</param>
    /// <param name="destination">The destination span.</param>
    /// <remarks>
    /// This method performs a bitwise right logical shift of each element in the source span by the specified count and stores the result in the corresponding element of the destination span.
    /// The shift operation is performed on each element independently.
    /// </remarks>
    public static void ShiftRightLogical<T>(ReadOnlySpan<T> value, int count, Span<T> destination)
        where T : struct, IShiftOperators<T, int, T>
        => ShiftRightLogical<T, T>(value, count, destination);

    /// <summary>
    /// Performs a bitwise right logical shift of the elements in the source span by the specified count and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source span.</typeparam>
    /// <typeparam name="TResult">The type of the elements in the destination span.</typeparam>
    /// <param name="value">The source span.</param>
    /// <param name="count">The number of bits to shift the elements by.</param>
    /// <param name="destination">The destination span.</param>
    /// <remarks>
    /// This method performs a bitwise right logical shift of each element in the source span by the specified count and stores the result in the corresponding element of the destination span.
    /// The shift operation is performed on each element independently.
    /// </remarks>
    public static void ShiftRightLogical<T, TResult>(ReadOnlySpan<T> value, int count, Span<TResult> destination)
        where T : struct, IShiftOperators<T, int, TResult>
        where TResult : struct
        => Tensor.ApplyScalar<T, int, TResult, ShiftRightLogicalOperator<T, TResult>>(value, count, destination);

    /// <summary>
    /// Performs a bitwise right logical shift of the elements in the source span by the specified count and stores the result in the destination span.
    /// </summary>
    /// <param name="value">The source span.</param>
    /// <param name="count">The number of bits to shift the elements by.</param>
    /// <param name="destination">The destination span.</param>
    /// <remarks>
    /// This method performs a bitwise right logical shift of each element in the source span by the specified count and stores the result in the corresponding element of the destination span.
    /// The shift operation is performed on each element independently.
    /// </remarks>
    public static void ShiftRightLogical(ReadOnlySpan<byte> value, int count, Span<byte> destination)
        => Tensor.ApplyScalar<byte, int, byte, ShiftRightLogicalByteOperator>(value, count, destination);

    /// <summary>
    /// Performs a bitwise right logical shift of the elements in the source span by the specified count and stores the result in the destination span.
    /// </summary>
    /// <param name="value">The source span.</param>
    /// <param name="count">The number of bits to shift the elements by.</param>
    /// <param name="destination">The destination span.</param>
    /// <remarks>
    /// This method performs a bitwise right logical shift of each element in the source span by the specified count and stores the result in the corresponding element of the destination span.
    /// The shift operation is performed on each element independently.
    /// </remarks>
    public static void ShiftRightLogical(ReadOnlySpan<ushort> value, int count, Span<ushort> destination)
        => Tensor.ApplyScalar<ushort, int, ushort, ShiftRightLogicalUInt16Operator>(value, count, destination);

    /// <summary>
    /// Performs a bitwise right logical shift of the elements in the source span by the specified count and stores the result in the destination span.
    /// </summary>
    /// <param name="value">The source span.</param>
    /// <param name="count">The number of bits to shift the elements by.</param>
    /// <param name="destination">The destination span.</param>
    /// <remarks>
    /// This method performs a bitwise right logical shift of each element in the source span by the specified count and stores the result in the corresponding element of the destination span.
    /// The shift operation is performed on each element independently.
    /// </remarks>
    public static void ShiftRightLogical(ReadOnlySpan<uint> value, int count, Span<uint> destination)
        => Tensor.ApplyScalar<uint, int, uint, ShiftRightLogicalUInt32Operator>(value, count, destination);

    /// <summary>
    /// Performs a bitwise right logical shift of the elements in the source span by the specified count and stores the result in the destination span.
    /// </summary>
    /// <param name="value">The source span.</param>
    /// <param name="count">The number of bits to shift the elements by.</param>
    /// <param name="destination">The destination span.</param>
    /// <remarks>
    /// This method performs a bitwise right logical shift of each element in the source span by the specified count and stores the result in the corresponding element of the destination span.
    /// The shift operation is performed on each element independently.
    /// </remarks>
    public static void ShiftRightLogical(ReadOnlySpan<ulong> value, int count, Span<ulong> destination)
        => Tensor.ApplyScalar<ulong, int, ulong, ShiftRightLogicalUInt64Operator>(value, count, destination);

    /// <summary>
    /// Performs a bitwise right logical shift of the elements in the source span by the specified count and stores the result in the destination span.
    /// </summary>
    /// <param name="value">The source span.</param>
    /// <param name="count">The number of bits to shift the elements by.</param>
    /// <param name="destination">The destination span.</param>
    /// <remarks>
    /// This method performs a bitwise right logical shift of each element in the source span by the specified count and stores the result in the corresponding element of the destination span.
    /// The shift operation is performed on each element independently.
    /// </remarks>
    public static void ShiftRightLogical(ReadOnlySpan<UIntPtr> value, int count, Span<UIntPtr> destination)
        => Tensor.ApplyScalar<UIntPtr, int, UIntPtr, ShiftRightLogicalUIntPtrOperator>(value, count, destination);

    /// <summary>
    /// Performs a bitwise right logical shift of the elements in the source span by the specified count and stores the result in the destination span.
    /// </summary>
    /// <param name="value">The source span.</param>
    /// <param name="count">The number of bits to shift the elements by.</param>
    /// <param name="destination">The destination span.</param>
    /// <remarks>
    /// This method performs a bitwise right logical shift of each element in the source span by the specified count and stores the result in the corresponding element of the destination span.
    /// The shift operation is performed on each element independently.
    /// </remarks>
    public static void ShiftRightLogical(ReadOnlySpan<sbyte> value, int count, Span<sbyte> destination)
        => Tensor.ApplyScalar<sbyte, int, sbyte, ShiftRightLogicalSByteOperator>(value, count, destination);

    /// <summary>
    /// Performs a bitwise right logical shift of the elements in the source span by the specified count and stores the result in the destination span.
    /// </summary>
    /// <param name="value">The source span.</param>
    /// <param name="count">The number of bits to shift the elements by.</param>
    /// <param name="destination">The destination span.</param>
    /// <remarks>
    /// This method performs a bitwise right logical shift of each element in the source span by the specified count and stores the result in the corresponding element of the destination span.
    /// The shift operation is performed on each element independently.
    /// </remarks>
    public static void ShiftRightLogical(ReadOnlySpan<short> value, int count, Span<short> destination)
        => Tensor.ApplyScalar<short, int, short, ShiftRightLogicalInt16Operator>(value, count, destination);

    /// <summary>
    /// Performs a bitwise right logical shift of the elements in the source span by the specified count and stores the result in the destination span.
    /// </summary>
    /// <param name="value">The source span.</param>
    /// <param name="count">The number of bits to shift the elements by.</param>
    /// <param name="destination">The destination span.</param>
    /// <remarks>
    /// This method performs a bitwise right logical shift of each element in the source span by the specified count and stores the result in the corresponding element of the destination span.
    /// The shift operation is performed on each element independently.
    /// </remarks>
    public static void ShiftRightLogical(ReadOnlySpan<int> value, int count, Span<int> destination)
        => Tensor.ApplyScalar<int, int, int, ShiftRightLogicalInt32Operator>(value, count, destination);

    /// <summary>
    /// Performs a bitwise right logical shift of the elements in the source span by the specified count and stores the result in the destination span.
    /// </summary>
    /// <param name="value">The source span.</param>
    /// <param name="count">The number of bits to shift the elements by.</param>
    /// <param name="destination">The destination span.</param>
    /// <remarks>
    /// This method performs a bitwise right logical shift of each element in the source span by the specified count and stores the result in the corresponding element of the destination span.
    /// The shift operation is performed on each element independently.
    /// </remarks>
    public static void ShiftRightLogical(ReadOnlySpan<long> value, int count, Span<long> destination)
        => Tensor.ApplyScalar<long, int, long, ShiftRightLogicalInt64Operator>(value, count, destination);

    /// <summary>
    /// Performs a bitwise right logical shift of the elements in the source span by the specified count and stores the result in the destination span.
    /// </summary>
    /// <param name="value">The source span.</param>
    /// <param name="count">The number of bits to shift the elements by.</param>
    /// <param name="destination">The destination span.</param>
    /// <remarks>
    /// This method performs a bitwise right logical shift of each element in the source span by the specified count and stores the result in the corresponding element of the destination span.
    /// The shift operation is performed on each element independently.
    /// </remarks>
    public static void ShiftRightLogical(ReadOnlySpan<IntPtr> value, int count, Span<IntPtr> destination)
        => Tensor.ApplyScalar<IntPtr, int, IntPtr, ShiftRightLogicalIntPtrOperator>(value, count, destination);
}