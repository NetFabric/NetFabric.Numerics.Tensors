namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void CheckedDecrement<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, IDecrementOperators<T>
        => Apply<T, CheckedDecrementOperator<T>>(left, destination);
}