namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void ShiftLeft<T>(ReadOnlySpan<T> value, int count, Span<T> destination)
        where T : struct, IShiftOperators<T, int, T>
        => ShiftLeft<T, T>(value, count, destination);

    public static void ShiftLeft<T, TResult>(ReadOnlySpan<T> value, int count, Span<TResult> destination)
        where T : struct, IShiftOperators<T, int, TResult>
        where TResult : struct
        => ApplyScalar<T, int, TResult, ShiftLeftOperator<T, TResult>>(value, count, destination);

    public static void ShiftLeft(ReadOnlySpan<sbyte> value, int count, Span<sbyte> destination)
        => ApplyScalar<sbyte, int, sbyte, ShiftLeftSByteOperator>(value, count, destination);

    public static void ShiftLeft(ReadOnlySpan<ushort> value, int count, Span<ushort> destination)
        => ApplyScalar<ushort, int, ushort, ShiftLeftUInt16Operator>(value, count, destination);

    public static void ShiftLeft(ReadOnlySpan<uint> value, int count, Span<uint> destination)
        => ApplyScalar<uint, int, uint, ShiftLeftUInt32Operator>(value, count, destination);

    public static void ShiftLeft(ReadOnlySpan<ulong> value, int count, Span<ulong> destination)
        => ApplyScalar<ulong, int, ulong, ShiftLeftUInt64Operator>(value, count, destination);

    public static void ShiftLeft(ReadOnlySpan<UIntPtr> value, int count, Span<UIntPtr> destination)
        => ApplyScalar<UIntPtr, int, UIntPtr, ShiftLeftUIntPtrOperator>(value, count, destination);

    public static void ShiftLeft(ReadOnlySpan<byte> value, int count, Span<byte> destination)
        => ApplyScalar<byte, int, byte, ShiftLeftByteOperator>(value, count, destination);

    public static void ShiftLeft(ReadOnlySpan<short> value, int count, Span<short> destination)
        => ApplyScalar<short, int, short, ShiftLeftInt16Operator>(value, count, destination);

    public static void ShiftLeft(ReadOnlySpan<int> value, int count, Span<int> destination)
        => ApplyScalar<int, int, int, ShiftLeftInt32Operator>(value, count, destination);

    public static void ShiftLeft(ReadOnlySpan<long> value, int count, Span<long> destination)
        => ApplyScalar<long, int, long, ShiftLeftInt64Operator>(value, count, destination);

    public static void ShiftLeft(ReadOnlySpan<IntPtr> value, int count, Span<IntPtr> destination)
        => ApplyScalar<IntPtr, int, IntPtr, ShiftLeftIntPtrOperator>(value, count, destination);
}