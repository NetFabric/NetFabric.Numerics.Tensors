namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static void Increment<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, IIncrementOperators<T>
        => Tensor.Apply<T, IncrementOperator<T>>(left, destination);

    public static void CheckedIncrement<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, IIncrementOperators<T>
        => Tensor.Apply<T, CheckedIncrementOperator<T>>(left, destination);
}