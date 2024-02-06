namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void Max<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, INumber<T>
        => Apply<T, MaxPropagateNaNOperator<T>>(x, y, destination);

    public static void MaxMagnitude<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, INumber<T>
        => Apply<T, MaxMagnitudePropagateNaNOperator<T>>(x, y, destination);
}