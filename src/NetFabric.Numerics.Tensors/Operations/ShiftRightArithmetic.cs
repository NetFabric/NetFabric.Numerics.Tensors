namespace NetFabric.Numerics.Tensors;

public static partial class Tensor
{
    public static void ShiftRightArithmetic<T>(ReadOnlySpan<T> value, int count, Span<T> destination)
        where T : struct, IShiftOperators<T, int, T>
        => ShiftRightArithmetic<T, T>(value, count, destination);

    public static void ShiftRightArithmetic<T, TResult>(ReadOnlySpan<T> value, int count, Span<TResult> destination)
        where T : struct, IShiftOperators<T, int, TResult>
        where TResult : struct
        => ApplyScalar<T, int, TResult, ShiftRightArithmeticOperator<T, TResult>>(value, count, destination);

    public static void ShiftRightArithmetic(ReadOnlySpan<sbyte> value, int count, Span<sbyte> destination)
        => ApplyScalar<sbyte, int, sbyte, ShiftRightArithmeticSByteOperator>(value, count, destination);

    public static void ShiftRightArithmetic(ReadOnlySpan<short> value, int count, Span<short> destination)
        => ApplyScalar<short, int, short, ShiftRightArithmeticInt16Operator>(value, count, destination);

    public static void ShiftRightArithmetic(ReadOnlySpan<int> value, int count, Span<int> destination)
        => ApplyScalar<int, int, int, ShiftRightArithmeticInt32Operator>(value, count, destination);

    public static void ShiftRightArithmetic(ReadOnlySpan<long> value, int count, Span<long> destination)
        => ApplyScalar<long, int, long, ShiftRightArithmeticInt64Operator>(value, count, destination);

    public static void ShiftRightArithmetic(ReadOnlySpan<IntPtr> value, int count, Span<IntPtr> destination)
        => ApplyScalar<IntPtr, int, IntPtr, ShiftRightArithmeticIntPtrOperator>(value, count, destination);
}