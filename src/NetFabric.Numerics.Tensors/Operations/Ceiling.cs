namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void Ceiling(ReadOnlySpan<float> left, Span<float> destination)
        => Apply<float, CeilingSingleOperator>(left, destination);

    public static void Ceiling(ReadOnlySpan<double> left, Span<double> destination)
        => Apply<double, CeilingDoubleOperator>(left, destination);

    public static void Ceiling<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, IFloatingPoint<T>
        => Apply<T, CeilingOperator<T>>(left, destination);

}