namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void Min<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, INumber<T>
        => Apply<T, MinPropagateNaNOperator<T>>(x, y, destination);

    public static void MinMagnitude<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, INumber<T>
        => Apply<T, MinMagnitudePropagateNaNOperator<T>>(x, y, destination);
}