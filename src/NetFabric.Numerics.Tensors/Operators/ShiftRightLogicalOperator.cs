namespace NetFabric.Numerics;

readonly struct ShiftRightLogicalOperator<T, TResult>
    : IGenericBinaryOperator<T, int, TResult>
    where T : struct, IShiftOperators<T, int, TResult>
    where TResult : struct
{
    public static bool IsVectorizable
        => false; 

    public static TResult Invoke(T value, int count)
        => value >>> count;

    public static Vector<TResult> Invoke(ref readonly Vector<T> value, int count)
        => Throw.NotSupportedException<Vector<TResult>>();
}

readonly struct ShiftRightLogicalByteOperator
    : IGenericBinaryOperator<byte, int, byte>
{
    public static byte Invoke(byte value, int count)
        => (byte)(value >>> count);

    public static Vector<byte> Invoke(ref readonly Vector<byte> value, int count)
        => Vector.ShiftRightLogical(value, count);
}

readonly struct ShiftRightLogicalUInt16Operator
    : IGenericBinaryOperator<ushort, int, ushort>
{
    public static ushort Invoke(ushort value, int count)
        => (ushort)(value >>> count);

    public static Vector<ushort> Invoke(ref readonly Vector<ushort> value, int count)
        => Vector.ShiftRightLogical(value, count);
}

readonly struct ShiftRightLogicalUInt32Operator
    : IGenericBinaryOperator<uint, int, uint>
{
    public static uint Invoke(uint value, int count)
        => value >>> count;

    public static Vector<uint> Invoke(ref readonly Vector<uint> value, int count)
        => Vector.ShiftRightLogical(value, count);
}

readonly struct ShiftRightLogicalUInt64Operator
    : IGenericBinaryOperator<ulong, int, ulong>
{
    public static ulong Invoke(ulong value, int count)
        => value >>> count;

    public static Vector<ulong> Invoke(ref readonly Vector<ulong> value, int count)
        => Vector.ShiftRightLogical(value, count);
}

readonly struct ShiftRightLogicalUIntPtrOperator
    : IGenericBinaryOperator<UIntPtr, int, UIntPtr>
{
    public static UIntPtr Invoke(UIntPtr value, int count)
        => value >>> count;

    public static Vector<UIntPtr> Invoke(ref readonly Vector<UIntPtr> value, int count)
        => Vector.ShiftRightLogical(value, count);
}

readonly struct ShiftRightLogicalSByteOperator
    : IGenericBinaryOperator<sbyte, int, sbyte>
{
    public static sbyte Invoke(sbyte value, int count)
        => (sbyte)(value >>> count);

    public static Vector<sbyte> Invoke(ref readonly Vector<sbyte> value, int count)
        => Vector.ShiftRightLogical(value, count);
}

readonly struct ShiftRightLogicalInt16Operator
    : IGenericBinaryOperator<short, int, short>
{
    public static short Invoke(short value, int count)
        => (short)(value >>> count);

    public static Vector<short> Invoke(ref readonly Vector<short> value, int count)
        => Vector.ShiftRightLogical(value, count);
}

readonly struct ShiftRightLogicalInt32Operator
    : IGenericBinaryOperator<int, int, int>
{
    public static int Invoke(int value, int count)
        => value >>> count;

    public static Vector<int> Invoke(ref readonly Vector<int> value, int count)
        => Vector.ShiftRightLogical(value, count);
}

readonly struct ShiftRightLogicalInt64Operator
    : IGenericBinaryOperator<long, int, long>
{
    public static long Invoke(long value, int count)
        => value >>> count;

    public static Vector<long> Invoke(ref readonly Vector<long> value, int count)
        => Vector.ShiftRightLogical(value, count);
}

readonly struct ShiftRightLogicalIntPtrOperator
    : IGenericBinaryOperator<IntPtr, int, IntPtr>
{
    public static IntPtr Invoke(IntPtr value, int count)
        => value >>> count;

    public static Vector<IntPtr> Invoke(ref readonly Vector<IntPtr> value, int count)
        => Vector.ShiftRightLogical(value, count);
}
