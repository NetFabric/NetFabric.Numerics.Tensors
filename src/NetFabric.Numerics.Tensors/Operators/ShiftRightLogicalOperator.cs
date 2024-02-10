namespace NetFabric.Numerics;

readonly struct ShiftRightLogicalOperator<T, TResult>
    : IBinaryScalarOperator<T, int, TResult>
    where T : struct, IShiftOperators<T, int, TResult>
    where TResult : struct
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TResult Invoke(T value, int count)
        => value >>> count;

    public static Vector<TResult> Invoke(ref readonly Vector<T> value, int count)
        => Throw.NotSupportedException<Vector<TResult>>();
}

readonly struct ShiftRightLogicalByteOperator
    : IBinaryScalarOperator<byte, int, byte>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte Invoke(byte value, int count)
        => (byte)(value >>> count);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<byte> Invoke(ref readonly Vector<byte> value, int count)
        => Vector.ShiftRightLogical(value, count);
}

readonly struct ShiftRightLogicalUInt16Operator
    : IBinaryScalarOperator<ushort, int, ushort>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort Invoke(ushort value, int count)
        => (ushort)(value >>> count);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<ushort> Invoke(ref readonly Vector<ushort> value, int count)
        => Vector.ShiftRightLogical(value, count);
}

readonly struct ShiftRightLogicalUInt32Operator
    : IBinaryScalarOperator<uint, int, uint>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint Invoke(uint value, int count)
        => value >>> count;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<uint> Invoke(ref readonly Vector<uint> value, int count)
        => Vector.ShiftRightLogical(value, count);
}

readonly struct ShiftRightLogicalUInt64Operator
    : IBinaryScalarOperator<ulong, int, ulong>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong Invoke(ulong value, int count)
        => value >>> count;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<ulong> Invoke(ref readonly Vector<ulong> value, int count)
        => Vector.ShiftRightLogical(value, count);
}

readonly struct ShiftRightLogicalUIntPtrOperator
    : IBinaryScalarOperator<UIntPtr, int, UIntPtr>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UIntPtr Invoke(UIntPtr value, int count)
        => value >>> count;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<UIntPtr> Invoke(ref readonly Vector<UIntPtr> value, int count)
        => Vector.ShiftRightLogical(value, count);
}

readonly struct ShiftRightLogicalSByteOperator
    : IBinaryScalarOperator<sbyte, int, sbyte>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static sbyte Invoke(sbyte value, int count)
        => (sbyte)(value >>> count);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<sbyte> Invoke(ref readonly Vector<sbyte> value, int count)
        => Vector.ShiftRightLogical(value, count);
}

readonly struct ShiftRightLogicalInt16Operator
    : IBinaryScalarOperator<short, int, short>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short Invoke(short value, int count)
        => (short)(value >>> count);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<short> Invoke(ref readonly Vector<short> value, int count)
        => Vector.ShiftRightLogical(value, count);
}

readonly struct ShiftRightLogicalInt32Operator
    : IBinaryScalarOperator<int, int, int>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Invoke(int value, int count)
        => value >>> count;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<int> Invoke(ref readonly Vector<int> value, int count)
        => Vector.ShiftRightLogical(value, count);
}

readonly struct ShiftRightLogicalInt64Operator
    : IBinaryScalarOperator<long, int, long>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long Invoke(long value, int count)
        => value >>> count;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<long> Invoke(ref readonly Vector<long> value, int count)
        => Vector.ShiftRightLogical(value, count);
}

readonly struct ShiftRightLogicalIntPtrOperator
    : IBinaryScalarOperator<IntPtr, int, IntPtr>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntPtr Invoke(IntPtr value, int count)
        => value >>> count;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<IntPtr> Invoke(ref readonly Vector<IntPtr> value, int count)
        => Vector.ShiftRightLogical(value, count);
}
