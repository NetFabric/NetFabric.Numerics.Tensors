namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void Floor(ReadOnlySpan<float> left, Span<float> destination)
        => Apply<float, FloorSingleOperator>(left, destination);

    public static void Floor(ReadOnlySpan<double> left, Span<double> destination)
        => Apply<double, FloorDoubleOperator>(left, destination);

    public static void Floor<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, IFloatingPoint<T>
        => Apply<T, FloorOperator<T>>(left, destination);

}