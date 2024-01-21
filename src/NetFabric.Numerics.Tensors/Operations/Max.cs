namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void Max<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, INumberBase<T>
        => Apply<T, MaxOperator<T>>(x, y, destination);
}