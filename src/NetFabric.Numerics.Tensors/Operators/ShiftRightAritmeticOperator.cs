namespace NetFabric.Numerics;

readonly struct ShiftRightArithmeticOperator<T, TResult>
    : IGenericBinaryOperator<T, int, TResult>
    where T : struct, IShiftOperators<T, int, TResult>
    where TResult : struct
{
    public static bool IsVectorizable
        => false; 

    public static TResult Invoke(T value, int count)
        => value >> count;

    public static Vector<TResult> Invoke(ref readonly Vector<T> value, int count)
        => Throw.NotSupportedException<Vector<TResult>>();
}

readonly struct ShiftRightArithmeticSByteOperator
    : IGenericBinaryOperator<sbyte, int, sbyte>
{
    public static sbyte Invoke(sbyte value, int count)
        => (sbyte)(value >> count);

    public static Vector<sbyte> Invoke(ref readonly Vector<sbyte> value, int count)
        => Vector.ShiftRightArithmetic(value, count);
}

readonly struct ShiftRightArithmeticInt16Operator
    : IGenericBinaryOperator<short, int, short>
{
    public static short Invoke(short value, int count)
        => (short)(value >> count);

    public static Vector<short> Invoke(ref readonly Vector<short> value, int count)
        => Vector.ShiftRightArithmetic(value, count);
}

readonly struct ShiftRightArithmeticInt32Operator
    : IGenericBinaryOperator<int, int, int>
{
    public static int Invoke(int value, int count)
        => value >> count;

    public static Vector<int> Invoke(ref readonly Vector<int> value, int count)
        => Vector.ShiftRightArithmetic(value, count);
}

readonly struct ShiftRightArithmeticInt64Operator
    : IGenericBinaryOperator<long, int, long>
{
    public static long Invoke(long value, int count)
        => value >> count;

    public static Vector<long> Invoke(ref readonly Vector<long> value, int count)
        => Vector.ShiftRightArithmetic(value, count);
}

readonly struct ShiftRightArithmeticIntPtrOperator
    : IGenericBinaryOperator<IntPtr, int, IntPtr>
{
    public static IntPtr Invoke(IntPtr value, int count)
        => value >> count;

    public static Vector<IntPtr> Invoke(ref readonly Vector<IntPtr> value, int count)
        => Vector.ShiftRightArithmetic(value, count);
}
