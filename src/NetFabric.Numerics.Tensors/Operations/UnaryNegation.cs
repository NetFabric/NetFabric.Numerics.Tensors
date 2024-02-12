namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static void Negate<T>(ReadOnlySpan<T> value, Span<T> destination)
        where T : struct, IUnaryNegationOperators<T, T>
        => Tensor.Apply<T, NegateOperator<T>>(value, destination);

    public static void CheckedNegate<T>(ReadOnlySpan<T> value, Span<T> destination)
        where T : struct, IUnaryNegationOperators<T, T>
        => Tensor.Apply<T, CheckedNegateOperator<T>>(value, destination);
}