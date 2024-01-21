namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void Abs<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, INumberBase<T>
        => Apply<T, AbsOperator<T>>(left, destination);
}