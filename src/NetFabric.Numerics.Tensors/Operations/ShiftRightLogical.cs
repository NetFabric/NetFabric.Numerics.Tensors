namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void ShiftRightLogical<T>(ReadOnlySpan<T> value, int count, Span<T> destination)
        where T : struct, IShiftOperators<T, int, T>
        => ShiftRightLogical<T, T>(value, count, destination);

    public static void ShiftRightLogical<T, TResult>(ReadOnlySpan<T> value, int count, Span<TResult> destination)
        where T : struct, IShiftOperators<T, int, TResult>
        where TResult : struct
        => ApplyGeneric<T, int, TResult, ShiftRightLogicalOperator<T, TResult>>(value, count, destination);

    public static void ShiftRightLogical(ReadOnlySpan<byte> value, int count, Span<byte> destination)
        => ApplyGeneric<byte, int, byte, ShiftRightLogicalByteOperator>(value, count, destination);

    public static void ShiftRightLogical(ReadOnlySpan<ushort> value, int count, Span<ushort> destination)
        => ApplyGeneric<ushort, int, ushort, ShiftRightLogicalUInt16Operator>(value, count, destination);

    public static void ShiftRightLogical(ReadOnlySpan<uint> value, int count, Span<uint> destination)
        => ApplyGeneric<uint, int, uint, ShiftRightLogicalUInt32Operator>(value, count, destination);

    public static void ShiftRightLogical(ReadOnlySpan<ulong> value, int count, Span<ulong> destination)
        => ApplyGeneric<ulong, int, ulong, ShiftRightLogicalUInt64Operator>(value, count, destination);

    public static void ShiftRightLogical(ReadOnlySpan<UIntPtr> value, int count, Span<UIntPtr> destination)
        => ApplyGeneric<UIntPtr, int, UIntPtr, ShiftRightLogicalUIntPtrOperator>(value, count, destination);

    public static void ShiftRightLogical(ReadOnlySpan<sbyte> value, int count, Span<sbyte> destination)
        => ApplyGeneric<sbyte, int, sbyte, ShiftRightLogicalSByteOperator>(value, count, destination);

    public static void ShiftRightLogical(ReadOnlySpan<short> value, int count, Span<short> destination)
        => ApplyGeneric<short, int, short, ShiftRightLogicalInt16Operator>(value, count, destination);

    public static void ShiftRightLogical(ReadOnlySpan<int> value, int count, Span<int> destination)
        => ApplyGeneric<int, int, int, ShiftRightLogicalInt32Operator>(value, count, destination);

    public static void ShiftRightLogical(ReadOnlySpan<long> value, int count, Span<long> destination)
        => ApplyGeneric<long, int, long, ShiftRightLogicalInt64Operator>(value, count, destination);

    public static void ShiftRightLogical(ReadOnlySpan<IntPtr> value, int count, Span<IntPtr> destination)
        => ApplyGeneric<IntPtr, int, IntPtr, ShiftRightLogicalIntPtrOperator>(value, count, destination);
}