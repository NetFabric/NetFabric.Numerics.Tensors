namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void Increment<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, IIncrementOperators<T>
        => Apply<T, IncrementOperator<T>>(left, destination);
}