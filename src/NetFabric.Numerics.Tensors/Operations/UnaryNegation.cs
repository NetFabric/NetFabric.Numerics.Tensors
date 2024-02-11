namespace NetFabric.Numerics.Tensors;

public static partial class Tensor
{
    public static void Negate<T>(ReadOnlySpan<T> value, Span<T> destination)
        where T : struct, IUnaryNegationOperators<T, T>
        => Apply<T, NegateOperator<T>>(value, destination);

    public static void CheckedNegate<T>(ReadOnlySpan<T> value, Span<T> destination)
        where T : struct, IUnaryNegationOperators<T, T>
        => Apply<T, CheckedNegateOperator<T>>(value, destination);
}