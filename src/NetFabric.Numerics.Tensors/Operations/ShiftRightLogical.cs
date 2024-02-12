namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static void ShiftRightLogical<T>(ReadOnlySpan<T> value, int count, Span<T> destination)
        where T : struct, IShiftOperators<T, int, T>
        => ShiftRightLogical<T, T>(value, count, destination);

    public static void ShiftRightLogical<T, TResult>(ReadOnlySpan<T> value, int count, Span<TResult> destination)
        where T : struct, IShiftOperators<T, int, TResult>
        where TResult : struct
        => Tensor.ApplyScalar<T, int, TResult, ShiftRightLogicalOperator<T, TResult>>(value, count, destination);

    public static void ShiftRightLogical(ReadOnlySpan<byte> value, int count, Span<byte> destination)
        => Tensor.ApplyScalar<byte, int, byte, ShiftRightLogicalByteOperator>(value, count, destination);

    public static void ShiftRightLogical(ReadOnlySpan<ushort> value, int count, Span<ushort> destination)
        => Tensor.ApplyScalar<ushort, int, ushort, ShiftRightLogicalUInt16Operator>(value, count, destination);

    public static void ShiftRightLogical(ReadOnlySpan<uint> value, int count, Span<uint> destination)
        => Tensor.ApplyScalar<uint, int, uint, ShiftRightLogicalUInt32Operator>(value, count, destination);

    public static void ShiftRightLogical(ReadOnlySpan<ulong> value, int count, Span<ulong> destination)
        => Tensor.ApplyScalar<ulong, int, ulong, ShiftRightLogicalUInt64Operator>(value, count, destination);

    public static void ShiftRightLogical(ReadOnlySpan<UIntPtr> value, int count, Span<UIntPtr> destination)
        => Tensor.ApplyScalar<UIntPtr, int, UIntPtr, ShiftRightLogicalUIntPtrOperator>(value, count, destination);

    public static void ShiftRightLogical(ReadOnlySpan<sbyte> value, int count, Span<sbyte> destination)
        => Tensor.ApplyScalar<sbyte, int, sbyte, ShiftRightLogicalSByteOperator>(value, count, destination);

    public static void ShiftRightLogical(ReadOnlySpan<short> value, int count, Span<short> destination)
        => Tensor.ApplyScalar<short, int, short, ShiftRightLogicalInt16Operator>(value, count, destination);

    public static void ShiftRightLogical(ReadOnlySpan<int> value, int count, Span<int> destination)
        => Tensor.ApplyScalar<int, int, int, ShiftRightLogicalInt32Operator>(value, count, destination);

    public static void ShiftRightLogical(ReadOnlySpan<long> value, int count, Span<long> destination)
        => Tensor.ApplyScalar<long, int, long, ShiftRightLogicalInt64Operator>(value, count, destination);

    public static void ShiftRightLogical(ReadOnlySpan<IntPtr> value, int count, Span<IntPtr> destination)
        => Tensor.ApplyScalar<IntPtr, int, IntPtr, ShiftRightLogicalIntPtrOperator>(value, count, destination);
}