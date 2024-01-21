namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void SquareRoot<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, IRootFunctions<T>
        => Apply<T, SquareRootOperator<T>>(left, destination);
}