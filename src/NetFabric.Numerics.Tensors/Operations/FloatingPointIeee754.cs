namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static void Atan2<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> destination)
        where T : struct, IFloatingPointIeee754<T>
        => Tensor.Apply<T, Atan2Operator<T>>(x, y, destination);

    public static void Atan2<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, IFloatingPointIeee754<T>
        => Tensor.Apply<T, Atan2Operator<T>>(x, y, destination);

    public static void Atan2Pi<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> destination)
        where T : struct, IFloatingPointIeee754<T>
        => Tensor.Apply<T, Atan2PiOperator<T>>(x, y, destination);

    public static void Atan2Pi<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, IFloatingPointIeee754<T>
        => Tensor.Apply<T, Atan2PiOperator<T>>(x, y, destination);

    public static void BitDecrement<T>(ReadOnlySpan<T> x, Span<T> destination)
        where T : struct, IFloatingPointIeee754<T>
        => Tensor.Apply<T, BitDecrementOperator<T>>(x, destination);

    public static void BitIncrement<T>(ReadOnlySpan<T> x, Span<T> destination)
        where T : struct, IFloatingPointIeee754<T>
        => Tensor.Apply<T, BitIncrementOperator<T>>(x, destination);

    public static void Ieee754Remainder<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, IFloatingPointIeee754<T>
        => Tensor.Apply<T, Ieee754RemainderOperator<T>>(x, y, destination);

    public static void Ieee754Remainder<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> destination)
        where T : struct, IFloatingPointIeee754<T>
        => Tensor.Apply<T, Ieee754RemainderOperator<T>>(x, y, destination);
    
    public static void ILogB<T>(ReadOnlySpan<T> x, Span<int> destination)
        where T : struct, IFloatingPointIeee754<T>
        => Tensor.Apply<T, int, ILogBOperator<T>>(x, destination);
    
    public static void Lerp<T>(ReadOnlySpan<T> value1, ReadOnlySpan<T> value2, ReadOnlySpan<T> amount, Span<T> destination)
        where T : struct, IFloatingPointIeee754<T>
        => Tensor.Apply<T, LerpOperator<T>>(value1, value2, amount, destination);

    public static void Lerp<T>(ReadOnlySpan<T> value1, T value2, ReadOnlySpan<T> amount, Span<T> destination)
        where T : struct, IFloatingPointIeee754<T>
        => Tensor.Apply<T, LerpOperator<T>>(value1, value2, amount, destination);

    public static void Lerp<T>(ReadOnlySpan<T> value1, ReadOnlySpan<T> value2, T amount, Span<T> destination)
        where T : struct, IFloatingPointIeee754<T>
        => Tensor.Apply<T, LerpOperator<T>>(value1, value2, amount, destination);
    
    public static void Lerp<T>(ReadOnlySpan<T> value1, T value2, T amount, Span<T> destination)
        where T : struct, IFloatingPointIeee754<T>
        => Tensor.Apply<T, LerpOperator<T>>(value1, value2, amount, destination);
    
    public static void ScaleB<T>(ReadOnlySpan<T> x, int n, Span<T> destination)
        where T : struct, IFloatingPointIeee754<T>
        => Tensor.ApplyScalar<T, T, T, MultiplyScalarOperator<T>>(x, T.CreateChecked(float.Pow(2, n)), destination);

}