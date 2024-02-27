namespace NetFabric.Numerics.Tensors.Operators;

public readonly struct ShiftRightArithmeticOperator<T, TResult>
    : IBinaryScalarOperator<T, int, TResult>
    where T : struct, IShiftOperators<T, int, TResult>
    where TResult : struct
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TResult Invoke(T value, int count)
        => value >> count;

    public static Vector<TResult> Invoke(ref readonly Vector<T> value, int count)
        => Throw.NotSupportedException<Vector<TResult>>();
}

public readonly struct ShiftRightArithmeticSByteOperator
    : IBinaryScalarOperator<sbyte, int, sbyte>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static sbyte Invoke(sbyte value, int count)
        => (sbyte)(value >> count);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<sbyte> Invoke(ref readonly Vector<sbyte> value, int count)
        => Vector.ShiftRightArithmetic(value, count);
}

public readonly struct ShiftRightArithmeticInt16Operator
    : IBinaryScalarOperator<short, int, short>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short Invoke(short value, int count)
        => (short)(value >> count);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<short> Invoke(ref readonly Vector<short> value, int count)
        => Vector.ShiftRightArithmetic(value, count);
}

public readonly struct ShiftRightArithmeticInt32Operator
    : IBinaryScalarOperator<int, int, int>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Invoke(int value, int count)
        => value >> count;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<int> Invoke(ref readonly Vector<int> value, int count)
        => Vector.ShiftRightArithmetic(value, count);
}

public readonly struct ShiftRightArithmeticInt64Operator
    : IBinaryScalarOperator<long, int, long>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long Invoke(long value, int count)
        => value >> count;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<long> Invoke(ref readonly Vector<long> value, int count)
        => Vector.ShiftRightArithmetic(value, count);
}

public readonly struct ShiftRightArithmeticIntPtrOperator
    : IBinaryScalarOperator<IntPtr, int, IntPtr>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntPtr Invoke(IntPtr value, int count)
        => value >> count;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<IntPtr> Invoke(ref readonly Vector<IntPtr> value, int count)
        => Vector.ShiftRightArithmetic(value, count);
}
