namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void Min<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, INumberBase<T>
        => Apply<T, MinOperator<T>>(x, y, destination);
}