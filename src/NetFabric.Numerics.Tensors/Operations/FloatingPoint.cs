namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static void Floor<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, IFloatingPoint<T>
        => Tensor.Apply<T, FloorOperator<T>>(left, destination);

    public static void Floor(ReadOnlySpan<float> left, Span<float> destination)
        => Tensor.Apply<float, FloorSingleOperator>(left, destination);

    public static void Floor(ReadOnlySpan<double> left, Span<double> destination)
        => Tensor.Apply<double, FloorDoubleOperator>(left, destination);

    public static void Ceiling<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, IFloatingPoint<T>
        => Tensor.Apply<T, CeilingOperator<T>>(left, destination);

    public static void Ceiling(ReadOnlySpan<float> left, Span<float> destination)
        => Tensor.Apply<float, CeilingSingleOperator>(left, destination);

    public static void Ceiling(ReadOnlySpan<double> left, Span<double> destination)
        => Tensor.Apply<double, CeilingDoubleOperator>(left, destination);

    public static void Round<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, IFloatingPoint<T>
        => Tensor.Apply<T, RoundOperator<T>>(left, destination);

    public static void Round<T>(ReadOnlySpan<T> left, int digits, Span<T> destination)
        where T : struct, IFloatingPoint<T>
        => Tensor.ApplyScalar<T, int, T, RoundDigitsOperator<T>>(left, digits, destination);

    public static void Round<T>(ReadOnlySpan<T> left, MidpointRounding mode, Span<T> destination)
        where T : struct, IFloatingPoint<T>
        => Tensor.ApplyScalar<T, MidpointRounding, T, RoundModeOperator<T>>(left, mode, destination);

    public static void Round<T>(ReadOnlySpan<T> left, int digits, MidpointRounding mode, Span<T> destination)
        where T : struct, IFloatingPoint<T>
        => Tensor.ApplyScalar<T, (int digits, MidpointRounding mode), T, RoundDigitModeOperator<T>>(left, (digits, mode), destination);

    public static void Truncate<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, IFloatingPoint<T>
        => Tensor.Apply<T, TruncateOperator<T>>(left, destination);

}