namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void Decrement<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, IDecrementOperators<T>
        => Apply<T, DecrementOperator<T>>(left, destination);
}